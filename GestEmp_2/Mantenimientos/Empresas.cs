using GestEmp_2.clsFunciones;
using GestEmp_2.clsFuncionesGenerales;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GestEmp_2.Mantenimientos
{
	public partial class Empresas : Form
	{
		string accionPantalla = "C";
		public Empresas()
		{
			InitializeComponent();
		}

		private void Empresas_Load(object sender, EventArgs e)
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
		private void cargaDatos()
		{
			try
			{
				accionPantalla = "C";
				comportamientoBotones();
				dtgvDatos.DataSource = BaseDeDatos.ExecuteSelect(true, "Select idEmpresa, RazonSocial from MantEmpresas Where Activo = 1");
				dtgvDatos.Columns[0].Visible = false;
				
				if (dtgvDatos.Rows.Count > 0 )
				{
					dtgvDatos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
					MuestraDetalle(dtgvDatos.Rows[0].Cells[0].Value.ToString());
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

					tNifCif.Text = dtDetalle.Rows[0]["DomicilioFActuracion"].ToString();
					tDescripcion.Text = dtDetalle.Rows[0]["RazonSocialFacturacion"].ToString();
					tDirecccion.Text = dtDetalle.Rows[0]["RazonSocialFacturacion"].ToString();

					tCodPais.Tag = dtDetalle.Rows[0]["idPaisFacturacion"].ToString();
					tCodPais.Text = dtDetalle.Rows[0]["CodigoPais"].ToString();
					tDescPais.Text = dtDetalle.Rows[0]["Pais"].ToString();

					tCodProvincia.Tag = dtDetalle.Rows[0]["idProvinciaFacturacion"].ToString();
					tCodProvincia.Text = dtDetalle.Rows[0]["CodigoProvincia"].ToString();
					tDescProvincia.Text = dtDetalle.Rows[0]["Provincia"].ToString();

					tCodPoblacion.Tag = dtDetalle.Rows[0]["idPoblacionFacturacion"].ToString();
					tCodPoblacion.Text = dtDetalle.Rows[0]["CodigoPoblacion"].ToString();
					tDescPoblacion.Text = dtDetalle.Rows[0]["Poblacion"].ToString();



					//Aqui seleccionamos los valores de las combos.

					pLogo.ImageLocation = dtDetalle.Rows[0]["RutaImagen"].ToString();

				}
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
				frm1.tamanyo = 3;
				frm1.OcultarColumnes[0] = 0;
				frm1.OcultarColumnes[1] = 3;
				frm1.OcultarColumnes[2] = 6;
				

				frm1.Titulo = "Selector de Paises";
				frm1.strSql = "Select po.idPoblacion, po.CodigoPoblacion, po.Descripcion as Poblacion, pr.idProvincia, pr.CodigoProvincia, pr.Descripcion as Provincia, pa.idPais, pa.CodigoPais, pa.Descripcion as Pais  " +
					"from MantPoblaciones po Inner Join MantProvincias pr on po.idProvincia = pr.idProvincia " + 
					"Inner join MantPaises pa on po.idPais = pa.idPais";
				frm1.ShowDialog();


				tCodPoblacion.Tag = frm1.dtDatos.Rows[0]["idPoblacion"].ToString();
				tCodPoblacion.Text = frm1.dtDatos.Rows[0]["CodigoPoblacion"].ToString();
				tDescPoblacion.Text = frm1.dtDatos.Rows[0]["Poblacion"].ToString();

				tCodProvincia.Tag = frm1.dtDatos.Rows[0]["idProvincia"].ToString();
				tCodProvincia.Text = frm1.dtDatos.Rows[0]["codigoProvincia"].ToString();
				tDescProvincia.Text = frm1.dtDatos.Rows[0]["Provincia"].ToString();

				tCodPais.Tag = frm1.dtDatos.Rows[0]["idPais"].ToString();
				tCodPais.Text = frm1.dtDatos.Rows[0]["codigoPais"].ToString();
				tDescPais.Text = frm1.dtDatos.Rows[0]["Pais"].ToString();
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
				pLogo.ImageLocation = opFile.FileName;

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
				//string guid = "";
				//if (ValidaDatos())
				//{
				//	if (accionPantalla == "N")
				//	{
				//		guid = Guid.NewGuid().ToString();
				//		GrabaEmpresa(accionPantalla, guid);
				//	}
				//	else
				//	{
				//		guid = tId.Text;
				//		GrabaEmpresa(accionPantalla, guid);
				//	}

				//	cargaDatos();
				//	accionPantalla = "A";
				//	comportamientoBotones();
				//}
			}
			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		//private bool ValidaDatos()
		//{
		//	try
		//	{
		//		// De moment ho deixem aixi despres posem validacions
		//		return true;
		//	}
		//	catch (Exception ex)
		//	{
		//		clsfuncionesGenerales obj = new clsfuncionesGenerales();
		//		obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
		//	}

		//}

		private void GrabaEmpresa(string accion, string guid)
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

								guid = Guid.NewGuid().ToString();
								SqlTransaction transacCab = connection.BeginTransaction();
								strCabecera = "INSERT INTO MantEmpresas (idEmpresa, RazonSocial, NIF, Domicilio, CodigoPostal, idPoblacion, idProvincia, idPais, Activo, UsuarioCreacion, FechaHoraCreacion)" +
									" VALUES ('" + guid + "','" +
									tDescripcion.Text + "', " +
									tNifCif.Text + "', " +
									tDirecccion.Text + "', " +
									tCodigoPostal.Text +
									tCodPoblacion.Tag + "', " +
									tCodProvincia.Tag + "', " +
									tCodPais.Tag + "',)" +
									"1, 'SYSTEM','" +
									DateTime.Now + "')";

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

		private void bNuevo_Click(object sender, EventArgs e)
		{
			accionPantalla = "N";
			comportamientoBotones();
		}
	}
}
