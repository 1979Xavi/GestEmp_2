using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using System.Data.SqlClient;
using System.Data;

namespace GestEmp_2.Classes.Framework
{
    class funcionesSQL
    {

        public abstract class classSelect : DataTable
        {
            #region PrivateMembers

            private string _connectionString = "";
            private SqlConnection _conn = null;
            private SqlTransaction _tx = null;

            #endregion

            #region PublicProperties

            public abstract string SelectCommand
            {
                get;
            }
            

            #endregion

            #region Constructor

     

            #endregion

            #region PrivateMethods

            private void CreateConnection()
            {
                this._conn = new System.Data.SqlClient.SqlConnection(this._connectionString);
            }
            private void Init()
            {
                if (this.Rows.Count != 0)
                    this.Rows.Clear();
                if (this._conn == null)
                    this.CreateConnection();

                if (this._conn.State != ConnectionState.Open)
                    this._conn.Open();
            }

            #endregion

            #region ProtectedMethods

            protected abstract void DefineColumns();

            #endregion

            #region PublicMethods

            public virtual void Execute()
            {
                this.Init();

                
                SqlCommand QueryMSSQL = new SqlCommand();
                QueryMSSQL.CommandTimeout = 900;
                QueryMSSQL.CommandText = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " + this.SelectCommand;
                QueryMSSQL.Connection = this._conn;
                if (this._tx != null)
                    QueryMSSQL.Transaction = this._tx;
                SqlDataAdapter readerMSSQL = new SqlDataAdapter();
                readerMSSQL.SelectCommand = QueryMSSQL;
                readerMSSQL.Fill(this);
                QueryMSSQL = null;
                readerMSSQL = null;
            }
            public virtual void ExecuteNonQuery()
            {
                this.Init();

                
                SqlCommand QueryMSSQL = new SqlCommand();
                QueryMSSQL.CommandTimeout = 9000;
                QueryMSSQL.CommandText = this.SelectCommand;
                QueryMSSQL.Connection = this._conn;
                if (this._tx != null)
                    QueryMSSQL.Transaction = this._tx;
                QueryMSSQL.ExecuteNonQuery();
                QueryMSSQL = null;
            }

            #endregion

            #region Miembros de IC2MeditaObject


            #endregion

            #region EventHandlers



            #endregion
        }

        public class GenericQueryExecutor : classSelect
        {
            #region PrivateMembers

            private string _query;

            #endregion

            #region ClassSelect

            public override string SelectCommand
            {
                get
                {
                    return this._query;
                }
            }
            protected override void DefineColumns()
            {
                ;
            }

            #endregion

            #region Constructor



            #endregion

            #region StaticMethods



            #endregion
        }
    }
}
