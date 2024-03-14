using GestEmp_2.clsFunciones;
using System;
using System.Data;
using System.Windows.Forms;

namespace GestEmp_2
{
	public partial class Login : Form
	{
		public Login()
		{
			InitializeComponent();

		}

		private void cargoCombo()
		{
			string strSql = "Select idFamilia, Descripcion from MantFamilias";
			DataTable dtFpago = BaseDeDatos.ExecuteSelect(true, strSql);
			if (dtFpago.Rows.Count > 0)
			{
				cEmpresa.DataSource = dtFpago;
				cEmpresa.DisplayMember = dtFpago.Columns[1].ColumnName; //Descripcion
				cEmpresa.ValueMember = dtFpago.Columns[0].ColumnName;   //id
			}
		}
		private void tCodUsuario_Leave(object sender, EventArgs e)
		{
			//Aqui deberemos validar el usuario.
			string strSQl = "Select * from MantUsuarios where CodigoUsuario = '" + tCodUsuario.Text + "'";
			DataTable dtDetalle = BaseDeDatos.ExecuteSelect(true, strSQl);

			if (dtDetalle.Rows.Count > 0)
			{
				if (dtDetalle.Rows[0]["ContrasenyaUsuario"].ToString() == "")
				{
					MessageBox.Show("Este usuario es nuevo, debe inicar una contraseña");
					contrasenyaNueva Frm = new GestEmp_2.contrasenyaNueva(tCodUsuario.Text);
					Frm.Show();
				}
			}
		}

		private void bAceptar_Click(object sender, EventArgs e)
		{
			//l'usuari es correcte, mirem si la contraseña tambe
			string strSQl = "Select * from MantUsuarios where CodigoUsuario = '" + tCodUsuario.Text + "' and ContrasenyaUsuario = '" +
				clsFuncionesGenerales.clsfuncionesGenerales.Encriptar(tContrasenya.Text) + "'";
			DataTable dtContrasnya = BaseDeDatos.ExecuteSelect(true, strSQl);

			if (dtContrasnya.Rows.Count == 1)
			{
				this.DialogResult = DialogResult.OK;
			}
			else
			{
				MessageBox.Show("Las credenciales son incorrectas", "GestEmp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void bCancelar_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void bCambiarContrasenya_Click(object sender, EventArgs e)
		{
			contrasenyaNueva Frm = new GestEmp_2.contrasenyaNueva(tCodUsuario.Text);
			Frm.Show();
		}
	}
}
