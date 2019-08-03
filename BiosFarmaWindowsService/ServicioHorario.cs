using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.IO;
using BiosFarmaWindowsService.ServicioFarma;
using System.Xml;
using System.Windows.Forms;

namespace BiosFarmaWindowsService
{
    partial class ServicioHorario : ServiceBase
    {
        int usuarioCI;
        DateTime hora_inicio;
        DateTime hora_fin;
        private bool estaLogueado;

        public ServicioHorario()
        {
            InitializeComponent();

            //1 - Inicializamos nuestro EventLog personalizado
            //Sino existe, se crea la entrada donde se "guardan" los mensajes a generar
            if (!System.Diagnostics.EventLog.SourceExists("MiServicioHorario"))
                System.Diagnostics.EventLog.CreateEventSource("MiServicioHorario", "ServicioHorarioLog");

            //2 - Indicamos a nuestro servicio que va a loguear bajo los siguientes elementos
            ELMensaje.Source = "MiServicioHorario";
            ELMensaje.Log = "ServicioHorarioLog";

            //Seteamos el path para el File System Watcher
            fswRevision.Path = Path.GetDirectoryName(Application.ExecutablePath);

            System.Timers.Timer unT = new System.Timers.Timer();
            unT.Enabled = true;
            //Lo ejecuto cada 5 segundos (5000 milisegundos).
            unT.Interval = 5000;
            unT.Elapsed += new System.Timers.ElapsedEventHandler(unT_Elapsed);
        }


        #region ***EventosStart,Stop,Pause,Continue***
        protected override void OnStart(string[] args)
        {
            //Avisamos del inicio del Servicio
            ELMensaje.WriteEntry("Inicia el servicio - ServicioHorario");
        }

        protected override void OnStop()
        {
            //Avisamos la detencion del servicio
            ELMensaje.WriteEntry("Se detiene el servicio - ServicioHorario");
        }

        protected override void OnPause()
        {
            //Avisamos la pausa del servicio
            ELMensaje.WriteEntry("Se pausa el servicio - ServicioHorario");
        }

        protected override void OnContinue()
        {
            //Avisamos la cotinuacion del servicio
            ELMensaje.WriteEntry("Se continua ejecutando el servicio - ServicioHorario");
        }

        #endregion


        void unT_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (estaLogueado)
                {
                    if (DateTime.Now > hora_fin)
                    {
                        TimeSpan result = DateTime.Now.Subtract(hora_fin);
                        int minutos = Convert.ToInt32(Math.Round(result.TotalMinutes));

                        IServicioWCF servicioFarma = new ServicioWCFClient();
                        servicioFarma.AgregaHorasExtras(usuarioCI, hora_inicio.Date, minutos);
                        //ELMensaje.WriteEntry("unT_Elapsed: " + nombreUsuario);
                    }
                }
                else
                {
                    //ELMensaje.WriteEntry("unT_Elapsed: ---  ---no esta logueado ---> " + estaLogueado.ToString());
                }
            }
            catch (Exception ex)
            {
                ELMensaje.WriteEntry("Error en el Timer: " + ex.Message);
            }
        }

        private void fswRevision_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            try
            {
                //revisamos si se ha creado el XML
                if (e.Name.ToLowerInvariant().Contains("sesion"))
                {
                    
                    XmlDocument _usuarioLogueado = new XmlDocument();
                    _usuarioLogueado.Load(e.FullPath);
                    
                    ELMensaje.WriteEntry("Login detectado: --->" + _usuarioLogueado.InnerText);

                    XmlNodeList nodo = _usuarioLogueado.GetElementsByTagName("Empleado");

                    usuarioCI = Convert.ToInt32(nodo[0].SelectSingleNode("CI").InnerText);
                    string inicio = nodo[0].SelectSingleNode("Horario_Inicio").InnerText;
                    string fin = nodo[0].SelectSingleNode("Horario_Fin").InnerText;

                    
                    if (!DateTime.TryParse(inicio, out hora_inicio))
                        throw new Exception("La hora de inicio no es valida.");

                    if (!DateTime.TryParse(fin, out hora_fin))
                        throw new Exception("La hora de fin no es valida.");

                    if (hora_inicio > hora_fin && DateTime.Now.Hour > hora_inicio.Hour)
                        hora_fin = hora_fin.AddDays(1);

                    if (hora_inicio > hora_fin && DateTime.Now.Hour < hora_inicio.Hour)
                        hora_inicio = hora_inicio.AddDays(-1);

                    //ELMensaje.WriteEntry("fswRevision_Created: HoraInicio: " + hora_inicio.ToShortDateString() + " HoraFin: "+ hora_fin.ToShortDateString());

                    estaLogueado = true;
                }
            }
            catch (Exception ex)
            {
                ELMensaje.WriteEntry("Error capturando archivo: " + ex.Message);
            }
        }

        private void fswRevision_Deleted(object sender, FileSystemEventArgs e)
        {
            estaLogueado = false;
            ELMensaje.WriteEntry("Deslogueo detectado");
        }


    }
}
