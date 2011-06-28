using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Threading;
using System.Globalization;
using SadipDAL;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;

namespace SadipDAL
{
    public class ServerSystem:DataAccess
    {
        #region Properties

        private int server_id = 0;
        public int Server_Id
        {
            get { return server_id; }
            set { server_id = value; }
        }

        private string server_name = "";
        public string Server_name
        {
            get { return server_name; }
            set { server_name = value; }
        }

        private string server_ip = "";
        public string Server_Ip
        {
            get { return server_ip; }
            set { server_ip = value; }
        }

        private string sql_sentence = "";
        public string Sql_Sentence
        {
            get { return sql_sentence; }
            set { sql_sentence = value; }
        }

        #endregion

        #region Constructors

        public ServerSystem()
        {
           
        }

        public ServerSystem(int Id)
        {
            this.Server_Id = Id;
            Read();
        }

        #endregion

        #region Read
        public void Read()
        {

            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("sp_GetServerSystemByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Server_ID", this.Server_Id));
                cn.Open();
                IDataReader dr = db.ExecuteReader(cmd);
                if (dr.Read())
                {
                    this.server_name = dr["Server_Name"].ToString();
                    this.Server_Ip = dr["Server_IP"].ToString();
                    this.Sql_Sentence = dr["Server_Sql_Sentence"].ToString();
                    this.IsDeleted = bool.Parse(dr["is_deleted"].ToString());
                }
                dr.Close();
            }

        }
        #endregion

        #region Insert
        public int Insert()
        {
            int deger = -1;
            using (DbConnection cn = Connect())
            {
                cn.Open();
                DbCommand cmd = db.GetStoredProcCommand("sp_InsertServerSystem");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Server_Name", this.Server_name));
                cmd.Parameters.Add(new SqlParameter("@Server_IP", this.Server_Ip));
                cmd.Parameters.Add(new SqlParameter("@Server_Sql_Sentence", this.Sql_Sentence));
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@Server_ID";
                p.Direction = ParameterDirection.Output;
                p.Size = int.MaxValue;
                cmd.Parameters.Add(p);
                deger = db.ExecuteNonQuery(cmd);
                cn.Close();
                Server_Id = int.Parse(cmd.Parameters["@Server_ID"].Value.ToString());
                return Server_Id;
            }

        }
        #endregion

        #region Update
        public void Update()
        {
            using (DbConnection cn = Connect())
            {
                int deger = -1;
                cn.Open();
                DbCommand cmd = db.GetStoredProcCommand("sp_UpdateServerSystem");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Server_Name", this.Server_name));
                cmd.Parameters.Add(new SqlParameter("@Server_IP", this.Server_Ip));
                cmd.Parameters.Add(new SqlParameter("@Server_Sql_Sentence", this.Sql_Sentence));
                cmd.Parameters.Add(new SqlParameter("@Server_ID", this.Server_Ip));
                deger = db.ExecuteNonQuery(cmd);
                cn.Close();
            }

        }
        #endregion

        #region Delete
        public void Delete()
        {
            using (DbConnection cn = Connect())
            {
                cn.Open();
                DbCommand cmd = db.GetStoredProcCommand("sp_DeleteServerSystem");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Server_ID", this.Server_Id));
                db.ExecuteNonQuery(cmd);
                cn.Close();
            }

        }
        #endregion
    }
}
