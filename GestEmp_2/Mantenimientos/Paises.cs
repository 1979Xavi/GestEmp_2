using GestEmp_2.clsFunciones;
using GestEmp_2.clsFuncionesGenerales;
using System;
using System.Windows.Forms;

namespace GestEmp_2.Mantenimientos
{
	public partial class Paises : Form
	{

		//Poslbles estados
		//C-Consultar, A-Modificar, M-Modificar, E-Eliminar (Activo = 0), D-Duplicar
		string accionPantalla = "C";
		public Paises()
		{
			InitializeComponent();
		}

		private void paises_Load(object sender, EventArgs e)
		{
			try
			{
				accionPantalla = "C";
				comportamientoBotones();
				dtgvDatos.DataSource = BaseDeDatos.ExecuteSelect(true, "Select * from clientes");
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		void comportamientoBotones()
		{
			switch (accionPantalla)
			{
				case "C":
				case "A":
					bAceptar.Enabled = false;
					bNuevo.Enabled = true;
					bModificar.Enabled = true;
					bEliminar.Enabled = true;
					bDuplicar.Enabled = true;
					bCancelar.Enabled = false;
					bSalir.Enabled = true;
					break;
				case "N":
				case "M":
				case "E":
				case "D":
					bAceptar.Enabled = true;
					bNuevo.Enabled = false;
					bModificar.Enabled = false;
					bEliminar.Enabled = false;
					bDuplicar.Enabled = false;
					bCancelar.Enabled = true;
					bSalir.Enabled = false;
					break;
			}

		}

		private void bAceptar_Click(object sender, EventArgs e)

		{
			try
			{
				string strSql = "";
				string guid = "";
				if (accionPantalla == "N")
				{
					guid = Guid.NewGuid().ToString();
					strSql = "Insert into clientes (idCliente, codCliente, nombre, direccion, codigoPostal, idPoblacion, idProvincia, idPais, telefono, email, url, personContacto, idIVA, idDto, " +
							"observaciones, imagen, activo, usuarioCreacion, fechaHoraCreacion) Values ('" +
							guid + "', '" +
							tCodigo.Text + "', '" +
							tNombre.Text + "', '" +
							"1, 'SYSTEM','" +
							DateTime.Now + "')";

					BaseDeDatos.ExecuteSelect(true, strSql);
				}
				else
					guid = tId.Text;

				accionPantalla = "A";
				comportamientoBotones();

			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bNuevo_Click(object sender, EventArgs e)
		{
			accionPantalla = "N";
			comportamientoBotones();
		}

		private void bModificar_Click(object sender, EventArgs e)
		{
			accionPantalla = "M";
			comportamientoBotones();
		}

		private void bEliminar_Click(object sender, EventArgs e)
		{
			accionPantalla = "E";
			comportamientoBotones();
		}

		private void bDuplicar_Click(object sender, EventArgs e)
		{
			accionPantalla = "D";
			comportamientoBotones();
		}

		private void bCancelar_Click(object sender, EventArgs e)
		{
			accionPantalla = "C";
			comportamientoBotones();
		}

		private void bSalir_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult dialogResult = MessageBox.Show("¿Desea salir del Mto. de Clientes?", "Mto. Clientes", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					this.Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Se ha producido el siguiente erro: " + ex.Message.ToString());
			}

		}
	}
}
