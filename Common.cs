using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Net;
using System.Web;

namespace SadipDAL  
{
    public class MyDatabase : ProjectObject
    {

       private string strConnection = "";
        public Database db;
        private DbConnection connection;

        public MyDatabase()
        {

            /* db = DatabaseFactory.CreateDatabase(ChooseConneciton());*/
            strConnection = "ClusterConnection"; //SamanyoluConnectionStringServer //ClusterConnection
            db = DatabaseFactory.CreateDatabase(strConnection);
        }

      

        public MyDatabase(string strApplication)
        {

            /*db = DatabaseFactory.CreateDatabase(ChooseConneciton(strApplication));*/
            strConnection = "SQLWriteConnection"; // KureConnection
            db = DatabaseFactory.CreateDatabase(strConnection);
        }

        public DbConnection Connect()
        {
            connection = db.CreateConnection();
            return connection;
        }

        public string ChooseConneciton(string strApplication)
        {
            string strDomain = "";
            strConnection = "";

            if (HttpContext.Current.Request.Url != null)
            {
                strDomain = HttpContext.Current.Request.Url.Host.ToString();
            }

            if (strDomain == "localhost")
            {
                if (strApplication == "LookUp")
                {
                    strConnection = "SQLWriteConnection";
                }

            }
            else if (strDomain == "test.kure.tv")
            {
                if (strApplication == "LookUp")
                {
                    strConnection = "SQLWriteConnection";
                }
                else
                {
                    strConnection = "ClusterConnection";
                }
            }
            else if (strDomain == "www.kure.tv")
            {
                if (strApplication == "LookUp")
                {
                    strConnection = "SQLWriteConnection";
                }
                else
                {
                    strConnection = "ClusterConnection";
                }
            }
            else
            {
                strDomain = "ClusterConnection";
            }


            return strConnection;
        }


        public string ChooseConneciton()
        {
            string strDomain = "";


            if (HttpContext.Current.Request.Url != null)
            {
                strDomain = HttpContext.Current.Request.Url.Host.ToString();
            }

            if (strDomain == "localhost")
            {
                strConnection = "SamanyoluConnectionStringServer"; //SamanyoluConnectionStringServer
            }
            else if (strDomain == "test.kure.tv")
            {
                strConnection = "SamanyoluConnectionStringServer";
            }
            else if (strDomain == "m.kure.tv")
            {
                strConnection = "ClusterConnection";
            }
            else if (strDomain == "testm.kure.tv")
            {
                strConnection = "ClusterConnection";
            }
            else if (strDomain == "www.kure.tv")
            {
                strConnection = "ClusterConnection";
            }
            else if (strDomain == "k175.stv.local")
            {
                strConnection = "ClusterConnection";
            }
            else if (strDomain == "k177.stv.local")
            {
                strConnection = "ClusterConnection";
            }
            else if (strDomain == "kure-sqltest.stv.local")
            {
                strConnection = "SamanyoluConnectionStringServer";
            }
            else
            {
                strDomain = "ClusterConnection";
            }


            return strConnection;
        }


        //protected static string ConnectionString
        //{
        //    get
        //    {
        //        //return  WebConfigurationManager.ConnectionStrings["SamanyoluHaberConnectionString"].ConnectionString;
        //        return WebConfigurationManager.ConnectionStrings ;
        //    }
        //}

        //protected static string ConnectionString2
        //{
        //    get
        //    {
        //        return WebConfigurationManager.ConnectionStrings["SamanyoluHaberConnectionString2"].ConnectionString;
        //    }
        //}
        //protected static string ConnectionStringSecim
        //{
        //    get
        //    {
        //        return WebConfigurationManager.ConnectionStrings["SecimConnection"].ConnectionString;
        //    }
        //}

        //protected static string SporConnectionString
        //{
        //    get
        //    {
        //        return WebConfigurationManager.ConnectionStrings["SporConnectionString"].ConnectionString;
        //    }
        //}

        //protected static string ConnectionStringSearch
        //{
        //    get
        //    {
        //        return WebConfigurationManager.ConnectionStrings["SHaberSearchConnectionString"].ConnectionString;
        //    }
        //}
        protected static int ConvertBoolToInt(bool check)
        {
            if (check)
                return 1;
            else
                return 0;
        }
        protected static bool ConvertIntToBool(int check)
        {
            if (check == 0)
                return false;
            else
                return true;
        }


        protected string AppPath
        {
            get
            {
                return System.Web.HttpContext.Current.Request.PhysicalApplicationPath;
            }
        }
    }
}
