namespace Administracion
{
    partial class NuevaContrasena
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NuevaContrasena));
            this.lblActual = new System.Windows.Forms.Label();
            this.lblNueva = new System.Windows.Forms.Label();
            this.txtActual = new System.Windows.Forms.TextBox();
            this.txtNueva = new System.Windows.Forms.TextBox();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnAceptar = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblError = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblActual
            // 
            this.lblActual.AutoSize = true;
            this.lblActual.Location = new System.Drawing.Point(116, 69);
            this.lblActual.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblActual.Name = "lblActual";
            this.lblActual.Size = new System.Drawing.Size(131, 17);
            this.lblActual.TabIndex = 0;
            this.lblActual.Text = "Contraseña actual: ";
            // 
            // lblNueva
            // 
            this.lblNueva.AutoSize = true;
            this.lblNueva.Location = new System.Drawing.Point(116, 69);
            this.lblNueva.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNueva.Name = "lblNueva";
            this.lblNueva.Size = new System.Drawing.Size(132, 17);
            this.lblNueva.TabIndex = 1;
            this.lblNueva.Text = "Nueva contraseña: ";
            // 
            // txtActual
            // 
            this.txtActual.Location = new System.Drawing.Point(289, 69);
            this.txtActual.Margin = new System.Windows.Forms.Padding(4);
            this.txtActual.Name = "txtActual";
            this.txtActual.Size = new System.Drawing.Size(132, 22);
            this.txtActual.TabIndex = 2;
            this.txtActual.Validating += new System.ComponentModel.CancelEventHandler(this.txtActual_Validating);
            // 
            // txtNueva
            // 
            this.txtNueva.Location = new System.Drawing.Point(289, 69);
            this.txtNueva.Margin = new System.Windows.Forms.Padding(4);
            this.txtNueva.Name = "txtNueva";
            this.txtNueva.Size = new System.Drawing.Size(132, 22);
            this.txtNueva.TabIndex = 3;
            this.txtNueva.Validating += new System.ComponentModel.CancelEventHandler(this.txtNueva_Validating);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Location = new System.Drawing.Point(119, 139);
            this.btnSiguiente.Margin = new System.Windows.Forms.Padding(4);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(100, 28);
            this.btnSiguiente.TabIndex = 4;
            this.btnSiguiente.Text = "Siguiente";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(119, 139);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(4);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(100, 28);
            this.btnAceptar.TabIndex = 6;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblError});
            this.statusStrip1.Location = new System.Drawing.Point(0, 240);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(593, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblError
            // 
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 17);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(321, 139);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 28);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // NuevaContrasena
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 262);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.txtActual);
            this.Controls.Add(this.lblActual);
            this.Controls.Add(this.lblNueva);
            this.Controls.Add(this.txtNueva);
            this.Controls.Add(this.btnAceptar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NuevaContrasena";
            this.Text = "NuevaContraseña";
            this.Load += new System.EventHandler(this.NuevaContrasena_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblActual;
        private System.Windows.Forms.Label lblNueva;
        private System.Windows.Forms.TextBox txtActual;
        private System.Windows.Forms.TextBox txtNueva;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblError;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}