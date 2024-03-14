//using GestEmp_2.Listados;
using GestEmp_2.clsFunciones;
using GestEmp_2.clsFuncionesGenerales;
using GestEmp_2.Listados;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace GestEmp_2.Pantallas
{
	public partial class Presupuestos : Form
	{
		//Poslbles estados
		//C-Consultar, A-Modificar, M-Modificar, E-Eliminar (Activo = 0), D-Duplicar
		string accionPantalla = "C";
		string accionPantallaArticulos = "C";
		string accionPantallaDocumentos = "C";
		DataTable dtArticulos = new DataTable();
		DataTable dtDocumentos = new DataTable();
		//DataTable dtTarifas = new DataTable();
		DataRow rowArti;
		DataRow rowDocu;
		public Presupuestos()
		{
			InitializeComponent();
		}

		private void Presupuestos_Load(object sender, EventArgs e)
		{

			try
			{
				cargaDatos();
				CargoCbos();
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
				accionPantallaArticulos = "C";
				accionPantallaDocumentos = "C";
				comportamientoBotones();
				dtgvDatos.DataSource = BaseDeDatos.ExecuteSelect(true, "Select idPresupuesto, CodigoPresupuesto, cli.RazonSocialFacturacion from Presupuesto pre " +
				"Inner Join MantClientes cli on pre.idCliente = cli.idCliente");
				dtgvDatos.Columns[0].Visible = false;
				dtgvDatos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				MuestraDetalle(dtgvDatos.Rows[0].Cells[0].Value.ToString());
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}
		void CargoCbos()
		{
			try
			{
				string strSql = "";
				strSql = "Select idDescuento, cast(porcentaje as varchar(6)) + ' - ' + Descripcion as Descripcion from MantDescuento";
				DataTable dtDto = BaseDeDatos.ExecuteSelect(true, strSql);
				if (dtDto.Rows.Count > 0)
				{
					cDto.DataSource = dtDto;
					cDto.DisplayMember = dtDto.Columns[1].ColumnName; //Descripcion
					cDto.ValueMember = dtDto.Columns[0].ColumnName;   //id
				}

				strSql = "Select idIVA, porcentaje from MantIVA order by porcentaje";
				DataTable dtIva = BaseDeDatos.ExecuteSelect(true, strSql);
				if (dtIva.Rows.Count > 0)
				{
					cIVA.DataSource = dtIva;
					cIVA.DisplayMember = dtIva.Columns[1].ColumnName; //Descripcion
					cIVA.ValueMember = dtIva.Columns[0].ColumnName;   //id
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}
		private void MuestraDetalle(string id)
		{
			try
			{
				tId.Text = id;
				string strSQl = "Select idPresupuesto, CodigoPresupuesto, pre.idCliente, cli.CodigoCliente, cli.RazonSocialFacturacion, cli.DomicilioFacturacion, cli.CodigoPostalFacturacion, " +
					" cli.idPoblacionFacturacion, pob.CodigoPoblacion, pob.Descripcion as Poblacion, cli.idProvinciaFacturacion, pro.CodigoProvincia, pro.Descripcion as Provincia ," +
					" cli.idPaisFacturacion, pai.CodigoPais, pai.Descripcion as Pais," +
					" pre.Status, pre.FechaHoraEmision, pre.FechaHoraValidez, RutaImagen, pre.idIVA as IVA from Presupuesto pre" +
					" Left Join MantClientes cli on pre.idCliente = cli.idCliente" +
					" Left Join MantProvincias pro on pro.idProvincia = cli.idProvinciaFacturacion" +
					" Left Join MantPaises pai on cli.idPaisFacturacion = pai.idPais" +
					" Left Join MantPoblaciones pob on cli.idPoblacionFacturacion = pob.idPoblacion" +
					" Where idPresupuesto = '" + id + "'";

				DataTable dtDetalle = BaseDeDatos.ExecuteSelect(true, strSQl);
				aplicaFormatoGridArticulos();
				if (dtDetalle.Rows.Count > 0)
				{
					lblPresu.Text = dtDetalle.Rows[0]["CodigoPresupuesto"].ToString();

					switch (dtDetalle.Rows[0]["Status"].ToString())
					{
						case "P":
							cStatus.SelectedIndex = 0;
							break;
						case "A":
							cStatus.SelectedIndex = 1;
							break;
						case "R":
							cStatus.SelectedIndex = 2;
							break;
					}


					tCodCliente.Tag = dtDetalle.Rows[0]["idCliente"].ToString();
					tCodCliente.Text = dtDetalle.Rows[0]["CodigoCliente"].ToString();
					tDescCliente.Text = dtDetalle.Rows[0]["RazonSocialFacturacion"].ToString();

					tDireccion.Text = dtDetalle.Rows[0]["DomicilioFacturacion"].ToString();
					tCodigoPostal.Text = dtDetalle.Rows[0]["CodigoPostalFacturacion"].ToString();

					tCodPoblacion.Tag = dtDetalle.Rows[0]["idPoblacionFacturacion"].ToString();
					tCodPoblacion.Text = dtDetalle.Rows[0]["CodigoPoblacion"].ToString();
					tDescPoblacion.Text = dtDetalle.Rows[0]["Poblacion"].ToString();

					tCodProvincia.Tag = dtDetalle.Rows[0]["idProvinciaFacturacion"].ToString();
					tCodProvincia.Text = dtDetalle.Rows[0]["CodigoProvincia"].ToString();
					tDescProvincia.Text = dtDetalle.Rows[0]["Provincia"].ToString();

					tCodPais.Tag = dtDetalle.Rows[0]["idPaisFacturacion"].ToString();
					tCodPais.Text = dtDetalle.Rows[0]["CodigoPais"].ToString();
					tDescPais.Text = dtDetalle.Rows[0]["Pais"].ToString();

					dtpFechaVencimiento.Value = Convert.ToDateTime(dtDetalle.Rows[0]["FechaHoraEmision"].ToString());
					dtpFechaEmision.Value = Convert.ToDateTime(dtDetalle.Rows[0]["FechaHoraValidez"].ToString());

					pCliente.ImageLocation = dtDetalle.Rows[0]["RutaImagen"].ToString();

					cIVA.SelectedValue = dtDetalle.Rows[0]["IVA"].ToString();

					//mostramos detalle articulos
					strSQl = "Select idPresupuesto, LineaPresupuesto, FechaHora, lin.idArticulo, CodigoArticulo, Descripcion, lin.idProveedor, CodigoProveedor, RazonSocial, Unidades, PrecioUnitario, Total, " +
						"lin.idDescuento, lin.idIVA as IVA, lin.ImpIVA as ImpIva, Status, lin.Observaciones, ArticuloVario, ProveedorConcreto, PrecioCoste, MargenPorcentaje, MargenUnitario, MargenTotalLinea " +
						"from PresupuestoLineas lin " +
						"Left Join MantArticulos art on lin.idArticulo = art.idArticulo " +
						"Left Join MantProveedores pro on lin.idProveedor = pro.idProveedor " +
						"where idPresupuesto = '" + id + "'";
					dtArticulos = BaseDeDatos.ExecuteSelect(true, strSQl);
					dtgvArticulos.DataSource = dtArticulos;
					calculamosTotales();

					//mostramos detalle documentos
					strSQl = "Select * from PresupuestoDocs " +
						"where idPresupuesto = '" + id + "'";
					dtDocumentos = BaseDeDatos.ExecuteSelect(true, strSQl);
					dtgvDocumentos.DataSource = dtDocumentos;
				}
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
					dtgvDatos.Enabled = true;
					pnlGeneral.Enabled = false;
					pnlBotonesArticulos.Enabled = false;
					pnlBotonesDoc.Enabled = false;
					pnlArticulos.Enabled = false;
					pnlOtrosDatos.Enabled = false;

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
					dtgvDatos.Enabled = false;
					pnlGeneral.Enabled = true;
					pnlBotonesArticulos.Enabled = true;
					pnlBotonesDoc.Enabled = true;
					pnlArticulos.Enabled = true;
					pnlOtrosDatos.Enabled = true;
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

		private void dtgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex > -1)
					MuestraDetalle(dtgvDatos.Rows[e.RowIndex].Cells[0].Value.ToString());
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bNuevo_Click(object sender, EventArgs e)
		{
			blanqueoCampos();
			dtArticulos.Clear();
			dtgvArticulos.DataSource = dtArticulos;
			accionPantalla = "N";
			comportamientoBotones();
			accionPantallaArticulos = "C";
			comportamientoBotonesArticulos();
			accionPantallaDocumentos = "C";
			comportamientoBotonesDocumentos();
			lblPresu.Text = "PRE_" + clsfuncionesGenerales.devuelveID("PRE", this.Name);
		}

		private void blanqueoCampos()
		{
			try
			{
				// Blanqueamos checkBoxs
				foreach (Control ctrl in pnlGeneral.Controls)
				{
					if (ctrl is TextBox)
					{
						ctrl.Text = "";
					}

					if (ctrl is CheckBox)
					{
						CheckBox chk;
						chk = (CheckBox)ctrl;
						chk.Checked = false;
					}

					if (ctrl is ComboBox)
					{
						ComboBox cbb;
						cbb = (ComboBox)ctrl;
						cbb.SelectedIndex = 0;
					}
				}

				// Blanqueamos textBoxs
				foreach (Control ctrl in pnlArticulos.Controls)
				{
					if (ctrl is TextBox)
					{
						ctrl.Text = "";
						ctrl.Tag = "";
					}
					else if (ctrl is GroupBox)
					{
						foreach (Control ctrl2 in ctrl.Controls)
						{
							if (ctrl2 is TextBox)
							{
								ctrl2.Text = "";
								ctrl2.Tag = "";
								ctrl2.Refresh();
							}
						}
					}

					lblCoste.Text = "";
					lblMargen.Text = "";
					lblPresu.Text = "";
					lblTotal.Text = "";
					lblUnidades.Text = "";

				}

				foreach (Control ctrl in pnlOtrosDatos.Controls)
				{
					if (ctrl is TextBox)
					{
						ctrl.Text = "";
					}
				}
				pCliente.ImageLocation = "";


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
				string guid = "";
				if (ValidaDatos())
				{
					if (accionPantalla == "N")
					{
						guid = Guid.NewGuid().ToString();
						GrabaPresupuesto(accionPantalla, guid);
					}
					else
					{
						guid = tId.Text;
						GrabaPresupuesto(accionPantalla, guid);
					}

					cargaDatos();
					accionPantalla = "A";
					comportamientoBotones();
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private bool ValidaDatos()
		{
			try
			{
				if (tCodCliente.Tag.ToString() == "")
				{
					MessageBox.Show("El cliente es un valor obligatorio");
					return false;
				}
				return true;
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
				return false;
			}
		}

		private void bModificar_Click(object sender, EventArgs e)
		{
			accionPantalla = "M";
			comportamientoBotones();
			accionPantallaArticulos = "M";
			comportamientoBotonesArticulos();
			accionPantallaDocumentos = "M";
			comportamientoBotonesDocumentos();
		}

		private void bEliminar_Click(object sender, EventArgs e)
		{
			frmInforme frm1 = new frmInforme();
			frm1.idPresupuesto = tId.Text;
			frm1.ShowDialog();

		}

		private void bDuplicar_Click(object sender, EventArgs e)
		{
			accionPantalla = "D";
			comportamientoBotones();
			cargaDatos();
		}

		private void bCancelar_Click(object sender, EventArgs e)
		{
			accionPantalla = "C";
			comportamientoBotones();
			cargaDatos();
		}

		private void bSalir_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult dialogResult = MessageBox.Show("¿Desea salir de presupuestos?", "Presupuestos", MessageBoxButtons.YesNo);
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

		void comportamientoBotonesArticulos()
		{
			switch (accionPantallaArticulos)
			{
				case "C":
				case "A":
					pnlArticulos.Enabled = false;
					bAceptarArticulo.Enabled = false;

					bNuevoArticulo.Enabled = true;
					bEliminarArticulo.Enabled = true;
					bCancelarArticulo.Enabled = false;
					break;
				case "N":
				case "M":
				case "E":
				case "D":
					pnlArticulos.Enabled = true;
					bAceptarArticulo.Enabled = true;
					pnlOtrosDatos.Enabled = true;
					bNuevoArticulo.Enabled = false;
					bEliminarArticulo.Enabled = false;
					bCancelarArticulo.Enabled = true;
					break;
			}
		}

		void comportamientoBotonesDocumentos()
		{
			switch (accionPantallaDocumentos)
			{
				case "C":
				case "A":
					pnlOtrosDatos.Enabled = false;
					bAceptarDoc.Enabled = false;
					bNuevoDoc.Enabled = true;
					bEliminarDoc.Enabled = true;
					bCancelarDoc.Enabled = false;

					break;
				case "N":
				case "M":
				case "E":
				case "D":
					pnlOtrosDatos.Enabled = true;
					bAceptarDoc.Enabled = false;
					bAceptarDoc.Enabled = true;
					bNuevoDoc.Enabled = false;
					bEliminarDoc.Enabled = false;
					bCancelarDoc.Enabled = true;

					break;
			}
		}

		private void bNuevoArticulo_Click(object sender, EventArgs e)
		{
			try
			{

				blanquearLineasArticulos();
				accionPantallaArticulos = "N";
				comportamientoBotonesArticulos();
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bCancelarArticulo_Click(object sender, EventArgs e)
		{
			try
			{
				blanquearLineasArticulos();
				accionPantallaArticulos = "C";
				comportamientoBotonesArticulos();
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bEliminarArticulo_Click(object sender, EventArgs e)
		{
			try
			{
				//miramos si tiene Id, si tiene nos cepillamos el registro de la BBDD, y cargamos dtProveedores
				//Si no tiene id, nos cepillamos la linea de dtProveedores y volvemos a cargar.

				string strSql = "";

				DialogResult dialogResult = MessageBox.Show("¿Desea eliminar el siguiente proveedor de manera permanente?. " + "\n" + "\n" + dtgvArticulos.CurrentRow.Cells[4].Value.ToString(), "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

				if (dialogResult == DialogResult.Yes)
				{
					if (dtgvArticulos.CurrentRow.Cells[0].Value.ToString() != "")
					{
						//BBDD
						strSql = "Delete MantArticuloProveedores Where idArticuloProveedor = '" + dtgvArticulos.CurrentRow.Cells[0].Value.ToString() + "'";
						BaseDeDatos.ExecuteSelect(false, strSql);
						//Tambie me lo cepillo del dtProveedores.
						foreach (DataRow row in dtgvArticulos.Rows)
						{
							if (row["idProveedor"].ToString() == tCodArticulo.Tag.ToString())
							{
								row.Delete();
								row.AcceptChanges();
								break;
							}
						}
					}
					else
					{
						foreach (DataRow row in dtArticulos.Rows)
						{
							if (row["idArticulo"].ToString() == tCodArticulo.Tag.ToString())
							{
								row.Delete();
								row.AcceptChanges();
								break;
							}
						}
					}
					accionPantallaArticulos = "E";
					comportamientoBotonesArticulos();
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bBuscaArticulo_Click(object sender, EventArgs e)
		{
			try
			{
				if (accionPantallaArticulos == "M")
				{
					MessageBox.Show("El artículo no se debe modificar. " + "\n" + "\n" +
						"Si quiere cambiar de articulo debe borrar previamente la linea y generar una nueva", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					frmSelector frm1 = new frmSelector();
					frm1.TopMost = true;
					frm1.Owner = this;
					frm1.Titulo = "Selector de Articulos";
					frm1.strSql = "Select idArticulo, CodigoArticulo, Descripcion, idIVA from MantArticulos";
					frm1.ShowDialog();

					tCodArticulo.Tag = frm1.id;
					tCodArticulo.Text = frm1.codigo;
					tDescArticulo.Text = frm1.nombre;
					cIVA.SelectedValue = BaseDeDatos.ObtenerValor("MantArticulos", "idIVA", "Where idArticulo = '" + frm1.id + "'");

					//Blanquemamos los campos calculados ya que se ha seleccionado articulo nuevamente.
					tUnidades.Text = "";
					tImporteUnitario.Text = "";
					tImporteDescuento.Text = "";
					tImportePorLinea.Text = "";
					tObservaciones.Text = "";
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}



		private void tUnidades_Leave(object sender, EventArgs e)
		{
			try
			{
				calculoLineaArticulo();
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}
		private void calculoLineaArticulo()
		{
			try
			{
				string strSql = "";
				strSql = "Select PrecioVenta from MantArticuloTarifa Where idArticulo = '" + tCodArticulo.Tag + "' and " +
					"( hastaUnidades >= " + tUnidades.Text + " and desdeUnidades <= " + tUnidades.Text + ")";
				DataTable dtTarifa = BaseDeDatos.ExecuteSelect(true, strSql);
				if (dtTarifa.Rows.Count > 0)
				{
					tImporteUnitario.Text = dtTarifa.Rows[0]["PrecioVenta"].ToString();
					tImportePorLinea.Text = (decimal.Parse(tUnidades.Text) * decimal.Parse(tImporteUnitario.Text)).ToString("N2");


					// Ahora miramos si hay que aplicar descuento segun el valor del combo
					strSql = "Select porcentaje from MantDescuento Where idDescuento = '" + cDto.SelectedValue + "'";
					DataTable dtDto = BaseDeDatos.ExecuteSelect(true, strSql);
					if (dtDto.Rows.Count > 0)
					{
						tImporteDescuento.Text = (decimal.Parse(tImportePorLinea.Text) * decimal.Parse(dtDto.Rows[0]["porcentaje"].ToString()) / 100).ToString("N2");
						tImportePorLinea.Text = (decimal.Parse(tImportePorLinea.Text) - decimal.Parse(tImporteDescuento.Text)).ToString("N2");
					}
					else
					{
						MessageBox.Show("No se ha encontrado tarifa para la relacion de este Articulo / Unidades");
					}
				}
				else
				{
					if (dtTarifa.Rows.Count == 0 && chkArticuloLibre.Checked == false)
					{
						MessageBox.Show("No se ha encontrado tarifa para la relacion de este Articulo / Unidades");
					}

				}
			}

			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}

		}

		private void bAceptarArticulo_Click(object sender, EventArgs e)
		{
			try
			{

				CreamosdtArticulos();

				if (accionPantallaArticulos == "N")
				{
					rowArti = dtArticulos.NewRow();
					rowArti["idArticulo"] = tCodArticulo.Tag;
					rowArti["CodigoArticulo"] = tCodArticulo.Text;
					rowArti["Descripcion"] = tDescArticulo.Text;

					rowArti["idProveedor"] = tCodProveedor.Tag == "" ? DBNull.Value : tCodProveedor.Tag;
					rowArti["codigoProveedor"] = tCodProveedor.Text;
					rowArti["RazonSocial"] = tDescProveedor.Text;

					rowArti["Unidades"] = tUnidades.Text;
					rowArti["PrecioUnitario"] = tImporteUnitario.Text;
					rowArti["Total"] = tImportePorLinea.Text;
					rowArti["idDescuento"] = cDto.SelectedValue;
					rowArti["IVA"] = cIVA.SelectedItem;
					rowArti["ImpIVA"] = tImpIVA.Text;

					rowArti["ArticuloVario"] = chkArticuloLibre.Checked;
					rowArti["ProveedorConcreto"] = chkProveedorArt.Checked;
					rowArti["PrecioCoste"] = tImporteCoste.Text == "" ? "0" : tImporteCoste.Text;
					rowArti["MargenPorcentaje"] = tPorcMargen.Text == "" ? "0" : tPorcMargen.Text;
					rowArti["MargenUnitario"] = tMargenUnitario.Text == "" ? "0" : tMargenUnitario.Text;
					rowArti["MargenTotalLinea"] = tMargenTotLinea.Text == "" ? "0" : tMargenTotLinea.Text;
					rowArti["Observaciones"] = tObservaciones.Text;

					dtArticulos.Rows.Add(rowArti);
				}
				else
				{
					foreach (DataRow row in dtArticulos.Rows)
					{
						if (row["LineaPresupuesto"].ToString() == tLineaArt.Text)
						{
							row["idArticulo"] = tCodArticulo.Tag;
							row["CodigoArticulo"] = tCodArticulo.Text;
							row["Descripcion"] = tDescArticulo.Text;

							row["idProveedor"] = tCodProveedor.Tag == "" ? DBNull.Value : tCodProveedor.Tag;
							row["codigoProveedor"] = tCodProveedor.Text;
							row["RazonSocial"] = tDescProveedor.Text;

							row["Unidades"] = tUnidades.Text;
							row["PrecioUnitario"] = tImporteUnitario.Text;
							row["Total"] = tImportePorLinea.Text;
							row["idDescuento"] = cDto.SelectedValue;

							row["IVA"] = cIVA.SelectedItem;
							row["ImpIVA"] = tImpIVA.Text;

							row["ArticuloVario"] = chkArticuloLibre.Checked;
							row["ProveedorConcreto"] = chkProveedorArt.Checked;
							row["PrecioCoste"] = tImporteCoste.Text == "" ? "0" : tImporteCoste.Text;
							row["MargenPorcentaje"] = tPorcMargen.Text == "" ? "0" : tPorcMargen.Text;
							row["MargenUnitario"] = tMargenUnitario.Text == "" ? "0" : tMargenUnitario.Text;
							row["MargenTotalLinea"] = tMargenTotLinea.Text == "" ? "0" : tMargenTotLinea.Text;
							row["Observaciones"] = tObservaciones.Text;
						}
					}
				}

				dtgvArticulos.DataSource = dtArticulos;
				// Si es la primera vez, le aplicamos formato a la Grid de Proveedores.
				if (dtgvArticulos.Rows.Count > 0)
				{
					aplicaFormatoGridArticulos();
				}
				//Blanqueamos campos
				blanquearLineasArticulos();
				accionPantallaArticulos = "A";
				comportamientoBotonesArticulos();
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}
		void aplicaFormatoGridArticulos()
		{
			try
			{
				if (dtgvArticulos.Columns.Count > 0)
				{
					dtgvArticulos.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
					dtgvArticulos.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
					dtgvArticulos.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
					dtgvArticulos.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
					dtgvArticulos.Columns[18].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
					dtgvArticulos.Columns[19].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
					dtgvArticulos.Columns[20].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

					dtgvArticulos.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
					/*				dtgvArticulos.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
										dtgvArticulos.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
										dtgvArticulos.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
										dtgvArticulos.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
										dtgvArticulos.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
										dtgvArticulos.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
										dtgvArticulos.Columns[13].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
										dtgvArticulos.Columns[15].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
										dtgvArticulos.Columns[16].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
										dtgvArticulos.Columns[17].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
										dtgvArticulos.Columns[18].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
										dtgvArticulos.Columns[19].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
										dtgvArticulos.Columns[20].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
					*/

					//Ok, aqui ocultamos lo que son las columnas guid
					dtgvArticulos.Columns[0].Visible = false;
					dtgvArticulos.Columns[1].Visible = false;
					dtgvArticulos.Columns[2].Visible = false;
					dtgvArticulos.Columns[3].Visible = false;
					dtgvArticulos.Columns[6].Visible = false;
					dtgvArticulos.Columns[12].Visible = false;
					dtgvArticulos.Columns[13].Visible = false;

				}

			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		void CreamosdtDocumentos()
		{
			try

			{
				if (dtDocumentos.Columns.Count == 0)
				{
					//0
					DataColumn column;
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "idDocumento";
					column.Namespace = "id Documento";
					dtDocumentos.Columns.Add(column);
					//1
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "idPresupuesto";
					column.Namespace = "id Presupuesto";
					dtDocumentos.Columns.Add(column);
					//2
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "Descripcion";
					dtDocumentos.Columns.Add(column);
					//3
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "Ruta";
					dtDocumentos.Columns.Add(column);
					//4
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "Observaciones";
					dtDocumentos.Columns.Add(column);
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		void CreamosdtArticulos()
		{
			try

			{
				if (dtArticulos.Columns.Count == 0)
				{
					//0
					DataColumn column;
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "idPresupuesto";
					column.Namespace = "id Presupuesto";
					dtArticulos.Columns.Add(column);
					//1
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.Int32");
					column.ColumnName = "Linea";
					column.Namespace = "Linea";
					dtArticulos.Columns.Add(column);
					//2
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.TimeSpan");
					column.ColumnName = "FechaHora";
					dtArticulos.Columns.Add(column);
					//3
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "idArticulo";
					dtArticulos.Columns.Add(column);
					//4
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "CodigoArticulo";
					dtArticulos.Columns.Add(column);
					//5
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "Descripcion";
					dtArticulos.Columns.Add(column);
					//6
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "idProveedor";
					dtArticulos.Columns.Add(column);
					//7
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "codigoProveedor";
					dtArticulos.Columns.Add(column);
					//8
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "RazonSocial";
					column.Namespace = "Proveedor";
					dtArticulos.Columns.Add(column);
					//9
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.Int32");
					column.ColumnName = "Unidades";
					dtArticulos.Columns.Add(column);
					//10
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.Decimal");
					column.ColumnName = "PrecioUnitario";
					column.Namespace = "P/U";
					dtArticulos.Columns.Add(column);
					//11
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.Decimal");
					column.ColumnName = "Total";
					dtArticulos.Columns.Add(column);
					//12
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "idDescuento";
					dtArticulos.Columns.Add(column);
					//13
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "idIVA";
					dtArticulos.Columns.Add(column);
					//14
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "ImpIva";
					dtArticulos.Columns.Add(column);
					//15
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.Int32");
					column.ColumnName = "Status";
					dtArticulos.Columns.Add(column);
					//16
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "Observaciones";
					dtArticulos.Columns.Add(column);
					//17
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.Boolean");
					column.ColumnName = "ArticuloVario";
					dtArticulos.Columns.Add(column);
					//18
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.Boolean");
					column.ColumnName = "ProveedorConcreto";
					dtArticulos.Columns.Add(column);
					//19
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.Double");
					column.ColumnName = "PrecioCoste";
					column.Namespace = "Precio Coste";
					dtArticulos.Columns.Add(column);
					//20
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.Double");
					column.ColumnName = "MargenPorcentaje";
					column.Namespace = "Margen %";
					dtArticulos.Columns.Add(column);
					//21
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.Double");
					column.ColumnName = "MargenUnitario";
					column.Namespace = "Margen U.";
					dtArticulos.Columns.Add(column);
					//22
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.Double");
					column.ColumnName = "MargenTotalLinea";
					column.Namespace = "Margen T. Linea";
					dtArticulos.Columns.Add(column);
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void cDto_SelectedValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (accionPantalla != "C")
					calculoLineaArticulo();
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bBuscaCliente_Click(object sender, EventArgs e)
		{
			try
			{
				string strSql = "";
				DataTable dtDirecciones = new DataTable();
				if (accionPantalla == "M")
				{
					MessageBox.Show("El cliente no se debe modificar. " + "\n" + "\n" +
						"Si quiere cambiar de proveedor debe borrar previamente la linea y generar una nueva", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					frmSelector frmSelCliente = new frmSelector();
					frmSelCliente.TopMost = true;
					frmSelCliente.Owner = this;
					frmSelCliente.Titulo = "Selector de Clientes";
					frmSelCliente.strSql = "Select idCliente, CodigoCliente, RazonSocialFacturacion from MantClientes";
					frmSelCliente.ShowDialog();

					tCodCliente.Tag = frmSelCliente.id;
					tCodCliente.Text = frmSelCliente.codigo;
					tDescCliente.Text = frmSelCliente.nombre;

					//Ahora que tenemos el cliente, miramos si tiene mas de una direccion, si es así mostramos las posibles direcciones.
					strSql = "Select idDireccion, Domicilio, CodigoPostal, " +
						" dir.idProvincia, pro.CodigoProvincia, pro.Descripcion as Provincia, " +
						" dir.idPais, pai.CodigoPais, pai.Descripcion as Pais, " +
						" dir.idPoblacion, pob.CodigoPoblacion, pob.Descripcion as Poblacion" +
						" from MantClienteDirecciones dir" +
						" Left Join MantPoblaciones pob on  dir.idpoblacion = pob.idPoblacion" +
						" Left Join MantProvincias pro on dir.idProvincia = pro.idProvincia" +
						" Left Join MantPaises pai on dir.idPais = pai.idPais" +
						" Where idCliente = '" + tCodCliente.Tag + "'";

					dtDirecciones = BaseDeDatos.ExecuteSelect(true, strSql);
					if (dtDirecciones.Rows.Count == 1)
					{
						tDireccion.Text = dtDirecciones.Rows[0]["Domicilio"].ToString();
						tCodigoPostal.Text = dtDirecciones.Rows[0]["CodigoPostal"].ToString();
						tCodPoblacion.Tag = dtDirecciones.Rows[0]["idPoblacion"].ToString();
						tCodPoblacion.Text = dtDirecciones.Rows[0]["CodigoPoblacion"].ToString();
						tDescPoblacion.Text = dtDirecciones.Rows[0]["Poblacion"].ToString();
						tCodProvincia.Tag = dtDirecciones.Rows[0]["idProvincia"].ToString();
						tCodProvincia.Text = dtDirecciones.Rows[0]["CodigoProvincia"].ToString();
						tDescProvincia.Text = dtDirecciones.Rows[0]["Provincia"].ToString();
						tCodPais.Tag = dtDirecciones.Rows[0]["idPais"].ToString();
						tCodPais.Text = dtDirecciones.Rows[0]["CodigoPais"].ToString();
						tDescPais.Text = dtDirecciones.Rows[0]["Pais"].ToString();
					}
					else if (dtDirecciones.Rows.Count > 1)
					{
						frmSelector frmSelDirCli = new frmSelector();
						frmSelDirCli.TopMost = true;
						frmSelDirCli.Owner = this;
						frmSelDirCli.Titulo = "Selector de Direcciones de clientes";
						frmSelDirCli.strSql = strSql;
						frmSelDirCli.ShowDialog();

						strSql = "Select idDireccion, Domicilio, CodigoPostal, " +
							" dir.idProvincia, pro.CodigoProvincia, pro.Descripcion as Provincia, " +
							" dir.idPais, pai.CodigoPais, pai.Descripcion as Pais, " +
							" dir.idPoblacion, pob.CodigoPoblacion, pob.Descripcion as Poblacion" +
							" from MantClienteDirecciones dir" +
							" Left Join MantPoblaciones pob on  dir.idpoblacion = pob.idPoblacion" +
							" Left Join MantProvincias pro on dir.idProvincia = pro.idProvincia" +
							" Left Join MantPaises pai on dir.idPais = pai.idPais" +
							" Where idCliente = '" + tCodCliente.Tag + "' and idDireccion = '" + frmSelDirCli.id + "'";

						dtDirecciones = BaseDeDatos.ExecuteSelect(true, strSql);
						if (dtDirecciones.Rows.Count == 1)
						{
							tDireccion.Tag = dtDirecciones.Rows[0]["idDireccion"].ToString();
							tDireccion.Text = dtDirecciones.Rows[0]["Domicilio"].ToString();
							tCodigoPostal.Text = dtDirecciones.Rows[0]["CodigoPostal"].ToString();
							tCodPoblacion.Tag = dtDirecciones.Rows[0]["idPoblacion"].ToString();
							tCodPoblacion.Text = dtDirecciones.Rows[0]["CodigoPoblacion"].ToString();
							tDescPoblacion.Text = dtDirecciones.Rows[0]["Poblacion"].ToString();
							tCodProvincia.Tag = dtDirecciones.Rows[0]["idProvincia"].ToString();
							tCodProvincia.Text = dtDirecciones.Rows[0]["CodigoProvincia"].ToString();
							tDescProvincia.Text = dtDirecciones.Rows[0]["Provincia"].ToString();
							tCodPais.Tag = dtDirecciones.Rows[0]["idPais"].ToString();
							tCodPais.Text = dtDirecciones.Rows[0]["CodigoPais"].ToString();
							tDescPais.Text = dtDirecciones.Rows[0]["Pais"].ToString();
						}
					}
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void chkArticuloLibre_CheckedChanged(object sender, EventArgs e)
		{
			string strSql = "";
			DataTable dtArtVar = new DataTable();
			try
			{
				if (chkArticuloLibre.Checked)
				{
					//Si esta actiu a les hores es un article lliure, seleccionare l'article comodi
					//Axiò fara que,el codi d'article quedi enabled = False i la descripcio a True al igual que el preu de cost i preu de venta.
					strSql = "Select * from MantArticulos where CodigoArticulo = 'VAR'";
					dtArtVar = BaseDeDatos.ExecuteSelect(true, strSql);
					if (dtArtVar.Rows.Count == 1)
					{
						tCodArticulo.Text = "VAR";
						tCodArticulo.Tag = dtArtVar.Rows[0]["idArticulo"].ToString();
						tCodArticulo.Enabled = false;
						tDescArticulo.Enabled = true;
						tImporteCoste.Enabled = true;
						tImporteUnitario.Enabled = true;
						tPorcMargen.Enabled = true;
					}
				}
				else
				{
					tCodArticulo.Text = "";
					tCodArticulo.Tag = null;
					tDescArticulo.Text = "";
					tImporteCoste.Text = "";
					tImporteUnitario.Text = "";

					tCodArticulo.Enabled = true;
					tDescArticulo.Enabled = false;
					tImporteCoste.Enabled = false;
					tImporteUnitario.Enabled = false;
					tPorcMargen.Enabled = false;
				}
			}

			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}

		}

		private void chkProveedorArt_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (chkProveedorArt.Checked)
				{
					tCodProveedor.Enabled = true;
					bBuscaProvArt.Enabled = true;
					bBuscaProvArt.Enabled = true;
				}
				else
				{
					tCodProveedor.Text = "";
					tCodProveedor.Tag = null;
					tDescProveedor.Text = "";
					tCodProveedor.Enabled = false;
					bBuscaProvArt.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void tCodProveedor_TextChanged(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				frmSelector frm1 = new frmSelector();
				frm1.TopMost = true;
				frm1.Owner = this;
				frm1.Titulo = "Selector de Proveedores por artículo";
				frm1.strSql = "Select pro.idProveedor, pro.CodigoProveedor, pro.RazonSocial  " +
					" from MantArticuloProveedores art " +
					" Left Join MantProveedores pro on art.idProveedor = pro.idProveedor" +
					" where idArticulo = '" + tCodArticulo.Tag + "'";
				frm1.ShowDialog();

				tCodProveedor.Tag = frm1.id;
				tCodProveedor.Text = frm1.codigo;
				tDescProveedor.Text = frm1.nombre;

				//Si tenim artcile i proveedor ja ho tenim tot per ficar el preus i calculs.

				tUnidades.Text = "";
				tImporteUnitario.Text = "";
				tImporteDescuento.Text = "";
				tImportePorLinea.Text = "";
				tObservaciones.Text = "";
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}
		private void blanquearLineasArticulos()
		{
			try
			{
				chkArticuloLibre.Checked = false;
				chkProveedorArt.Checked = false;

				tCodProveedor.Tag = "";
				tCodProveedor.Text = "";
				tDescProveedor.Text = "";

				tCodArticulo.Tag = "";
				tCodArticulo.Text = "";
				tDescArticulo.Text = "";

				tUnidades.Text = "";
				tImporteUnitario.Text = "";
				tImporteDescuento.Text = "";
				tImportePorLinea.Text = "";
				tImporteCoste.Text = "";
				tPorcMargen.Text = "";
				cDto.SelectedIndex = 0;
				cIVA.SelectedIndex = 0;
				tMargenUnitario.Text = "";
				tMargenTotLinea.Text = "";
				tObservaciones.Text = "";
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void blanquearLineasDoc()
		{
			try
			{
				tDescripcionDoc.Text = "";
				tObservacionesDoc.Text = "";
				tPathDoc.Text = "";
				wDoc.Navigate("");
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void cMargen_SelectedIndexChanged(object sender, EventArgs e)
		{
			double margenUnitario = 0;
			double margenLinea = 0;
			double impCoste = 0;
			double importeVentaUnitario = 0;
			try
			{
				//Si lo tenemos todo, aqui aplicaremos el margen de veneficio sobre el precio de coste.

				if (tUnidades.Text != "" && tImporteCoste.Text != "" && tPorcMargen.Text != "")
				{
					margenUnitario = Math.Round(double.Parse(tImporteCoste.Text) * ((double.Parse(tPorcMargen.Text) / 100) + 1), 2);
					tImporteUnitario.Text = margenUnitario.ToString("N2");

					//margenLinea = margenUnitario * double.Parse(tUnidades.Text);
					margenUnitario = (double.Parse(tImporteUnitario.Text) - double.Parse(tImporteCoste.Text));
					margenLinea = margenUnitario * double.Parse(tUnidades.Text);
					impCoste = (double.Parse(tImporteCoste.Text));
					importeVentaUnitario = margenUnitario + impCoste;
					tImporteUnitario.Text = importeVentaUnitario.ToString("N2");
					// una vez calculado mostrammos importe de margen de beneficio e importe por lina.
					tMargenUnitario.Text = margenUnitario.ToString("N2");
					tMargenTotLinea.Text = margenLinea.ToString("N2");

					tImportePorLinea.Text = ((decimal.Parse(tUnidades.Text) * decimal.Parse(tImporteCoste.Text)) * ((decimal.Parse(tPorcMargen.Text) / 100) + 1)).ToString();
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void dtgvArticulos_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex > -1)
				{

					//accionPantallaArticulos = "M";
					//comportamientoBotonesArticulos();

					tLineaArt.Text = dtgvArticulos.Rows[e.RowIndex].Cells["LineaPresupuesto"].Value.ToString();

					chkArticuloLibre.Checked = bool.Parse(dtgvArticulos.Rows[e.RowIndex].Cells["ArticuloVario"].Value.ToString());
					tCodArticulo.Tag = dtgvArticulos.Rows[e.RowIndex].Cells["idArticulo"].Value.ToString();
					tCodArticulo.Text = dtgvArticulos.Rows[e.RowIndex].Cells["CodigoArticulo"].Value.ToString();
					tDescArticulo.Text = dtgvArticulos.Rows[e.RowIndex].Cells["Descripcion"].Value.ToString();

					chkProveedorArt.Checked = bool.Parse(dtgvArticulos.Rows[e.RowIndex].Cells["ProveedorConcreto"].Value.ToString());
					tCodProveedor.Tag = dtgvArticulos.Rows[e.RowIndex].Cells["idProveedor"].Value.ToString();
					tCodProveedor.Text = dtgvArticulos.Rows[e.RowIndex].Cells["CodigoProveedor"].Value.ToString();
					tDescProveedor.Text = dtgvArticulos.Rows[e.RowIndex].Cells["RazonSocial"].Value.ToString();

					tUnidades.Text = dtgvArticulos.Rows[e.RowIndex].Cells["Unidades"].Value.ToString();
					cDto.SelectedValue = dtgvArticulos.Rows[e.RowIndex].Cells["idDescuento"].Value.ToString();
					tImporteDescuento.Text = "";
					cIVA.SelectedValue = dtgvArticulos.Rows[e.RowIndex].Cells["IVA"].Value.ToString();
					tImpIVA.Text = dtgvArticulos.Rows[e.RowIndex].Cells["ImpIVA"].Value.ToString();
					tImporteCoste.Text = dtgvArticulos.Rows[e.RowIndex].Cells["PrecioCoste"].Value.ToString();
					tPorcMargen.Text = dtgvArticulos.Rows[e.RowIndex].Cells["MargenPorcentaje"].Value.ToString();
					tImporteUnitario.Text = dtgvArticulos.Rows[e.RowIndex].Cells["PrecioUnitario"].Value.ToString();
					tMargenUnitario.Text = dtgvArticulos.Rows[e.RowIndex].Cells["MargenUnitario"].Value.ToString();
					tMargenTotLinea.Text = dtgvArticulos.Rows[e.RowIndex].Cells["MargenTotalLinea"].Value.ToString();
					tImportePorLinea.Text = dtgvArticulos.Rows[e.RowIndex].Cells["Total"].Value.ToString();
					tObservaciones.Text = dtgvArticulos.Rows[e.RowIndex].Cells["Observaciones"].Value.ToString();

					//tPorcMargen.Text = dtgvArticulos.Rows[e.RowIndex].Cells[20].Value.ToString();
				}
			}

			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}
		private void GrabaPresupuesto(string accion, string guid)
		{
			try
			{
				string connectionString = "Data Source=XAVI-DESKTOP\\SQLEXPRESS;Initial Catalog=GestEmp;Integrated Security=true";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					// Inicia una transacción


					string strCabecera = "";
					string strLineas = "";
					string strDoc = "";
					int linea = 0;
					try
					{
						if (accion == "N")
						//Es nou fem un insert
						{
							SqlTransaction transacCab = connection.BeginTransaction();
							string codPre = clsfuncionesGenerales.devuelveID("PRE", this.Name);
							strCabecera = "INSERT INTO Presupuesto (idPresupuesto, CodigoPresupuesto, idCliente, idDireccionCliente, idIva, FechaHoraEmision, FechaHoraValidez, status, Activo, UsuarioCreacion, FechaHoraCreacion)" +
								" VALUES (@idPresupuesto, @CodigoPresupuesto, @idCliente, @idDireccionCliente, @idIva, @FechaHoraEmision, @FechaHoraValidez, @status, @Activo, @UsuarioCreacion, @FechahoraCreacion)";
							using (SqlCommand command1 = new SqlCommand(strCabecera, connection))
							{
								command1.Parameters.AddWithValue("@idPresupuesto", guid);
								command1.Parameters.AddWithValue("@CodigoPresupuesto", lblPresu.Text);
								command1.Parameters.AddWithValue("@idCliente", tCodCliente.Tag);
								command1.Parameters.AddWithValue("@idDireccionCliente", tDireccion.Tag ?? DBNull.Value);
								command1.Parameters.AddWithValue("@idIva", cIVA.SelectedValue);
								command1.Parameters.AddWithValue("@FechaHoraEmision", dtpFechaEmision.Value);
								command1.Parameters.AddWithValue("@FechaHoraValidez", dtpFechaVencimiento.Value);
								command1.Parameters.AddWithValue("@status", cStatus.Text.Substring(0, 1));
								command1.Parameters.AddWithValue("@Activo", 1);
								command1.Parameters.AddWithValue("@UsuarioCreacion", "SYSTEM");
								command1.Parameters.AddWithValue("@FechahoraCreacion", DateTime.Now);

								command1.Transaction = transacCab;
								command1.ExecuteNonQuery();
								transacCab.Commit();
							}
							SqlTransaction transacLin = connection.BeginTransaction();
							foreach (DataRow row in dtArticulos.Rows)
							{
								linea++;
								strLineas = "INSERT INTO PresupuestoLineas (idPresupuesto, LineaPresupuesto, FechaHora, idArticulo, idProveedor, DescripcionArticulo, ArticuloVario, ProveedorConcreto, Unidades, PrecioUnitario," +
								" Total, idDescuento, idIVA, ImpIVA, Status, CodigoDocumentoDestino, PrecioCoste, MargenPorcentaje, MargenUnitario, MargenTotalLinea, Observaciones, Activo, UsuarioCreacion, FechaHoraCreacion)" +
								" VALUES (@idPresupuesto, @LineaPresupuesto, @FechaHora, @idArticulo, @idProveedor, @DescripcionArticulo, @ArticuloVario, @ProveedorConcreto, @Unidades, @PrecioUnitario," +
								" @Total, @idDescuento, @idIVA, @ImpIVA, @Status, @CodigoDocumentoDestino, @PrecioCoste, @MargenPorcentaje, @MargenUnitario, @MargenTotalLinea, @Observaciones, @Activo, @UsuarioCreacion, @FechaHoraCreacion)";

								using (SqlCommand command2 = new SqlCommand(strLineas, connection))
								{
									command2.Parameters.AddWithValue("@idPresupuesto", guid);
									command2.Parameters.AddWithValue("@LineaPresupuesto", linea);
									command2.Parameters.AddWithValue("@FechaHora", DateTime.Now);
									command2.Parameters.AddWithValue("@idArticulo", row.ItemArray[3].ToString());
									command2.Parameters.AddWithValue("@idProveedor", row.ItemArray[6] ?? DBNull.Value);
									command2.Parameters.AddWithValue("@DescripcionArticulo", row.ItemArray[5].ToString());
									command2.Parameters.AddWithValue("@ArticuloVario", row.ItemArray[17].ToString());
									command2.Parameters.AddWithValue("@ProveedorConcreto", row.ItemArray[18].ToString());
									command2.Parameters.AddWithValue("@Unidades", row.ItemArray[9].ToString());
									command2.Parameters.AddWithValue("@PrecioUnitario", row.ItemArray[10].ToString());
									command2.Parameters.AddWithValue("@Total", row.ItemArray[11].ToString());
									command2.Parameters.AddWithValue("@idDescuento", row.ItemArray[12].ToString());
									command2.Parameters.AddWithValue("@idIVA", row.ItemArray[12].ToString());
									command2.Parameters.AddWithValue("@ImpIVA", row.ItemArray[13].ToString());
									command2.Parameters.AddWithValue("@Status", row.ItemArray[14].ToString());
									command2.Parameters.AddWithValue("@CodigoDocumentoDestino", "");
									command2.Parameters.AddWithValue("@PrecioCoste", row.ItemArray[18].ToString());
									command2.Parameters.AddWithValue("@MargenPorcentaje", row.ItemArray[20].ToString());
									command2.Parameters.AddWithValue("@MargenUnitario", row.ItemArray[21].ToString());
									command2.Parameters.AddWithValue("@MargenTotalLinea", row.ItemArray[22].ToString());
									command2.Parameters.AddWithValue("@Observaciones", row.ItemArray[16].ToString());
									command2.Parameters.AddWithValue("@Activo", 1);
									command2.Parameters.AddWithValue("@UsuarioCreacion", "SYSTEM");
									command2.Parameters.AddWithValue("@FechaHoraCreacion", DateTime.Now);

									command2.Transaction = transacLin;
									command2.ExecuteNonQuery();
								}

							}
							transacLin.Commit();
							SqlTransaction transacDoc = connection.BeginTransaction();
							foreach (DataRow row in dtDocumentos.Rows)
							{
								string guidDoc = "";
								guidDoc = Guid.NewGuid().ToString();

								strDoc = "Insert into PresupuestoDocs (idDocumento, idPresupuesto, Descripcion, Ruta, Observaciones, Activo, UsuarioCreacion, FechaHoraCreacion)" +
								" VALUES (@idDocumento, @idPresupuesto, @Descripcion, @Ruta, @Observaciones, @Activo, @UsuarioCreacion, @FechaHoraCreacion)";
								using (SqlCommand command3 = new SqlCommand(strDoc, connection))
								{
									command3.Parameters.AddWithValue("@idDocumento", guid);
									command3.Parameters.AddWithValue("@idPresupuesto", guidDoc);
									command3.Parameters.AddWithValue("@Descripcion", row.ItemArray[2].ToString());
									command3.Parameters.AddWithValue("@Ruta", row.ItemArray[3].ToString());
									command3.Parameters.AddWithValue("@Observaciones", row.ItemArray[4].ToString());
									command3.Parameters.AddWithValue("@Activo", 1);
									command3.Parameters.AddWithValue("@UsuarioCreacion", "SYSTEM");
									command3.Parameters.AddWithValue("@FechaHoraCreacion", DateTime.Now);
									command3.Transaction = transacDoc;
									command3.ExecuteNonQuery();
								}
							}
							transacDoc.Commit();
							//Ara que ja tenim tot guardat incrementem el sequencial
							string strSql = "Update SecuencialesConf Set Secuencial = " + (int.Parse(codPre) + 1) + " Where Referencia = 'PRE' and Pantalla = '" + this.Name + "'";
							BaseDeDatos.ExecuteSelect(true, strSql);
						}
						else
						//Es una modificacio, fem un Update
						{
							SqlTransaction transacCab = connection.BeginTransaction();
							strCabecera = "UPDATE Presupuesto SET idCliente = @idCliente, " +
							" idDireccionCliente = @idDireccionCliente, " +
							" idIva = @idIva, " +
							" FechahoraEmision = @FechaHoraEmision, " +
							" FechaHoraValidez = @FechaHoraValidez, " +
							" status = @status, " +
							" UsuarioModificacion = @UsuarioModificacion, " +
							" FechaHoraModificacion = @FechaHoraModificacion " +
							" WHERE idPresupuesto = '" + guid + "'";

							using (SqlCommand command1 = new SqlCommand(strCabecera, connection))
							{
								command1.Parameters.AddWithValue("@idCliente", tCodCliente.Tag);
								command1.Parameters.AddWithValue("@idDireccionCliente", tDireccion.Tag ?? DBNull.Value);
								command1.Parameters.AddWithValue("@idIva", cIVA.SelectedValue);
								command1.Parameters.AddWithValue("@FechaHoraEmision", dtpFechaEmision.Value);
								command1.Parameters.AddWithValue("@FechaHoraValidez", dtpFechaVencimiento.Value);
								command1.Parameters.AddWithValue("@status", cStatus.Text.Substring(0, 1));
								command1.Parameters.AddWithValue("@UsuarioModificacion", "SYSTEM");
								command1.Parameters.AddWithValue("@FechaHoraModificacion", DateTime.Now);
								command1.Transaction = transacCab;
								command1.ExecuteNonQuery();
								transacCab.Commit();
							}

							//string strSql = "Delete PresupuestoLineas where idPresupuesto = '" + guid + "'";
							//BaseDeDatos.ExecuteSelect(true, strSql);


							foreach (DataRow row in dtArticulos.Rows)
							{
								SqlTransaction transacLin = connection.BeginTransaction();
								if (!string.IsNullOrEmpty(row.ItemArray[1].ToString()))
								{
									// Existeix la linea, per tant updatajem


									strLineas = "Update PresupuestoLineas Set FechaHora  = @FechaHora, " +
										"idArticulo = @idArticulo, " +
										"idProveedor = @idProveedor, " +
										"DescripcionArticulo = @DescripcionArticulo, " +
										"ArticuloVario = @ArticuloVario, " +
										"ProveedorConcreto = @ProveedorConcreto, " +
										"Unidades = @Unidades, " +
										"PrecioUnitario=@PrecioUnitario, " +
										"Total=@Total, " +
										"idDescuento=@idDescuento, " +
										"idIVA=@idIVA, " +
										"ImpIVA=@ImpIVA, " +
										"Status=@Status, " +
										"CodigoDocumentoDestino=@CodigoDocumentoDestino, " +
										"PrecioCoste=@PrecioCoste, " +
										"MargenPorcentaje=@MargenPorcentaje, " +
										"MargenUnitario=@MargenUnitario, " +
										"MargenTotalLinea=@MargenTotalLinea, " +
										"Observaciones=@Observaciones, " +
										"Activo=@Activo, " +
										"UsuarioCreacion=@UsuarioCreacion, " +
										"FechaHoraCreacion=@FechaHoraCreacion " +
										"Where idPresupuesto = '" + guid + "' and LineaPresupuesto = " + row.ItemArray[1].ToString();

									using (SqlCommand command2 = new SqlCommand(strLineas, connection))
									{

										command2.Parameters.AddWithValue("@FechaHora", DateTime.Now);
										command2.Parameters.AddWithValue("@idArticulo", row.ItemArray[3].ToString());
										command2.Parameters.AddWithValue("@idProveedor", row.ItemArray[6] ?? DBNull.Value);
										command2.Parameters.AddWithValue("@DescripcionArticulo", row.ItemArray[5].ToString());
										command2.Parameters.AddWithValue("@ArticuloVario", row.ItemArray[17].ToString());
										command2.Parameters.AddWithValue("@ProveedorConcreto", row.ItemArray[18].ToString());
										command2.Parameters.AddWithValue("@Unidades", row.ItemArray[9].ToString());
										command2.Parameters.AddWithValue("@PrecioUnitario", row.ItemArray[10].ToString());
										command2.Parameters.AddWithValue("@Total", row.ItemArray[11].ToString());
										command2.Parameters.AddWithValue("@idDescuento", row.ItemArray[12].ToString());
										command2.Parameters.AddWithValue("@idIVA", row.ItemArray[13].ToString());
										command2.Parameters.AddWithValue("@ImpIVA", row.ItemArray[14].ToString());
										command2.Parameters.AddWithValue("@Status", row.ItemArray[15].ToString());
										command2.Parameters.AddWithValue("@CodigoDocumentoDestino", "");
										command2.Parameters.AddWithValue("@PrecioCoste", row.ItemArray[19].ToString());
										command2.Parameters.AddWithValue("@MargenPorcentaje", row.ItemArray[20].ToString());
										command2.Parameters.AddWithValue("@MargenUnitario", row.ItemArray[21].ToString());
										command2.Parameters.AddWithValue("@MargenTotalLinea", row.ItemArray[22].ToString());
										command2.Parameters.AddWithValue("@Observaciones", row.ItemArray[16].ToString());
										command2.Parameters.AddWithValue("@Activo", 1);
										command2.Parameters.AddWithValue("@UsuarioCreacion", "SYSTEM");
										command2.Parameters.AddWithValue("@FechaHoraCreacion", DateTime.Now);
										command2.Transaction = transacLin;
										command2.ExecuteNonQuery();
									}
								}
								else
								{
									// No existeix la linea. la fem nova.

									int.TryParse(BaseDeDatos.ObtenerValor("PresupuestoLineas", "CAST(MAX(LineaPresupuesto)+1 AS VARCHAR(2) ) AS LineaPresupuesto ", "Where idPresupuesto = '" + guid + "' "), out linea);
									strLineas = "Insert into PresupuestoLineas (idPresupuesto, LineaPresupuesto, FechaHora, idArticulo, idProveedor, DescripcionArticulo, ArticuloVario, ProveedorConcreto, Unidades, PrecioUnitario," +
									" Total, idDescuento, Status, CodigoDocumentoDestino, PrecioCoste, MargenPorcentaje, MargenUnitario, MargenTotalLinea, Observaciones, Activo, UsuarioCreacion, FechaHoraCreacion)" +
									" VALUES (@idPresupuesto, @LineaPresupuesto, @FechaHora, @idArticulo, @idProveedor, @DescripcionArticulo, @ArticuloVario, @ProveedorConcreto, @Unidades, @PrecioUnitario," +
									" @Total, @idDescuento, @Status, @CodigoDocumentoDestino, @PrecioCoste, @MargenPorcentaje, @MargenUnitario, @MargenTotalLinea, @Observaciones, @Activo, @UsuarioCreacion, @FechaHoraCreacion)";
									using (SqlCommand command2 = new SqlCommand(strLineas, connection))
									{
										command2.Parameters.AddWithValue("@idPresupuesto", guid);
										command2.Parameters.AddWithValue("@LineaPresupuesto", linea);
										command2.Parameters.AddWithValue("@FechaHora", DateTime.Now);
										command2.Parameters.AddWithValue("@idArticulo", row.ItemArray[3].ToString());
										command2.Parameters.AddWithValue("@idProveedor", row.ItemArray[6] ?? DBNull.Value);
										command2.Parameters.AddWithValue("@DescripcionArticulo", row.ItemArray[5].ToString());
										command2.Parameters.AddWithValue("@ArticuloVario", row.ItemArray[15].ToString());
										command2.Parameters.AddWithValue("@ProveedorConcreto", row.ItemArray[16].ToString());
										command2.Parameters.AddWithValue("@Unidades", row.ItemArray[9].ToString());
										command2.Parameters.AddWithValue("@PrecioUnitario", row.ItemArray[10].ToString());
										command2.Parameters.AddWithValue("@Total", row.ItemArray[11].ToString());
										command2.Parameters.AddWithValue("@idDescuento", row.ItemArray[12].ToString());
										command2.Parameters.AddWithValue("@Status", row.ItemArray[13].ToString());
										command2.Parameters.AddWithValue("@CodigoDocumentoDestino", "");
										command2.Parameters.AddWithValue("@PrecioCoste", row.ItemArray[17].ToString());
										command2.Parameters.AddWithValue("@MargenPorcentaje", row.ItemArray[18].ToString());
										command2.Parameters.AddWithValue("@MargenUnitario", row.ItemArray[19].ToString());
										command2.Parameters.AddWithValue("@MargenTotalLinea", row.ItemArray[20].ToString());
										command2.Parameters.AddWithValue("@Observaciones", row.ItemArray[14].ToString());
										command2.Parameters.AddWithValue("@Activo", 1);
										command2.Parameters.AddWithValue("@UsuarioCreacion", "SYSTEM");
										command2.Parameters.AddWithValue("@FechaHoraCreacion", DateTime.Now);
										command2.Transaction = transacLin;
										command2.ExecuteNonQuery();
									}
								}
								transacLin.Commit();
							}

							foreach (DataRow row in dtDocumentos.Rows)
							{
								SqlTransaction transacDoc = connection.BeginTransaction();
								if (!string.IsNullOrEmpty(row.ItemArray[1].ToString()))
								{
									// Existeix la linea, per tant updatajem
									strLineas = "Update PresupuestoDocs Set Ruta  = @Ruta, " +
										"Descripcion = @Descripcion, " +
										"Observaciones = @Observaciones, " +
										"UsuarioModificacion=@UsuarioModificacion, " +
										"FechaHoraModificacion=@FechaHoraModificacion " +
										"Where idPresupuesto = '" + guid + "' and idDocumento = '" + row.ItemArray[0].ToString() + "'";

									using (SqlCommand command2 = new SqlCommand(strLineas, connection))
									{

										command2.Parameters.AddWithValue("@Ruta", row.ItemArray[3].ToString());
										command2.Parameters.AddWithValue("@Descripcion", row.ItemArray[2].ToString());
										command2.Parameters.AddWithValue("@Observaciones", row.ItemArray[4].ToString());
										command2.Parameters.AddWithValue("@UsuarioModificacion", "SYSTEM");
										command2.Parameters.AddWithValue("@FechaHoraModificacion", DateTime.Now);
										command2.Transaction = transacDoc;
										command2.ExecuteNonQuery();
									}
								}
								else
								{
									string guidDoc = "";
									guidDoc = Guid.NewGuid().ToString();

									strDoc = "Insert into PresupuestoDocs (idDocumento, idPresupuesto, Descripcion, Ruta, Observaciones, Activo, UsuarioCreacion, FechaHoraCreacion)" +
									" VALUES (@idDocumento, @idPresupuesto, @Descripcion, @Ruta, @Observaciones, @Activo, @UsuarioCreacion, @FechaHoraCreacion)";
									using (SqlCommand command3 = new SqlCommand(strDoc, connection))
									{
										command3.Parameters.AddWithValue("@idDocumento", guidDoc);
										command3.Parameters.AddWithValue("@idPresupuesto", guid);
										command3.Parameters.AddWithValue("@Descripcion", row.ItemArray[2].ToString());
										command3.Parameters.AddWithValue("@Ruta", row.ItemArray[3].ToString());
										command3.Parameters.AddWithValue("@Observaciones", row.ItemArray[4].ToString());
										command3.Parameters.AddWithValue("@Activo", 1);
										command3.Parameters.AddWithValue("@UsuarioCreacion", "SYSTEM");
										command3.Parameters.AddWithValue("@FechaHoraCreacion", DateTime.Now);
										command3.Transaction = transacDoc;
										command3.ExecuteNonQuery();
									}
								}
								transacDoc.Commit();
							}

						}
					}
					catch (Exception ex)
					{
						// Ocurrió un error, deshace la transacción
						clsfuncionesGenerales obj = new clsfuncionesGenerales();
						obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
					}
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}
		private bool validamosLinea()
		{
			try
			{
				return true;
				if (tCodArticulo.Text == "" || tCodArticulo.Tag == "")
				{
					MessageBox.Show("El articulo es un valor obligaotrio", "Faltan datos");
					return false;
				}

				if (tUnidades.Text == "")
				{
					MessageBox.Show("Falta indicar las unidades", "Faltan datos");
					return false;
				}

			}
			catch (Exception ex)
			{
				// Ocurrió un error, deshace la transacción
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
				return false;
			}
		}

		private void calculamosTotales()
		{
			try

			{
				float unidades = 0;
				float coste = 0;
				float margen = 0;
				float total = 0;
				foreach (DataRow row in dtArticulos.Rows)
				{
					unidades = unidades + float.Parse(row["Unidades"].ToString());
					coste = coste + (float.Parse(row["PrecioCoste"].ToString()) * float.Parse(row["Unidades"].ToString()));
					margen = margen + float.Parse(row["MargenTotalLinea"].ToString());
					total = total + float.Parse(row["Total"].ToString());
				}

				lblCoste.Text = coste.ToString("F2");
				lblUnidades.Text = unidades.ToString();
				lblTotal.Text = total.ToString("F2");
				lblMargen.Text = margen.ToString("F2");
			}

			catch (Exception ex)
			{

				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);

			}
		}

		private void bAceptarDoc_Click(object sender, EventArgs e)
		{
			try
			{
				CreamosdtDocumentos();
				string guid = "";
				if (accionPantallaDocumentos == "N")
				{
					rowDocu = dtDocumentos.NewRow();
					guid = Guid.NewGuid().ToString();
					//rowDocu["idDocumento"] = guid;
					//rowDocu["idPresupuesto"] = tCodArticulo.Text;
					rowDocu["Descripcion"] = tDescripcionDoc.Text;
					rowDocu["Observaciones"] = tObservacionesDoc.Text;
					rowDocu["Ruta"] = tPathDoc.Text;

					dtDocumentos.Rows.Add(rowDocu);
				}
				else
				{
					foreach (DataRow rowDocu in dtDocumentos.Rows)
					{
						if (rowDocu["idDocumento"].ToString() == tidDocumento.Text)
						{
							rowDocu["Descripcion"] = tDescripcionDoc.Text;
							rowDocu["Observaciones"] = tObservacionesDoc.Text;
							rowDocu["Ruta"] = tPathDoc.Text;
						}
					}
				}

				dtgvDocumentos.DataSource = dtDocumentos;
				// Si es la primera vez, le aplicamos formato a la Grid de Proveedores.
				if (dtgvDocumentos.Rows.Count > 0)
				{
					//aplicaFormatoGridArticulos();
				}
				//Blanqueamos campos
				blanquearLineasDoc();
				accionPantallaDocumentos = "A";
				comportamientoBotonesDocumentos();
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bNuevoDoc_Click(object sender, EventArgs e)
		{
			try
			{
				blanquearLineasDoc();
				accionPantallaDocumentos = "N";
				comportamientoBotonesDocumentos();
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bEliminarDoc_Click(object sender, EventArgs e)
		{
			try
			{
				//miramos si tiene Id, si tiene nos cepillamos el registro de la BBDD, y cargamos dtProveedores
				//Si no tiene id, nos cepillamos la linea de dtProveedores y volvemos a cargar.

				string strSql = "";

				DialogResult dialogResult = MessageBox.Show("¿Desea eliminar el siguiente proveedor de manera permanente?. " + "\n" + "\n" + dtgvArticulos.CurrentRow.Cells[4].Value.ToString(), "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

				if (dialogResult == DialogResult.Yes)
				{
					if (dtgvArticulos.CurrentRow.Cells[0].Value.ToString() != "")
					{
						//BBDD
						strSql = "Delete MantArticuloProveedores Where idArticuloProveedor = '" + dtgvArticulos.CurrentRow.Cells[0].Value.ToString() + "'";
						BaseDeDatos.ExecuteSelect(false, strSql);
						//Tambie me lo cepillo del dtProveedores.
						foreach (DataRow row in dtgvArticulos.Rows)
						{
							if (row["idProveedor"].ToString() == tCodArticulo.Tag.ToString())
							{
								row.Delete();
								row.AcceptChanges();
								break;
							}
						}
					}
					else
					{
						foreach (DataRow row in dtArticulos.Rows)
						{
							if (row["idArticulo"].ToString() == tCodArticulo.Tag.ToString())
							{
								row.Delete();
								row.AcceptChanges();
								break;
							}
						}
					}
					accionPantallaArticulos = "E";
					comportamientoBotonesArticulos();
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bCancelarDoc_Click(object sender, EventArgs e)
		{
			try
			{
				blanquearLineasDoc();
				accionPantallaDocumentos = "C";
				comportamientoBotonesDocumentos();
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bBuscarDoc_Click_1(object sender, EventArgs e)
		{
			try
			{
				opFile.InitialDirectory = @"C:\";
				opFile.ShowDialog();
				tPathDoc.Text = opFile.FileName;

			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void tPathDoc_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (File.Exists(tPathDoc.Text))
				{
					wDoc.Navigate(tPathDoc.Text);
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}
		private void dtgvDocumentos_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex > -1)
				{

					tidDocumento.Text = dtgvDocumentos.Rows[e.RowIndex].Cells["idDocumento"].Value.ToString();
					tDescripcionDoc.Text = dtgvDocumentos.Rows[e.RowIndex].Cells["Descripcion"].Value.ToString();
					tObservacionesDoc.Text = dtgvDocumentos.Rows[e.RowIndex].Cells["Observaciones"].Value.ToString();
					tPathDoc.Text = dtgvDocumentos.Rows[e.RowIndex].Cells["Ruta"].Value.ToString();
					string rutaDoc = tPathDoc.Text;
					wDoc.Navigate(Path.Combine(rutaDoc.ToString()));
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}
		private void tPorcMargen_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				calculaImports();
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void cDto_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				calculaImports();
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void cIVA_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				calculaImports();
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}


		private void calculaImports()
		{
			double margenUnitario = 0;
			double margenLinea = 0;
			double impCoste = 0;
			double importeVentaUnitario = 0;
			double impIVA = 0;
			try
			{
				//Nota: primero se aplica el descuento sobre la B.I del producto unitario con el margen ya aplicado si es el caso.
				//Nota: una vez ya aplicado el descuento, se aplica el IVA sobre el importe anteriormente calcula unitario
				//Si lo tenemos todo, aqui aplicaremos el margen de veneficio sobre el precio de coste.
				if (chkArticuloLibre.Checked)
				{
					//Se es un article Stard.

				}
				else
				{
					//Si es un article libre, vol dir que te uns marges obligatoriament.
					if (tUnidades.Text != "" && tImporteCoste.Text != "" && tPorcMargen.Text != "")
					{
						margenUnitario = Math.Round(double.Parse(tImporteCoste.Text) * ((double.Parse(tPorcMargen.Text) / 100) + 1), 2);
						tImporteUnitario.Text = margenUnitario.ToString("N2");

						//margenLinea = margenUnitario * double.Parse(tUnidades.Text);
						margenUnitario = (double.Parse(tImporteUnitario.Text) - double.Parse(tImporteCoste.Text));
						margenLinea = margenUnitario * double.Parse(tUnidades.Text);
						impCoste = (double.Parse(tImporteCoste.Text));
						importeVentaUnitario = margenUnitario + impCoste;
						tImporteUnitario.Text = importeVentaUnitario.ToString("N2");
						impIVA = double.Parse(tImporteUnitario.Text) * double.Parse(cIVA.Text);
						impIVA = impIVA / 100;
						tImpIVA.Text = impIVA.ToString("N2");
						// una vez calculado mostrammos importe de margen de beneficio e importe por lina.
						tMargenUnitario.Text = margenUnitario.ToString("N2");
						tMargenTotLinea.Text = margenLinea.ToString("N2");

						tImportePorLinea.Text = ((decimal.Parse(tUnidades.Text) * decimal.Parse(tImporteCoste.Text)) * ((decimal.Parse(tPorcMargen.Text) / 100) + 1)).ToString();
					}
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
