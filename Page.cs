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
   public class Page : DataAccess
    {
       //Kategoriye ait link yönlendirilmesinin yapılması,yeni kategori oluşturulması sayfasında kullanıldı.Mehtap Acar.02.05.2011
        public DataSet GetPageParemeterCategoryLink(string categoryname)
        {
            using (DbConnection cn = Connect())
            {
                DbCommand cmd = db.GetStoredProcCommand("sp_GetCategoryParameterPage");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@CategoryName", categoryname));
                cn.Open();
                return ExecuteSqlCommandToDataSet(cmd);
            }
        }
    }
}
