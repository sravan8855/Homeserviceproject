using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using HomeService.Web.Models;
using System.Web.Mvc;

namespace HomeService.Web.Controllers
{
    public class ServicesController : Controller
    {
        // GET: Services
        public ActionResult Categories()
        {
            List<ServiceCategory> list =
                new List<ServiceCategory>();

            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "SELECT * FROM ServiceCategories";

            SqlCommand cmd =
                new SqlCommand(query, con);

            con.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();

            while (dr.Read())
            {
                ServiceCategory obj =
                    new ServiceCategory();

                obj.CategoryId =
                    Convert.ToInt32(
                        dr["CategoryId"]);

                obj.CategoryName =
                    dr["CategoryName"].ToString();

                list.Add(obj);
            }

            con.Close();

            return View(list);
        }

        public ActionResult List(int id)
        {
            List<Service> list =
                new List<Service>();

            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "SELECT * FROM Services WHERE CategoryId=@CategoryId";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@CategoryId",
                id);

            con.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();

            while (dr.Read())
            {
                Service obj =
                    new Service();

                obj.ServiceId =
                    Convert.ToInt32(
                        dr["ServiceId"]);

                obj.ServiceName =
                    dr["ServiceName"].ToString();

                obj.Description =
                    dr["Description"].ToString();

                obj.Price =
                    Convert.ToDecimal(
                        dr["Price"]);

                list.Add(obj);
            }

            con.Close();

            return View(list);
        }
    }
}