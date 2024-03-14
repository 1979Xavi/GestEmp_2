using GestEmp_2.clsFunciones;
using GestEmp_2.clsFuncionesGenerales;
using System;
using System.Data;
using System.Windows.Forms;

namespace GestEmp_2.Mantenimientos
{
	public partial class Poblaciones : Form
	{
		string accionPantalla = "C";

		public Poblaciones()
		{
			InitializeComponent();
		}

		private void poblaciones_Load(object sender, EventArgs e)
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
				dtgvDatos.DataSource = BaseDeDatos.ExecuteSelect(true, "Select idPoblacion, codigoPoblacion, pro.Descripcion as Provincia, pai.Descripcion as Pais, pob.Descripcion " +
					" from MantPoblaciones pob" +
					" Left Join MantProvincias pro on pro.idProvincia = pob.idProvincia and pro.idPais = pob.idPais" +
					" Left Join MantPaises pai on pai.idpais = pob.idPais");
				dtgvDatos.Columns[0].Visible = false;
				dtgvDatos.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
					strSql = "Insert into MantPoblaciones (idPoblacion, codigoPoblacion, idProvincia, idPais, Descripcion, Activo, UsuarioCreacion, FechaHoraCreacion) Values ('" +
							guid + "', '" +
							tCodigo.Text + "', '" +
							tCodProvincia.Tag + "', '" +
							tCodPais.Tag + "', '" +
							tNombre.Text + "', " +
							"1, 'SYSTEM','" +
							DateTime.Now + "')";

				}
				else
				{
					strSql = "Update MantPoblaciones Set codigoPoblacion = '" + tCodigo.Text + "', idPais = '" + tCodPais.Tag + "', idProvincia = '" + tCodProvincia.Tag +
						"', Descripcion = '" + tNombre.Text + "', UsuarioModificacion = 'SYSTEM', FechaHoraModificacion = '" + DateTime.Now.ToString() + "'" +
						" Where idPoblacion = '" + tId.Text + "'";
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
				DialogResult dialogResult = MessageBox.Show("¿Desea salir del Mto. de Clientes?", "Mto. Clientes", MessageBoxButtons.YesNo);
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

		private void bBuscaProvincia_Click(object sender, EventArgs e)
		{
			try
			{
				if (tCodPais.Tag.ToString() != "")
				{
					frmSelector frm1 = new frmSelector();
					frm1.TopMost = true;
					frm1.Owner = this;
					frm1.Titulo = "Selector de provincias";
					frm1.strSql = "Select idProvincia, codigoProvincia, Descripcion from MantProvincias where idPais = '" + tCodPais.Tag + "'";
					frm1.ShowDialog();

					tCodProvincia.Tag = frm1.id;
					tCodProvincia.Text = frm1.codigo;
					tDescProvincia.Text = frm1.nombre;
				}
				else
				{
					MessageBox.Show("Primero debe indicar el pais", "¡Atención!");
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bBuscaPais_Click(object sender, EventArgs e)
		{
			try
			{
				frmSelector frm1 = new frmSelector();
				frm1.TopMost = true;
				frm1.Owner = this;
				frm1.Titulo = "Selector de Paises";
				frm1.strSql = "Select idPais, codigoPais, Descripcion from MantPaises";
				frm1.ShowDialog();

				tCodPais.Tag = frm1.id;
				tCodPais.Text = frm1.codigo;
				tDescPais.Text = frm1.nombre;
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

				tId.Text = dtgvDatos.Rows[e.RowIndex].Cells[0].Value.ToString();
				string strSQl = "Select idPoblacion, CodigoPoblacion, pob.Descripcion as Poblacion, " +
					" pob.idProvincia, CodigoProvincia, pro.Descripcion as Provincia, " +
					" pob.idPais, CodigoPais, pai.Descripcion as Pais" +
					" from MantPoblaciones pob" +
					" Left Join MantProvincias pro on pro.idProvincia = pob.idProvincia and pro.idPais = pob.idPais" +
					" Left Join MantPaises pai on pai.idpais = pob.idPais where idPoblacion = '" + dtgvDatos.Rows[e.RowIndex].Cells[0].Value.ToString() + "'";
				DataTable dtDetalle = BaseDeDatos.ExecuteSelect(true, strSQl);

				if (dtgvDatos.Rows.Count > 0)
				{
					tCodigo.Text = dtDetalle.Rows[0]["CodigoPoblacion"].ToString();
					tNombre.Text = dtDetalle.Rows[0]["Poblacion"].ToString();

					tCodPais.Tag = dtDetalle.Rows[0]["idPais"].ToString();
					tCodPais.Text = dtDetalle.Rows[0]["CodigoPais"].ToString();
					tDescPais.Text = dtDetalle.Rows[0]["Pais"].ToString();

					tCodProvincia.Tag = dtDetalle.Rows[0]["idProvincia"].ToString();
					tCodProvincia.Text = dtDetalle.Rows[0]["CodigoProvincia"].ToString();
					tDescProvincia.Text = dtDetalle.Rows[0]["Provincia"].ToString();
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
				tNombre.Text = "";
				tCodPais.Tag = "";
				tCodPais.Text = "";
				tDescPais.Text = "";
				tCodProvincia.Tag = "";
				tCodProvincia.Text = "";
				tDescProvincia.Text = "";


			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);

			}
		}
	}
}
