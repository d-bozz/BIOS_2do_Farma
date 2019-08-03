using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Entidades_Compartidas;
using Persistencia;

namespace Logica
{
    internal class LogicaUsuarios : ILogicaUsuarios
    {
        //Singleton*************************************
        private static LogicaUsuarios _instancia = null;

        private LogicaUsuarios()
        {
        }

        public static LogicaUsuarios GetInstance()
        {
            if (_instancia == null)
                _instancia = new LogicaUsuarios();
            return _instancia;
        }
        //************************************

        //Operaciones
        public void AgregarUsuario(Usuario U, Encargado encargadoAutorizante)
        {
            try
            {
                if (U is Empleado)
                    FabricaPersistencia.GetPersistenciaEmpleado().AltaEmpleado((Empleado)U, encargadoAutorizante);
                else if (U is Encargado)
                    FabricaPersistencia.GetPersistenciaEncargado().AltaEncargado((Encargado)U, encargadoAutorizante);
                else
                    throw new Exception("Usuario invalido.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarUsuario(Usuario U, Encargado encargadoAutorizante)
        {
            try
            {
                if (U is Empleado)
                    FabricaPersistencia.GetPersistenciaEmpleado().EliminarEmpleado((Empleado)U, encargadoAutorizante);
                else if (U is Encargado)
                    throw new Exception("No se pueden dar de baja los Encargados.");
                else
                    throw new Exception("Usuario invalido.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarUsuario(Usuario U, Encargado encargadoAutorizante)
        {
            try
            {
                if (U is Empleado)
                    FabricaPersistencia.GetPersistenciaEmpleado().ModificarEmpleado((Empleado)U, encargadoAutorizante);
                else if (U is Encargado)
                    throw new Exception("No se pueden modificar los Encargados.");
                else
                    throw new Exception("Usuario invalido.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CambioContrasena(Usuario pUsuario, string newContrasena)
        {
            try
            {
                if (pUsuario.Contra == newContrasena)
                    throw new Exception("La contrasena es igual. Media Pila!");

                string letras = newContrasena;
                letras = letras.Replace("0", "");
                letras = letras.Replace("1", "");
                letras = letras.Replace("2", "");
                letras = letras.Replace("3", "");
                letras = letras.Replace("4", "");
                letras = letras.Replace("5", "");
                letras = letras.Replace("6", "");
                letras = letras.Replace("7", "");
                letras = letras.Replace("8", "");
                letras = letras.Replace("9", "");

                if (letras.Length != 5)
                    throw new Exception("La contrasena debe ser con el formato abcde12");

                if (newContrasena.Trim().Length == 7)
                {
                    string valor = newContrasena.Substring(5, 2);
                    int salida;
                    if (!Int32.TryParse(valor, out salida))
                        throw new Exception("La contrasena debe ser con el formato abcde12");
                }
                else
                { throw new Exception("La contraseña debe tener 7 caracteres."); }

                FabricaPersistencia.GetPersistenciaUsuario().CambioContrasena(pUsuario, newContrasena);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public Usuario BuscarUsuario(int pci, Encargado encargadoAutorizante)
        {
            Usuario u = Persistencia.FabricaPersistencia.GetPersistenciaEncargado().BuscarEncargados(pci, encargadoAutorizante);

            if (u == null)
                u = Persistencia.FabricaPersistencia.GetPersistenciaEmpleado().BuscarEmpleadosActivos(pci, encargadoAutorizante);

            return u;
        }

        public Usuario Login(string pNombreUsuario, string pContrasena)
        {
            Usuario usuLogueando = null;
            try
            {
                usuLogueando = FabricaPersistencia.GetPersistenciaEmpleado().Login(pNombreUsuario, pContrasena);
                
                if (usuLogueando == null)
                    usuLogueando = FabricaPersistencia.GetPersistenciaEncargado().Login(pNombreUsuario, pContrasena);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            return usuLogueando;
        }

        public void AgregaHorasExtras(int pCi, DateTime pFecha, int pMinutos)
        {
            try
            {
                FabricaPersistencia.GetPersistenciaEmpleado().AgregaHorasExtras(pCi, pFecha, pMinutos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*
        public List<Usuario> Listar(Encargado encargadoAutorizante)
        {

            List<Usuario> Lista = new List<Usuario>();
            Lista.AddRange(Persistencia.FabricaPersistencia.GetPersistenciaEncargado().ListarEncargados(encargadoAutorizante));
            Lista.AddRange(Persistencia.FabricaPersistencia.GetPersistenciaEmpleado().ListadoEmpleados(encargadoAutorizante));
            return Lista;
        }
         * 
        public List<Empleado> ListarActivos(Encargado encargadoAutorizante)
        {
            return (Persistencia.FabricaPersistencia.GetPersistenciaEmpleado().ListadoEmpleadosActivos(encargadoAutorizante));        
            
        }
        */
    }
}
