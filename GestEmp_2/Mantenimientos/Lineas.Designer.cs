namespace GestEmp_2.Mantenimientos
{
	partial class Lineas
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.bAceptar = new System.Windows.Forms.Button();
			this.bSalir = new System.Windows.Forms.Button();
			this.bCancelar = new System.Windows.Forms.Button();
			this.bDuplicar = new System.Windows.Forms.Button();
			this.bEliminar = new System.Windows.Forms.Button();
			this.bModificar = new System.Windows.Forms.Button();
			this.bNuevo = new System.Windows.Forms.Button();
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
			this.splitContainer1.Size = new System.Drawing.Size(1085, 634);
			this.splitContainer1.SplitterDistance = 359;
			this.splitContainer1.TabIndex = 5;
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
			this.dtgvDatos.Size = new System.Drawing.Size(359, 634);
			this.dtgvDatos.TabIndex = 2;
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
			this.splitContainer2.Size = new System.Drawing.Size(722, 634);
			this.splitContainer2.SplitterDistance = 125;
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
			this.label15.Size = new System.Drawing.Size(44, 13);
			this.label15.TabIndex = 0;
			this.label15.Text = "id Linea";
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.tDescripcion);
			this.splitContainer3.Panel1.Controls.Add(this.label2);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.tableLayoutPanel1);
			this.splitContainer3.Size = new System.Drawing.Size(722, 505);
			this.splitContainer3.SplitterDistance = 582;
			this.splitContainer3.TabIndex = 0;
			// 
			// tDescripcion
			// 
			this.tDescripcion.Location = new System.Drawing.Point(127, 62);
			this.tDescripcion.Name = "tDescripcion";
			this.tDescripcion.Size = new System.Drawing.Size(396, 20);
			this.tDescripcion.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 65);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Descripción";
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
			this.tableLayoutPanel1.Size = new System.Drawing.Size(136, 505);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// bAceptar
			// 
			this.bAceptar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bAceptar.Location = new System.Drawing.Point(3, 152);
			this.bAceptar.Name = "bAceptar";
			this.bAceptar.Size = new System.Drawing.Size(130, 44);
			this.bAceptar.TabIndex = 2;
			this.bAceptar.Text = "Aceptar";
			this.bAceptar.UseVisualStyleBackColor = true;
			// 
			// bSalir
			// 
			this.bSalir.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bSalir.Location = new System.Drawing.Point(3, 452);
			this.bSalir.Name = "bSalir";
			this.bSalir.Size = new System.Drawing.Size(130, 50);
			this.bSalir.TabIndex = 2;
			this.bSalir.Text = "Salir";
			this.bSalir.UseVisualStyleBackColor = true;
			// 
			// bCancelar
			// 
			this.bCancelar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bCancelar.Location = new System.Drawing.Point(3, 402);
			this.bCancelar.Name = "bCancelar";
			this.bCancelar.Size = new System.Drawing.Size(130, 44);
			this.bCancelar.TabIndex = 2;
			this.bCancelar.Text = "Cancelar";
			this.bCancelar.UseVisualStyleBackColor = true;
			// 
			// bDuplicar
			// 
			this.bDuplicar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bDuplicar.Location = new System.Drawing.Point(3, 352);
			this.bDuplicar.Name = "bDuplicar";
			this.bDuplicar.Size = new System.Drawing.Size(130, 44);
			this.bDuplicar.TabIndex = 2;
			this.bDuplicar.Text = "Duplicar";
			this.bDuplicar.UseVisualStyleBackColor = true;
			// 
			// bEliminar
			// 
			this.bEliminar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bEliminar.Location = new System.Drawing.Point(3, 302);
			this.bEliminar.Name = "bEliminar";
			this.bEliminar.Size = new System.Drawing.Size(130, 44);
			this.bEliminar.TabIndex = 2;
			this.bEliminar.Text = "Eliminar";
			this.bEliminar.UseVisualStyleBackColor = true;
			// 
			// bModificar
			// 
			this.bModificar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bModificar.Location = new System.Drawing.Point(3, 252);
			this.bModificar.Name = "bModificar";
			this.bModificar.Size = new System.Drawing.Size(130, 44);
			this.bModificar.TabIndex = 2;
			this.bModificar.Text = "Modificar";
			this.bModificar.UseVisualStyleBackColor = true;
			// 
			// bNuevo
			// 
			this.bNuevo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bNuevo.Location = new System.Drawing.Point(3, 202);
			this.bNuevo.Name = "bNuevo";
			this.bNuevo.Size = new System.Drawing.Size(130, 44);
			this.bNuevo.TabIndex = 2;
			this.bNuevo.Text = "Nuevo";
			this.bNuevo.UseVisualStyleBackColor = true;
			// 
			// Lineas
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1085, 634);
			this.Controls.Add(this.splitContainer1);
			this.Name = "Lineas";
			this.Text = "Lineas";
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
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button bAceptar;
		private System.Windows.Forms.Button bSalir;
		private System.Windows.Forms.Button bCancelar;
		private System.Windows.Forms.Button bDuplicar;
		private System.Windows.Forms.Button bEliminar;
		private System.Windows.Forms.Button bModificar;
		private System.Windows.Forms.Button bNuevo;
	}
}