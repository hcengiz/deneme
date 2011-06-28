using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Security.Principal;
using System.Web;
//using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.Caching;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using System.Security.Principal;
using System.Data.SqlClient;
using System.Net;

namespace SadipDAL
{
  public  class ProjectObject
    {
        protected const int MAXROWS = int.MaxValue;

        //protected static Cache Cache
        //{
        //    get { return HttpContext.Current.Cache; }
        //}

        //protected static IPrincipal CurrentUser
        //{
        //    get { return HttpContext.Current.User; }
        //}

        protected static string CurrentUserName
        {
            get
            {
                string userName = "";
                if (HttpContext.Current.User != null)
                {
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                        userName = HttpContext.Current.User.Identity.Name;
                }
                return userName;
            }

        }

      

        //protected static string CurrentUserIP
        //{
        //    get {

        //        IPHostEntry IPHost = Dns.GetHostEntry(Dns.GetHostName());
        //        string externalIP = IPHost.AddressList[0].ToString();
        //        return externalIP; }
        //}

        protected static int GetPageIndex(int startRowIndex, int maximumRows)
        {
            if (maximumRows <= 0)
                return 0;
            else
                return (int)Math.Floor((double)startRowIndex / (double)maximumRows);
        }

        protected static string ConvertNullToEmptyString(string input)
        {
            return (input == null ? "" : input);
        }

        /// <summary>
        /// Remove from the ASP.NET cache all items whose key starts with the input prefix
        /// </summary>
        protected static void PurgeCacheItems(string prefix)
        {
            prefix = prefix.ToLower();
            List<string> itemsToRemove = new List<string>();

           /*   IDictionaryEnumerator enumerator = ProjectObject.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString().ToLower().StartsWith(prefix))
                    itemsToRemove.Add(enumerator.Key.ToString());
            }

            foreach (string itemToRemove in itemsToRemove)
                ProjectObject.Cache.Remove(itemToRemove);
        */}
    }
}
