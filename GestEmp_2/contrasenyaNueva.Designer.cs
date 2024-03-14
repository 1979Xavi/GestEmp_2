namespace GestEmp_2
{
	partial class contrasenyaNueva
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
			this.label6 = new System.Windows.Forms.Label();
			this.tContrasenya = new System.Windows.Forms.TextBox();
			this.tContrasenyaConfirm = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.bAceptar = new System.Windows.Forms.Button();
			this.bCancelar = new System.Windows.Forms.Button();
			this.bMostrarContUno = new System.Windows.Forms.Button();
			this.bMostrarContDos = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(21, 42);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(61, 13);
			this.label6.TabIndex = 51;
			this.label6.Text = "Contraseña";
			// 
			// tContrasenya
			// 
			this.tContrasenya.Location = new System.Drawing.Point(133, 39);
			this.tContrasenya.Name = "tContrasenya";
			this.tContrasenya.PasswordChar = '*';
			this.tContrasenya.Size = new System.Drawing.Size(165, 20);
			this.tContrasenya.TabIndex = 1;
			// 
			// tContrasenyaConfirm
			// 
			this.tContrasenyaConfirm.Location = new System.Drawing.Point(133, 65);
			this.tContrasenyaConfirm.Name = "tContrasenyaConfirm";
			this.tContrasenyaConfirm.PasswordChar = '*';
			this.tContrasenyaConfirm.Size = new System.Drawing.Size(165, 20);
			this.tContrasenyaConfirm.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(21, 68);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(105, 13);
			this.label1.TabIndex = 51;
			this.label1.Text = "Repita la contraseña";
			// 
			// bAceptar
			// 
			this.bAceptar.Location = new System.Drawing.Point(198, 105);
			this.bAceptar.Name = "bAceptar";
			this.bAceptar.Size = new System.Drawing.Size(75, 23);
			this.bAceptar.TabIndex = 5;
			this.bAceptar.Text = "Aceptar";
			this.bAceptar.UseVisualStyleBackColor = true;
			this.bAceptar.Click += new System.EventHandler(this.bAceptar_Click);
			// 
			// bCancelar
			// 
			this.bCancelar.Location = new System.Drawing.Point(279, 105);
			this.bCancelar.Name = "bCancelar";
			this.bCancelar.Size = new System.Drawing.Size(75, 23);
			this.bCancelar.TabIndex = 6;
			this.bCancelar.Text = "Cancelar";
			this.bCancelar.UseVisualStyleBackColor = true;
			this.bCancelar.Click += new System.EventHandler(this.bCancelar_Click);
			// 
			// bMostrarContUno
			// 
			this.bMostrarContUno.Location = new System.Drawing.Point(304, 39);
			this.bMostrarContUno.Name = "bMostrarContUno";
			this.bMostrarContUno.Size = new System.Drawing.Size(26, 20);
			this.bMostrarContUno.TabIndex = 2;
			this.bMostrarContUno.Text = "...";
			this.bMostrarContUno.UseVisualStyleBackColor = true;
			this.bMostrarContUno.Click += new System.EventHandler(this.bMostrarContUno_Click);
			// 
			// bMostrarContDos
			// 
			this.bMostrarContDos.Location = new System.Drawing.Point(304, 65);
			this.bMostrarContDos.Name = "bMostrarContDos";
			this.bMostrarContDos.Size = new System.Drawing.Size(26, 20);
			this.bMostrarContDos.TabIndex = 4;
			this.bMostrarContDos.Text = "...";
			this.bMostrarContDos.UseVisualStyleBackColor = true;
			this.bMostrarContDos.Click += new System.EventHandler(this.bMostrarContDos_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(24, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 54;
			this.label2.Text = "label2";
			// 
			// contrasenyaNueva
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(377, 140);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.bMostrarContDos);
			this.Controls.Add(this.bMostrarContUno);
			this.Controls.Add(this.bCancelar);
			this.Controls.Add(this.bAceptar);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.tContrasenyaConfirm);
			this.Controls.Add(this.tContrasenya);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "contrasenyaNueva";
			this.Text = "Nueva cotraseña";
			this.Load += new System.EventHandler(this.contrasenyaNueva_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tContrasenya;
		private System.Windows.Forms.TextBox tContrasenyaConfirm;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button bAceptar;
		private System.Windows.Forms.Button bCancelar;
		private System.Windows.Forms.Button bMostrarContUno;
		private System.Windows.Forms.Button bMostrarContDos;
		private System.Windows.Forms.Label label2;
	}
}