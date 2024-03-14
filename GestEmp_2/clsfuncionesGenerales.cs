using GestEmp_2.clsFunciones;
using System;
using Tulpep.NotificationWindow;

namespace GestEmp_2.clsFuncionesGenerales
{
	class clsfuncionesGenerales
	{
		public void creoPopup(string mensaje, string cabecera, int imagen)
		{
			PopupNotifier popup = new PopupNotifier();
			switch (imagen)
			{
				case 1:
					popup.Image = Properties.Resources.info;
					break;
				case 2:
					popup.Image = Properties.Resources.error;
					break;
			}

			popup.TitleText = cabecera;
			popup.ContentText = mensaje;
			popup.Popup();

		}

		public static string devuelveID(string referencia, string formulario)
		{
			string valor;
			try
			{
				valor = BaseDeDatos.ObtenerValor("SecuencialesConf", "cast(Secuencial + 1 AS varchar(10))", "Where Referencia = '" + referencia + "' and Pantalla = '" + formulario + "'");
				return valor;
			}
			catch (Exception e)
			{
				throw e;
#pragma warning disable CS0162 // Se detectó código inaccesible
				return "";
#pragma warning restore CS0162 // Se detectó código inaccesible
			}
		}

		public static string Encriptar(string _cadenaAencriptar)
		{
			string result = string.Empty;
			byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
			result = Convert.ToBase64String(encryted);
			return result;
		}

		public static string DesEncriptar(string _cadenaAdesencriptar)
		{
			string result = string.Empty;
			byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
			//result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
			result = System.Text.Encoding.Unicode.GetString(decryted);
			return result;
		}

	}
}
