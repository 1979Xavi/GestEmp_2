using System;
using System.Windows.Forms;

namespace GestEmp_2.Listados
{
	public partial class frmInforme : Form
	{
		public frmInforme()
		{
			InitializeComponent();
		}
		public string idPresupuesto { get; set; }
		private void frmInforme_Load(object sender, EventArgs e)
		{
			//crystalReportViewer1.ReportSource = GestEmp_2.clsFunciones.BaseDeDatos.ExecuteStoredProcedure("sp_viewPresupuesto", idPresupuesto);
			//rptPresupuesto1.SetParameterValue("@idPresupuesto", idPresupuesto.ToString());
			rptPresupuesto1.SetDataSource(GestEmp_2.clsFunciones.BaseDeDatos.ExecuteStoredProcedure("sp_viewPresupuesto", idPresupuesto));
			rptPresupuesto1.Refresh();
		}
	}
}
