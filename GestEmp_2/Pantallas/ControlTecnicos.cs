using System;
using System.Globalization;
using System.Windows.Forms;

namespace GestEmp_2.Pantallas
{
	public partial class ControlTecnicos : Form
	{
		public ControlTecnicos()
		{
			InitializeComponent();
		}

		private void mCalendario_DateChanged(object sender, DateRangeEventArgs e)
		{
			// Obtén la fecha seleccionada por el usuario
			DateTime fechaSeleccionada = mCalendario.SelectionStart;
			CultureInfo cultura = CultureInfo.CurrentCulture;
			Calendar calendario = cultura.Calendar;

			//int numeroSemana = calendario.GetWeekOfYear(fechaSeleccionada, cultura.DateTimeFormat.CalendarWeekRule, cultura.DateTimeFormat.FirstDayOfWeek);

			int numeroSemana = calendario.GetWeekOfYear(fechaSeleccionada, cultura.DateTimeFormat.CalendarWeekRule, cultura.DateTimeFormat.FirstDayOfWeek);


			DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
			Calendar calendar = dfi.Calendar;

			DateTime primerDiaDeLaSemana = new DateTime(fechaSeleccionada.Year, 1, 1).AddDays((numeroSemana) * 7);
			DayOfWeek primerDia = dfi.FirstDayOfWeek;

			while (primerDiaDeLaSemana.DayOfWeek != primerDia)
			{
				primerDiaDeLaSemana = primerDiaDeLaSemana.AddDays(-1);

			}
			DateTime ultimoDiaDeLaSemana = primerDiaDeLaSemana.AddDays(6);
			MessageBox.Show("1er dia: " + primerDiaDeLaSemana + ", ultimo dia de la semana " + ultimoDiaDeLaSemana + " de la semana: " + numeroSemana);



			for (int semanas = 0; semanas < 6; semanas++)
			{
				for (int dias = 0; dias < 6; dias++)
				{

				}
			}



		}
	}
}
