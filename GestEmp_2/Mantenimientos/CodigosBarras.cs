using GestEmp_2.clsFunciones;
using GestEmp_2.clsFuncionesGenerales;
using System;
using System.Windows.Forms;

namespace GestEmp_2.Mantenimientos
{
	public partial class TiposCodigosBarras : Form
	{
		string accionPantalla = "C";
		public TiposCodigosBarras()
		{
			InitializeComponent();
		}

		private void TiposCodigosBarras_Load(object sender, EventArgs e)
		{
			try
			{
				cargaDatos();
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}
		private void cargaDatos()
		{
			try
			{
				accionPantalla = "C";
				comportamientoBotones();
				dtgvDatos.DataSource = BaseDeDatos.ExecuteSelect(true, "Select idTipoCodigoBarra, descripcion from MantTipoCodigosBarras ");
				dtgvDatos.Columns[0].Visible = false;
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
					splitContainer3.Panel1.Enabled = false;
					dtgvDatos.Enabled = true;
					bAceptar.Enabled = false;
					bNuevo.Enabled = true;
					bModificar.Enabled = true;
					bEliminar.Enabled = true;
					bDuplicar.Enabled = true;
					bCancelar.Enabled = false;
					bSalir.Enabled = true;
					break;
				case "N":
					blanqueoCampos();
					splitContainer3.Panel1.Enabled = true;
					dtgvDatos.Enabled = false;
					bAceptar.Enabled = true;
					bNuevo.Enabled = false;
					bModificar.Enabled = false;
					bEliminar.Enabled = false;
					bDuplicar.Enabled = false;
					bCancelar.Enabled = true;
					bSalir.Enabled = false;
					break;
				case "M":
				case "E":
				case "D":
					splitContainer3.Panel1.Enabled = true;
					dtgvDatos.Enabled = false;
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
					strSql = "Insert into MantTipoCodigosBarras (idEmpresa, idTipoCodigoBarra, descripcion, prefijo, tamaño,  Activo, UsuarioCreacion, FechaHoraCreacion) Values ('" +
							guid + "', '" +
							tCodigo.Text + "', '" +
							tDescripcion.Text + "', " +
							tPrefijo.Text + ", " +
							tTamanyo.Text + ", " +
							"1, 'SYSTEM','" +
							DateTime.Now + "')";
				}
				else
				{
					strSql = "Update MantTipoCodigosBarras Set Descripcion = '" + tDescripcion.Text + "', Prefijo = " + tPrefijo.Text + ", Tamanyo = " + tTamanyo.Text +
						"', UsuarioModificacion = 'SYSTEM', FechaHoraModificacion = '" + DateTime.Now.ToString() + "'" +
						" Where idTipoCodigoBarra = '" + tId.Text + "'";
				}
				BaseDeDatos.ExecuteSelect(true, strSql);
				cargaDatos();
				accionPantalla = "A";
				comportamientoBotones();

			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void blanqueoCampos()
		{
			try
			{
				tId.Text = "";
				tCodigo.Text = "";
				tDescripcion.Text = "";
				tPrefijo.Text = "";
				tTamanyo.Text = "";
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
				DialogResult dialogResult = MessageBox.Show("¿Desea salir del Mto. de IVA?", "Mto. Clientes", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					this.Close();
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}
	}
}
