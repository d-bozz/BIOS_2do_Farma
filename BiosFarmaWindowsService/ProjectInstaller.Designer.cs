namespace BiosFarmaWindowsService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.instaladorProcesoServicio = new System.ServiceProcess.ServiceProcessInstaller();
            this.InstaladorServicioHorario = new System.ServiceProcess.ServiceInstaller();
            // 
            // instaladorProcesoServicio
            // 
            this.instaladorProcesoServicio.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.instaladorProcesoServicio.Password = null;
            this.instaladorProcesoServicio.Username = null;
            // 
            // InstaladorServicioHorario
            // 
            this.InstaladorServicioHorario.Description = "Controla las horas extras de los Empleados de BiosFarma.";
            this.InstaladorServicioHorario.DisplayName = "Servicio Horario BiosFarma";
            this.InstaladorServicioHorario.ServiceName = "ServicioHorario";
            this.InstaladorServicioHorario.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.instaladorProcesoServicio,
            this.InstaladorServicioHorario});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller instaladorProcesoServicio;
        private System.ServiceProcess.ServiceInstaller InstaladorServicioHorario;
    }
}