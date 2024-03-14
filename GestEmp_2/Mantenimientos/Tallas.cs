using GestEmp_2.clsFunciones;
using GestEmp_2.clsFuncionesGenerales;
using System;
using System.Data;
using System.Windows.Forms;

namespace GestEmp_2.Mantenimientos
{
	public partial class Tallas : Form
	{
		string accionPantalla = "C";
		public Tallas()
		{
			InitializeComponent();
		}

		private void Tallas_Load(object sender, EventArgs e)
		{
			try
			{
				cargaDatos();
				if (dtgvDatos.Rows.Count > 0)
				{
					MuestraDetalle(dtgvDatos.Rows[0].Cells[0].Value.ToString());
				}

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
				dtgvDatos.DataSource = BaseDeDatos.ExecuteSelect(true, "Select idTalla, CodigoTalla, Descripcion from MantTallas");
				dtgvDatos.Columns[0].Visible = false;
				dtgvDatos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
		private void blanqueoCampos()
		{
			try
			{
				tId.Text = "";
				tCodigo.Text = "";
				tDescripcion.Text = "";
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);

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
					strSql = "Insert into MantTallas (idTalla, CodigoTalla, descripcion, Activo, usuarioCreacion, fechaHoraCreacion) Values ('" +
							guid + "', '" +
							tCodigo.Text + "', '" +
							tDescripcion.Text + "', " +
							"1, 'SYSTEM','" +
							DateTime.Now + "')";
				}
				else
				{
					strSql = "Update MantTallas Set CodigoTalla = '" + tCodigo.Text + "', Descripcion = '" + tDescripcion.Text +
					", UsuarioModificacion = 'SYSTEM', FechaHoraModificacion = '" + DateTime.Now.ToString() + "'" +
					" Where idTalla = '" + tId.Text + "'";
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
				DialogResult dialogResult = MessageBox.Show("¿Desea salir del Mto. de Tallas?", "Mto. Tallas", MessageBoxButtons.YesNo);
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

		private void MuestraDetalle(string id)
		{
			try
			{

				tId.Text = id;
				string strSQl = "Select idTalla, CodigoTalla, Descripcion" +
					" from MantTallas " +
					" where idTalla = '" + id + "'";
				DataTable dtDetalle = BaseDeDatos.ExecuteSelect(true, strSQl);

				if (dtgvDatos.Rows.Count > 0)
				{
					tCodigo.Text = dtDetalle.Rows[0]["codigoTalla"].ToString();
					tDescripcion.Text = dtDetalle.Rows[0]["Descripcion"].ToString();
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void dtgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				MuestraDetalle(dtgvDatos.Rows[e.RowIndex].Cells[0].Value.ToString());
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}
	}
}
