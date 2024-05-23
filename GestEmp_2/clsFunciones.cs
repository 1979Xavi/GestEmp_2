using System;
using System.Data;
using System.Data.SqlClient;

namespace GestEmp_2.clsFunciones
{

	class BaseDeDatos
	{
		public static SqlConnection CreateConnection()
		{
			return new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["GestEmp"].ConnectionString);

		}
		public static DataTable ExecuteSelect(bool readOnly, string query)
		{
			SqlConnection Conn = BaseDeDatos.CreateConnection();
			Conn.Open();
			DataTable dtResult = ExecuteSelect(readOnly, query, Conn, null);
			Conn.Close();

			return dtResult;
		}

		public static DataTable ExecuteSelect(bool readOnly, string query, SqlConnection Conn, SqlTransaction Trans)
		{
			DataTable dtResult = new DataTable();
			string frase = "";

			try
			{
				SqlCommand Query = new SqlCommand();
				Query.CommandTimeout = 600;
				SqlDataAdapter reader = new SqlDataAdapter();

				if (readOnly) frase = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED ";
				frase += query;
				Query.CommandText = frase;

				Query.Connection = Conn;
				Query.Transaction = Trans;

				reader.SelectCommand = Query;
				reader.Fill(dtResult);

				Query = null;
				reader = null;
			}
			catch (Exception e)
			{
				//classLog.Log.AddLog("Error al ejecutar la sentencia " + frase + " (" + e.Message + ").", TipoLog.tpError);
				return null;
			}

			return dtResult;
		}

		public static DataTable InsertSelect(bool readOnly, string query)
		{
			SqlConnection Conn = BaseDeDatos.CreateConnection();
			Conn.Open();
			DataTable dtResult = ExecuteSelect(readOnly, query, Conn, null);
			Conn.Close();

			return dtResult;
		}

		public static string ObtenerValor(string nombreTabla, string nombreColumna, string condicion)
		{
			string valor = null;

			SqlConnection Conn = BaseDeDatos.CreateConnection();
			using (SqlConnection connection = new SqlConnection(Conn.ConnectionString))
			{
				string consulta = "SELECT " + nombreColumna + " FROM " + nombreTabla + " " + condicion;
				SqlCommand command = new SqlCommand(consulta, connection);

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();
				if (reader.HasRows && reader.Read())
				{
					if (!reader.IsDBNull(0))
					{
						valor = reader.GetString(0);
					}
				}
				reader.Close();
				connection.Close();
			}

			return valor;
		}

		public static DataTable ExecuteStoredProcedure(string storedProcedureName, string valor)
		{
			SqlConnection Conn = BaseDeDatos.CreateConnection();
			using (SqlConnection connection = new SqlConnection(Conn.ConnectionString))
			{
				SqlCommand command = new SqlCommand(storedProcedureName, connection);
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@idPresupuesto", valor);

				DataTable resultTable = new DataTable();

				try
				{
					connection.Open();
					SqlDataReader reader = command.ExecuteReader();
					resultTable.Load(reader);

					return resultTable;
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error al ejecutar el procedimiento almacenado: " + ex.Message);
					return null;
				}
			}
		}


	}
}






