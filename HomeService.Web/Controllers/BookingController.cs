using HomeService.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace HomeService.Web.Controllers
{
    public class BookingController : Controller
    {
        public ActionResult Create(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction(
                    "Login",
                    "Accounts");
            }

            Booking obj =
                new Booking();

            string cs =
        ConfigurationManager
        .ConnectionStrings["MyConnection"]
        .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "SELECT Providers.*, Services.Price " +
                "FROM Providers " +
                "INNER JOIN Services " +
                "ON Providers.ServiceId = Services.ServiceId " +
                "WHERE ProviderId=@ProviderId";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@ProviderId",
                id);

            con.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();

            if (dr.Read())
            {
                obj.ProviderId =
                    Convert.ToInt32(
                        dr["ProviderId"]);

                obj.ServicePrice =
                    Convert.ToDecimal(
                        dr["Price"]);
            }

            con.Close();

            return View(obj);
        }

        [HttpPost]
        public ActionResult Create(Booking model)
        {
            decimal commission =
                model.ServicePrice * 20 / 100;

            decimal providerAmount =
                model.ServicePrice - commission;

            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
            "INSERT INTO Bookings " +

            "(CustomerName," +
            "MobileNumber," +
            "Address," +
            "BookingDate," +
            "BookingTime," +
            "ProviderId," +
            "BookingStatus," +
            "UserId," +
            "ServicePrice," +
            "PlatformCommission," +
            "ProviderAmount," +
            "PaymentStatus) " +

            "VALUES " +

            "(@CustomerName," +
            "@MobileNumber," +
            "@Address," +
            "@BookingDate," +
            "@BookingTime," +
            "@ProviderId," +
            "@BookingStatus," +
            "@UserId," +
            "@ServicePrice," +
            "@PlatformCommission," +
            "@ProviderAmount," +
            "@PaymentStatus)";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@CustomerName",
                model.CustomerName);

            cmd.Parameters.AddWithValue(
                "@MobileNumber",
                model.MobileNumber);

            cmd.Parameters.AddWithValue(
                "@Address",
                model.Address);

            cmd.Parameters.AddWithValue(
                "@BookingDate",
                model.BookingDate);

            cmd.Parameters.AddWithValue(
                "@BookingTime",
                model.BookingTime);

            cmd.Parameters.AddWithValue(
                "@ProviderId",
                model.ProviderId);

            cmd.Parameters.AddWithValue(
                "@BookingStatus",
                "Pending");

            cmd.Parameters.AddWithValue(
                "@UserId",
                Session["UserId"]);

            cmd.Parameters.AddWithValue(
                "@ServicePrice",
                model.ServicePrice);

            cmd.Parameters.AddWithValue(
                "@PlatformCommission",
                commission);

            cmd.Parameters.AddWithValue(
                "@ProviderAmount",
                providerAmount);

            cmd.Parameters.AddWithValue(
                "@PaymentStatus",
                "Pending");

            con.Open();

            cmd.ExecuteNonQuery();

            string query2 =
                "SELECT MAX(BookingId) FROM Bookings";

            SqlCommand cmd2 =
                new SqlCommand(query2, con);

            int bookingId =
                Convert.ToInt32(
                    cmd2.ExecuteScalar());

            con.Close();

            return RedirectToAction(
                "PayNow",
                new { id = bookingId });

        }

        public ActionResult MyBookings()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction(
                    "Login",
                    "Accounts");
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
                "WHERE UserId=@UserId";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@UserId",
                Session["UserId"]);

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

        public ActionResult PayNow(int id)
        {
            Booking obj =
                new Booking();

            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "SELECT * FROM Bookings " +
                "WHERE BookingId=@BookingId";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@BookingId",
                id);

            con.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();

            if (dr.Read())
            {
                obj.BookingId =
                    Convert.ToInt32(
                        dr["BookingId"]);

                obj.CustomerName =
                    dr["CustomerName"].ToString();

                obj.ServicePrice =
                    Convert.ToDecimal(
                        dr["ServicePrice"]);
            }

            con.Close();

            return View(obj);
        }
        public ActionResult PaymentSuccess(int id)
        {
            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "UPDATE Bookings " +
                "SET PaymentStatus='Paid' " +
                "WHERE BookingId=@BookingId";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@BookingId",
                id);

            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();

            return Content(
                "Payment Successful");
        }

        public ActionResult PaymentFailed(int id)
        {
            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "UPDATE Bookings " +
                "SET PaymentStatus='Failed' " +
                "WHERE BookingId=@BookingId";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@BookingId",
                id);

            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();

            return Content(
                "Payment Failed");
        }
    }
}