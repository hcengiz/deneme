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
    public class Type:DataAccess
    {
        #region Properties

        private int _content_typeid = 0;
        public int ContentTypeID
        {
            get { return _content_typeid; }
            set { _content_typeid = value; }
        }

        private string _typename = "";
        public string Typename
        {
            get { return _typename; }
            set { _typename = value; }
        }

        private int _siteid = 0;
        public int SiteID
        {
            get { return _siteid; }
            set { _siteid = value; }
        }

        #endregion

        #region Constructors

        public Type()
        {
            
        }

        public Type(int Id)
        {
            //this = Id;
            //Read();
        }
        #endregion

        public DataSet GetAllTypes(int Site_id)
        {

            using (DbConnection cn = Connect())
            {
                string sql = string.Format("SELECT content_type_id,type_name,site_id FROM cms_content_type where is_deleted = 0 and site_id={0} order by type_name asc", Site_id);  
                cn.Open();
                return ExecuteReadSqlDataSet(sql);        
            }
        }


    }
}
