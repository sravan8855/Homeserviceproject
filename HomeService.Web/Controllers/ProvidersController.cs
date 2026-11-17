using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using HomeService.Web.Models;

namespace HomeService.Web.Controllers
{
    public class ProvidersController : Controller
    {
        public ActionResult List(int id)
        {
            List<Provider> list =
                new List<Provider>();

            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query = 
    "SELECT Providers.*, Reviews.Rating, Reviews.ReviewText " +
    "FROM Providers " +
    "LEFT JOIN Reviews " +
    "ON Providers.ProviderId = Reviews.ProviderId " +
    "WHERE Providers.ServiceId=@ServiceId";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@ServiceId",
                id);

            con.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();

            while (dr.Read())
            {
                Provider obj =
                    new Provider();

                obj.ProviderId =
                    Convert.ToInt32(
                        dr["ProviderId"]);

                obj.FullName =
                    dr["FullName"].ToString();

                obj.Experience =
                    dr["Experience"].ToString();

                obj.Location =
                    dr["Location"].ToString();

                obj.MobileNumber =
                    dr["MobileNumber"].ToString();

                if (dr["Rating"] != DBNull.Value)
                {
                    obj.Rating =
                        Convert.ToInt32(
                            dr["Rating"]);
                }

                if (dr["ReviewText"] != DBNull.Value)
                {
                    obj.ReviewText =
                        dr["ReviewText"].ToString();
                }

                list.Add(obj);
            }

            con.Close();

            return View(list);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Provider model)
        {
            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "INSERT INTO Providers " +
                "(FullName," +
                "MobileNumber," +
                "Email," +
                "Password," +
                "ServiceId," +
                "Experience," +
                "Location) " +

                "VALUES " +

                "(@FullName," +
                "@MobileNumber," +
                "@Email," +
                "@Password," +
                "@ServiceId," +
                "@Experience," +
                "@Location)";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@FullName",
                model.FullName);

            cmd.Parameters.AddWithValue(
                "@MobileNumber",
                model.MobileNumber);

            cmd.Parameters.AddWithValue(
                "@Email",
                model.Email);

            cmd.Parameters.AddWithValue(
                "@Password",
                model.Password);

            cmd.Parameters.AddWithValue(
                "@ServiceId",
                model.ServiceId);

            cmd.Parameters.AddWithValue(
                "@Experience",
                model.Experience);

            cmd.Parameters.AddWithValue(
                "@Location",
                model.Location);

            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();

            return RedirectToAction(
                "Login");
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Provider model)
        {
            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "SELECT * FROM Providers " +
                "WHERE Email=@Email " +
                "AND Password=@Password " +
                "AND IsApproved=1";

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
                Session["ProviderId"] =
                    dr["ProviderId"];

                Session["ProviderName"] =
                    dr["FullName"].ToString();

                return RedirectToAction(
                    "Dashboard");
            }

            ViewBag.Message =
                "Provider not approved or invalid login";

            return View();
        }


        public ActionResult Dashboard()
        {
            if (Session["ProviderId"] == null)
            {
                return RedirectToAction(
                    "Login");
            }

            return View();
        }

        public ActionResult MyBookings()
        {
            if (Session["ProviderId"] == null)
            {
                return RedirectToAction(
                    "Login");
            }

            List<Booking> list =
                new List<Booking>();

            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "SELECT * FROM Bookings " +
                "WHERE ProviderId=@ProviderId";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@ProviderId",
                Session["ProviderId"]);

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

                obj.Address =
                    dr["Address"].ToString();

                obj.BookingDate =
                    Convert.ToDateTime(
                        dr["BookingDate"]);

                obj.BookingTime =
                    dr["BookingTime"].ToString();

                obj.BookingStatus =
                    dr["BookingStatus"].ToString();

                list.Add(obj);
            }

            con.Close();

            return View(list);
        }

        public ActionResult Accept(int id)
        {
            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "UPDATE Bookings " +
                "SET BookingStatus='Accepted' " +
                "WHERE BookingId=@BookingId";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@BookingId",
                id);

            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();

            return RedirectToAction(
                "MyBookings");
        }

        public ActionResult Reject(int id)
        {
            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "UPDATE Bookings " +
                "SET BookingStatus='Rejected' " +
                "WHERE BookingId=@BookingId";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@BookingId",
                id);

            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();

            return RedirectToAction(
                "MyBookings");
        }

        public ActionResult Logout()
        {
            Session.Clear();

            Session.Abandon();

            return RedirectToAction(
                "Login");
        }


        public ActionResult MyReviews()
        {
            List<Review> reviews =
                new List<Review>();

            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "SELECT * FROM Reviews " +
                "WHERE ProviderId=@ProviderId";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@ProviderId",
                Convert.ToInt32(Session["UserId"]));

            con.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();

            while (dr.Read())
            {
                Review r =
                    new Review();

                r.Rating =
                    Convert.ToInt32(dr["Rating"]);

                r.ReviewText =
                    dr["ReviewText"].ToString();

                r.CreatedDate =
                    Convert.ToDateTime(dr["CreatedDate"]);

                reviews.Add(r);
            }

            con.Close();

            return View(reviews);
        }



        public ActionResult Earnings()
        {
            decimal totalBookingAmount = 0;

            decimal totalCommission = 0;

            decimal totalProviderAmount = 0;

            int totalJobs = 0;

            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "SELECT * FROM Bookings " +
                "WHERE ProviderId=@ProviderId " +
                "AND BookingStatus='Completed'";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@ProviderId",
                Session["ProviderId"]);

            con.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();

            while (dr.Read())
            {
                totalJobs++;

                totalBookingAmount +=
                    dr["ServicePrice"] != DBNull.Value
                    ?
                    Convert.ToDecimal(
                        dr["ServicePrice"])
                    :
                    0;

                totalCommission +=
                    dr["PlatformCommission"] != DBNull.Value
                    ?
                    Convert.ToDecimal(
                        dr["PlatformCommission"])
                    :
                    0;

                totalProviderAmount +=
                    dr["ProviderAmount"] != DBNull.Value
                    ?
                    Convert.ToDecimal(
                        dr["ProviderAmount"])
                    :
                    0;
            }

            con.Close();

            ViewBag.TotalJobs =
                totalJobs;

            ViewBag.TotalBookingAmount =
                totalBookingAmount;

            ViewBag.TotalCommission =
                totalCommission;

            ViewBag.TotalProviderAmount =
                totalProviderAmount;

            return View();
        }
    }
}