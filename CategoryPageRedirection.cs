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
    public class CategoryPageRedirection:DataAccess
    {
        #region Properties
        private int page_redirect_id = 0;
        public int PageRedirectID
        {
            get { return page_redirect_id; }
            set { page_redirect_id = value; }
        }

        private int page_id = 0;
        public int PageID
        {
            get { return page_id; }
            set { page_id = value; }
        }

        private int category_id = 0;
        public int CatID
        {
            get { return category_id; }
            set { category_id = value; }
        }

        private int redirection_subdomain_id = 0;
        public int SubdomainRedirectionID
        {
            get { return redirection_subdomain_id; }
            set { redirection_subdomain_id = value; }
        }

        private string redirectionadress = "";
        public string RedirectionAdress
        {
            get { return redirectionadress; }
            set { redirectionadress = value; }
        }

        private string pageurl = "";
        public string PageUrl
        {
            get { return pageurl; }
            set { pageurl = value; }
        }

        #endregion
               
        #region Constructors
        public CategoryPageRedirection()
        {

        }
        #endregion

        #region InsertWithoutSubdomainID
        public int InsertWithoutSubdomainID()
        {
            int deger = -1;
            using (DbConnection cn = Connect())
            {
                cn.Open();
                DbCommand cmd = db.GetStoredProcCommand("sp_InsertCategoryPageRedirectionWithout");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@page_id", this.PageID));
                cmd.Parameters.Add(new SqlParameter("@page_url", this.PageUrl));
                cmd.Parameters.Add(new SqlParameter("@category_id", this.CatID));
                cmd.Parameters.Add(new SqlParameter("@redirection_adress", this.RedirectionAdress));
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@page_redirect_id";
                p.Direction = ParameterDirection.Output;
                p.Size = int.MaxValue;
                cmd.Parameters.Add(p);
                deger = db.ExecuteNonQuery(cmd);
                cn.Close();
                PageRedirectID = int.Parse(cmd.Parameters["@page_redirect_id"].Value.ToString());
                return PageRedirectID;
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
                DbCommand cmd = db.GetStoredProcCommand("sp_InsertCategoryPageRedirection");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@page_id", this.PageID));
                cmd.Parameters.Add(new SqlParameter("@page_url", this.PageUrl));
                cmd.Parameters.Add(new SqlParameter("@category_id", this.CatID));
                cmd.Parameters.Add(new SqlParameter("@redirection_subdomain_id", this.SubdomainRedirectionID));
                cmd.Parameters.Add(new SqlParameter("@redirection_adress", this.RedirectionAdress));
                SqlParameter p = new SqlParameter();
                p.ParameterName = "@page_redirect_id";
                p.Direction = ParameterDirection.Output;
                p.Size = int.MaxValue;
                cmd.Parameters.Add(p);
                deger = db.ExecuteNonQuery(cmd);
                cn.Close();
                PageRedirectID = int.Parse(cmd.Parameters["@page_redirect_id"].Value.ToString());
                return PageRedirectID;
            }

        }
        #endregion

        public  DataSet GetPageLinkRedirection(int catid)
        {
            using (DbConnection con =Connect())
            {
                string sql = string.Format("select page_redirect_id,page_url,redirection_address from cms_categoryPage_Redirection where category_id={0}",catid);
                con.Open();
                return ExecuteReadSqlDataSet(sql);        

            }
        }
       

    }
}
