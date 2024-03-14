
namespace GestEmp_2.Mantenimientos
{
	partial class Colores
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.dtgvDatos = new System.Windows.Forms.DataGridView();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.tId = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.tDescripcion = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tCodigo = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.bAceptar = new System.Windows.Forms.Button();
			this.bSalir = new System.Windows.Forms.Button();
			this.bCancelar = new System.Windows.Forms.Button();
			this.bDuplicar = new System.Windows.Forms.Button();
			this.bEliminar = new System.Windows.Forms.Button();
			this.bModificar = new System.Windows.Forms.Button();
			this.bNuevo = new System.Windows.Forms.Button();
			this.cdColor = new System.Windows.Forms.ColorDialog();
			this.pColor = new System.Windows.Forms.Panel();
			this.bColor = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dtgvDatos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.dtgvDatos);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(1048, 517);
			this.splitContainer1.SplitterDistance = 348;
			this.splitContainer1.TabIndex = 4;
			// 
			// dtgvDatos
			// 
			this.dtgvDatos.AllowUserToAddRows = false;
			this.dtgvDatos.AllowUserToDeleteRows = false;
			this.dtgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dtgvDatos.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dtgvDatos.Location = new System.Drawing.Point(0, 0);
			this.dtgvDatos.Name = "dtgvDatos";
			this.dtgvDatos.ReadOnly = true;
			this.dtgvDatos.RowHeadersVisible = false;
			this.dtgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dtgvDatos.Size = new System.Drawing.Size(348, 517);
			this.dtgvDatos.TabIndex = 2;
			this.dtgvDatos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvDatos_CellContentClick);
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.tId);
			this.splitContainer2.Panel1.Controls.Add(this.label15);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
			this.splitContainer2.Size = new System.Drawing.Size(696, 517);
			this.splitContainer2.SplitterDistance = 148;
			this.splitContainer2.TabIndex = 0;
			// 
			// tId
			// 
			this.tId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tId.Location = new System.Drawing.Point(13, 103);
			this.tId.Name = "tId";
			this.tId.Size = new System.Drawing.Size(697, 20);
			this.tId.TabIndex = 1;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(10, 87);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(47, 13);
			this.label15.TabIndex = 0;
			this.label15.Text = "idFamilia";
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.bColor);
			this.splitContainer3.Panel1.Controls.Add(this.pColor);
			this.splitContainer3.Panel1.Controls.Add(this.tDescripcion);
			this.splitContainer3.Panel1.Controls.Add(this.label2);
			this.splitContainer3.Panel1.Controls.Add(this.tCodigo);
			this.splitContainer3.Panel1.Controls.Add(this.label1);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.tableLayoutPanel1);
			this.splitContainer3.Size = new System.Drawing.Size(696, 365);
			this.splitContainer3.SplitterDistance = 561;
			this.splitContainer3.TabIndex = 0;
			// 
			// tDescripcion
			// 
			this.tDescripcion.Location = new System.Drawing.Point(136, 130);
			this.tDescripcion.Name = "tDescripcion";
			this.tDescripcion.Size = new System.Drawing.Size(396, 20);
			this.tDescripcion.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(24, 133);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Descripción";
			// 
			// tCodigo
			// 
			this.tCodigo.Location = new System.Drawing.Point(136, 104);
			this.tCodigo.Name = "tCodigo";
			this.tCodigo.Size = new System.Drawing.Size(108, 20);
			this.tCodigo.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(24, 107);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Código";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.bAceptar, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.bSalir, 0, 7);
			this.tableLayoutPanel1.Controls.Add(this.bCancelar, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.bDuplicar, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.bEliminar, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.bModificar, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.bNuevo, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 8;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.52221F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.06826F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.06826F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.06826F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.06826F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.06826F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.06826F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.06826F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(131, 365);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// bAceptar
			// 
			this.bAceptar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bAceptar.Location = new System.Drawing.Point(3, 110);
			this.bAceptar.Name = "bAceptar";
			this.bAceptar.Size = new System.Drawing.Size(125, 30);
			this.bAceptar.TabIndex = 2;
			this.bAceptar.Text = "Aceptar";
			this.bAceptar.UseVisualStyleBackColor = true;
			this.bAceptar.Click += new System.EventHandler(this.bAceptar_Click);
			// 
			// bSalir
			// 
			this.bSalir.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bSalir.Location = new System.Drawing.Point(3, 326);
			this.bSalir.Name = "bSalir";
			this.bSalir.Size = new System.Drawing.Size(125, 36);
			this.bSalir.TabIndex = 2;
			this.bSalir.Text = "Salir";
			this.bSalir.UseVisualStyleBackColor = true;
			this.bSalir.Click += new System.EventHandler(this.bSalir_Click);
			// 
			// bCancelar
			// 
			this.bCancelar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bCancelar.Location = new System.Drawing.Point(3, 290);
			this.bCancelar.Name = "bCancelar";
			this.bCancelar.Size = new System.Drawing.Size(125, 30);
			this.bCancelar.TabIndex = 2;
			this.bCancelar.Text = "Cancelar";
			this.bCancelar.UseVisualStyleBackColor = true;
			this.bCancelar.Click += new System.EventHandler(this.bCancelar_Click);
			// 
			// bDuplicar
			// 
			this.bDuplicar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bDuplicar.Location = new System.Drawing.Point(3, 254);
			this.bDuplicar.Name = "bDuplicar";
			this.bDuplicar.Size = new System.Drawing.Size(125, 30);
			this.bDuplicar.TabIndex = 2;
			this.bDuplicar.Text = "Duplicar";
			this.bDuplicar.UseVisualStyleBackColor = true;
			this.bDuplicar.Click += new System.EventHandler(this.bDuplicar_Click);
			// 
			// bEliminar
			// 
			this.bEliminar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bEliminar.Location = new System.Drawing.Point(3, 218);
			this.bEliminar.Name = "bEliminar";
			this.bEliminar.Size = new System.Drawing.Size(125, 30);
			this.bEliminar.TabIndex = 2;
			this.bEliminar.Text = "Eliminar";
			this.bEliminar.UseVisualStyleBackColor = true;
			this.bEliminar.Click += new System.EventHandler(this.bEliminar_Click);
			// 
			// bModificar
			// 
			this.bModificar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bModificar.Location = new System.Drawing.Point(3, 182);
			this.bModificar.Name = "bModificar";
			this.bModificar.Size = new System.Drawing.Size(125, 30);
			this.bModificar.TabIndex = 2;
			this.bModificar.Text = "Modificar";
			this.bModificar.UseVisualStyleBackColor = true;
			this.bModificar.Click += new System.EventHandler(this.bModificar_Click);
			// 
			// bNuevo
			// 
			this.bNuevo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bNuevo.Location = new System.Drawing.Point(3, 146);
			this.bNuevo.Name = "bNuevo";
			this.bNuevo.Size = new System.Drawing.Size(125, 30);
			this.bNuevo.TabIndex = 2;
			this.bNuevo.Text = "Nuevo";
			this.bNuevo.UseVisualStyleBackColor = true;
			this.bNuevo.Click += new System.EventHandler(this.bNuevo_Click);
			// 
			// pColor
			// 
			this.pColor.Location = new System.Drawing.Point(27, 156);
			this.pColor.Name = "pColor";
			this.pColor.Size = new System.Drawing.Size(468, 43);
			this.pColor.TabIndex = 3;
			// 
			// bColor
			// 
			this.bColor.Location = new System.Drawing.Point(501, 176);
			this.bColor.Name = "bColor";
			this.bColor.Size = new System.Drawing.Size(31, 23);
			this.bColor.TabIndex = 7;
			this.bColor.Text = "...";
			this.bColor.UseVisualStyleBackColor = true;
			this.bColor.Click += new System.EventHandler(this.bColor_Click);
			// 
			// Colores
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1048, 517);
			this.Controls.Add(this.splitContainer1);
			this.Name = "Colores";
			this.Text = "Colores";
			this.Load += new System.EventHandler(this.Colores_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dtgvDatos)).EndInit();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel1.PerformLayout();
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.DataGridView dtgvDatos;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.TextBox tId;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.TextBox tDescripcion;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tCodigo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button bAceptar;
		private System.Windows.Forms.Button bSalir;
		private System.Windows.Forms.Button bCancelar;
		private System.Windows.Forms.Button bDuplicar;
		private System.Windows.Forms.Button bEliminar;
		private System.Windows.Forms.Button bModificar;
		private System.Windows.Forms.Button bNuevo;
		private System.Windows.Forms.Panel pColor;
		private System.Windows.Forms.ColorDialog cdColor;
		private System.Windows.Forms.Button bColor;
	}
}