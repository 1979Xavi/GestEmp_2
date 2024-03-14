using GestEmp_2.clsFunciones;
using System;
using System.Data;
using System.Windows.Forms;

namespace GestEmp_2
{
	public partial class contrasenyaNueva : Form
	{


		public contrasenyaNueva(string codUsuario)
		{

			InitializeComponent();
			label2.Text = codUsuario;
		}

		private void contrasenyaNueva_Load(object sender, EventArgs e)
		{

		}

		private void bAceptar_Click(object sender, EventArgs e)
		{
			if (tContrasenya.Text != tContrasenyaConfirm.Text)
			{
				MessageBox.Show("Las contraseñas deben ser iguales");
			}
			else
			{
				string strSQl = "Update MantUsuarios Set Contrasenyausuario = '" + clsFuncionesGenerales.clsfuncionesGenerales.Encriptar(tContrasenya.Text) +
					"' where CodigoUsuario = '" + label2.Text + "'";
				DataTable dtDetalle = BaseDeDatos.ExecuteSelect(true, strSQl);
				this.Close();
			}
		}

		private void bMostrarContUno_Click(object sender, EventArgs e)
		{
			if (tContrasenya.PasswordChar == '*')
			{
				tContrasenya.PasswordChar = '\0';
			}
			else
				tContrasenya.PasswordChar = '*';
		}

		private void bMostrarContDos_Click(object sender, EventArgs e)
		{
			if (tContrasenyaConfirm.PasswordChar == '*')
			{
				tContrasenyaConfirm.PasswordChar = '\0';
			}
			else
				tContrasenyaConfirm.PasswordChar = '*';
		}

		private void bCancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
