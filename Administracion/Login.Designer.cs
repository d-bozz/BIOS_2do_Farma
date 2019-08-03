namespace Administracion
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblError = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this._btnLogin = new System.Windows.Forms.Button();
            this._txtUser = new System.Windows.Forms.TextBox();
            this._txtPass = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblError});
            this.statusStrip1.Location = new System.Drawing.Point(0, 197);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(472, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblError
            // 
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 17);
            // 
            // timer1
            // 
            this.timer1.Interval = 15000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // _btnLogin
            // 
            this._btnLogin.Location = new System.Drawing.Point(137, 162);
            this._btnLogin.Name = "_btnLogin";
            this._btnLogin.Size = new System.Drawing.Size(188, 23);
            this._btnLogin.TabIndex = 1;
            this._btnLogin.Text = "Login";
            this._btnLogin.UseVisualStyleBackColor = true;
            this._btnLogin.Click += new System.EventHandler(this._btnLogin_Click);
            // 
            // _txtUser
            // 
            this._txtUser.ForeColor = System.Drawing.Color.DimGray;
            this._txtUser.Location = new System.Drawing.Point(137, 35);
            this._txtUser.Name = "_txtUser";
            this._txtUser.Size = new System.Drawing.Size(188, 22);
            this._txtUser.TabIndex = 2;
            this._txtUser.Text = "USUARIO";
            this._txtUser.Enter += new System.EventHandler(this.txtUser_Enter);
            this._txtUser.Leave += new System.EventHandler(this._txtUser_Leave);
            this._txtUser.Validating += new System.ComponentModel.CancelEventHandler(this._txtUser_Validating_1);
            // 
            // _txtPass
            // 
            this._txtPass.ForeColor = System.Drawing.Color.DimGray;
            this._txtPass.Location = new System.Drawing.Point(137, 100);
            this._txtPass.Name = "_txtPass";
            this._txtPass.Size = new System.Drawing.Size(188, 22);
            this._txtPass.TabIndex = 3;
            this._txtPass.Text = "CONTRASENIA";
            this._txtPass.Enter += new System.EventHandler(this.txtPass_Enter);
            this._txtPass.Leave += new System.EventHandler(this.txtPass_Leave);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 219);
            this.Controls.Add(this._txtPass);
            this.Controls.Add(this._txtUser);
            this.Controls.Add(this._btnLogin);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Login";
            this.Text = "Login";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button _btnLogin;
        private System.Windows.Forms.TextBox _txtUser;
        private System.Windows.Forms.TextBox _txtPass;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStripStatusLabel lblError;
    }
}