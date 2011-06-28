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
   public class Site:DataAccess
    {
       public DataSet GetAllSite()
       {
           using (DbConnection cn = Connect())
           {
               DbCommand cmd = db.GetStoredProcCommand("sp_GetAllSites");
               cmd.CommandType = CommandType.StoredProcedure;
               cn.Open();
               return ExecuteSqlCommandToDataSet(cmd);
           }
       }

       public string GetSiteBySiteID(int site_id)
       {

           using (DbConnection con = Connect())
           {
               string sql = string.Format("select site_id,site_name from cms_site where site_id={0}",site_id);
               con.Open();
               return ExecuteReadSqlDataSet(sql).Tables[0].Rows[0]["site_name"].ToString();
           }
       }

       public DataSet GetAllSiteBySiteID(int site_id)
       {
           using (DbConnection con = Connect())
           {
               string sql = string.Format("select * from cms_site where site_id={0}", site_id);
               con.Open();
               return ExecuteReadSqlDataSet(sql); 
           }
       }
    }
}
