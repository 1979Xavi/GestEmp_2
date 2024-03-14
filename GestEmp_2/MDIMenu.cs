using System;
using System.Drawing;
using System.ServiceProcess;
using System.Windows.Forms;

namespace GestEmp_2
{
	public partial class MDIMenu : Form
	{
		private int childFormNumber = 0;

		public MDIMenu()
		{
			InitializeComponent();
			//string accion = "C";
		}

		private void ShowNewForm(object sender, EventArgs e)
		{
			Form childForm = new Form();
			childForm.MdiParent = this;
			childForm.Text = "Ventana " + childFormNumber++;
			childForm.Show();
		}

		private void OpenFile(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
			if (openFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				string FileName = openFileDialog.FileName;
			}
		}

		private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
			if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				string FileName = saveFileDialog.FileName;
			}
		}

		private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}


		private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LayoutMdi(MdiLayout.Cascade);
		}

		private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LayoutMdi(MdiLayout.TileVertical);
		}

		private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LayoutMdi(MdiLayout.TileHorizontal);
		}

		private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LayoutMdi(MdiLayout.ArrangeIcons);
		}

		private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (Form childForm in MdiChildren)
			{
				childForm.Close();
			}
		}

		private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
		{

			clientes Frm = new clientes();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void MDIMenu_Load(object sender, EventArgs e)
		{
			ServiceController sc = new ServiceController("MSSQL$SQLEXPRESS");

			if ((sc.Status.Equals(ServiceControllerStatus.Stopped)) || (sc.Status.Equals(ServiceControllerStatus.StopPending)))
			{
				//sc.Start();
				lblServicio.Text = "Servicio parado";
				lblServicio.BackColor = Color.Red;
			}
			else
			{
				//sc.Stop();
				lblServicio.Text = "Servicio arrancado";
				lblServicio.BackColor = Color.Green;
			}

			// Refresh and display the current service status.
			sc.Refresh();
		}

		private void poblacionesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.Poblaciones Frm = new Mantenimientos.Poblaciones();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();

		}

		private void provinciasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.Provincias Frm = new Mantenimientos.Provincias();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void paisesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.Paises Frm = new Mantenimientos.Paises();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void iVAToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.Ivas Frm = new Mantenimientos.Ivas();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void descuentosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.Descuentos Frm = new Mantenimientos.Descuentos();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.Proveedores Frm = new Mantenimientos.Proveedores();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void familiasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.Familias Frm = new Mantenimientos.Familias();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void coloresToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.Colores Frm = new Mantenimientos.Colores();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void tallasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.Tallas Frm = new Mantenimientos.Tallas();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void unidadesDeMedidaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.UnidadesMedida Frm = new Mantenimientos.UnidadesMedida();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void articulosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.Articulos Frm = new Mantenimientos.Articulos();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void presupuestosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Pantallas.Presupuestos Frm = new Pantallas.Presupuestos();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void tiposDeCodigoDeBarrasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.TiposCodigosBarras Frm = new Mantenimientos.TiposCodigosBarras();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void listaDeComponentesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.ListaComponentes Frm = new Mantenimientos.ListaComponentes();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void listaDeOperacionesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.ManoObra Frm = new Mantenimientos.ManoObra();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void lineasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.Lineas Frm = new Mantenimientos.Lineas();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void seccionesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.Secciones Frm = new Mantenimientos.Secciones();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void maquinasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.Maquinas Frm = new Mantenimientos.Maquinas();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void directorioDeOperacionesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.Operaciones Frm = new Mantenimientos.Operaciones();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void almacenesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.Almacenes Frm = new Mantenimientos.Almacenes();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void ubicacionesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.Ubicaciones Frm = new Mantenimientos.Ubicaciones();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void tiposUbiacionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.TiposUbicacion Frm = new Mantenimientos.TiposUbicacion();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void empresasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.Empresas Frm = new Mantenimientos.Empresas();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void usuariosToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			Mantenimientos.Usuarios Frm = new Mantenimientos.Usuarios();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void perfilesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Mantenimientos.Perfiles Frm = new Mantenimientos.Perfiles();
			Frm.TopLevel = false;
			splitContainer1.Panel1.Controls.Add(Frm);
			Frm.Show();
		}

		private void gestionDeRutasToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}
	}
}
