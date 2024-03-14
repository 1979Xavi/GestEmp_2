using System;
using System.Windows.Forms;

namespace GestEmp_2
{
	static class Program
	{
		/// <summary>
		/// Punto de entrada principal para la aplicación.
		/// </summary>
		[STAThread]
		static void Main()
		{

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			///Application.Run(new MDIMenu());
			///
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Login loginForm = new Login();
			if (loginForm.ShowDialog() == DialogResult.OK)
			{
				// Si el login tiene éxito, mostramos el formulario MDI
				MDIMenu mdiForm = new MDIMenu();
				Application.Run(mdiForm);
			}

		}
	}
}
