using GestEmp_2.clsFunciones;
using GestEmp_2.clsFuncionesGenerales;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GestEmp_2.Mantenimientos
{

	public partial class Articulos : Form
	{
		//Poslbles estados
		//C-Consultar, A-Modificar, M-Modificar, E-Eliminar (Activo = 0), D-Duplicar
		string accionPantalla = "C";
		string accionPantallaProveedor = "C";
		string accionPantallaTarifas = "C";
		DataTable dtProveedores = new DataTable();
		DataTable dtTarifas = new DataTable();
		DataRow row;
		public Articulos()
		{
			InitializeComponent();
		}

		private void panel2_Paint(object sender, PaintEventArgs e)
		{

		}

		private void Articulos_Load(object sender, EventArgs e)
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
				accionPantallaProveedor = "C";
				accionPantallaTarifas = "C";
				comportamientoBotones();
				comportamientoBotonesProveedores();
				comportamientoBotonesTarifas();
				dtgvDatos.DataSource = BaseDeDatos.ExecuteSelect(true, "Select idArticulo, CodigoArticulo as Codigo, Descripcion from MantArticulos");
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
				strSql = "Select idTalla, Descripcion from MantTallas";
				DataTable dtTallas = BaseDeDatos.ExecuteSelect(true, strSql);
				if (dtTallas.Rows.Count > 0)
				{
					cTallas.DataSource = dtTallas;
					cTallas.DisplayMember = dtTallas.Columns[1].ColumnName; //Descripcion
					cTallas.ValueMember = dtTallas.Columns[0].ColumnName;   //id

				}

				strSql = "Select idColor, Descripcion from MantColores";
				DataTable dtDto = BaseDeDatos.ExecuteSelect(true, strSql);
				if (dtDto.Rows.Count > 0)
				{
					chkColores.DataSource = dtDto;
					chkColores.DisplayMember = dtDto.Columns[1].ColumnName; //Descripcion
					chkColores.ValueMember = dtDto.Columns[0].ColumnName;   //id
				}

				strSql = "Select idFamilia, Descripcion from MantFamilias";
				DataTable dtFpago = BaseDeDatos.ExecuteSelect(true, strSql);
				if (dtFpago.Rows.Count > 0)
				{
					cFamilia.DataSource = dtFpago;
					cFamilia.DisplayMember = dtFpago.Columns[1].ColumnName; //Descripcion
					cFamilia.ValueMember = dtFpago.Columns[0].ColumnName;   //id
				}

				strSql = "Select idUnidadMedida, Descripcion from MantUnidadesMedida";
				DataTable dtUnidadesMedida = BaseDeDatos.ExecuteSelect(true, strSql);
				if (dtUnidadesMedida.Rows.Count > 0)
				{
					cUnidadMedida.DataSource = dtUnidadesMedida;
					cUnidadMedida.DisplayMember = dtUnidadesMedida.Columns[1].ColumnName; //Descripcion
					cUnidadMedida.ValueMember = dtUnidadesMedida.Columns[0].ColumnName;   //id
				}

				strSql = "Select idIVA, Porcentaje from MantIVA";
				DataTable dtIva = BaseDeDatos.ExecuteSelect(true, strSql);
				if (dtUnidadesMedida.Rows.Count > 0)
				{
					cIVA.DataSource = dtIva;
					cIVA.DisplayMember = dtIva.Columns[1].ColumnName; //Porcentaje
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
				string strSQl = "Select idArticulo, CodigoArticulo, CompraVenta, art.Descripcion, DescripcionAmplia, art.idFamilia, " +
					" art.idTalla, idIVA, StockMinimo, CosteTiempo, Colores, RutaImagen " +
					" from MantArticulos art" +
					" Left Join MantFamilias fam on art.idFamilia = fam.idFamilia" +
					" Left Join MantTallas tal on art.idTalla = tal.idTalla" +
					" Where idArticulo = '" + id + "'";

				DataTable dtDetalle = BaseDeDatos.ExecuteSelect(true, strSQl);

				if (dtDetalle.Rows.Count > 0)
				{

					tCodigo.Text = dtDetalle.Rows[0]["CodigoArticulo"].ToString();
					cCompraVenta.SelectedIndex = Convert.ToInt32(dtDetalle.Rows[0]["CompraVenta"].ToString());
					tDescripcion.Text = dtDetalle.Rows[0]["Descripcion"].ToString();
					tDescripcionAmplia.Text = dtDetalle.Rows[0]["DescripcionAmplia"].ToString();

					cFamilia.SelectedValue = dtDetalle.Rows[0]["idFamilia"].ToString();
					cTallas.SelectedValue = dtDetalle.Rows[0]["idTalla"].ToString();
					cIVA.SelectedValue = dtDetalle.Rows[0]["idIVA"].ToString();

					tStockMinimo.Text = dtDetalle.Rows[0]["StockMinimo"].ToString();
					tTiempoProd.Text = dtDetalle.Rows[0]["CosteTiempo"].ToString();

					pArticulo.ImageLocation = dtDetalle.Rows[0]["RutaImagen"].ToString();

					//Aqui montamos "historia" para marcar los checks de los colores...

					for (int i = 0; i < chkColores.Items.Count; i++)
					{
						chkColores.SetItemChecked(i, false);
					}

					string[] colores = dtDetalle.Rows[0]["Colores"].ToString().Split(',');
					for (int item = 0; item < chkColores.Items.Count; item++) //recorremos checklist
					{
						for (int i = 0; i < colores.Length; i++) //recorremos colorines
							if (colores[i] == ((System.Data.DataRowView)chkColores.Items[item]).Row.ItemArray[0].ToString()) // si el id del colorin es el mismo que el de la check marcamos.
								chkColores.SetItemChecked(item, true);
					}

					mostramosDetalleProveedor(id);
					mostramosDetalleTarifas(id);
					calculoPrecioCoste();
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
					pnlTarifas.Enabled = false;
					pnlProveedores.Enabled = false;
					pnlBotonesProveedores.Enabled = false;

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
					pnlFacturacion.Enabled = true;
					pnlOtrosDatos.Enabled = true;
					pnlBotonesProveedores.Enabled = true;
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

		void comportamientoBotonesProveedores()
		{
			switch (accionPantallaProveedor)
			{
				case "C":
				case "A":
					pnlProveedores.Enabled = false;
					bAceptarProveedor.Enabled = false;
					bNuevoProveedor.Enabled = true;
					bEliminarProveedor.Enabled = true;
					bCancelarProveedor.Enabled = false;
					break;
				case "N":
				case "M":
				case "E":
				case "D":
					pnlProveedores.Enabled = true;
					bAceptarProveedor.Enabled = true;
					bNuevoProveedor.Enabled = false;
					bEliminarProveedor.Enabled = false;
					bCancelarProveedor.Enabled = true;
					break;
			}
		}

		void comportamientoBotonesTarifas()
		{
			switch (accionPantallaTarifas)
			{
				case "C":
				case "A":
					pnlTarifas.Enabled = false;
					bAceptarTarifa.Enabled = false;
					bNuevoTarifa.Enabled = true;
					bEliminarTarifa.Enabled = true;
					bCancelarTarifa.Enabled = false;
					break;
				case "N":
				case "M":
				case "E":
				case "D":
					pnlTarifas.Enabled = true;
					bAceptarTarifa.Enabled = true;
					bNuevoTarifa.Enabled = false;
					bEliminarTarifa.Enabled = false;
					bCancelarTarifa.Enabled = true;
					break;
			}
		}

		private void bAceptar_Click(object sender, EventArgs e)
		{
			try
			{
				string strSql = "";
				string guid = "";
				string colores = "";
				TimeSpan tiempo;

				if (ValidaDatos())
				{

					//Aqui montamos la matriz de colores para "encasquetarselo" a la Query
					foreach (DataRowView item in chkColores.CheckedItems)
					{
						colores += string.Format("{0},", item["idColor"]);
					}
					//Damos formato al tiempo 

					if (!tTiempoProd.Text.Contains(':') && tTiempoProd.Text != "")
					{
						tiempo = new TimeSpan(Convert.ToInt32(Math.Floor(decimal.Parse(tTiempoProd.Text))),
									Convert.ToInt32((decimal.Parse(tTiempoProd.Text) - Math.Floor(decimal.Parse(tTiempoProd.Text))) * 60), 0);
						tTiempoProd.Text = $"{tiempo.Hours}:{tiempo.Minutes}:{tiempo.Seconds}";
					}

					if (accionPantalla == "N")
					{
						guid = Guid.NewGuid().ToString();
						strSql = "Insert into MantArticulos (idArticulo" +
						 ", CodigoArticulo , idFamilia, idTalla, idIVA, CompraVenta, Descripcion, DescripcionAmplia, idUnidadMedida" +
						 ", PrecioUltimo, CosteTiempo, Colores, StockMinimo, RutaImagen" +
						 ", Tipo, TratamientoExistencia, ControlDelote, MesesCaducidad, DiasCuarentena, AdmiteFraccion, CodigoBarras, TipoCodigoBarras, UnidadesBultoPrimario" +
						 ", UnidadesBultoPalet, LeadTime, LoteLeadTime, TipoUbicacion, TipoObligatoriedad, Pictograma1, Pictograma2, Pictograma3, Pictograma4, Pictograma5 " +
						 ", Frase1, Frase2, Frase3, Frase4, Frase5, PesoNeto, PesoBruto, Alto, Ancho, Profundo, LoteMinimoFaboCom, Multiplos, NomenclaturaConvinada, idGrupoContable" +
						 "Activo, UsuarioCreacion, FechaHoraCreacion) Values ('" +
								guid + "', '" +
								tCodigo.Text + "', '" +
								cFamilia.SelectedValue.ToString() + "', '" +
								cTallas.SelectedValue.ToString() + "', '" +
								cIVA.SelectedValue.ToString() + "', '" +
								cCompraVenta.SelectedIndex + "', '" +
								tDescripcion.Text + "', '" +
								tDescripcionAmplia.Text + "', '" +
								cUnidadMedida.SelectedValue.ToString() + "', '" +
								tPrecioUltiimaCompra.Text + "', '" +
								tTiempoProd.Text + "', '" +
								colores + "', '" +
								int.Parse(tStockMinimo.Text) + "', '" +
								pArticulo.ImageLocation + "', " +
								cTipo.SelectedIndex + "', " +
								cbTratamientoExistencias.Checked + "', " +
								cControlLotes.SelectedIndex + "', " +
								tMesesCaducidad.Text + "', " +
								tDiasCuarentena.Text + "', " +
								cbAdmiteFraccion.Checked + "', " +
								tCodigoBarras.Text + "', " +
								tCodTipoCodigoBarras.Tag + "', " +
								tUnidadesBultoPrimario.Text + "', " +
								tUnidadesBultosPalet.Text + "', " +
								dtpLeadTime.Text + "', " +
								dtpLoteLeadTime.Text + "', " +
								tCodTipoUbicacion.Tag + "', " +
								cTipoObligatorierdad.Text + "', " +
								pbPictograma1.ImageLocation + "', " +
								pbPictograma2.ImageLocation + "', " +
								pbPictograma3.ImageLocation + "', " +
								pbPictograma4.ImageLocation + "', " +
								pbPictograma5.ImageLocation + "', " +
								tTexto1.Text + "', " +
								tTexto2.Text + "', " +
								tTexto3.Text + "', " +
								tTexto4.Text + "', " +
								tTexto5.Text + "', " +
								tPesoBruto.Text + "', " +
								tPesoBruto.Text + "', " +
								tAlto.Text + "', " +
								tAncho.Text + "', " +
								tProfundo.Text + "', " +
								tLoteMinFaboCom.Text + "', " +
								tMultiplos.Text + "', " +
								tNomenclaturaConvinada.Text + "', " +
								tCodGrupoContable.Tag + "', " +
								"1, 'SYSTEM','" +
								DateTime.Now + "')";
						BaseDeDatos.ExecuteSelect(true, strSql);
						foreach (DataRow row in dtProveedores.Rows)
						{

							strSql = "Insert into MantArticuloProveedores (idArticuloProveedor, idArticulo, idProveedor, PrecioProveedor, PrecioEnvioProveedor, " +
								"Activo, UsuarioCreacion, FechaHoraCreacion) Values" +
								"('" + Guid.NewGuid().ToString() + "', " +   //Guid articulo proveedor
								"'" + guid + "', " +                     //idArticulo
								"'" + row.ItemArray[2].ToString() + "', " +  //idProveedor
								row.ItemArray[5].ToString() + ", " +         //PrecioProveedor
								row.ItemArray[6].ToString() + ", " +         //PrevioEnvio
								"1, 'SYSTEM','" +
								DateTime.Now + "')";
							BaseDeDatos.ExecuteSelect(true, strSql);
						}

						foreach (DataRow row in dtTarifas.Rows)
						{
							strSql = "Insert into MantArticuloTarifa (idTarifa, idArticulo, desdeUnidades, hastaUnidades, precioVenta) Values" +
								"('" + Guid.NewGuid().ToString() + "', " +   //Guid articulo proveedor
								"'" + guid + "', " +                     //idArticulo
								"'" + row.ItemArray[2].ToString() + "', " +  //desde
								row.ItemArray[3].ToString() + ", " +         //hasta
								row.ItemArray[4].ToString() + ") ";          //PrecioVenta
							BaseDeDatos.ExecuteSelect(true, strSql);
						}
					}

					else
					{
						string talla = cTallas.SelectedIndex > 0 ? "'" + cTallas.SelectedValue.ToString() + "'" : "null";
						guid = tId.Text;
						strSql = "Update MantArticulos Set CodigoArticulo = '" + tCodigo.Text +
							"', idFamilia = '" + cFamilia.SelectedValue.ToString() +
							"', idTalla = " + talla +
							", idIVA = '" + cIVA.SelectedValue.ToString() +
							"', CompraVenta = '" + cCompraVenta.SelectedIndex +
							"', Descripcion = '" + tDescripcion.Text +
							"', DescripcionAmplia = '" + tDescripcionAmplia.Text +
							"', idUnidadMedida = '" + cUnidadMedida.SelectedValue.ToString() +
							"', CosteTiempo = '" + tTiempoProd.Text +
							"', Colores = '" + colores +
							"', StockMinimo = '" + tStockMinimo.Text +
							"', RutaImagen = '" + pArticulo.ImageLocation +
						"', UsuarioModificacion = 'SYSTEM', FechaHoraModificacion = '" + DateTime.Now.ToString() + "' Where idArticulo = '" + tId.Text + "'";
						BaseDeDatos.ExecuteSelect(true, strSql);
						//Aqui montamos las lineas de proveedor
						foreach (DataRow row in dtProveedores.Rows)
						{
							if (row.ItemArray[0].ToString() == "")
							{
								strSql = "Insert into MantArticuloProveedores (idArticuloProveedor, idArticulo, idProveedor, PrecioProveedor, PrecioEnvioProveedor, " +
									"Activo, UsuarioCreacion, FechaHoraCreacion) Values" +
									"('" + Guid.NewGuid().ToString() + "', " +   //Guid articulo proveedor
									"'" + tId.Text + "', " +                     //idArticulo
									"'" + row.ItemArray[2].ToString() + "', " +  //idProveedor
									row.ItemArray[5].ToString() + ", " +         //PrecioProveedor
									row.ItemArray[6].ToString() + ", " +         //PrevioEnvio
									"1, 'SYSTEM','" +
									DateTime.Now + "')";
							}
							else
							{
								strSql = "Update MantArticuloProveedores Set idProveedor = '" + row.ItemArray[2].ToString() + "', " +
									"PrecioProveedor = " + row.ItemArray[5].ToString() + ", PrecioEnvioProveedor = " + row.ItemArray[6].ToString() + ", " +
									" UsuarioModificacion = 'SYSTEM', FechaHoraModificacion = '" + DateTime.Now.ToString() + "'" +
									" Where idArticuloProveedor = '" + row.ItemArray[0].ToString() + "'";
							}
							BaseDeDatos.ExecuteSelect(true, strSql);
						}

						//Aqui montamos las lineas de tarifas
						foreach (DataRow row in dtTarifas.Rows)
						{
							if (row.ItemArray[0].ToString() == "")
							{
								strSql = "Insert into MantArticuloTarifa (idTarifa, idArticulo, desdeUnidades, hastaUnidades, precioVenta, UsuarioCreacion, FechaHoraCreacion) Values" +
									"('" + Guid.NewGuid().ToString() + "', " +   //Guid articulo proveedor
									"'" + tId.Text + "', " +                     //idArticulo
									"'" + row.ItemArray[2].ToString() + "', " +  //desde
									row.ItemArray[3].ToString() + ", " +         //hasta
									row.ItemArray[4].ToString() + ", " +         //PrecioVenta
									"'SYSTEM','" +
									DateTime.Now + "')";
							}
							else
							{
								strSql = "Update MantArticuloTarifa Set desdeUnidades = " + row.ItemArray[2].ToString() + ", " +
									"hastaUnidades = " + row.ItemArray[3].ToString() + ", " +
									"PrecioVenta = " + row.ItemArray[4].ToString() + ", " +
									"FechaInicioVigencia = '" + row.ItemArray[5].ToString() + "', " +
									"FechaFinVigencia = '" + row.ItemArray[6].ToString() + "', " +
									"UsuarioModificacion = 'SYSTEM', FechaHoraModificacion = '" + DateTime.Now.ToString() + "'" +
									"Where idTarifa = '" + row.ItemArray[0].ToString() + "'";
							}
							BaseDeDatos.ExecuteSelect(true, strSql);
						}
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

				//if (tCosteProd.Text == "")
				//	tCosteProd.Text = "0";

				if (tPrecioCosteMedio.Text == "")
					tPrecioCosteMedio.Text = "0";

				if (tCodigo.Text == "")
				{
					MessageBox.Show("El Codigo es un valor obligatorio", "Advertencia");
					tCodigo.Focus();
					return false;
				}

				if (tStockMinimo.Text == "")
				{
					tStockMinimo.Text = "0";
				}

				//Puntos a tener en cuenta en la validacion...
				//1.- Que al menos exista una tarifa en vigor.
				//2.- Relacion de tarifas, que sean por ej. 1 - 10 ; 11 - 20; 21- 50,....
				//3.- Que el precio de tarifa nunca sea inferior al precio de coste. (calculado)
				//4.- Integridad referencial, que si elimino algo no me cepille referencias de otros reigstros que estan en uso.
				//5.- Mas, seguro que hay mas.

				return true;
			}

			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
				return false;
			}
		}

		private void bNuevo_Click(object sender, EventArgs e)
		{
			blanqueoCampos();
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
				DialogResult dialogResult = MessageBox.Show("¿Desea salir del Mto. de Articulos?", "Mto. Artículos", MessageBoxButtons.YesNo);
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

		private void cFamilia_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void dtgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0)
					MuestraDetalle(dtgvDatos.Rows[e.RowIndex].Cells[0].Value.ToString());
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
				foreach (Control ctrl in pnlFacturacion.Controls)
				{
					if (ctrl is TextBox)
					{
						ctrl.Text = "";
					}
				}

				foreach (Control ctrl in pnlOtrosDatos.Controls)
				{
					if (ctrl is TextBox)
					{
						ctrl.Text = "";
					}
				}
				dtTarifas.Clear();
				dtProveedores.Clear();
				pArticulo.ImageLocation = "";
				dtInicioVigencia.Format = DateTimePickerFormat.Custom;
				dtInicioVigencia.CustomFormat = " ";
				dtFinVigencia.Format = DateTimePickerFormat.Custom;
				dtFinVigencia.CustomFormat = " ";


			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bBuscaProveedor_Click(object sender, EventArgs e)
		{
			try
			{
				if (accionPantallaProveedor == "M")
				{
					MessageBox.Show("El proveedor no se debe modificar. " + "\n" + "\n" +
						"Si quiere cambiar de proveedor debe borrar previamente la linea y generar una nueva", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					frmSelector frm1 = new frmSelector();
					frm1.TopMost = true;
					frm1.Owner = this;
					frm1.Titulo = "Selector de Proveedores";
					frm1.strSql = "Select idProveedor, CodigoProveedor, RazonSocial from MantProveedores";
					frm1.ShowDialog();

					tCodProveedor.Tag = frm1.id;
					tCodProveedor.Text = frm1.codigo;
					tDescProveedor.Text = frm1.nombre;
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bAceptarProveedor_Click(object sender, EventArgs e)
		{
			try
			{

				CreamosdtProveedores();

				if (accionPantallaProveedor == "N")
				{
					row = dtProveedores.NewRow();
					row["idArticulo"] = tId.Text;
					row["idProveedor"] = tCodProveedor.Tag;
					row["CodigoProveedor"] = tCodProveedor.Text;
					row["Proveedor"] = tDescProveedor.Text;
					row["Precio"] = tPrecioCosteProveedor.Text;
					row["PrecioEnvio"] = tPrecioEnvioProveedor.Text;
					dtProveedores.Rows.Add(row);
				}
				else
				{
					foreach (DataRow row in dtProveedores.Rows)
					{
						if (row["idProveedor"].ToString() == tCodProveedor.Tag.ToString())
						{
							row["idArticulo"] = tId.Text;
							row["idProveedor"] = tCodProveedor.Tag;
							row["CodigoProveedor"] = tCodProveedor.Text;
							row["Proveedor"] = tDescProveedor.Text;
							row["Precio"] = tPrecioCosteProveedor.Text;
							row["PrecioEnvio"] = tPrecioEnvioProveedor.Text;
						}
					}
				}

				dtgvProveedores.DataSource = dtProveedores;
				// Si es la primera vez, le aplicamos formato a la Grid de Proveedores.
				if (dtgvProveedores.Rows.Count > 0)
				{
					aplicaFormatoGridProveedores();
				}
				//Blanqueamos campos
				tCodProveedor.Tag = "";
				tCodProveedor.Text = "";
				tDescProveedor.Text = "";
				tPrecioCosteProveedor.Text = "";
				tPrecioEnvioProveedor.Text = "";
				accionPantallaProveedor = "A";
				comportamientoBotonesProveedores();

			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}

		}

		void CreamosdtProveedores()
		{
			try

			{
				if (dtProveedores.Columns.Count == 0)
				{
					DataColumn column;
					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "idArticuloProveedor";
					column.Namespace = "id Articulo Proveedor";
					dtProveedores.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "idArticulo";
					column.Namespace = "id Articulo";
					dtProveedores.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "idProveedor";
					dtProveedores.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "CodigoProveedor";
					dtProveedores.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "Proveedor";
					dtProveedores.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.Decimal");
					column.ColumnName = "Precio";
					dtProveedores.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.Decimal");
					column.ColumnName = "PrecioEnvio";
					dtProveedores.Columns.Add(column);
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		void CreamosdtTarifas()
		{
			try

			{
				if (dtTarifas.Columns.Count == 0)
				{
					DataColumn column;

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "idTarifa";
					column.Namespace = "Tarifa";
					dtTarifas.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "idArticulo";
					column.Namespace = "id Articulo";
					dtTarifas.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.Int32");
					column.ColumnName = "desde";
					dtTarifas.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.Int32");
					column.ColumnName = "hasta";
					dtTarifas.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.Decimal");
					column.ColumnName = "Precio";
					dtTarifas.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.Decimal");
					column.ColumnName = "FechaInicioVigencia";
					column.Namespace = "Inicio vigencia";
					dtTarifas.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.Decimal");
					column.ColumnName = "FechaFinVigencia";
					column.Namespace = "Fin vigencia";
					dtTarifas.Columns.Add(column);
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}
		void aplicaFormatoGridProveedores()
		{
			try
			{
				dtgvProveedores.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
				dtgvProveedores.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
				dtgvProveedores.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				//Ok, aqui ocultamos lo que son las columnas guid
				dtgvProveedores.Columns[0].Visible = false;
				dtgvProveedores.Columns[1].Visible = false;
				dtgvProveedores.Columns[2].Visible = false;
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		void aplicaFormatoGridTarifas()
		{
			try
			{
				dtgvTarifas.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
				dtgvTarifas.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
				dtgvTarifas.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
				dtgvTarifas.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				//Ok, aqui ocultamos lo que son las columnas guid
				dtgvTarifas.Columns[0].Visible = false;
				dtgvTarifas.Columns[1].Visible = false;

			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void dtgvProveedores_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (accionPantalla == "M")
					accionPantallaProveedor = "M";
				comportamientoBotonesProveedores();
				tCodProveedor.Tag = dtgvProveedores.Rows[e.RowIndex].Cells[2].Value;
				tCodProveedor.Text = dtgvProveedores.Rows[e.RowIndex].Cells[3].Value.ToString();
				tDescProveedor.Text = dtgvProveedores.Rows[e.RowIndex].Cells[4].Value.ToString();
				tPrecioCosteProveedor.Text = dtgvProveedores.Rows[e.RowIndex].Cells[5].Value.ToString();
				tPrecioEnvioProveedor.Text = dtgvProveedores.Rows[e.RowIndex].Cells[6].Value.ToString();
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bNuevoProveedor_Click(object sender, EventArgs e)
		{
			try
			{

				tCodProveedor.Tag = "";
				tCodProveedor.Text = "";
				tDescProveedor.Text = "";
				tPrecioCosteProveedor.Text = "";
				tPrecioEnvioProveedor.Text = "";
				accionPantallaProveedor = "N";
				comportamientoBotonesProveedores();
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bEliminarProveedor_Click(object sender, EventArgs e)
		{
			try
			{
				//miramos si tiene Id, si tiene nos cepillamos el registro de la BBDD, y cargamos dtProveedores
				//Si no tiene id, nos cepillamos la linea de dtProveedores y volvemos a cargar.
				string strSql = "";

				DialogResult dialogResult = MessageBox.Show("¿Desea eliminar el siguiente proveedor de manera permanente?. " + "\n" + "\n" + dtgvProveedores.CurrentRow.Cells[4].Value.ToString(), "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				accionPantallaProveedor = "E";
				comportamientoBotonesProveedores();
				if (dialogResult == DialogResult.Yes)
				{
					if (dtgvProveedores.CurrentRow.Cells[0].Value.ToString() != "")
					{
						//BBDD
						strSql = "Delete MantArticuloProveedores Where idArticuloProveedor = '" + dtgvProveedores.CurrentRow.Cells[0].Value.ToString() + "'";
						BaseDeDatos.ExecuteSelect(false, strSql);
						//Tambie me lo cepillo del dtProveedores.
						foreach (DataRow row in dtProveedores.Rows)
						{
							if (row["idProveedor"].ToString() == tCodProveedor.Tag.ToString())
							{
								row.Delete();
								row.AcceptChanges();
								break;
							}
						}
					}
					else
					{
						foreach (DataRow row in dtProveedores.Rows)
						{
							if (row["idProveedor"].ToString() == tCodProveedor.Tag.ToString())
							{
								row.Delete();
								row.AcceptChanges();
								break;
							}
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

		private void bCancelarProveedor_Click(object sender, EventArgs e)
		{
			try
			{
				accionPantallaProveedor = "C";
				comportamientoBotonesProveedores();
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}
		private void mostramosDetalleProveedor(string id)
		{
			try
			{
				if (id != null)
				{
					//Aqui mostramos el detalle de los proveedores...
					string strSQl = "Select idArticuloProveedor, idArticulo, art.idProveedor, CodigoProveedor ,pro.RazonSocial as Proveedor ,PrecioProveedor as Precio, " +
						" PrecioEnvioProveedor as PrecioEnvio  from MantArticuloProveedores art" +
						" Left Join MantProveedores pro on art.idProveedor = pro.idProveedor" +
						" Where art.idArticulo = '" + id + "'";
					dtProveedores = BaseDeDatos.ExecuteSelect(true, strSQl);
					dtgvProveedores.DataSource = dtProveedores;
					aplicaFormatoGridProveedores();
				}

			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}


		private void mostramosDetalleTarifas(string id)
		{
			try
			{
				if (id != null)
				{
					//Aqui mostramos el detalle de los proveedores...
					string strSQl = "Select idTarifa, idArticulo, desdeUnidades as Desde, hastaUnidades as Hasta, precioVenta as Precio, " +
						" FechaInicioVigencia as 'Inicio vigencia', Case When FechaFinVigencia = '01/01/1900' Then null Else FechaFinVigencia End as 'Fin vigencia' " +
						" from MantArticuloTarifa tar" +
						" Where tar.idArticulo = '" + id + "'";
					dtTarifas = BaseDeDatos.ExecuteSelect(true, strSQl);
					dtgvTarifas.DataSource = dtTarifas;
					aplicaFormatoGridTarifas();
				}

			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bBuscarImagen_Click(object sender, EventArgs e)
		{
			try
			{
				opFile.InitialDirectory = @"C:\";
				opFile.ShowDialog();
				pArticulo.ImageLocation = opFile.FileName;

			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void calculoPrecioCoste()
		{
			try
			{
				//Aqui deebemos calcular el precio de coste segun los precios de proveeedores.
				//Esto es, ... suma de precio compra + gasto de envio / nnumero de proveedores (registros de MantArticuloProveedores)

				decimal sumaTotal = 0;
				decimal calculo = 0;

				foreach (DataRow row in dtProveedores.Rows)
				{
					sumaTotal += decimal.Parse(row.ItemArray[5].ToString()) + decimal.Parse(row.ItemArray[6].ToString());
				}

				if (dtProveedores.Rows.Count > 1)
				{
					calculo = Convert.ToDecimal(sumaTotal.ToString()) / dtProveedores.Rows.Count;
					tPrecioCosteMedio.Text = Convert.ToString(calculo);
				}
				else
				{
					tPrecioCosteMedio.Text = sumaTotal.ToString();
				}

			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bNuevoTarifa_Click(object sender, EventArgs e)
		{
			try
			{
				tUdsDesde.Text = "";
				tUdsHasta.Text = "";
				tPrecioTarifa.Text = "";
				accionPantallaTarifas = "N";
				comportamientoBotonesTarifas();

			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bAceptarTarifa_Click(object sender, EventArgs e)
		{
			try
			{

				CreamosdtTarifas();

				if (accionPantallaTarifas == "N")
				{
					row = dtTarifas.NewRow();
					row["idArticulo"] = tId.Text;
					row["desde"] = tUdsDesde.Text;
					row["hasta"] = tUdsHasta.Text;
					row["precio"] = tPrecioTarifa.Text;
					row["Inicio Vigencia"] = dtInicioVigencia.Text;
					if (chkActivaFinVigencia.Checked)
						row["Fin Vigencia"] = dtFinVigencia.Text;
					dtTarifas.Rows.Add(row);
				}
				else
				{
					foreach (DataRow row in dtTarifas.Rows)
					{
						if (row["desde"].ToString() == tUdsDesde.Text && row["hasta"].ToString() == tUdsHasta.Text)
						{
							row["idArticulo"] = tId.Text;
							row["desde"] = tUdsDesde.Text;
							row["hasta"] = tUdsHasta.Text;
							row["precio"] = tPrecioTarifa.Text;
							row["Inicio Vigencia"] = dtInicioVigencia.Value;
							if (chkActivaFinVigencia.Checked)
								row["Fin Vigencia"] = dtFinVigencia.Value;
						}
					}
				}

				dtgvTarifas.DataSource = dtTarifas;
				// Si es la primera vez que lo hacemos, le aplicamos formato a la Grid de Proveedores.
				if (dtgvTarifas.Rows.Count > 0)
				{
					aplicaFormatoGridTarifas();
				}
				//Blanqueamos campos
				tUdsDesde.Text = "";
				tUdsHasta.Text = "";
				tPrecioTarifa.Text = "";
				accionPantallaTarifas = "A";
				comportamientoBotonesTarifas();
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bEliminarTarifa_Click(object sender, EventArgs e)
		{
			try
			{
				//miramos si tiene Id, si tiene nos cepillamos el registro de la BBDD, y cargamos dtProveedores
				//Si no tiene id, nos cepillamos la linea de dtProveedores y volvemos a cargar.
				string strSql = "";

				DialogResult dialogResult = MessageBox.Show("¿Desea eliminar el rango de tarifas?. " + "\n" + "\n" + dtgvTarifas.CurrentRow.Cells[4].Value.ToString(), "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				accionPantallaTarifas = "E";
				comportamientoBotonesTarifas();
				if (dialogResult == DialogResult.Yes)
				{
					if (dtgvTarifas.CurrentRow.Cells[0].Value.ToString() != "")
					{
						//BBDD
						strSql = "Delete MantArticuloTarifa Where idArticulo = '" + dtgvTarifas.CurrentRow.Cells[0].Value.ToString() + "'";
						BaseDeDatos.ExecuteSelect(false, strSql);
						//Tambie me lo cepillo del dtProveedores.
						foreach (DataRow row in dtTarifas.Rows)
						{
							if (row["desde"].ToString() == tUdsDesde.Text && row["hasta"].ToString() == tUdsHasta.Text)
							{
								row.Delete();
								row.AcceptChanges();
								break;
							}
						}
					}
					else
					{
						foreach (DataRow row in dtProveedores.Rows)
						{
							if (row["idProveedor"].ToString() == tCodProveedor.Tag.ToString())
							{
								row.Delete();
								row.AcceptChanges();
								break;
							}
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

		private void bCancelarTarifa_Click(object sender, EventArgs e)
		{
			try
			{
				accionPantallaTarifas = "C";
				comportamientoBotonesTarifas();
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void dtgvTarifas_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (accionPantalla == "M")
					accionPantallaTarifas = "M";
				comportamientoBotonesTarifas();
				tUdsDesde.Text = dtgvTarifas.Rows[e.RowIndex].Cells[2].Value.ToString();
				tUdsHasta.Text = dtgvTarifas.Rows[e.RowIndex].Cells[3].Value.ToString();
				tPrecioTarifa.Text = dtgvTarifas.Rows[e.RowIndex].Cells[4].Value.ToString();
				if (dtgvTarifas.Rows[e.RowIndex].Cells[5].Value.ToString() != "")
					dtInicioVigencia.Text = dtgvTarifas.Rows[e.RowIndex].Cells[5].Value.ToString();

				if (dtgvTarifas.Rows[e.RowIndex].Cells[6].Value.ToString() != "")
				{
					dtFinVigencia.Text = dtgvTarifas.Rows[e.RowIndex].Cells[6].Value.ToString();
					chkActivaFinVigencia.Checked = true;
				}
				else
				{
					chkActivaFinVigencia.Checked = false;
					dtFinVigencia.Enabled = false;
					dtFinVigencia.Text = "";
				}





			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void chkActivaFinVigencia_CheckedChanged(object sender, EventArgs e)
		{
			if (dtFinVigencia.Enabled)
				dtFinVigencia.Enabled = false;
			else
				dtFinVigencia.Enabled = true;
		}

		private void bPictograma1_Click(object sender, EventArgs e)
		{
			try
			{
				opFile.InitialDirectory = @"C:\";
				opFile.ShowDialog();
				pbPictograma1.ImageLocation = opFile.FileName;

			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bPictograma2_Click(object sender, EventArgs e)
		{
			try
			{
				opFile.InitialDirectory = @"C:\";
				opFile.ShowDialog();
				pbPictograma2.ImageLocation = opFile.FileName;

			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bPictograma3_Click(object sender, EventArgs e)
		{
			try
			{
				opFile.InitialDirectory = @"C:\";
				opFile.ShowDialog();
				pbPictograma3.ImageLocation = opFile.FileName;

			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bPictograma4_Click(object sender, EventArgs e)
		{
			try
			{
				opFile.InitialDirectory = @"C:\";
				opFile.ShowDialog();
				pbPictograma4.ImageLocation = opFile.FileName;

			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bPictograma5_Click(object sender, EventArgs e)
		{
			try
			{
				opFile.InitialDirectory = @"C:\";
				opFile.ShowDialog();
				pbPictograma5.ImageLocation = opFile.FileName;

			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}
	}
}
