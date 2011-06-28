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
//using Microsoft.ApplicationBlocks.ExceptionManagement;
using System.Data.Common;



namespace SadipDAL
{
    public class Category : DataAccess
    {
      
        #region Properties

        private int category_id = 0;
        public int CategoryId
        {
            get { return category_id; }
            set { category_id = value; }
        }

        private int category_order = 0;
        public int CategoryOrder
        {
            get { return category_order; }
            set { category_order = value; }
        }

        private int parent_id = 0;
        public int ParentId
        {
            get { return parent_id; }
            set { parent_id = value; }
        }

        private string category_name = "";
        public string CategoryName
        {
            get { return category_name; }
            set { category_name = value; }
        }

        private string category_url = "";
        public string CategoryUrl
        {
            get { return category_url; }
            set { category_url = value; }
        }

        private string category_metaDescription = "";
        public string CategoryMetaDescription
        {
            get { return category_metaDescription; }
            set { category_metaDescription = value; }
        }

        private string category_metaKeyword = "";
        public string CategoryMetaKeyword
        {
            get { return category_metaKeyword; }
            set { category_metaKeyword = value; }
        }


        private bool is_published = false;
        public bool IsPublished
        {
            get { return is_deleted; }
            set { is_deleted = value; }
        }

        private bool is_category_Redirect = false;
        public bool IsCategoryRedirect
        {
            get { return is_category_Redirect; }
            set { is_category_Redirect = value; }
        }

        private bool is_deleted = false;
        public bool IsDeleted
        {
            get { return is_deleted; }
            set { is_deleted = value; }
        }

        private string _url = "";
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }    
        #endregion

        #region Constructors

        public Category()
        {

        }

        public Category(int Id)
        {
            this.CategoryId = Id;
            Read();
        }

        #endregion

        #region Read
        private void Read()
        {

            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("shbr_GetCategoryByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CategoryId",this.CategoryId));
                cn.Open();


                IDataReader dr = db.ExecuteReader(cmd);
                if (dr.Read())
                {
                    this.CategoryName = dr["category_name"].ToString().TrimEnd();
                    this.IsPublished = bool.Parse((dr["is_published"].ToString()));
                    this.CategoryOrder = int.Parse(dr["order"].ToString());
                    this.ParentId = int.Parse(dr["parent_id"].ToString() == "" ? "0" : dr["parent_id"].ToString());
                    this.CategoryMetaDescription= dr["category_metaDescription"].ToString().TrimEnd();
                    this.CategoryMetaKeyword = dr["category_metaKeyword"].ToString().TrimEnd();
                    this.IsDeleted = bool.Parse(dr["is_deleted"].ToString());
                    this.Url = dr["url"].ToString();
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
                cn.Open();
                DbCommand cmd = db.GetStoredProcCommand("shbr_InsertCategory");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name", this.CategoryName));
                cmd.Parameters.Add(new SqlParameter("@Published", this.IsPublished));
                cmd.Parameters.Add(new SqlParameter("@Order", this.CategoryOrder));
                cmd.Parameters.Add(new SqlParameter("@InsertUser", this.InsertUser));
                cmd.Parameters.Add(new SqlParameter("@InsertDate", this.InsertDate));
                cmd.Parameters.Add(new SqlParameter("@ParentId", this.ParentId));
                cmd.Parameters.Add(new SqlParameter("@CategoryMetaKeyword", this.CategoryMetaKeyword));
                cmd.Parameters.Add(new SqlParameter("@CategoryMetaDescription", this.CategoryMetaDescription));
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@CategoryId";
                p.Direction = ParameterDirection.Output;
                p.Size = int.MaxValue;
                cmd.Parameters.Add(p);
                deger = db.ExecuteNonQuery(cmd);
                cn.Close();
                CategoryId= int.Parse(cmd.Parameters["@CategoryId"].Value.ToString());
                return CategoryId;


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
                DbCommand cmd = db.GetStoredProcCommand("shbr_UpdateCategory");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name",this.CategoryName));
                cmd.Parameters.Add(new SqlParameter("@Published",this.IsPublished));
                cmd.Parameters.Add(new SqlParameter("@Order",this.CategoryOrder));
                cmd.Parameters.Add(new SqlParameter("@ParentId",this.ParentId));
                cmd.Parameters.Add(new SqlParameter("@Url", this.Url));
                cmd.Parameters.Add(new SqlParameter("@CategoryMetaDescription", this.CategoryMetaDescription));
                cmd.Parameters.Add(new SqlParameter("@CategoryMetaKeyword", this.CategoryMetaKeyword));
                cmd.Parameters.Add(new SqlParameter("@UpdateUser",this.UpdateUser));
                cmd.Parameters.Add(new SqlParameter("@UpdateDate", this.UpdateDate));
                cmd.Parameters.Add(new SqlParameter("@CategoryId",this.category_id));                
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
                DbCommand cmd = db.GetStoredProcCommand("shbr_DeleteCategory");
                cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.Add(new SqlParameter("@CategoryID", this.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@DeleteUser",  this.DeleteUser));
                cmd.Parameters.Add(new SqlParameter("@DeleteDate",  this.DeleteDate));
                db.ExecuteNonQuery(cmd);
                cn.Close();
            }

        }


        #endregion


        public  DataSet GetAllCategories()
        {
            
            using (DbConnection cn = Connect())
            {

                DbCommand cmd = db.GetStoredProcCommand("shbr_GetAllCategories");
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                return ExecuteSqlCommandToDataSet(cmd);
               
            }
        }

        //İçerik türüne göre içerik kategorisi getirtilir.
        public DataSet ContentCategories(int contenttype)
        {

            using (DbConnection con = Connect())
            {
                string sql = string.Format("SELECT dbo.cms_categories.category_name, dbo.cms_categories.category_id "+
                                            "FROM dbo.cms_content_type INNER JOIN dbo.cms_contentType_categories ON "+
                                            "dbo.cms_content_type.content_type_id = dbo.cms_contentType_categories.content_type_id INNER JOIN "+
                                            "dbo.cms_categories ON dbo.cms_contentType_categories.category_id = dbo.cms_categories.category_id "+
                                            "WHERE (dbo.cms_contentType_categories.content_type_id = {0})",contenttype);
                con.Open();
                return ExecuteReadSqlDataSet(sql);

            }
        }

       //tüm üst kategoriler getirtilir.(parent_id si null olan,alt kategori olmayan kategoriler)
        public DataSet GetCategoriesByParentIdNull(int site_id)
        {
            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("shbr_GetCategoriesByParentIdNull");
                cmd.Parameters.Add(new SqlParameter("@site_id", site_id));
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                return ExecuteSqlCommandToDataSet(cmd);
            }
        }

        public  DataSet GetCategoriesByParentId(int parentId)
        {

           
            using (DbConnection cn = Connect())
            {
                DbCommand cmd =db.GetStoredProcCommand("shbr_GetCategoriesByParentId");
                cmd.Parameters.Add(new SqlParameter("@ParentId",parentId));
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                return ExecuteSqlCommandToDataSet(cmd);
            }
        }



        public  DataSet GetPublishedCategories()
        {
            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("shbr_GetPublishedCategories");
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                return ExecuteSqlCommandToDataSet(cmd);
            }
        }



        public  DataSet GetSubCategoriesByCategoryId(int id)
        {

            using (DbConnection con =Connect())
            {
                string sql = string.Format(@"SELECT category_id,category_name,url
                                             FROM  cms_categories  WHERE parent_id= " + id + " order by category_id");
                      
                con.Open();
                return ExecuteReadSqlDataSet(sql);        

            }
        }


        public  string GetSubCategoriesNameById(int parent_id)
        {
             using (DbConnection con =Connect())
            
                 con.Open();
             string data = string.Format("SELECT category_name FROM  cms_categories  WHERE parent_id= " + parent_id);

                DbCommand dbCommand = db.GetSqlStringCommand(data);
                dbCommand.Parameters.Add(new SqlParameter("@ParentId", parent_id));          
               

                if (ExecuteScalar(dbCommand) == null)
                {
                    return null;
                }
                else
                {
                    return ExecuteScalar(dbCommand).ToString();

                }
            }
     }
 }