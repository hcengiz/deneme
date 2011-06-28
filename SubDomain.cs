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
   public class SubDomain:DataAccess
    {
       //siteye ait subdomainler getirtilir.
       public DataSet GetAllSubDomain(int site_id)
        {
            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("shbr_GetAllSiteSubdomain");
                cmd.Parameters.Add(new SqlParameter("@site_id", site_id));
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                return ExecuteSqlCommandToDataSet(cmd);
            }
        }
      }
}