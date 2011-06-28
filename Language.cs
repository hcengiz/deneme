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
   public class Language:DataAccess
    {
        # region Properties

        private int _id = 0;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _language = "";
        public string Languagee
        {
            get { return _language; }
            set { _language = value; }
        }

        private string _culture = "";
        public string Culture
        {
            get { return _culture; }
            set { _culture = value; }
        }

        #endregion

        #region Constructors

        public Language()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Language(int Id)
        {
            this.Id = Id;
            Read();
        }

        #endregion

        #region Read
        public void Read()
        {

            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("dk_GetLanguageByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@LanguageId", this.Id));
                cn.Open();
                IDataReader dr = db.ExecuteReader(cmd);
                if (dr.Read())
                {
                    this.Languagee = dr["language"].ToString();
                    this.Culture = dr["culture"].ToString();
                    this.IsDeleted = bool.Parse(dr["is_deleted"].ToString());
                    this.InsertUser = dr["insert_user"].ToString();
                    this.InsertDate = DateTime.Parse(dr["insert_date"].ToString());
                    this.UpdateUser = dr["update_user"] == DBNull.Value ? CurrentUserName : dr["update_user"].ToString();
                    this.UpdateDate = dr["update_date"] == DBNull.Value ? DateTime.Now : DateTime.Parse(dr["update_date"].ToString());
                    this.DeleteUser = dr["delete_user"] == DBNull.Value ? CurrentUserName : dr["delete_user"].ToString();
                    this.DeleteDate = dr["delete_date"] == DBNull.Value ? DateTime.Now : DateTime.Parse(dr["delete_date"].ToString());
                }
            }

        }
        #endregion

        public DataSet GetLanguages()
        {
            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("sp_GetLanguages");
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                return ExecuteSqlCommandToDataSet(cmd);
            }
        }

        //public static DataSet GetCulture(int id)
        //{
        //    string sql = "select id, culture from ser_languages where id=" + id;
        //    return ExecuteSqlDataSet(sql);
        //}

        //public static string ReadCulture(int id)
        //{
        //    string sql = "select id, culture from ser_languages where id=" + id;
        //    return ExecuteSqlDataSet(sql).Tables[0].Rows[0]["culture"].ToString();
        //}

    }
}
