namespace GestEmp_2
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
			this.label1 = new System.Windows.Forms.Label();
			this.tCodUsuario = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tContrasenya = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cEmpresa = new System.Windows.Forms.ComboBox();
			this.bAceptar = new System.Windows.Forms.Button();
			this.bCancelar = new System.Windows.Forms.Button();
			this.bCambiarContrasenya = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(104, 61);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Usuario";
			// 
			// tCodUsuario
			// 
			this.tCodUsuario.Location = new System.Drawing.Point(171, 58);
			this.tCodUsuario.Name = "tCodUsuario";
			this.tCodUsuario.Size = new System.Drawing.Size(122, 20);
			this.tCodUsuario.TabIndex = 1;
			this.tCodUsuario.Leave += new System.EventHandler(this.tCodUsuario_Leave);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(104, 87);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Contraseña";
			// 
			// tContrasenya
			// 
			this.tContrasenya.Location = new System.Drawing.Point(171, 84);
			this.tContrasenya.Name = "tContrasenya";
			this.tContrasenya.Size = new System.Drawing.Size(122, 20);
			this.tContrasenya.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(104, 117);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Empresa";
			// 
			// cEmpresa
			// 
			this.cEmpresa.FormattingEnabled = true;
			this.cEmpresa.Location = new System.Drawing.Point(172, 114);
			this.cEmpresa.Name = "cEmpresa";
			this.cEmpresa.Size = new System.Drawing.Size(121, 21);
			this.cEmpresa.TabIndex = 3;
			// 
			// bAceptar
			// 
			this.bAceptar.Location = new System.Drawing.Point(251, 153);
			this.bAceptar.Name = "bAceptar";
			this.bAceptar.Size = new System.Drawing.Size(75, 23);
			this.bAceptar.TabIndex = 4;
			this.bAceptar.Text = "Aceptar";
			this.bAceptar.UseVisualStyleBackColor = true;
			this.bAceptar.Click += new System.EventHandler(this.bAceptar_Click);
			// 
			// bCancelar
			// 
			this.bCancelar.Location = new System.Drawing.Point(332, 153);
			this.bCancelar.Name = "bCancelar";
			this.bCancelar.Size = new System.Drawing.Size(75, 23);
			this.bCancelar.TabIndex = 5;
			this.bCancelar.Text = "Cancelar";
			this.bCancelar.UseVisualStyleBackColor = true;
			this.bCancelar.Click += new System.EventHandler(this.bCancelar_Click);
			// 
			// bCambiarContrasenya
			// 
			this.bCambiarContrasenya.Location = new System.Drawing.Point(251, 182);
			this.bCambiarContrasenya.Name = "bCambiarContrasenya";
			this.bCambiarContrasenya.Size = new System.Drawing.Size(156, 23);
			this.bCambiarContrasenya.TabIndex = 6;
			this.bCambiarContrasenya.Text = "Cambiar contraseña";
			this.bCambiarContrasenya.UseVisualStyleBackColor = true;
			this.bCambiarContrasenya.Click += new System.EventHandler(this.bCambiarContrasenya_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.panel1.Controls.Add(this.label4);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(433, 42);
			this.panel1.TabIndex = 4;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(12, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(311, 25);
			this.label4.TabIndex = 0;
			this.label4.Text = "GestEmp - Gestión empresarial";
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.panel2.Location = new System.Drawing.Point(0, 41);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(83, 177);
			this.panel2.TabIndex = 5;
			// 
			// Login
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(433, 217);
			this.ControlBox = false;
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.bCambiarContrasenya);
			this.Controls.Add(this.bCancelar);
			this.Controls.Add(this.bAceptar);
			this.Controls.Add(this.cEmpresa);
			this.Controls.Add(this.tContrasenya);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tCodUsuario);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Login";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Login";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tCodUsuario;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tContrasenya;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cEmpresa;
		private System.Windows.Forms.Button bAceptar;
		private System.Windows.Forms.Button bCancelar;
		private System.Windows.Forms.Button bCambiarContrasenya;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label4;
	}
}