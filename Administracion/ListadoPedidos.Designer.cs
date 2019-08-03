namespace Administracion
{
    partial class ListadoPedidos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListadoPedidos));
            this.btnResumenEmpleado = new System.Windows.Forms.Button();
            this.btnResumenMedicamento = new System.Windows.Forms.Button();
            this.btnResumenFarmaceutica = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.gvResultado = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblError = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtFarma = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gvResultado)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnResumenEmpleado
            // 
            this.btnResumenEmpleado.Location = new System.Drawing.Point(443, 28);
            this.btnResumenEmpleado.Margin = new System.Windows.Forms.Padding(4);
            this.btnResumenEmpleado.Name = "btnResumenEmpleado";
            this.btnResumenEmpleado.Size = new System.Drawing.Size(100, 54);
            this.btnResumenEmpleado.TabIndex = 0;
            this.btnResumenEmpleado.Text = "Resumen Empleado";
            this.btnResumenEmpleado.UseVisualStyleBackColor = true;
            this.btnResumenEmpleado.Click += new System.EventHandler(this.btnResumenEmpleado_Click);
            // 
            // btnResumenMedicamento
            // 
            this.btnResumenMedicamento.Location = new System.Drawing.Point(562, 28);
            this.btnResumenMedicamento.Margin = new System.Windows.Forms.Padding(4);
            this.btnResumenMedicamento.Name = "btnResumenMedicamento";
            this.btnResumenMedicamento.Size = new System.Drawing.Size(109, 54);
            this.btnResumenMedicamento.TabIndex = 1;
            this.btnResumenMedicamento.Text = "Resumen Medicamento";
            this.btnResumenMedicamento.UseVisualStyleBackColor = true;
            this.btnResumenMedicamento.Click += new System.EventHandler(this.btnResumenMedicamento_Click);
            // 
            // btnResumenFarmaceutica
            // 
            this.btnResumenFarmaceutica.Location = new System.Drawing.Point(260, 36);
            this.btnResumenFarmaceutica.Margin = new System.Windows.Forms.Padding(4);
            this.btnResumenFarmaceutica.Name = "btnResumenFarmaceutica";
            this.btnResumenFarmaceutica.Size = new System.Drawing.Size(105, 38);
            this.btnResumenFarmaceutica.TabIndex = 2;
            this.btnResumenFarmaceutica.Text = "Filtro Farma";
            this.btnResumenFarmaceutica.UseVisualStyleBackColor = true;
            this.btnResumenFarmaceutica.Click += new System.EventHandler(this.btnResumenFarmaceutica_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(794, 28);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(4);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(105, 54);
            this.btnLimpiar.TabIndex = 3;
            this.btnLimpiar.Text = "Limpiar Filtros";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // gvResultado
            // 
            this.gvResultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvResultado.Location = new System.Drawing.Point(34, 103);
            this.gvResultado.Name = "gvResultado";
            this.gvResultado.RowTemplate.Height = 24;
            this.gvResultado.Size = new System.Drawing.Size(865, 351);
            this.gvResultado.TabIndex = 4;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblError});
            this.statusStrip1.Location = new System.Drawing.Point(0, 466);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(928, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblError
            // 
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 17);
            // 
            // txtFarma
            // 
            this.txtFarma.Location = new System.Drawing.Point(34, 44);
            this.txtFarma.Name = "txtFarma";
            this.txtFarma.Size = new System.Drawing.Size(200, 22);
            this.txtFarma.TabIndex = 6;
            this.txtFarma.Validating += new System.ComponentModel.CancelEventHandler(this.txtFarma_Validating);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ListadoPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 488);
            this.Controls.Add(this.txtFarma);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gvResultado);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnResumenFarmaceutica);
            this.Controls.Add(this.btnResumenMedicamento);
            this.Controls.Add(this.btnResumenEmpleado);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ListadoPedidos";
            this.Text = "ListadoPedidos";
            this.Load += new System.EventHandler(this.ListadoPedidos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvResultado)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnResumenEmpleado;
        private System.Windows.Forms.Button btnResumenMedicamento;
        private System.Windows.Forms.Button btnResumenFarmaceutica;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView gvResultado;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblError;
        private System.Windows.Forms.TextBox txtFarma;
        private System.Windows.Forms.ErrorProvider errorProvider;

    }
}