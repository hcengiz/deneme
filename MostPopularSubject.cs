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
   public  class MostPopularSubject : DataAccess
   {

       #region Properties

       private int m_id = 0;
       public int m_Id
       {
           get { return m_id; }
           set { m_id = value; }
       }

       private string mptitle = "";
       public string mpTitle
       {
           get { return mptitle; }
           set { mptitle = value; }
       }

       private int m_order = 0;
       public int m_Order
       {
           get { return m_order; }
           set { m_order = value; }
       }

       private int m_categoryid = 0;
       public int m_Categoryid
       {
           get { return m_categoryid; }
           set { m_categoryid = value; }
       }

       #endregion

       #region Constructors

        public MostPopularSubject()
        {
           
        }

        public MostPopularSubject(int Id)
        {
            this.m_Id = Id;
            Read();
        }

        #endregion

        #region Read
        public void Read()
        {

            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("shbr_GetMostPopulerSubjectByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MpsId", this.m_Id));
                cn.Open();
                IDataReader dr = db.ExecuteReader(cmd);
                if (dr.Read())
                {
                    this.mpTitle = dr["title"].ToString();
                    this.m_Order =int.Parse(dr["order"].ToString());
                    this.m_Categoryid = int.Parse(dr["category_id"].ToString());
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
                DbCommand cmd = db.GetStoredProcCommand("shbr_InsertMostPopulerSubject");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Title", this.mpTitle));
                cmd.Parameters.Add(new SqlParameter("@Order", this.m_Order));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.m_Categoryid));
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@MpsId";
                p.Direction = ParameterDirection.Output;
                p.Size = int.MaxValue;
                cmd.Parameters.Add(p);
                deger = db.ExecuteNonQuery(cmd);
                cn.Close();
                m_Id = int.Parse(cmd.Parameters["@MpsId"].Value.ToString());
                return m_Id;
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
                DbCommand cmd = db.GetStoredProcCommand("shbr_UpdateMostPopulerSubject");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Title", this.mpTitle));
                cmd.Parameters.Add(new SqlParameter("@Order", this.m_Order));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", this.m_Categoryid));
                cmd.Parameters.Add(new SqlParameter("@MpsId", this.m_Id));   
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
                DbCommand cmd = db.GetStoredProcCommand("shbr_DeleteMostPopulerSubject");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MpsId", this.m_Id));
                db.ExecuteNonQuery(cmd);
                cn.Close();
            }

        }
        #endregion

        public  DataSet GetServerSystem()
        {
            using (DbConnection con = Connect())
            {
            string sql = "Select * from cms_Server_System where is_deleted='False' orde by server_id asc";
            con.Open();
            return ExecuteReadSqlDataSet(sql);

            }
        }
   }
}
