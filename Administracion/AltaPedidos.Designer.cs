namespace Administracion
{
    partial class AltaPedidos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AltaPedidos));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblError = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.txtDire = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCant = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAgregar = new System.Windows.Forms.ToolStripButton();
            this.btnDeshacer = new System.Windows.Forms.ToolStripButton();
            this.txtMed = new System.Windows.Forms.TextBox();
            this.btnAgMed = new System.Windows.Forms.Button();
            this.DGVMedicamentos = new System.Windows.Forms.DataGridView();
            this.Ep1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtFarmaceutica = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBorrarMedicamento = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMedicamentos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ep1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblError});
            this.statusStrip1.Location = new System.Drawing.Point(0, 432);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(729, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblError
            // 
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 17);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 359);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Direccion de Entrega:";
            // 
            // txtDire
            // 
            this.txtDire.Location = new System.Drawing.Point(161, 356);
            this.txtDire.Margin = new System.Windows.Forms.Padding(4);
            this.txtDire.Name = "txtDire";
            this.txtDire.Size = new System.Drawing.Size(507, 22);
            this.txtDire.TabIndex = 4;
            this.txtDire.Validating += new System.ComponentModel.CancelEventHandler(this.txtDire_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 107);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Medicamento:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 169);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Cantidad:";
            // 
            // txtCant
            // 
            this.txtCant.Location = new System.Drawing.Point(161, 164);
            this.txtCant.Margin = new System.Windows.Forms.Padding(4);
            this.txtCant.Name = "txtCant";
            this.txtCant.Size = new System.Drawing.Size(132, 22);
            this.txtCant.TabIndex = 2;
            this.txtCant.Validating += new System.ComponentModel.CancelEventHandler(this.txtCant_Validating);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAgregar,
            this.btnDeshacer});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(729, 25);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAgregar
            // 
            this.btnAgregar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAgregar.Image = global::Administracion.Properties.Resources.add_icon_icons_com_52393;
            this.btnAgregar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(23, 22);
            this.btnAgregar.Text = "toolStripButton1";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnDeshacer
            // 
            this.btnDeshacer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeshacer.Image = global::Administracion.Properties.Resources.Undo_icon_icons_com_73701;
            this.btnDeshacer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeshacer.Name = "btnDeshacer";
            this.btnDeshacer.Size = new System.Drawing.Size(23, 22);
            this.btnDeshacer.Text = "toolStripButton1";
            this.btnDeshacer.Click += new System.EventHandler(this.btnDeshacer_Click);
            // 
            // txtMed
            // 
            this.txtMed.Location = new System.Drawing.Point(161, 104);
            this.txtMed.Margin = new System.Windows.Forms.Padding(4);
            this.txtMed.Name = "txtMed";
            this.txtMed.Size = new System.Drawing.Size(132, 22);
            this.txtMed.TabIndex = 1;
            this.txtMed.Validating += new System.ComponentModel.CancelEventHandler(this.txtMed_Validating);
            // 
            // btnAgMed
            // 
            this.btnAgMed.Location = new System.Drawing.Point(11, 245);
            this.btnAgMed.Margin = new System.Windows.Forms.Padding(4);
            this.btnAgMed.Name = "btnAgMed";
            this.btnAgMed.Size = new System.Drawing.Size(280, 28);
            this.btnAgMed.TabIndex = 3;
            this.btnAgMed.Text = "Agregar Medicamento";
            this.btnAgMed.UseVisualStyleBackColor = true;
            this.btnAgMed.Click += new System.EventHandler(this.btnAgMed_Click);
            // 
            // DGVMedicamentos
            // 
            this.DGVMedicamentos.AllowUserToAddRows = false;
            this.DGVMedicamentos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVMedicamentos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVMedicamentos.BackgroundColor = System.Drawing.Color.LightGray;
            this.DGVMedicamentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVMedicamentos.Cursor = System.Windows.Forms.Cursors.Default;
            this.DGVMedicamentos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DGVMedicamentos.Location = new System.Drawing.Point(363, 31);
            this.DGVMedicamentos.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.DGVMedicamentos.MultiSelect = false;
            this.DGVMedicamentos.Name = "DGVMedicamentos";
            this.DGVMedicamentos.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVMedicamentos.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGVMedicamentos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.DGVMedicamentos.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Teal;
            this.DGVMedicamentos.RowTemplate.Height = 24;
            this.DGVMedicamentos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGVMedicamentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVMedicamentos.Size = new System.Drawing.Size(305, 292);
            this.DGVMedicamentos.TabIndex = 5;
            this.DGVMedicamentos.VirtualMode = true;
            // 
            // Ep1
            // 
            this.Ep1.ContainerControl = this;
            // 
            // txtFarmaceutica
            // 
            this.txtFarmaceutica.Location = new System.Drawing.Point(161, 43);
            this.txtFarmaceutica.Margin = new System.Windows.Forms.Padding(4);
            this.txtFarmaceutica.Name = "txtFarmaceutica";
            this.txtFarmaceutica.Size = new System.Drawing.Size(132, 22);
            this.txtFarmaceutica.TabIndex = 0;
            this.txtFarmaceutica.Validating += new System.ComponentModel.CancelEventHandler(this.txtFarmaceutica_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Farmaceutica:";
            // 
            // btnBorrarMedicamento
            // 
            this.btnBorrarMedicamento.Location = new System.Drawing.Point(11, 281);
            this.btnBorrarMedicamento.Margin = new System.Windows.Forms.Padding(4);
            this.btnBorrarMedicamento.Name = "btnBorrarMedicamento";
            this.btnBorrarMedicamento.Size = new System.Drawing.Size(280, 28);
            this.btnBorrarMedicamento.TabIndex = 12;
            this.btnBorrarMedicamento.Text = "Borrar Medicamento";
            this.btnBorrarMedicamento.UseVisualStyleBackColor = true;
            this.btnBorrarMedicamento.Click += new System.EventHandler(this.btnBorrarMedicamento_Click);
            // 
            // AltaPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 454);
            this.Controls.Add(this.btnBorrarMedicamento);
            this.Controls.Add(this.txtFarmaceutica);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DGVMedicamentos);
            this.Controls.Add(this.btnAgMed);
            this.Controls.Add(this.txtMed);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.txtCant);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDire);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AltaPedidos";
            this.Text = "AltaPedidos";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMedicamentos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ep1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDire;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCant;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAgregar;
        private System.Windows.Forms.ToolStripButton btnDeshacer;
        private System.Windows.Forms.TextBox txtMed;
        private System.Windows.Forms.Button btnAgMed;
        private System.Windows.Forms.DataGridView DGVMedicamentos;
        private System.Windows.Forms.ErrorProvider Ep1;
        private System.Windows.Forms.Button btnBorrarMedicamento;
        private System.Windows.Forms.TextBox txtFarmaceutica;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripStatusLabel lblError;
    }
}