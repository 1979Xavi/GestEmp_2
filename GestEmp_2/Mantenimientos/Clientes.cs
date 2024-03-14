using GestEmp_2.clsFunciones;
using GestEmp_2.clsFuncionesGenerales;
using System;
using System.Data;
using System.Windows.Forms;


namespace GestEmp_2
{
	public partial class clientes : Form
	{
		//Poslbles estados
		//C-Consultar, A-Modificar, M-Modificar, E-Eliminar (Activo = 0), D-Duplicar
		string accionPantalla = "C";
		string accionPantallaDirecciones = "C";
		DataTable dtDirecciones = new DataTable();
		DataRow row;
		public clientes()
		{
			InitializeComponent();
		}

		private void clientes_Load(object sender, EventArgs e)
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

		void comportamientoBotones()
		{
			switch (accionPantalla)
			{
				case "C":
				case "A":
					pnlGeneral.Enabled = false;
					pnlFacturacion.Enabled = false;
					pnlOtrosDatos.Enabled = false;
					pnlDirecciones.Enabled = false;
					pnlBotonesDirecciones.Enabled = false;

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
					pnlGeneral.Enabled = true;
					pnlFacturacion.Enabled = true;
					pnlOtrosDatos.Enabled = true;
					pnlDirecciones.Enabled = true;
					pnlBotonesDirecciones.Enabled = true;
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
				if (ValidaDatos())
				{
					if (accionPantalla == "N")

					{
						//Aqui insertem
						guid = Guid.NewGuid().ToString();
						strSql = "Insert into MantClientes (idCliente" +
						 ",CodigoCliente" +
						 ",RazonSocialFacturacion" +
						 ",DomicilioFacturacion" +
						 ",TelefonoContactoFacturacion" +
						 ",PersonaContactoFacturacion " +
						 ",EmailFacturacion" +
						 ",Web" +
						 ",CodigoPostalFacturacion" +
						 ",idPoblacionFacturacion" +
						 ",idProvinciaFacturacion" +
						 ",idPaisFacturacion" +
						 ",NIFCIF" +
						 ",idFormaPago" +
						 ",DiaPago" +
						 ",DomicilioBancario" +
						 ",idIVA" +
						 ",idDescuento" +
						 ",Observaciones" +
						 ",RutaImagen" +
						 ",Activo" +
						 ",UsuarioCreacion" +
						 ",FechaHoraCreacion) Values ('" +
								guid + "', '" +
								tCodigo.Text + "', '" +
								tNombre.Text + "', '" +
								tDireccion.Text + "', '" +
								tTelefono.Text + "', '" +
								tPersonaContacto.Text + "', '" +
								tEmail.Text + "', '" +
								tWeb.Text + "', '" +
								tCodigoPostal.Text + "', '" +
								tCodPoblacion.Tag + "', '" +
								tCodProvincia.Tag + "', '" +
								tCodPais.Tag + "', '" +
								tNIFCIF.Text + "', '" +
								cFormaPago.SelectedValue.ToString() + "', '" +
								tDiaCobro.Text + "', '" +
								tDomiciliacionBancaria.Text + "', '" +
								cIVA.SelectedValue.ToString() + "', '" +
								cDescuento.SelectedValue.ToString() + "', '" +
								tObservaciones.Text + "', '" +
								pCliente.ImageLocation + "', " +
								"1, 'SYSTEM','" +
								DateTime.Now + "')";
						BaseDeDatos.ExecuteSelect(true, strSql);


						foreach (DataRow row in dtDirecciones.Rows)
						{
							strSql = "Insert into MantClienteDirecciones (idCliente, idDireccion, RazonSocial, Domicilio, CodigoPostal, idPoblacion, idProvincia, idPais, Activo, UsuarioCreacion, FechaHoraCreacion) Values" +
								"('" + guid + "', " +                         //Guid cliente
								"'" + Guid.NewGuid().ToString() + "', " +     //idDireccion
								"'" + row.ItemArray[2].ToString() + "', '" +  //RazonSocial
								row.ItemArray[3].ToString() + "', '" +        //Domicilio
								row.ItemArray[4].ToString() + "', '" +        //CodigoPostal
								row.ItemArray[11].ToString() + "', '" +       //idPoblacion
								row.ItemArray[8].ToString() + "', '" +        //idProvincia
								row.ItemArray[5].ToString() + "', " +         //idPais
								"1, 'SYSTEM','" +
								DateTime.Now + "')";
							BaseDeDatos.ExecuteSelect(true, strSql);
						}

					}

					else
					{
						//Aqui modifiquem
						guid = tId.Text;
						strSql = "Update MantClientes Set codigoCliente = '" + tCodigo.Text + "', RazonSocialFacturacion = '" + tNombre.Text + "',  DomicilioFacturacion = '" + tDireccion.Text +
							"', TelefonoContactoFacturacion = '" + tTelefono.Text + "', PersonaContactoFacturacion = '" + tPersonaContacto.Text + "', EmailFacturacion = '" + tEmail.Text +
							"', Web = '" + tWeb.Text + "', CodigoPostalFacturacion = '" + tCodigoPostal.Text + "', idPoblacionFacturacion = '" + tCodPoblacion.Tag + "', idProvinciaFacturacion = '" + tCodProvincia.Tag +
							"', idPaisFacturacion = '" + tCodPais.Tag + "', NIFCIF = '" + tNIFCIF.Text + "', idFormaPago = '" + cFormaPago.SelectedValue.ToString() + "', DiaPago = " + tDiaCobro.Text +
							", DomicilioBancario = '" + tDomiciliacionBancaria.Text + "', idIva = '" + cIVA.SelectedValue.ToString() + "', idDescuento = '" + cDescuento.SelectedValue.ToString() + "', Observaciones = '" + tObservaciones.Text +
							"', RutaImagen = '" + pCliente.ImageLocation + "', UsuarioModificacion = 'SYSTEM', FechaHoraModificacion = '" + DateTime.Now.ToString() + "' Where idCliente = '" + tId.Text + "'";
						BaseDeDatos.ExecuteSelect(true, strSql);
					}

					//Y aqui modificammos las direcciones.
					foreach (DataRow row in dtDirecciones.Rows)
					{
						if (row.ItemArray[1].ToString() == "")
						{
							strSql = "Insert into MantClienteDirecciones (idCliente, idDireccion, RazonSocial, Domicilio, CodigoPostal, idPoblacion, idProvincia, idPais, Activo, UsuarioCreacion, FechaHoraCreacion) Values" +
								"('" + guid + "', " +                         //Guid cliente
								"'" + Guid.NewGuid().ToString() + "', " +     //idDireccion
								"'" + row.ItemArray[2].ToString() + "', '" +  //RazonSocial
								row.ItemArray[3].ToString() + "', '" +        //Domicilio
								row.ItemArray[4].ToString() + "', '" +        //CodigoPostal
								row.ItemArray[11].ToString() + "', '" +       //idPoblacion
								row.ItemArray[8].ToString() + "', '" +        //idProvincia
								row.ItemArray[5].ToString() + "', " +         //idPais
								"1, 'SYSTEM','" +
								DateTime.Now + "')";
							BaseDeDatos.ExecuteSelect(true, strSql);
						}
						else
						{
							strSql = "Update MantClienteDirecciones Set " +
								"RazonSocial = '" + row.ItemArray[2].ToString() + "', " +
								"Domicilio = '" + row.ItemArray[3].ToString() + "', " +
								"CodigoPostal = '" + row.ItemArray[4].ToString() + "', " +
								"idPoblacion = '" + row.ItemArray[11].ToString() + "', " +
								"idProvincia = '" + row.ItemArray[8].ToString() + "', " +
								"idPais = '" + row.ItemArray[5].ToString() + "', " +
								"UsuarioModificacion = 'SYSTEM', " +
								"FechaHoraModificacion = '" + DateTime.Now.ToString() + "' " +
								"Where idCliente = '" + row.ItemArray[0].ToString() + "' and idDireccion = '" + row.ItemArray[1].ToString() + "'";
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
			eliminarCliente();
			cargaDatos();

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

		private void bBuscaProvincia_Click(object sender, EventArgs e)
		{
			try
			{
				if (tCodPais.Tag != null)
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
					MessageBox.Show("Primero debe indicar el pais");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Se ha producido el siguiente erro: " + ex.Message.ToString());
			}
		}

		private void bBuscaPoblacion_Click(object sender, EventArgs e)
		{

			if (tCodPais.Tag != null && tCodProvincia.Tag != null)
			{
				frmSelector frm1 = new frmSelector();
				frm1.TopMost = true;
				frm1.Owner = this;
				frm1.Titulo = "Selector de poblaciones";
				frm1.strSql = "Select idPoblacion, codigoPoblacion, Descripcion from MantPoblaciones where idPais = '" + tCodPais.Tag + "' and idProvincia = '" + tCodProvincia.TabIndex + "'";
				frm1.ShowDialog();

				tCodPoblacion.Tag = frm1.id;
				tCodPoblacion.Text = frm1.codigo;
				tDescPoblacion.Text = frm1.nombre;
			}
			else
			{
				MessageBox.Show("Primero debe indicar el pais y la provincia");
			}

		}

		private void dtgvDatos_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
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

		void CargoCbos()
		{
			try
			{
				string strSql = "";
				strSql = "Select * from MantIva";
				DataTable dtIva = BaseDeDatos.ExecuteSelect(true, strSql);
				if (dtIva.Rows.Count > 0)
				{
					cIVA.DataSource = dtIva;
					cIVA.DisplayMember = dtIva.Columns[3].ColumnName; //Porcentaje
					cIVA.ValueMember = dtIva.Columns[0].ColumnName;   //id

				}

				strSql = "Select * from MantDescuento";
				DataTable dtDto = BaseDeDatos.ExecuteSelect(true, strSql);
				if (dtDto.Rows.Count > 0)
				{
					cDescuento.DataSource = dtDto;
					cDescuento.DisplayMember = dtDto.Columns[3].ColumnName; //Porcentaje
					cDescuento.ValueMember = dtDto.Columns[0].ColumnName;   //id
				}

				strSql = "Select * from MantFormasPago";
				DataTable dtFpago = BaseDeDatos.ExecuteSelect(true, strSql);
				if (dtFpago.Rows.Count > 0)
				{
					cFormaPago.DataSource = dtFpago;
					cFormaPago.DisplayMember = dtFpago.Columns[2].ColumnName; //Porcentaje
					cFormaPago.ValueMember = dtFpago.Columns[0].ColumnName;   //id
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
				dtgvDatos.DataSource = BaseDeDatos.ExecuteSelect(true, "Select idCliente, CodigoCliente, RazonSocialFacturacion from MantClientes Where Activo = 1");
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

		private void bBuscaPais_Click_1(object sender, EventArgs e)
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

		private void bBuscaProvincia_Click_1(object sender, EventArgs e)
		{
			try
			{
				if (tCodPais.Text != "")
				{
					frmSelector frm1 = new frmSelector();
					frm1.TopMost = true;
					frm1.Owner = this;
					frm1.Titulo = "Selector de Provincias";
					frm1.strSql = "Select idProvincia, CodigoProvincia, Descripcion from MantProvincias where idPais = '" + tCodPais.Tag + "'";
					frm1.ShowDialog();

					tCodProvincia.Tag = frm1.id;
					tCodProvincia.Text = frm1.codigo;
					tDescProvincia.Text = frm1.nombre;
				}
				else
					MessageBox.Show("Antes debe seleccionar el País");

			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bBuscaPoblacion_Click_1(object sender, EventArgs e)
		{
			try
			{
				frmSelector frm1 = new frmSelector();
				frm1.TopMost = true;
				frm1.Owner = this;
				frm1.Titulo = "Selector de Poblaciones";
				frm1.strSql = "Select idPoblacion, codigoPoblacion, Descripcion from MantPoblaciones";
				frm1.ShowDialog();

				tCodPoblacion.Tag = frm1.id;
				tCodPoblacion.Text = frm1.codigo;
				tDescPoblacion.Text = frm1.nombre;
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

				if (tDiaCobro.Text == "")
				{
					tDiaCobro.Text = "1";
				}

				if (tNombre.Text == "")
				{
					MessageBox.Show("El nombre o Razon social es un valor obligatorio");
					tNombre.Focus();
					return false;
				}

				if (tCodigo.Text == "")
				{
					MessageBox.Show("El Codigo es un valor obligatorio", "Advertencia");
					tCodigo.Focus();
					return false;
				}

				if (tCodPoblacion.Text == "" || tCodProvincia.Text == "" || tCodPais.Text == "")
				{
					MessageBox.Show("El pais, poblacion y provincia son valores obbligatorios", "Advertencia");
					tCodigo.Focus();
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

		private bool ValidaDatosDireccion()
		{
			try
			{
				if (tRazonSocialDir.Text == "")
				{
					MessageBox.Show("La razon social es un valor obligatorio", "Advertencia");
					tRazonSocialDir.Focus();
					return false;
				}

				if (tDomicilioDir.Text == "")
				{
					MessageBox.Show("El domicilio es un valor obligatorio", "Advertencia");
					tDomicilioDir.Focus();
					return false;
				}
				if (tCodPoblacionDir.Text == "" || tCodProvinciaDir.Text == "" || tCodPaisDir.Text == "")
				{
					MessageBox.Show("El pais, poblacion y provincia son valores obbligatorios", "Advertencia");
					tCodPoblacionDir.Focus();
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

		private void blanqueoCampos()
		{
			try
			{
				tId.Text = "";
				foreach (Control ctrl in pnlGeneral.Controls)
				{
					if (ctrl is TextBox)
					{
						ctrl.Text = "";
					}
				}

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
				dtDirecciones.Clear();
				dtgvDirecciones.DataSource = dtDirecciones;


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
				pCliente.ImageLocation = opFile.FileName;

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
				string strSQl = "Select idCliente, CodigoCliente, RazonSocialFacturacion, DomicilioFActuracion, TelefonoContactoFacturacion, PersonaContactoFacturacion, " +
					" EmailFacturacion, Web, CodigoPostalFacturacion, idPoblacionFacturacion, pob.CodigoPoblacion, pob.Descripcion as 'Poblacion', " +
					" idProvinciaFacturacion, pro.CodigoProvincia, pro.Descripcion as Provincia, " +
					" idPaisFacturacion, pai.CodigoPais, pai.Descripcion as 'Pais', " +
					" NIFCIF, cli.idFormaPago, fpa.Descripcion as 'Forma de Pago', DiaPago, DomicilioBancario, cli.idIVA, iva.descripcion as IVA, " +
					" cli.idDescuento, dto.descripcion, Observaciones, RutaImagen from MantClientes cli" +
					" Left Join MantPoblaciones pob on cli.idPoblacionFacturacion = pob.idPoblacion and cli.idProvinciaFacturacion = pob.idProvincia and cli.idPaisFacturacion = pob.idPais" +
					" Left Join MantProvincias pro on cli.idProvinciaFacturacion = pro.idProvincia and cli.idPaisFacturacion = pro.idPais" +
					" Left Join MantPaises pai on cli.idPaisFacturacion = pai.idPais" +
					" Left Join MantFormasPago fpa on cli.idFormaPago = fpa.idFormaPago" +
					" Left Join MantIVA iva on cli.idIVA = iva.idIVA" +
					" Left Join MantDescuento dto on cli.idDescuento = dto.idDescuento where idCliente = '" + id + "'";
				DataTable dtDetalle = BaseDeDatos.ExecuteSelect(true, strSQl);

				if (dtDetalle.Rows.Count > 0)
				{
					tCodigo.Text = dtDetalle.Rows[0]["CodigoCliente"].ToString();
					tNombre.Text = dtDetalle.Rows[0]["RazonSocialFacturacion"].ToString();

					tDireccion.Text = dtDetalle.Rows[0]["DomicilioFActuracion"].ToString();
					tCodigoPostal.Text = dtDetalle.Rows[0]["CodigoPostalFacturacion"].ToString();

					tCodPais.Tag = dtDetalle.Rows[0]["idPaisFacturacion"].ToString();
					tCodPais.Text = dtDetalle.Rows[0]["CodigoPais"].ToString();
					tDescPais.Text = dtDetalle.Rows[0]["Pais"].ToString();

					tCodProvincia.Tag = dtDetalle.Rows[0]["idProvinciaFacturacion"].ToString();
					tCodProvincia.Text = dtDetalle.Rows[0]["CodigoProvincia"].ToString();
					tDescProvincia.Text = dtDetalle.Rows[0]["Provincia"].ToString();

					tCodPoblacion.Tag = dtDetalle.Rows[0]["idPoblacionFacturacion"].ToString();
					tCodPoblacion.Text = dtDetalle.Rows[0]["CodigoPoblacion"].ToString();
					tDescPoblacion.Text = dtDetalle.Rows[0]["Poblacion"].ToString();

					tTelefono.Text = dtDetalle.Rows[0]["TelefonoContactoFacturacion"].ToString();
					tEmail.Text = dtDetalle.Rows[0]["EmailFacturacion"].ToString();
					tWeb.Text = dtDetalle.Rows[0]["Web"].ToString();
					tPersonaContacto.Text = dtDetalle.Rows[0]["PersonaContactoFacturacion"].ToString();

					//Aqui seleccionamos los valores de las combos.

					cDescuento.SelectedValue = dtDetalle.Rows[0]["idDescuento"].ToString();
					cIVA.SelectedValue = dtDetalle.Rows[0]["idIVA"].ToString();
					cFormaPago.SelectedValue = dtDetalle.Rows[0]["idFormaPago"].ToString();

					tObservaciones.Text = dtDetalle.Rows[0]["Observaciones"].ToString();
					pCliente.ImageLocation = dtDetalle.Rows[0]["RutaImagen"].ToString();

					//Aqui mostramos las direcciones del cliente seleccionado
					strSQl = "Select dir.idCliente, dir.idDireccion, RazonSocial, Domicilio, CodigoPostal, pai.idPais, CodigoPais, pai.Descripcion as Pais, dir.idProvincia, CodigoProvincia, pro.Descripcion as Provincia, " +
						 " dir.idPoblacion, CodigoPoblacion, pob.Descripcion as Poblacion " +
						 " from MantClienteDirecciones dir" +
						 " Left Join MantPaises pai on dir.idPais = pai.idPais" +
						 " Left Join MantProvincias pro on dir.idProvincia = pro.idProvincia and dir.idPais = pro.idPais" +
						 " Left Join MantPoblaciones pob on dir.idPoblacion = pob.idPoblacion  and dir.idProvincia = pro.idProvincia   and dir.idPais = pai.idPais" +
						 " Where idCliente = '" + id + "'";

					dtDirecciones = BaseDeDatos.ExecuteSelect(true, strSQl);

					//if (dtDirecciones.Rows.Count > 0)
					//{
					dtgvDirecciones.DataSource = dtDirecciones;
					aplicaFormatoGridDireccciones();
					//}
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bNuevaDireccion_Click(object sender, EventArgs e)
		{
			try
			{
				tIdDireccion.Text = "";
				tRazonSocialDir.Text = "";
				tDomicilioDir.Text = "";
				tCodigoPostalDir.Text = "";
				tCodPaisDir.Tag = "";
				tCodPaisDir.Text = "";
				tDescPaisDir.Text = "";
				tCodProvinciaDir.Tag = "";
				tCodProvinciaDir.Text = "";
				tDescProvinciaDir.Text = "";
				tCodPoblacionDir.Tag = "";
				tCodPoblacionDir.Text = "";
				tDescPoblacionDir.Text = "";

				accionPantallaDirecciones = "N";
				comportamientoBotonesDirecciones();
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		void comportamientoBotonesDirecciones()
		{
			switch (accionPantallaDirecciones)
			{
				case "C":
				case "A":
					pnlDirecciones.Enabled = false;
					bAceptarDirecccion.Enabled = false;
					bNuevoDireccion.Enabled = true;
					bEliminarDireccion.Enabled = true;
					bCancelarDireccion.Enabled = false;
					break;
				case "N":
				case "M":
				case "E":
				case "D":
					pnlDirecciones.Enabled = true;
					bAceptarDirecccion.Enabled = true;
					bNuevoDireccion.Enabled = false;
					bEliminarDireccion.Enabled = false;
					bCancelarDireccion.Enabled = true;
					break;
			}
		}

		private void bAceptarDireccion_Click(object sender, EventArgs e)
		{
			try
			{
				if (ValidaDatosDireccion())
				{
					CreamosdtDirecciones();

					if (accionPantallaDirecciones == "N")
					{
						row = dtDirecciones.NewRow();
						//row["idCliente"] = "";
						//row["idDireccion"] = "";

						row["Domicilio"] = tDomicilioDir.Text;
						row["RazonSocial"] = tRazonSocialDir.Text;
						row["CodigoPostal"] = tCodigoPostalDir.Text;

						row["idPais"] = tCodPaisDir.Tag;
						row["CodigoPais"] = tCodPaisDir.Text;
						row["Pais"] = tDescPaisDir.Text;

						row["idProvincia"] = tCodProvinciaDir.Tag.ToString();
						row["CodigoProvincia"] = tCodProvinciaDir.Text;
						row["Provincia"] = tDescProvinciaDir.Text;

						row["idPoblacion"] = tCodPoblacionDir.Tag.ToString();
						row["CodigoPoblacion"] = tCodPoblacionDir.Text;
						row["Poblacion"] = tDescPoblacionDir.Text;

						dtDirecciones.Rows.Add(row);
					}
					else
					{
						foreach (DataRow row in dtDirecciones.Rows)
						{
							if (row["idDireccion"].ToString() == tIdDireccion.Text)
							{
								row["Domicilio"] = tDomicilioDir.Text;
								row["RazonSocial"] = tRazonSocialDir.Text;
								row["CodigoPostal"] = tCodigoPostalDir.Text;

								row["idPais"] = tCodPaisDir.Tag;
								row["CodigoPais"] = tCodPaisDir.Text;
								row["Pais"] = tDescPaisDir.Text;

								row["idProvincia"] = tCodProvinciaDir.Tag.ToString();
								row["CodigoProvincia"] = tCodProvinciaDir.Text;
								row["Provincia"] = tDescProvinciaDir.Text;

								row["idPoblacion"] = tCodPoblacionDir.Tag.ToString();
								row["CodigoPoblacion"] = tCodPoblacionDir.Text;
								row["Poblacion"] = tDescPoblacionDir.Text;
							}
						}
					}

					dtgvDirecciones.DataSource = dtDirecciones;
					// Si es la primera vez, le aplicamos formato a la Grid de Clientes.
					if (dtgvDirecciones.Rows.Count > 0)
					{
						aplicaFormatoGridDireccciones();
					}
					//Blanqueamos campos
					blanqueamosCamposDireccion();
				}
				accionPantallaDirecciones = "A";
				comportamientoBotonesDirecciones();

			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}
		void CreamosdtDirecciones()
		{
			try

			{
				if (dtDirecciones.Columns.Count == 0)
				{

					DataColumn column;

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "idCliente";
					column.Namespace = "idCliente";
					dtDirecciones.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "idDireccion";
					column.Namespace = "idDireccion";
					column.AllowDBNull = true;
					dtDirecciones.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "RazonSocial";
					column.Namespace = "Razon Social";
					dtDirecciones.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "Domicilio";
					column.Namespace = "Domicilio";
					dtDirecciones.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "CodigoPostal";
					column.Namespace = "Codigo Postal";
					dtDirecciones.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "idPais";
					column.AllowDBNull = true;
					dtDirecciones.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "CodigoPais";
					dtDirecciones.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "Pais";
					dtDirecciones.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "idProvincia";
					column.AllowDBNull = true;
					dtDirecciones.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "CodigoProvincia";
					dtDirecciones.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "Provincia";
					dtDirecciones.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "idPoblacion";
					column.AllowDBNull = true;
					dtDirecciones.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "CodigoPoblacion";
					dtDirecciones.Columns.Add(column);

					column = new DataColumn();
					column.DataType = System.Type.GetType("System.String");
					column.ColumnName = "Poblacion";
					dtDirecciones.Columns.Add(column);
				}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		void aplicaFormatoGridDireccciones()
		{
			try
			{
				dtgvDirecciones.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
				dtgvDirecciones.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
				dtgvDirecciones.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				//Ok, aqui ocultamos lo que son las columnas guid
				dtgvDirecciones.Columns[0].Visible = false;
				dtgvDirecciones.Columns[1].Visible = false;
				dtgvDirecciones.Columns[2].Visible = false;
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bBuscaPaisDir_Click(object sender, EventArgs e)
		{
			try
			{

				frmSelector frm1 = new frmSelector();
				frm1.TopMost = true;
				frm1.Owner = this;
				frm1.Titulo = "Selector de Paises";
				frm1.strSql = "Select idPais, CodigoPais, Descripcion from MantPaises";
				frm1.ShowDialog();

				tCodPaisDir.Tag = frm1.id;
				tCodPaisDir.Text = frm1.codigo;
				tDescPaisDir.Text = frm1.nombre;
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bBuscaProvinciaDir_Click(object sender, EventArgs e)
		{
			try
			{
				frmSelector frm1 = new frmSelector();
				frm1.TopMost = true;
				frm1.Owner = this;
				frm1.Titulo = "Selector de Provincias";
				frm1.strSql = "Select idProvincia, CodigoProvincia, Descripcion from MantProvincias";
				frm1.ShowDialog();

				tCodProvinciaDir.Tag = frm1.id;
				tCodProvinciaDir.Text = frm1.codigo;
				tDescProvinciaDir.Text = frm1.nombre;
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bBuscaPoblacionDir_Click(object sender, EventArgs e)
		{
			try
			{
				frmSelector frm1 = new frmSelector();
				frm1.TopMost = true;
				frm1.Owner = this;
				frm1.Titulo = "Selector de Poblaciones";
				frm1.strSql = "Select idPoblacion, CodigoPoblacion, Descripcion from MantPoblaciones";
				frm1.ShowDialog();

				tCodPoblacionDir.Tag = frm1.id;
				tCodPoblacionDir.Text = frm1.codigo;
				tDescPoblacionDir.Text = frm1.nombre;
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void dtgvDirecciones_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (accionPantalla == "M")
					accionPantallaDirecciones = "M";
				comportamientoBotonesDirecciones();

				tIdDireccion.Text = dtgvDirecciones.Rows[e.RowIndex].Cells[1].Value.ToString();
				tRazonSocialDir.Text = dtgvDirecciones.Rows[e.RowIndex].Cells[2].Value.ToString();
				tDomicilioDir.Text = dtgvDirecciones.Rows[e.RowIndex].Cells[3].Value.ToString();
				tCodigoPostalDir.Text = dtgvDirecciones.Rows[e.RowIndex].Cells[4].Value.ToString();

				tCodPaisDir.Tag = dtgvDirecciones.Rows[e.RowIndex].Cells[5].Value.ToString();
				tCodPaisDir.Text = dtgvDirecciones.Rows[e.RowIndex].Cells[6].Value.ToString();
				tDescPaisDir.Text = dtgvDirecciones.Rows[e.RowIndex].Cells[7].Value.ToString();

				tCodProvinciaDir.Tag = dtgvDirecciones.Rows[e.RowIndex].Cells[8].Value.ToString();
				tCodProvinciaDir.Text = dtgvDirecciones.Rows[e.RowIndex].Cells[9].Value.ToString();
				tDescProvinciaDir.Text = dtgvDirecciones.Rows[e.RowIndex].Cells[10].Value.ToString();

				tCodPoblacionDir.Tag = dtgvDirecciones.Rows[e.RowIndex].Cells[11].Value.ToString();
				tCodPoblacionDir.Text = dtgvDirecciones.Rows[e.RowIndex].Cells[12].Value.ToString();
				tDescPoblacionDir.Text = dtgvDirecciones.Rows[e.RowIndex].Cells[13].Value.ToString();
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bCancelarDireccion_Click(object sender, EventArgs e)
		{
			blanqueamosCamposDireccion();
			accionPantallaDirecciones = "C";
			comportamientoBotonesDirecciones();
		}

		private void blanqueamosCamposDireccion()
		{
			try
			{

				tIdDireccion.Text = "";
				tRazonSocialDir.Text = "";
				tDomicilioDir.Text = "";
				tCodigoPostalDir.Text = "";

				tCodPaisDir.Tag = "";
				tCodPaisDir.Text = "";
				tDescPaisDir.Text = "";

				tCodProvinciaDir.Tag = "";
				tCodProvinciaDir.Text = "";
				tDescProvinciaDir.Text = "";

				tCodPoblacionDir.Tag = "";
				tCodPoblacionDir.Text = "";
				tDescPoblacionDir.Text = "";
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bEliminarDireccion_Click(object sender, EventArgs e)
		{
			try
			{
				//miramos si tiene Id, si tiene nos cepillamos el registro de la BBDD, y cargamos dtDirecciones
				//Si no tiene id, nos cepillamos la linea de dtDirecciones y volvemos a cargar.
				string strSql = "";

				DialogResult dialogResult = MessageBox.Show("¿Desea eliminar la siguiente dirección de manera permanente?. " + "\n" + "\n" + dtgvDirecciones.CurrentRow.Cells[4].Value.ToString(), "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				accionPantallaDirecciones = "E";
				comportamientoBotonesDirecciones();
				if (dialogResult == DialogResult.Yes)
				{
					if (dtgvDirecciones.CurrentRow.Cells[0].Value.ToString() != "")
					{
						//BBDD
						strSql = "Delete MantClienteDirecciones Where idCliente = '" + dtgvDirecciones.CurrentRow.Cells[0].Value.ToString() + "' and idDireccion = '" + dtgvDirecciones.CurrentRow.Cells[1].Value.ToString() + "'";
						BaseDeDatos.ExecuteSelect(false, strSql);
						//Tambie me lo cepillo del dtDirecciones.
						foreach (DataRow row in dtDirecciones.Rows)
						{
							if (row["idCliente"].ToString() == tId.Text && row["idDireccion"].ToString() == tDireccion.Text)
							{
								row.Delete();
								row.AcceptChanges();
								break;
							}
						}
					}
					else
					{
						foreach (DataRow row in dtDirecciones.Rows)
						{
							if (row["idCliente"].ToString() == tId.Text && row["idDireccion"].ToString() == tDireccion.Text)
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

		private void eliminarCliente()
		{
			string strSql = "";
			try
			{
				//Aqui deberemos hacer una validacion de integridad referenciada, en realidad, no eliminamos, si no que desacctivamos el cliente pero siempre y cuando no este en uso.
				DialogResult dialogResult = MessageBox.Show("¿Desea desactivar el cliente de manera permanente?. " + "\n" + "\n" + dtgvDatos.CurrentRow.Cells[2].Value.ToString(), "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

				if (dialogResult == DialogResult.Yes)
				{
					strSql = "Update MantClientes Set Activo = 0 Where idCliente = '" + dtgvDatos.CurrentRow.Cells[0].Value.ToString() + "'";
					BaseDeDatos.ExecuteSelect(false, strSql);
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
