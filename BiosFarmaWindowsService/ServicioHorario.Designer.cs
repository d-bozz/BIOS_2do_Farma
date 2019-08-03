namespace BiosFarmaWindowsService
{
    partial class ServicioHorario
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
            this.ELMensaje = new System.Diagnostics.EventLog();
            this.fswRevision = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.ELMensaje)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fswRevision)).BeginInit();
            // 
            // fswRevision
            // 
            this.fswRevision.EnableRaisingEvents = true;
            this.fswRevision.Created += new System.IO.FileSystemEventHandler(this.fswRevision_Created);
            this.fswRevision.Deleted += new System.IO.FileSystemEventHandler(this.fswRevision_Deleted);
            // 
            // ServicioHorario
            // 
            this.CanPauseAndContinue = true;
            this.ServiceName = "ServicioHorario";
            ((System.ComponentModel.ISupportInitialize)(this.ELMensaje)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fswRevision)).EndInit();

        }

        #endregion

        private System.Diagnostics.EventLog ELMensaje;
        private System.IO.FileSystemWatcher fswRevision;
    }
}
