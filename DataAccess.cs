using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Web.Caching;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SadipDAL;

namespace SadipDAL
{
    public abstract class DataAccess : MyDatabase
    {
        private DbConnection connection;
        private bool _isDeleted = false;
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }

        private string _insertUser = CurrentUserName;
        public string InsertUser
        {
            get { return _insertUser; }
            protected set { _insertUser = value; }
        }

        private DateTime _insertDate = DateTime.Now;
        public DateTime InsertDate
        {
            get { return _insertDate; }
            protected set { _insertDate = value; }
        }

        private string _updateUser = CurrentUserName;
        public string UpdateUser
        {
            get { return _updateUser; }
            protected set { _updateUser = value; }
        }

        private DateTime _updateDate = DateTime.Now;
        public DateTime UpdateDate
        {
            get { return _updateDate; }
            protected set { _updateDate = value; }
        }

        private string _deleteUser = CurrentUserName;
        public string DeleteUser
        {
            get { return _deleteUser; }
            protected set { _deleteUser = value; }
        }

        private DateTime _deleteDate = DateTime.Now;
        public DateTime DeleteDate
        {
            get { return _deleteDate; }
            protected set { _deleteDate = value; }
        }

        protected Cache Cache
        {
            get { return HttpContext.Current.Cache; }
        }

        protected int ExecuteNonQuery(DbCommand cmd)
        {
            if (HttpContext.Current.User.Identity.Name.ToLower() == "sampleeditor")
            {
                foreach (DbParameter param in cmd.Parameters)
                {
                    if (param.Direction == ParameterDirection.Output ||
                       param.Direction == ParameterDirection.ReturnValue)
                    {
                        switch (param.DbType)
                        {
                            case DbType.AnsiString:
                            case DbType.AnsiStringFixedLength:
                            case DbType.String:
                            case DbType.StringFixedLength:
                            case DbType.Xml:
                                param.Value = "";
                                break;
                            case DbType.Boolean:
                                param.Value = false;
                                break;
                            case DbType.Byte:
                                param.Value = byte.MinValue;
                                break;
                            case DbType.Date:
                            case DbType.DateTime:
                                param.Value = DateTime.MinValue;
                                break;
                            case DbType.Currency:
                            case DbType.Decimal:
                                param.Value = decimal.MinValue;
                                break;
                            case DbType.Guid:
                                param.Value = Guid.Empty;
                                break;
                            case DbType.Double:
                            case DbType.Int16:
                            case DbType.Int32:
                            case DbType.Int64:
                                param.Value = 0;
                                break;
                            default:
                                param.Value = null;
                                break;
                        }
                    }
                }
                return 1;
            }
            else
                return cmd.ExecuteNonQuery();
        }

        protected IDataReader ExecuteReader(DbCommand cmd)
        {
            return ExecuteReader(cmd, CommandBehavior.Default);
        }

        protected IDataReader ExecuteReader(DbCommand cmd, CommandBehavior behavior)
        {
            return cmd.ExecuteReader(behavior);
        }

        protected object ExecuteScalar(DbCommand cmd)
        {
            return cmd.ExecuteScalar();
        }

        //protected static SqlConnection OpenConnWrite()
        //{
        //    SqlConnection conn = new SqlConnection(.ConnectionString);
        //    return conn;
        //}

        //protected static SqlConnection OpenConnRead()
        //{
        //    SqlConnection conn = new SqlConnection(Common.ConnectionString2);
        //    return conn;
        //}
        //protected static SqlConnection SecimConnection()
        //{
        //    SqlConnection conn = new SqlConnection(Common.ConnectionStringSecim);
        //    return conn;
        //}

        //protected static SqlConnection OpenSporConn()
        //{
        //    SqlConnection conn = new SqlConnection(Common.SporConnectionString);
        //    return conn;
        //}

        //protected static SqlConnection OpenConnSearch()
        //{
        //    SqlConnection conn = new SqlConnection(Common.ConnectionStringSearch);
        //    return conn;
        //}


        protected void ExecuteSql(string sqlstring)
        {
            using (DbConnection conn = Connect())
            {

                conn.Open();
                DbCommand comm = db.GetSqlStringCommand(sqlstring);
                DataSet ds = db.ExecuteDataSet(comm);

                conn.Close();

            }
        }




        protected DataSet ExecuteReadSqlDataSet(string sqlstring)
        {
            using (DbConnection conn = Connect())
            {


                conn.Open();
                DbCommand comm = db.GetSqlStringCommand(sqlstring);
                DataSet ds = db.ExecuteDataSet(comm);

                conn.Close();
                return ds;
            }
        }

        protected DataSet ExecuteSearchSqlDataSet(string sqlstring)
        {
            using (DbConnection conn = Connect())
            {


                conn.Open();
                DbCommand comm = db.GetSqlStringCommand(sqlstring);
                DataSet ds = db.ExecuteDataSet(comm);

                conn.Close();
                return ds;
            }
        }
        protected DataSet ExecuteWriteSqlDataSet(string sqlstring)
        {
            using (DbConnection conn = Connect())
            {
                conn.Open();
                DbCommand comm = db.GetSqlStringCommand(sqlstring);
                DataSet ds = db.ExecuteDataSet(comm);

                conn.Close();
                return ds;
            }
        }

        //protected static IDataReader ExecuteReader(string sqlstring)
        //{
        //    SqlConnection conn = OpenConn();
        //    conn.Open();

        //    SqlCommand comm = new SqlCommand(sqlstring, conn);
        //    IDataReader reader;
        //    reader = comm.ExecuteReader(CommandBehavior.Default);
        //    //conn.Close();

        //    return reader;

        //}


        protected DataSet ExecuteSqlCommandToDataSet(DbCommand sqlComm)
        {
            using (DbConnection conn = Connect())
            {
                conn.Open();
                DataSet ds = db.ExecuteDataSet(sqlComm);
                conn.Close();
                return ds;
            }
        }
        // sonradan ramazan için eklendi
        protected DataSet ExecuteSqlDataSet(string sqlstring)
        {
            using (DbConnection conn = Connect())
            {


                conn.Open();
                DbCommand comm = db.GetSqlStringCommand(sqlstring);
                DataSet ds = db.ExecuteDataSet(comm);

                conn.Close();
                return ds;
            }
        }

    }
}
        



