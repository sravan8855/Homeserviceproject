using HomeService.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeService.Web.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
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


        public ActionResult ChooseRegister()
        {
            return View();
        }

    }

    /*public class HomeApiController : Controller
    {
        public JsonResult GetCategories6()
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

            return Json(
                list,
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Login(User model)
        {
            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "SELECT * FROM Users " +
                "WHERE Email=@Email " +
                "AND Password=@Password";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@Email",
                model.Email);

            cmd.Parameters.AddWithValue(
                "@Password",
                model.Password);

            con.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();

            if (dr.Read())
            {
                var data = new
                {
                    success = true,

                    userId =
                        dr["UserId"],

                    fullName =
                        dr["FullName"].ToString(),

                    roleId =
                        dr["RoleId"]
                };

                con.Close();

                return Json(data,
                    JsonRequestBehavior.AllowGet);
            }

            con.Close();

            var failed = new
            {
                success = false,
                message = "Invalid Login"
            };

            return Json(failed,
                JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetBookings()
        {
            List<Booking> list =
                new List<Booking>();

            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "SELECT * FROM Bookings";

            SqlCommand cmd =
                new SqlCommand(query, con);

            con.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();

            while (dr.Read())
            {
                Booking obj =
                    new Booking();

                obj.BookingId =
                    Convert.ToInt32(
                        dr["BookingId"]);

                obj.CustomerName =
                    dr["CustomerName"].ToString();

                obj.MobileNumber =
                    dr["MobileNumber"].ToString();

                obj.BookingStatus =
                    dr["BookingStatus"].ToString();

                obj.BookingDate =
                    Convert.ToDateTime(
                        dr["BookingDate"]);

                list.Add(obj);
            }

            con.Close();

            return Json(
                list,
                JsonRequestBehavior.AllowGet);
        }

*/
    // }
}