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
    public class ImageColumnist:DataAccess
    {

        #region Properties

        private int image_columnist_id = 0;
        public int Image_Columnist_Id
        {
            get { return image_columnist_id; }
            set { image_columnist_id = value; }
        }

        private int columnist_id = 0;
        public int Columnist_id
        {
            get { return columnist_id; }
            set { columnist_id = value; }
        }

        private int image_id = 0;
        public int Image_id
        {
            get { return image_id; }
            set { image_id = value; }
        }

        #endregion
               
        #region Constructors
        public ImageColumnist()
        {

        }
        #endregion

        #region Insert
        public int Insert()
        {
            int deger = -1;
            using (DbConnection cn = Connect())
            {
                cn.Open();
                DbCommand cmd = db.GetStoredProcCommand("sp_InsertImageColumnist");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@columnist_id", this.Columnist_id));
                cmd.Parameters.Add(new SqlParameter("@image_id", this.Image_id));
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@image_columnist_id";
                p.Direction = ParameterDirection.Output;
                p.Size = int.MaxValue;
                cmd.Parameters.Add(p);
                deger = db.ExecuteNonQuery(cmd);
                cn.Close();
                Image_Columnist_Id = int.Parse(cmd.Parameters["@image_columnist_id"].Value.ToString());
                return Image_Columnist_Id;
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
                DbCommand cmd = db.GetStoredProcCommand("sp_UpdateColumnistImage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Image_id", this.Image_id));
                cmd.Parameters.Add(new SqlParameter("@columnist_id", this.Columnist_id));
                deger = db.ExecuteNonQuery(cmd);
                cn.Close();
            }

        }
        #endregion

       
    }


}
