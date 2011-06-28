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
using SadipDAL;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;

namespace SadipDAL
{
   public class Columnist:DataAccess
    {
        #region Properties

        private int columnist_id = 0;
        public int ColumnistId
        {
            get { return columnist_id; }
            set { columnist_id = value; }
        }

        private string category_id = "";
        public string CategoryId
        {
            get { return category_id; }
            set { category_id = value; }
        }

        private string columnist_name = "";
        public string ColumnistName
        {
            get { return columnist_name; }
            set { columnist_name = value; }
        }

        private string columnist_email = "";
        public string ColumnistEmail
        {
            get { return columnist_email; }
            set { columnist_email = value; }
        }

        private string columnist_company = "";
        public string ColumnistCompany
        {
            get { return columnist_company; }
            set { columnist_company = value; }
        }

        private int image_id;
        public int ImageId
        {
            get { return image_id; }
            set { image_id = value; }
        }

        private bool is_published;
        public bool IsPublished
        {
            get { return is_published; }
            set { is_published = value; }
        }


        private bool is_deleted;
        public bool IsDeleted
        {
            get { return is_deleted; }
            set { is_deleted = value; }
        }

        private bool is_foregin;
        public bool IsForegin
        {
            get { return is_foregin; }
            set { is_foregin = value; }
        }


        private int _type = 0;
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private int columnist_order = 0;
        public int ColumnistOrder
        {
            get { return columnist_order; }
            set { columnist_order = value; }
        }


        #endregion


        #region Constructors

        public Columnist()
        {

        }

        public Columnist(int Id)
        {
            this.ColumnistId = Id;
            Read();
        }

        #endregion

        #region Read
        private void Read()
        {

            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("shbr_GetColumnistByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ColumnistId", this.ColumnistId));
                cn.Open();

                IDataReader dr = db.ExecuteReader(cmd); 
                if (dr.Read())
                {
                    this.ColumnistName = dr["columnist_name"].ToString().TrimEnd();
                    this.ColumnistEmail = dr["columnist_email"].ToString();
                    this.IsForegin =bool.Parse( dr["is_foreignColumnist"].ToString());
                    this.ColumnistCompany = dr["columnist_company"].ToString();
                    this.CategoryId = dr["category_id"].ToString();
                    this.IsDeleted = bool.Parse(dr["is_deleted"].ToString());
                    this.InsertUser = dr["insert_user"].ToString();
                    this.InsertDate = DateTime.Parse(dr["insert_date"].ToString());
                    this.UpdateUser = dr["update_user"].ToString();
                    this.UpdateDate = dr["update_date"] == DBNull.Value ? DateTime.Now : DateTime.Parse(dr["update_date"].ToString());
                    this.DeleteUser = dr["delete_user"].ToString();
                    this.DeleteDate = dr["delete_date"] == DBNull.Value ? DateTime.Now : DateTime.Parse(dr["delete_date"].ToString());
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
                DbCommand cmd = db.GetStoredProcCommand("sp_InsertColumnist");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Name", this.ColumnistName));
                cmd.Parameters.Add(new SqlParameter("@Category", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@IsForegin", this.IsForegin));
                //cmd.Parameters.Add(new SqlParameter("@ImageId",this.ImageId));
                cmd.Parameters.Add(new SqlParameter("@ColumnistCompany", this.ColumnistCompany));
                cmd.Parameters.Add(new SqlParameter("@InsertUser",this.InsertUser));
                cmd.Parameters.Add(new SqlParameter("@InsertDate", this.InsertDate));
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@ColumnistId";
                p.Direction = ParameterDirection.Output;
                p.Size = int.MaxValue;
                cmd.Parameters.Add(p);
                deger = db.ExecuteNonQuery(cmd);
                cn.Close();
                ColumnistId = int.Parse(cmd.Parameters["@ColumnistId"].Value.ToString());
                return ColumnistId;
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
                DbCommand cmd = db.GetStoredProcCommand("shbr_UpdateColumnist");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name", this.ColumnistName));
                cmd.Parameters.Add(new SqlParameter("@columnist_company", this.ColumnistCompany));
                cmd.Parameters.Add(new SqlParameter("@Category", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@is_foreignColumnist", this.IsForegin));
                cmd.Parameters.Add(new SqlParameter("@UpdateUser", this.UpdateUser));
                cmd.Parameters.Add(new SqlParameter("@UpdateDate", this.UpdateDate));
                cmd.Parameters.Add(new SqlParameter("@columnist_id", this.ColumnistId));
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
                DbCommand cmd = db.GetStoredProcCommand("shbr_DeleteColumnist");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ColumnistId",this.ColumnistId));
                cmd.Parameters.Add(new SqlParameter("@DeleteUser",this.DeleteUser));
                cmd.Parameters.Add(new SqlParameter("@DeleteDate",  this.DeleteDate));
                db.ExecuteNonQuery(cmd);
                cn.Close();
            }

        }
        #endregion


        public DataSet GetEditors()
        {
            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("shbr_GetColumnist");
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                return ExecuteSqlCommandToDataSet(cmd);
            }
        }

        public DataSet GetPublishedEditors(int type)
        {
            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("shbr_GetPublishedColumnist");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Type", type));
                cn.Open();
                return ExecuteSqlCommandToDataSet(cmd);
            }
        }

        public DataSet GetIamgeColumnist(int columnist)
        {
            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("sp_GetColumnistImage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ColumnistId", columnist));
                cn.Open();
                return ExecuteSqlCommandToDataSet(cmd);
            }
        }


        public DataSet GetEditorsWithLastArticles(int type)
        {//ana sayfada yayınlanmayan yazarlar
            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("shbr_GetColumnistWithLastArticles");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Type", type));
                cn.Open();
                return ExecuteSqlCommandToDataSet(cmd);
            }
        }

        public DataSet GetPublishedEditorsWithLastArticles(int type)
        {
            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("shbr_GetPublishedColumnistWithLastArticles");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Type", type));
                cn.Open();
                return ExecuteSqlCommandToDataSet(cmd);
            }
        }

        public int GetPublishedEditorsCount()
        {
            using (DbConnection cn = Connect())
            {
                string sql = string.Format(@"select count(columnist_id) from cms_columnist where published=1 and is_deleted=0", cn);
                DbCommand cmd = db.GetSqlStringCommand(sql);
                cn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void UpdateOrder(int columnistId, int order)
        {
            using (DbConnection cn = Connect())
            {
                string data = string.Format("update cms_columnist set columnist_order=" + order + " where columnist_id=" + columnistId, cn);
                DbCommand dbCommand = db.GetSqlStringCommand(data);
                cn.Open();
                dbCommand.ExecuteNonQuery();
            }
        }

        public void UpdatePublished(int columnistId, bool published)
        {
            using (DbConnection cn = Connect())
            {

                string data = string.Format("update cms_columnist set is_published=" + (published ? "1" : "0") + " where columnist_id=" + columnistId, cn);
                DbCommand dbCommand = db.GetSqlStringCommand(data);

                cn.Open();
                dbCommand.ExecuteNonQuery();
            }
        }


        //sql cumlecigi kontrol edilmeli editor_id kalkmıs tablodan.

        //public void UpdateOrderAndPublished(System.Collections.ArrayList arr)
        //{//yazarın idsi,published,order ozellikleri sırayla geliyor dizide
        //    using (DbConnection cn = Connect())
        //    {
        //        cn.Open();
        //        SqlTransaction tr = cn.BeginTransaction();
        //        SqlCommand cmd = new SqlCommand();
        //        try
        //        {
        //            string sql = "";
        //            for (int i = 0; i < arr.Count; i = i + 4)
        //            {
        //                sql = "update cms_columnist set is_published=" + (arr[i + 1].ToString() == "True" ? "1" : "0") + ",[columnist_order]=" + arr[i + 2].ToString() + " where editor_id=" + arr[i].ToString();
        //                cmd = new SqlCommand(sql, cn, tr);
        //                cmd.CommandType = CommandType.Text;
        //                cmd.ExecuteNonQuery();

        //            }

        //            tr.Commit();

        //        }
        //        catch (Exception e)
        //        {
        //            tr.Rollback();
        //            cn.Close();
        //        }
        //    }

        //}
    }
}