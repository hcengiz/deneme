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
  public   class Content :DataAccess
    {
        #region Properties

        private int _id = 0;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _sourceid = 0;
        public int Content_source_id
        {
            get { return _sourceid; }
            set { _sourceid = value; }
        }

        private string _title = "";
        public string ContentTitle
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _googletitle = "";//mansette font degeri atanmis manset basligi
        public string google_title
        {
            get { return _googletitle; }
            set { _googletitle = value; }
        }

        private string _abstract = "";
        public string Abstract
        {
            get { return _abstract; }
            set { _abstract = value; }
        }

        private string _body = "";
        public string Body
        {
            get
            {
                return _body;
            }
            set { _body = value; }
        }

        private bool _comment = false;
        public bool Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        private string _relatednews = "";
        public string RelatedNews
        {
            get { return _relatednews; }
            set { _relatednews = value; }
        }

        private bool _isheadline = false;
        public bool is_headline
        {
            get { return _isheadline; }
            set { _isheadline = value; }
        }

        private string _url = "";
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        private string _insertIp = "";
        public string InsertIp
        {
            get { return _insertIp; }
            set { _insertIp = value; }
        }

        private int _oldid = 0;
        public int OldId
        {
            get { return _oldid; }
            set { _oldid = value; }
        }

        private string _headtitle = "";
        public string HeadlineTitle
        {
            get { return _headtitle; }
            set { _headtitle = value; }
        }

        private string _addpath = "";
        public string AddPath
        {
            get { return _addpath; }
            set { _addpath = value; }
        }

        private int _point = 0;
        public int Point
        {
            get { return _point; }
            set { _point = value; }
        }

        private int _pointTime;
        public int PointTime
        {
            get { return _pointTime; }
            set { _pointTime = value; }
        }

        private string _contenttypeid = "";
        public string Type
        {
            get { return _contenttypeid; }
            set { _contenttypeid = value; }
        }

        private bool _ispublished = false;
        public bool ispublished
        {
            get { return _ispublished; }
            set { _ispublished = value; }
        }

        private int _galleryId = 0;
        public int GalleryId
        {
            get { return _galleryId; }
            set { _galleryId = value; }
        }

        private int _columnistid = 0;
        public int Columnistid
        {
            get { return _columnistid; }
            set { _columnistid = value; }
        }

        private int _category_id = 0;
        public int Category_id
        {
            get { return _category_id; }
            set { _category_id = value; }
        }
      
        #endregion
        #region Constructors

        public Content()
        {
            
        }

        public Content(int Id)
        {
            this.Id = Id;
            //Read();
        }

        public Content(int Id, SqlTransaction tr)
        {
            this.Id = Id;
            //Read(tr);
        }

      #endregion 

        //#region Read
        //public void Read()
        //{

        //    using (DbConnection cn = Connect())
        //    {
        //        DbCommand cmd = db.GetStoredProcCommand("shbr_GetNewsById");
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add(new SqlParameter("@NewsId", this.Id));
        //        cn.Open();
                
        //        IDataReader dr = cmd.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            this.Title = dr["title"].ToString();
        //            this.Title2 = dr["title2"].ToString();
        //            this.HTitle = dr["htitle"].ToString();
        //            this.Abstract = dr["abstract"].ToString();
        //            this.Body = dr["body"].ToString();
        //            this.Type =dr["type"].ToString();
        //            this.CategoryId = int.Parse(dr["category_id"].ToString());
        //            this.PhotoGalleryId = int.Parse(dr["photo_gallery_id"].ToString());
        //            this.IsDeleted = bool.Parse(dr["is_deleted"].ToString());
        //            this.InsertUser = dr["insert_user"].ToString();
        //            this.InsertDate= DateTime.Parse(dr["insert_date"].ToString());
        //            this.UpdateUser= dr["update_user"].ToString();
        //            this.UpdateDate= dr["update_date"] == DBNull.Value ? DateTime.Now : DateTime.Parse(dr["update_date"].ToString());
        //            this.Image2UpdateDate = dr["image2_update_date"] == DBNull.Value ? DateTime.Now.AddYears(-3): DateTime.Parse(dr["image2_update_date"].ToString());
        //            this.Image3UpdateDate = dr["image3_update_date"] == DBNull.Value ? DateTime.Now.AddYears(-3) : DateTime.Parse(dr["image3_update_date"].ToString());
        //            this.DeleteUser = dr["delete_user"].ToString();
        //            this.DeleteDate = dr["delete_date"] == DBNull.Value ? DateTime.Now : DateTime.Parse(dr["delete_date"].ToString());
        //            this.Hit = int.Parse(dr["hit"].ToString());
        //            this.InsertIp = dr["insert_ip"].ToString();
        //            this.ImageId = int.Parse(dr["image_id"].ToString());
        //            this.ImageId2 = int.Parse(dr["image_id2"].ToString());
        //            this.ImageId3 = int.Parse(dr["image_id3"].ToString());
        //            this.VideoImageId = int.Parse(dr["video_image_id"].ToString());
        //            this.RelatedNews = dr["related_news"].ToString();
        //            this.Url = dr["url"].ToString();
        //            this.Labels = dr["labels"].ToString();
        //            this.NewsSource = dr["news_source"] == "" ? "0" : dr["news_source"].ToString();
        //            this.Comment = bool.Parse(dr["comment"].ToString());
        //            this.MainPage = bool.Parse(dr["main_page"].ToString());
        //            this.AddPath = dr["add_path"].ToString() == "" ? "http://media.samanyoluhaber.com/media/Reklam/Reklam.flv" : dr["add_path"].ToString();
        //            this.SubCategoryId = dr["sub_category_id"].ToString() == "" ? 0 : int.Parse(dr["sub_category_id"].ToString());
        //            this.TeamIds = dr["team_ids"].ToString() == "" ? "" : dr["team_ids"].ToString();
        //            this.SporImageId = dr["spor_image_id"].ToString()== "" ?0 :int.Parse(dr["spor_image_id"].ToString());
        //            this.Point=dr["point"].ToString()== "" ?0 :int.Parse(dr["point"].ToString());
        //            this.PointTime = dr["point_time"].ToString() == "" ? 0 : int.Parse(dr["point_time"].ToString());

        //        }
        //        dr.Close();

        //    }
        
        //}

        //public void Read(SqlTransaction tr)
        //{

        //    //using (SqlConnection cn = OpenConn())
        //    //{
        //        SqlCommand cmd = new SqlCommand("shbr_GetNewsById", tr.Connection);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@NewsId", SqlDbType.Int).Value = this.Id;
        //        cmd.Transaction = tr;
        //       // cn.Open();

        //        IDataReader dr = cmd.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            this.Title = dr["title"].ToString();
        //            this.Title2 = dr["title2"].ToString();
        //            this.HTitle = dr["htitle"].ToString();
        //            this.Abstract = dr["abstract"].ToString();
        //            this.Body = dr["body"].ToString();
        //            this.Type = dr["type"].ToString();
        //            this.CategoryId = int.Parse(dr["category_id"].ToString());
        //            this.IsDeleted = bool.Parse(dr["is_deleted"].ToString());
        //            this.InsertUser = dr["insert_user"].ToString();
        //            this.InsertDate = DateTime.Parse(dr["insert_date"].ToString());
        //            this.UpdateUser = dr["update_user"].ToString();
        //            this.UpdateDate = dr["update_date"] == DBNull.Value ? DateTime.Now : DateTime.Parse(dr["update_date"].ToString());
        //            this.Image2UpdateDate = dr["image2_update_date"] == DBNull.Value ? DateTime.Now.AddYears(-3) : DateTime.Parse(dr["image2_update_date"].ToString());
        //            this.Image3UpdateDate = dr["image3_update_date"] == DBNull.Value ? DateTime.Now.AddYears(-3) : DateTime.Parse(dr["image3_update_date"].ToString());
        //            this.DeleteUser = dr["delete_user"].ToString();
        //            this.DeleteDate = dr["delete_date"] == DBNull.Value ? DateTime.Now : DateTime.Parse(dr["delete_date"].ToString());
        //            this.Hit = int.Parse(dr["hit"].ToString());
        //            this.InsertIp = dr["insert_ip"].ToString();
        //            this.ImageId = int.Parse(dr["image_id"].ToString());
        //            this.ImageId2 = int.Parse(dr["image_id2"].ToString());
        //            this.ImageId3 = int.Parse(dr["image_id3"].ToString());
        //            this.VideoImageId = int.Parse(dr["video_image_id"].ToString());
        //            this.RelatedNews = dr["related_news"].ToString();
        //            this.Comment = bool.Parse(dr["comment"].ToString());
        //            this.Url = dr["url"].ToString();
        //            this.Labels = dr["labels"].ToString();
        //            this.MainPage = bool.Parse(dr["main_page"].ToString());
        //            this.PhotoGalleryId = int.Parse(dr["photo_gallery_id"].ToString());
        //            this.NewsSource = dr["news_source"] == "" ? "0" : dr["news_source"].ToString();
        //            this.AddPath = dr["add_path"] == null ? "http://media.samanyoluhaber.com/media/Reklam/Reklam.flv" : dr["add_path"].ToString();
        //            this.SubCategoryId = dr["sub_category_id"].ToString() == "" ? 0 : int.Parse(dr["sub_category_id"].ToString());
        //            this.TeamIds = dr["team_ids"].ToString() == "" ? "" : dr["team_ids"].ToString();
        //            this.SporImageId = dr["spor_image_id"].ToString()== "" ?0 :int.Parse(dr["spor_image_id"].ToString());
        //            this.Point = dr["point"].ToString() == "" ? 0 : int.Parse(dr["point"].ToString());
        //            this.PointTime = dr["point_time"].ToString() == "" ? 0 : int.Parse(dr["point_time"].ToString());
        //        }
        //        dr.Close();

        //    //}

        //}
        //#endregion

        //#region Insert
        //public int Insert()
        //{
        //    using (DbConnection cn = Connect())
        //    {
        //        DbCommand cmd = db.GetSqlStringCommand("shbr_InsertNews");
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.Add(new SqlParameter("@Type", this.Type));
        //        cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
        //        cmd.Parameters.Add(new SqlParameter("@SubCategoryId", this.SubCategoryId));
        //        cmd.Parameters.Add(new SqlParameter("@Title", this.Title));
        //        cmd.Parameters.Add(new SqlParameter("@Title2", this.Title2));
        //        cmd.Parameters.Add(new SqlParameter("@HTitle", this.HTitle));
        //        cmd.Parameters.Add(new SqlParameter("@SubCategoryId", this.SubCategoryId));
        //        cmd.Parameters.Add(new SqlParameter("@Abstract", this.Abstract));
        //        cmd.Parameters.Add(new SqlParameter("@Body", this.Body));
        //        cmd.Parameters.Add(new SqlParameter("@ImageId", this.ImageId));
        //        cmd.Parameters.Add(new SqlParameter("@PhotoGalleryId", this.PhotoGalleryId));
        //        cmd.Parameters.Add(new SqlParameter("@ImageId2", this.ImageId2));
        //        cmd.Parameters.Add(new SqlParameter("@ImageId3", this.ImageId3));
        //        cmd.Parameters.Add(new SqlParameter("@VideoImageId", this.VideoImageId));
        //        cmd.Parameters.Add(new SqlParameter("@Hit", this.Hit));
        //        cmd.Parameters.Add(new SqlParameter("@Comment", this.Comment));
        //        cmd.Parameters.Add(new SqlParameter("@InsertIp", this.InsertIp));
        //        cmd.Parameters.Add(new SqlParameter("@MainPage", this.MainPage));
        //        cmd.Parameters.Add(new SqlParameter("@RelatedNews", this.RelatedNews));
        //        cmd.Parameters.Add(new SqlParameter("@Url", this.Url));
        //        cmd.Parameters.Add(new SqlParameter("@Labels", this.Labels));
        //        cmd.Parameters.Add(new SqlParameter("@InsertDate", this.InsertDate));
        //        cmd.Parameters.Add(new SqlParameter("@InsertUser", this.InsertUser));
        //        cmd.Parameters.Add(new SqlParameter("@AddPAth", this.AddPath));
        //        cmd.Parameters.Add(new SqlParameter("@TeamIds", this.TeamIds));
        //        cmd.Parameters.Add(new SqlParameter("@Image2UpdateDate", this.Image2UpdateDate == DateTime.MinValue ? DateTime.Now.AddYears(-5) : this.Image2UpdateDate));
        //        cmd.Parameters.Add(new SqlParameter("@Image3UpdateDate", this.Image3UpdateDate == DateTime.MinValue ? DateTime.Now.AddYears(-5) : this.Image3UpdateDate));
        //        cmd.Parameters.Add(new SqlParameter("@NewsSource", this.NewsSource));
        //        cmd.Parameters.Add(new SqlParameter("@SporNewsId", 0));
        //        cmd.Parameters.Add(new SqlParameter("@SporImageId", this.SporImageId));
        //        cmd.Parameters.Add(new SqlParameter("@Point", this.Point));
        //        cmd.Parameters.Add(new SqlParameter("@PointTime", this.PointTime));
        //        cmd.Parameters.Add(new SqlParameter("@NewsId", SqlDbType.BigInt).Direction = ParameterDirection.Output);

        //        cn.Open();
        //        int ret = cmd.ExecuteNonQuery();
        //        return (int)cmd.Parameters["@NewsId"].Value;
        //    }
        
        //}
        //#endregion

        //#region Update
        //public void Update()
        //{
        //    using (DbConnection cn = Connect())
        //    {
        //           DbCommand cmd = db.GetStoredProcCommand("shbr_UpdateNews");
        //           cmd.CommandType = CommandType.StoredProcedure;
        //           cmd.Parameters.Add(new SqlParameter("@Type", this.Type));
        //           cmd.Parameters.Add(new SqlParameter("@CategoryId", this.CategoryId));
        //           cmd.Parameters.Add(new SqlParameter("@SubCategoryId", this.SubCategoryId));
        //           cmd.Parameters.Add(new SqlParameter("@Title", this.Title));
        //           cmd.Parameters.Add(new SqlParameter("@Title2", this.Title2));
        //           cmd.Parameters.Add(new SqlParameter("@HTitle", this.HTitle));
        //           cmd.Parameters.Add(new SqlParameter("@Abstract", this.Abstract));
        //           cmd.Parameters.Add(new SqlParameter("@Body", this.Body));
        //           cmd.Parameters.Add(new SqlParameter("@ImageId", this.ImageId));
        //           cmd.Parameters.Add(new SqlParameter("@VideoImageId", this.VideoImageId));
        //           cmd.Parameters.Add(new SqlParameter("@PhotoGalleryId", this.PhotoGalleryId));
        //           cmd.Parameters.Add(new SqlParameter("@ImageId2", this.ImageId2));
        //           cmd.Parameters.Add(new SqlParameter("@ImageId3", this.ImageId3));
        //           cmd.Parameters.Add(new SqlParameter("@Hit", this.Hit));
        //           cmd.Parameters.Add(new SqlParameter("@Comment", this.Comment));
        //           cmd.Parameters.Add(new SqlParameter("@MainPage", this.MainPage));
        //           cmd.Parameters.Add(new SqlParameter("@RelatedNews", this.RelatedNews));
        //           cmd.Parameters.Add(new SqlParameter("@Url", this.Url));
        //           cmd.Parameters.Add(new SqlParameter("@Labels", this.Labels));
        //           cmd.Parameters.Add(new SqlParameter("@UpdateDate",  DateTime.Now));
        //           cmd.Parameters.Add(new SqlParameter("@UpdateUser", this.UpdateUser));
        //           cmd.Parameters.Add(new SqlParameter("@Image2UpdateDate", this.Image2UpdateDate));
        //           cmd.Parameters.Add(new SqlParameter("@Image3UpdateDate", this.Image3UpdateDate));
        //           cmd.Parameters.Add(new SqlParameter("@AddPAth", this.AddPath));
        //           cmd.Parameters.Add(new SqlParameter("@TeamIds", this.TeamIds));
        //           cmd.Parameters.Add(new SqlParameter("@NewsSource", this.NewsSource));
        //           cmd.Parameters.Add(new SqlParameter("@SporImageId", this.SporImageId));
        //           cmd.Parameters.Add(new SqlParameter("@Point", this.Point));
        //           cmd.Parameters.Add(new SqlParameter("@PointTime", this.PointTime));
        //           cmd.Parameters.Add(new SqlParameter("@NewsId", SqlDbType.BigInt).Direction = ParameterDirection.Output);

        //            cn.Open();
        //            cmd.ExecuteNonQuery();
        //        }
        //}
        //#endregion

        //#region Delete
        //public void Delete()
        //{
        //    using (DbConnection cn = Connect())
        //    {
        //        DbCommand cmd = db.GetStoredProcCommand("shbr_DeleteNews");
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add(new SqlParameter("@NewsId", this.Id));
        //        cmd.Parameters.Add(new SqlParameter("@DeleteUser", this.DeleteUser));
        //        cmd.Parameters.Add(new SqlParameter("@DeleteDate", this.DeleteDate));
        //        cn.Open();
        //        cmd.ExecuteNonQuery();
        //    }

        //}
        //#endregion

        public DataSet GetAllContent()
        {

            using (DbConnection cn = Connect())
            {

                DbCommand cmd = db.GetStoredProcCommand("shbr_GetContentById");
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                return ExecuteSqlCommandToDataSet(cmd);

            }
        }

      //İçerik türüne göre fieldlar getirilmesi için yazıldı.

      public DataSet ContentFields(int content_typeid)
      {
          using (DbConnection con = Connect())
          {
              string sql = string.Format("SELECT dbo.cms_content_fields.field_name FROM dbo.cms_content_fields INNER JOIN " +
                                         "dbo.cms_contentType_fields ON dbo.cms_content_fields.field_id = dbo.cms_contentType_fields.field_id INNER JOIN " +
                                         "dbo.cms_content_type ON dbo.cms_contentType_fields.content_type_id = dbo.cms_content_type.content_type_id " +
                                         " WHERE  (dbo.cms_contentType_fields.content_type_id = {0})", content_typeid);
              con.Open();
              return ExecuteReadSqlDataSet(sql);
          }
      }


    }
}
