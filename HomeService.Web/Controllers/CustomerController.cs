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
    public class CustomerController : Controller
    {
        public ActionResult Dashboard()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction(
                    "Login",
                    "Accounts");
            }

            return View();
        }

        public ActionResult MyBookings()
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
                "SELECT * FROM Bookings "; //+
              //  "WHERE CustomerName=@CustomerName";

            SqlCommand cmd =
                new SqlCommand(query, con);

            //cmd.Parameters.AddWithValue(
            //    "@CustomerName",
            //    Session["FullName"]);

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

                obj.ProviderId =
                    Convert.ToInt32(
                        dr["ProviderId"]);

                obj.BookingStatus =
                    dr["BookingStatus"].ToString();

                list.Add(obj);
            }

            con.Close();

            return View(list);
        }

        public ActionResult AddReview(
    int bookingId,
    int providerId)
        {
            Review obj =
                new Review();

            obj.BookingId =
                bookingId;

            obj.ProviderId =
                providerId;

            return View(obj);
        }

        [HttpPost]
        public ActionResult AddReview(
    Review model)
        {
            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "INSERT INTO Reviews " +
                "(BookingId,ProviderId,CustomerId,Rating,ReviewText) " +
                "VALUES " +
                "(@BookingId,@ProviderId,@CustomerId,@Rating,@ReviewText)";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@BookingId",
                model.BookingId);

            cmd.Parameters.AddWithValue(
                "@ProviderId",
                model.ProviderId);

            cmd.Parameters.AddWithValue("@CustomerId", Session["UserId"]);

            cmd.Parameters.AddWithValue(
                "@Rating",
                model.Rating);

            cmd.Parameters.AddWithValue(
                "@ReviewText",
                model.ReviewText);

            con.Open();

            cmd.ExecuteNonQuery();

            // 2. UPDATE BOOKING AS REVIEWED
            SqlCommand updateCmd = new SqlCommand(
                "UPDATE Bookings SET IsReviewed = 1 WHERE BookingId=@BookingId", con);

            updateCmd.Parameters.AddWithValue("@BookingId", model.BookingId);

            updateCmd.ExecuteNonQuery();


            con.Close();

            return RedirectToAction(
                "MyBookings");
        }
    }
}