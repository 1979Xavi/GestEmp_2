using GestEmp_2.clsFunciones;
using GestEmp_2.clsFuncionesGenerales;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace GestEmp_2.Mantenimientos
{
	public partial class Colores : Form
	{
		string accionPantalla = "C";
		public Colores()
		{
			InitializeComponent();
		}

		private void Colores_Load(object sender, EventArgs e)
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
				dtgvDatos.DataSource = BaseDeDatos.ExecuteSelect(true, "Select idColor, CodigoColor, Descripcion from MantColores");
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
		private void MuestraDetalle(string id)
		{
			try
			{

				tId.Text = id;
				string strSQl = "Select idColor, CodigoColor, Descripcion, RGB" +
					" from MantColores " +
					" where idColor = '" + id + "'";
				DataTable dtDetalle = BaseDeDatos.ExecuteSelect(true, strSQl);

				if (dtgvDatos.Rows.Count > 0)
				{
					tCodigo.Text = dtDetalle.Rows[0]["codigoColor"].ToString();
					tDescripcion.Text = dtDetalle.Rows[0]["Descripcion"].ToString();
					string[] colorines;
					colorines = dtDetalle.Rows[0]["RGB"].ToString().Split('=', ',', ']');

					int alfa = Int32.Parse(colorines[1]);
					int rojo = Int32.Parse(colorines[3]);
					int verde = Int32.Parse(colorines[5]);
					int azul = Int32.Parse(colorines[7]);

					pColor.BackColor = Color.FromArgb(alfa, rojo, verde, azul);
				}
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
					strSql = "Insert into MantColores (idColor, CodigoColor, Descripcion, RGB, Activo, usuarioCreacion, fechaHoraCreacion) Values ('" +
							guid + "', '" +
							tCodigo.Text + "', '" +
							tDescripcion.Text + "', '" +
							Color.FromArgb(pColor.BackColor.ToArgb()) + "', " +
							"1, 'SYSTEM','" +
							DateTime.Now + "')";
				}
				else
				{
					strSql = "Update MantColores Set CodigoColor = '" + tCodigo.Text + "', Descripcion = '" + tDescripcion.Text +
					"', RGB = '" + Color.FromArgb(pColor.BackColor.ToArgb()) +
					"', UsuarioModificacion = 'SYSTEM', FechaHoraModificacion = '" + DateTime.Now.ToString() + "'" +
					" Where idColor = '" + tId.Text + "'";
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

		private void bColor_Click(object sender, EventArgs e)
		{
			try
			{
				cdColor.ShowDialog();
				pColor.BackColor = cdColor.Color;
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
				DialogResult dialogResult = MessageBox.Show("¿Desea salir del Mto. de Colores?", "Mto. Colores", MessageBoxButtons.YesNo);
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
