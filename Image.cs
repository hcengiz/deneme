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
    public class Image :DataAccess
    {
        #region Properties

        private int image_id = 0;
        public int ImageId
        {
            get { return image_id; }
            set { image_id = value; }
        }

        private string image_path = "";
        public string ImagePath
        {
            get { return image_path; }
            set { image_path = value; }
        }

        private string image_description;
        public string ImageDescription
        {
            get { return image_description; }
            set { image_description = value; }
        }
        private int image_width = 0;
        public int ImageWidth
        {
            get { return image_width; }
            set { image_width = value; }
        }

        private int iamge_height = 0;
        public int ImageHeight
        {
            get { return iamge_height; }
            set { iamge_height = value; }
        }

        private string image_url;
        public string ImageUrl
        {
            get { return image_url; }
            set { image_url = value; }
        }
        

        private string image_title;
        public string ImageTitle
        {
            get { return image_title; }


            set { image_title = value; }
        }


        private int image_hit = 0;
        public int ImageHit
        {
            get { return image_hit; }
            set { image_hit = value; }
        }

        private int image_type_id;
        public int ImageTypeId
        {
            get { return image_type_id; }


            set { image_type_id = value; }
        }

        private bool is_deleted=false;
        public bool IsDeleted
        {
            get { return is_deleted; }
            set { is_deleted = value; }
        }

      

        #endregion

        #region Constructors

        public Image()
        {

        }

        public Image(int Id)
        {
            this.ImageId = Id;
            Read();
        }

        #endregion

        #region Read
        private void Read()
        {
            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("shbr_GetImageByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ImageId", this.ImageId));
                cn.Open();

                IDataReader dr = db.ExecuteReader(cmd);
                if (dr.Read())
                {
                    this.ImagePath = dr["image_path"].ToString().TrimEnd();
                    this.ImageDescription = dr["image_description"].ToString().TrimEnd();
                    this.IsDeleted = bool.Parse(dr["is_deleted"].ToString());
                    this.ImageWidth = int.Parse(dr["image_width"].ToString());
                    this.ImageHeight = int.Parse(dr["image_height"].ToString());
                    this.ImageUrl = dr["image_url"].ToString();
                    this.ImageHit =int.Parse(dr["image_hit"].ToString());
                    this.ImageDescription = dr["image_description"].ToString();
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
        #endregion --http://localhost:9256/Shbr2010/App_Code/BLL/Image.cs

        #region Insert
        public int Insert()
        {
            int deger = -1;
            using (DbConnection cn = Connect())
            {
                cn.Open();
                DbCommand cmd = db.GetStoredProcCommand("sp_InsertImage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ImageTypeId", this.ImageTypeId));
                cmd.Parameters.Add(new SqlParameter("@Content", this.ImageDescription));
                cmd.Parameters.Add(new SqlParameter("@image_width", this.ImageWidth));
                cmd.Parameters.Add(new SqlParameter("@image_height", this.ImageHeight));
                cmd.Parameters.Add(new SqlParameter("@image_hit", this.image_hit));
                cmd.Parameters.Add(new SqlParameter("@ImageUrl", this.ImageUrl));
                cmd.Parameters.Add(new SqlParameter("@ImagePath", this.ImagePath));
                cmd.Parameters.Add(new SqlParameter("@InsertUser", this.InsertUser));
                cmd.Parameters.Add(new SqlParameter("@InsertDate", this.InsertDate));
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@ImageId";
                p.Direction = ParameterDirection.Output;
                p.Size = int.MaxValue;
                cmd.Parameters.Add(p);
                deger = db.ExecuteNonQuery(cmd);
                cn.Close();
                ImageId = int.Parse(cmd.Parameters["@ImageId"].Value.ToString());
                return ImageId;
            }

        }
        #endregion

        #region Update
        public void Update()
        {
            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("shbr_UpdateImage");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Path",this.ImagePath));
                cmd.Parameters.Add(new SqlParameter("@Content", this.ImageDescription));
                cmd.Parameters.Add(new SqlParameter("@UpdateUser", this.UpdateUser));
                cmd.Parameters.Add(new SqlParameter("@UpdateDate",  this.UpdateDate));

                cmd.Parameters.Add(new SqlParameter("@ImageID", SqlDbType.Int).Value = this.ImageId);

                cn.Open();
                int ret = cmd.ExecuteNonQuery();
            }

        }
        #endregion

        public void UpdateUrlandPath()
        {
            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("shbr_UpdateUrlandPath");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Path", this.ImagePath));
                cmd.Parameters.Add(new SqlParameter("@Url", this.ImageUrl));
                cmd.Parameters.Add(new SqlParameter("@UpdateUser",this.UpdateUser));
                cmd.Parameters.Add(new SqlParameter("@UpdateDate", this.UpdateDate));

                cmd.Parameters.Add(new SqlParameter("@ImageID", SqlDbType.Int).Value = this.ImageId);

                cn.Open();
                int ret = cmd.ExecuteNonQuery();
            }

        }

        #region Delete
        public void Delete()
        {
            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("shbr_DeleteImage");
                cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.Add(new SqlParameter("@ImageID", this.ImageId));
                 cmd.Parameters.Add(new SqlParameter("@DeleteUser", this.DeleteUser));
                cmd.Parameters.Add(new SqlParameter("@DeleteDate",  this.DeleteDate));
                cn.Open();
                cmd.ExecuteNonQuery();
            }

        }
        #endregion

        public object GetImages()
        {
            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("shbr_GetImages");
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                return  ExecuteSqlCommandToDataSet(cmd);
            }
        }


        public  object SearchImages(string searchText, int width, int height)
        {
            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("shbr_SearchImages");
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                cmd.Parameters.Add(new SqlParameter("@searchText",searchText));
                cmd.Parameters.Add(new SqlParameter("@Height", height));
                cmd.Parameters.Add(new SqlParameter("@Width", width));
                return ExecuteSqlCommandToDataSet(cmd);
            }
        }

        public  object GetImages(int type_id)
        {
            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("sp_GetImagesByTypeID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@type_id", type_id));
                cn.Open();
                return ExecuteSqlCommandToDataSet(cmd);
            }
        }

        public  int GetImagesCountForSiteMap()
        {
            using (DbConnection cn = Connect())
            {
                string sql = string.Format(@"select count(image_id)
                                    from cms_images  where is_deleted=0 ");
                DbCommand cmd = db.GetSqlStringCommand(sql);
                cn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public  DataSet GetImagesForSiteMap(int beginNo, int endNo)
        {
            string sql = string.Format(@"select * from ( select image_id,'' title,image_description abstract,'' body,image_path url
                  ,convert(varchar,insert_date,104) date ,
                  row_number() over (order by insert_date) rownum
                  from cms_images  
                  where is_deleted=0) t
                  where t.rownum between {0} and {1}", beginNo, endNo);
            return ExecuteReadSqlDataSet(sql);
        }

        public  DataSet TypeParentIDImages(int type_parent_id)
        {
            string sql = string.Format(@"Select * from cms_image_type where type_parent_id={0}", type_parent_id);
            return ExecuteReadSqlDataSet(sql);
        }

        //get image type according to type_name.
        public DataSet GetImagesTypeName(string TypeName,int site_id)
        {
            string sql = string.Format(@"select width,height,type_name,image_type_id from cms_image_type where type_name='{0}' and site_id={1}", TypeName,site_id);
            return ExecuteReadSqlDataSet(sql);
        }

        //SertacTanrıkulu tarafından editlenmiştir

        //public int InsertSporNewsImage()
        //{
        //    using (SqlConnection cn = OpenSporConn())
        //    {
        //        SqlCommand cmd = new SqlCommand("spr_InsertImage", cn);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.Add("@Path", SqlDbType.NText).Value = this.Path;
        //        cmd.Parameters.Add("@Content", SqlDbType.NText).Value = this.Content;
        //        cmd.Parameters.Add("@InsertUser", SqlDbType.NVarChar).Value = this.InsertUser;
        //        cmd.Parameters.Add("@InsertDate", SqlDbType.DateTime).Value = this.InsertDate;

        //        cmd.Parameters.Add("@ImageID", SqlDbType.Int).Direction = ParameterDirection.Output;
        //        cn.Open();
        //        int ret = cmd.ExecuteNonQuery();
        //        return (int)cmd.Parameters["@ImageID"].Value;
        //    }
        //}


    }
}