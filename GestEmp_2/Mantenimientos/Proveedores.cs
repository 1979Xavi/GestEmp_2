using GestEmp_2.clsFunciones;
using GestEmp_2.clsFuncionesGenerales;
using System;
using System.Data;
using System.Windows.Forms;

namespace GestEmp_2.Mantenimientos
{
	public partial class Proveedores : Form
	{
		//Poslbles estados
		//C-Consultar, A-Modificar, M-Modificar, E-Eliminar (Activo = 0), D-Duplicar
		string accionPantalla = "C";
		public Proveedores()
		{
			InitializeComponent();
		}

		private void Proveedores_Load(object sender, EventArgs e)
		{
			try
			{
				cargaDatos();
				CargoCbos();
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
				dtgvDatos.DataSource = BaseDeDatos.ExecuteSelect(true, "Select idProveedor, CodigoProveedor, RazonSocial from MantProveedores");
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

		void comportamientoBotones()
		{
			switch (accionPantalla)
			{
				case "C":
				case "A":
					pnlGeneral.Enabled = false;
					pnlFacturacion.Enabled = false;
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
					pnlGeneral.Enabled = true;
					pnlFacturacion.Enabled = true;
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
		private void MuestraDetalle(string id)
		{
			try
			{
				tId.Text = id;
				string strSQl = "Select idProveedor, CodigoProveedor, RazonSocial, Domicilio, TelefonoContacto, PersonaContacto, " +
					" Email, Web, CodigoPostal, pro.idPoblacion, pob.CodigoPoblacion, pob.Descripcion as 'Poblacion', " +
					" pro.idProvincia, prv.CodigoProvincia, prv.Descripcion as Provincia, " +
					" pro.idPais, pai.CodigoPais, pai.Descripcion as 'Pais', " +
					" NIFCIF, pro.idFormaPago, fpa.Descripcion as 'Forma de Pago', DiaPago, DomicilioBancario, pro.idIVA, iva.descripcion as IVA, " +
					" pro.idDescuento, dto.descripcion, Observaciones, RutaImagen from MantProveedores pro" +
					" Left Join MantPoblaciones pob on pro.idPoblacion = pob.idPoblacion and pro.idProvincia = pob.idProvincia and pro.idPais = pob.idPais" +
					" Left Join MantProvincias prv on pro.idProvincia = prv.idProvincia and pro.idPais = prv.idPais" +
					" Left Join MantPaises pai on pro.idPais = pai.idPais" +
					" Left Join MantFormasPago fpa on pro.idFormaPago = fpa.idFormaPago" +
					" Left Join MantIVA iva on pro.idIVA = iva.idIVA" +
					" Left Join MantDescuento dto on pro.idDescuento = dto.idDescuento where idProveedor = '" + id + "'";
				DataTable dtDetalle = BaseDeDatos.ExecuteSelect(true, strSQl);

				if (dtDetalle.Rows.Count > 0)
				{
					tCodigo.Text = dtDetalle.Rows[0]["CodigoProveedor"].ToString();
					tNombre.Text = dtDetalle.Rows[0]["RazonSocial"].ToString();

					tDireccion.Text = dtDetalle.Rows[0]["Domicilio"].ToString();
					tCodigoPostal.Text = dtDetalle.Rows[0]["CodigoPostal"].ToString();

					tCodPais.Tag = dtDetalle.Rows[0]["idPais"].ToString();
					tCodPais.Text = dtDetalle.Rows[0]["CodigoPais"].ToString();
					tDescPais.Text = dtDetalle.Rows[0]["Pais"].ToString();

					tCodProvincia.Tag = dtDetalle.Rows[0]["idProvincia"].ToString();
					tCodProvincia.Text = dtDetalle.Rows[0]["CodigoProvincia"].ToString();
					tDescProvincia.Text = dtDetalle.Rows[0]["Provincia"].ToString();

					tCodPoblacion.Tag = dtDetalle.Rows[0]["idPoblacion"].ToString();
					tCodPoblacion.Text = dtDetalle.Rows[0]["CodigoPoblacion"].ToString();
					tDescPoblacion.Text = dtDetalle.Rows[0]["Poblacion"].ToString();

					tTelefono.Text = dtDetalle.Rows[0]["TelefonoContacto"].ToString();
					tEmail.Text = dtDetalle.Rows[0]["Email"].ToString();
					tWeb.Text = dtDetalle.Rows[0]["Web"].ToString();
					tPersonaContacto.Text = dtDetalle.Rows[0]["PersonaContacto"].ToString();

					//Aqui seleccionamos los valores de las combos.

					cDescuento.SelectedValue = dtDetalle.Rows[0]["idDescuento"].ToString();
					cIVA.SelectedValue = dtDetalle.Rows[0]["idIVA"].ToString();

					tObservaciones.Text = dtDetalle.Rows[0]["Observaciones"].ToString();
					pCliente.ImageLocation = dtDetalle.Rows[0]["RutaImagen"].ToString();



				}
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
						guid = Guid.NewGuid().ToString();
						strSql = "Insert into MantProveedores (idProveedor" +
						 ", CodigoProveedor" +
						 ", RazonSocial" +
						 ", Domicilio" +
						 ", TelefonoContacto" +
						 ", PersonaContacto" +
						 ", Email" +
						 ", Web" +
						 ", CodigoPostal" +
						 ", idProvincia" +
						 ", idPoblacion" +
						 ", idPais" +
						 ", NIFCIF" +
						 ", idFormaPago" +
						 ", DiaPago" +
						 ", DomicilioBancario" +
						 ", idIVA" +
						 ", idDescuento" +
						 ", GastosEnvio" +
						 ", RutaImagen" +
						 ", Observaciones" +
						 ", Activo" +
						 ", UsuarioCreacion " +
						 ", FechaHoraCreacion) Values ('" +
								guid + "', '" +
								tCodigo.Text + "', '" +
								tNombre.Text + "', '" +
								tDireccion.Text + "', '" +
								tTelefono.Text + "', '" +
								tPersonaContacto.Text + "', '" +
								tEmail.Text + "', '" +
								tWeb.Text + "', '" +
								tCodigoPostal.Text + "', '" +
								tCodProvincia.Tag + "', '" +
								tCodPoblacion.Tag + "', '" +
								tCodPais.Tag + "', '" +
								tNIFCIF.Text + "', '" +
								cFormaPago.SelectedValue.ToString() + "', '" +
								tDiaCobro.Text + "', '" +
								tDomiciliacionBancaria.Text + "', '" +
								cIVA.SelectedValue.ToString() + "', '" +
								cDescuento.SelectedValue.ToString() + "', " +
								tGastosEnvio.Text + ", '" +
								tObservaciones.Text + "', '" +
								pCliente.ImageLocation + "', " +
								"1, 'SYSTEM','" +
								DateTime.Now + "')";
					}
					else
					{
						guid = tId.Text;
						strSql = "Update MantProveedores Set CodigoProveedor = '" + tCodigo.Text + "', RazonSocial = '" + tNombre.Text + "',  Domicilio = '" + tDireccion.Text +
							"', TelefonoContacto = '" + tTelefono.Text + "', PersonaContacto = '" + tPersonaContacto.Text + "', Email = '" + tEmail.Text + "', Web = '" + tWeb.Text +
							"', CodigoPostal = '" + tCodigoPostal.Text + "', idProvincia = '" + tCodProvincia.Tag + "', idPoblacion = '" + tCodPoblacion.Tag +
							"', idPais = '" + tCodPais.Tag + "', NIFCIF = '" + tNIFCIF.Text + "', DiaPago = " + tDiaCobro.Text + ", idFormaPago = '" + cFormaPago.SelectedValue.ToString() +
							"', DomicilioBancario = '" + tDomiciliacionBancaria.Text + "', idIva = '" + cIVA.SelectedValue.ToString() + "', idDescuento = '" + cDescuento.SelectedValue.ToString() +
							"', Observaciones = '" + tObservaciones.Text + "', GastosEnvio = " + tGastosEnvio.Text +
							", RutaImagen = '" + pCliente.ImageLocation + "', UsuarioModificacion = 'SYSTEM', FechaHoraModificacion = '" + DateTime.Now.ToString() + "' Where idProveedor = '" + tId.Text + "'";
					}

					BaseDeDatos.ExecuteSelect(true, strSql);
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

				if (tDiaCobro.Text == "")
				{
					tDiaCobro.Text = "1";
				}

				if (tGastosEnvio.Text == "")
				{
					tGastosEnvio.Text = "0";
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

		private void blanqueoCampos()
		{
			try
			{
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


			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);

			}
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
				DialogResult dialogResult = MessageBox.Show("¿Desea salir del Mto. de Proveedores?", "Mto. Proveedores", MessageBoxButtons.YesNo);
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

		private void bBuscaPoblacion_Click(object sender, EventArgs e)
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
	}
}
