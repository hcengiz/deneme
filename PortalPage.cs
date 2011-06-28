using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Data.Linq.Provider;
using System.Linq.Expressions;



using System.Reflection;
using System.Collections;




namespace SadipDAL
{


    public class PortalPage 
    {
        SadipDBDataContext db = new SadipDBDataContext();

        public  DataTable ObtainDataTableFromIEnumerable(IEnumerable ien)
        {
            DataTable dt = new DataTable();
            foreach (object obj in ien)
            {
                //Type t = obj.GetType();
                PropertyInfo[] pis = obj.GetType().GetProperties();
                if (dt.Columns.Count == 0)
                {
                    foreach (PropertyInfo pi in pis)
                    {
                        dt.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (PropertyInfo pi in pis)
                {
                    object value = pi.GetValue(obj, null);
                    dr[pi.Name] = value;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public DataTable GetPageById(int TemplateID)
        {

            IEnumerable<object> Pageq = from page in db.View_Pages.AsEnumerable()

                                    where page.template_id == TemplateID
                                    select new { page.page_url, page.page_name, page.componen_UserControlName, page.component_position, page.component_height, page.component_width,page.component_css };

            return ObtainDataTableFromIEnumerable(Pageq);

        }


        #region LinQ Testler

        public class Pages
        {
            //public int template_id { get; set; }
            public string page_name { get; set; }
        }

        public DataTable test()
        {

            /* List<Product> products = GetProductList();
              var expensiveInStockProducts =
              from p in products
              where p.UnitsInStock > 0 && p.UnitPrice > 20.00M
              select p ;
              ArrayList list = new ArrayList();
              foreach (var i in expensiveInStockProducts)
              list.Add(i);
              */


            var a = from page in db.cms_pages.AsEnumerable()
                    select new { };

            var a1 = from page in db.cms_pages.AsQueryable()
                     select new {page.page_id };
            

            return (DataTable)a;

        }


        /* public DataSet GetDataSet()
         {
             IQueryable q = GetAllQuery();       
             return q.ToDataSet( db.GetCommand(GetAllQuery()) );
         }

         private IQueryable<Pages> GetAllQuery()
         {
             IQueryable<Pages> q = from page in db.cms_pages
                                     select page;
             return q;
         }*/

        public void ConvertToIEnumerable()
        {

            var prodQuery =
                from page in db.cms_pages.AsEnumerable()

                select page;
            string w = prodQuery.ToString();

            IQueryable<object> q = from page in db.cms_pages.AsQueryable()
                                   select page;
            List<object> l = new List<object>(q);
        }


        public List<object> ConvertToIEnumerableList()
        {

            IQueryable<object> q = from page in db.cms_pages.AsQueryable()
                                   select page;
            List<object> l = new List<object>(q);
            return l;

        }


        public IQueryable<object> ConvertToIEnumerableQueryable()
        {

            IQueryable<object> q = from page in db.cms_pages.AsQueryable()
                                   select new
                                   {


                                   };
            return q;

        }

        public IEnumerable<object> GetPageByIad(int TemplateID)
        {

            IEnumerable<object> q = from page in db.View_Pages.AsEnumerable()
                                    //join ci in db.cms_component_instances.AsQueryable() on page.page_id equals ci.page_id
                                    where page.template_id == TemplateID
                                    select new { page.page_url, page.page_name, page.componen_UserControlName, page.component_position }; //, page.component_height, page.component_width


            //foreach (DataRow dr in q)
            //{
            //  string a=   dr["page_name"].ToString();
            //}

            return q;

        }


        public List<object> GetListPageById(int TemplateID)
        {

            IQueryable<object> q = from page in db.View_Pages.AsQueryable()
                                   //join ci in db.cms_component_instances.AsQueryable() on page.page_id equals ci.page_id
                                   where page.template_id == TemplateID
                                   select new { page.page_url, page.page_name, page.componen_UserControlName, page.component_position, page.component_height, page.component_width };
            List<object> l = new List<object>(q);

            l.GetType().GetProperties();

            return l;

        }

        #endregion
      

    }


}
