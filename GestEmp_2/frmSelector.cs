using GestEmp_2.clsFunciones;
using GestEmp_2.clsFuncionesGenerales;
using System;
using System.Data;
using System.Windows.Forms;


namespace GestEmp_2
{
	public partial class frmSelector : Form
	{
		public frmSelector()
		{
			InitializeComponent();
		}
		public string strSql;
		public string Titulo;

		public object id;
		public string codigo;
		public string nombre;
		public int tamanyo;
		public int[] OcultarColumnes = new int[100];

		public DataTable dtDatos;

		private void frmSelector_Load(object sender, EventArgs e)
		{
			OcultarColumnes = new int[tamanyo];

			this.Text = Titulo;
			dtDatos = BaseDeDatos.ExecuteSelect(true, strSql);

			dtgvDatos.DataSource = dtDatos;
			dtgvDatos.Columns[0].Visible = false;
			dtgvDatos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

			if (OcultarColumnes.Length > 0)
				{
				for (int i = 0; i < OcultarColumnes.Length; i++)
				{
						dtgvDatos.Columns[i].Visible = false;
				}
			}

		}

		private void bAceptar_Click(object sender, EventArgs e)
		{
			try
			{
				Aceptamos();
			}

			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void bSalir_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void dtgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				Aceptamos();
			}

			catch (Exception ex)
			{
				clsfuncionesGenerales obj = new clsfuncionesGenerales();
				obj.creoPopup("Se ha producido el siguiente error: " + ex.Message, "Error", 1);
			}
		}

		private void Aceptamos()
		{
			try
			{
				id = dtgvDatos.Rows[dtgvDatos.CurrentRow.Index].Cells[0].Value.ToString();
				codigo = dtgvDatos.Rows[dtgvDatos.CurrentRow.Index].Cells[1].Value.ToString();
				nombre = dtgvDatos.Rows[dtgvDatos.CurrentRow.Index].Cells[2].Value.ToString();
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Se ha producido el siguiente error: " + ex.Message.ToString());
			}
		}
	}
}
