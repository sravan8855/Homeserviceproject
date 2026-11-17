using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using HomeService.Web.Models;

namespace HomeService.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Dashboard()
        {
            if (Session["RoleId"] == null)
            {
                return RedirectToAction(
                    "Login",
                    "Accounts");
            }

            if (Convert.ToInt32(
                Session["RoleId"]) != 1)
            {
                return RedirectToAction(
                    "Index",
                    "Home");
            }

            return View();
        }

        public ActionResult Bookings()
        {
            if (Session["RoleId"] == null)
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

                obj.Address =
                    dr["Address"].ToString();

                obj.BookingDate =
                    Convert.ToDateTime(
                        dr["BookingDate"]);

                obj.BookingTime =
                    dr["BookingTime"].ToString();

                obj.BookingStatus =
                    dr["BookingStatus"].ToString();

                obj.ProviderId =
                    Convert.ToInt32(
                        dr["ProviderId"]);

                list.Add(obj);
            }

            con.Close();

            return View(list);
        }

        public ActionResult ApproveBooking(int id)
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
                "Bookings");
        }

        public ActionResult RejectBooking(int id)
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
                "Bookings");
        }
        public ActionResult Providers()
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
                "SELECT * FROM Providers";

            SqlCommand cmd =
                new SqlCommand(query, con);

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

                obj.MobileNumber =
                    dr["MobileNumber"].ToString();

                obj.Email =
                    dr["Email"].ToString();

                obj.Location =
                    dr["Location"].ToString();

                obj.IsApproved =
        dr["IsApproved"] != DBNull.Value
        ?
        Convert.ToBoolean(
            dr["IsApproved"])
        :
        false; 

                list.Add(obj);
            }

            con.Close();

            return View(list);
        }

        public ActionResult ApproveProvider(int id)
        {
            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "UPDATE Providers " +
                "SET IsApproved=1 " +
                "WHERE ProviderId=@ProviderId";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@ProviderId",
                id);

            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();

            return RedirectToAction(
                "Providers");
        }

        public ActionResult EditService(int id)
        {
            Service obj =
                new Service();

            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "SELECT * FROM Services " +
                "WHERE ServiceId=@ServiceId";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@ServiceId",
                id);

            con.Open();

            SqlDataReader dr =
                cmd.ExecuteReader();

            if (dr.Read())
            {
                obj.ServiceId =
                    Convert.ToInt32(
                        dr["ServiceId"]);

                obj.CategoryId =
                    Convert.ToInt32(
                        dr["CategoryId"]);

                obj.ServiceName =
                    dr["ServiceName"].ToString();

                obj.Description =
                    dr["Description"].ToString();

                obj.Price =
                    Convert.ToDecimal(
                        dr["Price"]);

                obj.IsActive =
                    Convert.ToBoolean(
                        dr["IsActive"]);
            }

            con.Close();

            return View(obj);
        }

        [HttpPost]
        public ActionResult EditService(Service model)
        {
            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "UPDATE Services " +
                "SET " +
                "CategoryId=@CategoryId, " +
                "ServiceName=@ServiceName, " +
                "Description=@Description, " +
                "Price=@Price, " +
                "IsActive=@IsActive " +
                "WHERE ServiceId=@ServiceId";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@ServiceId",
                model.ServiceId);

            cmd.Parameters.AddWithValue(
                "@CategoryId",
                model.CategoryId);

            cmd.Parameters.AddWithValue(
                "@ServiceName",
                model.ServiceName);

            cmd.Parameters.AddWithValue(
                "@Description",
                model.Description);

            cmd.Parameters.AddWithValue(
                "@Price",
                model.Price);

            cmd.Parameters.AddWithValue(
                "@IsActive",
                model.IsActive);

            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();

            return RedirectToAction(
                "Services");
        }
        public ActionResult Completed(int id)
        {
            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "UPDATE Bookings " +
                "SET BookingStatus='Completed' " +
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
                "Bookings");
        }

        public ActionResult Services()
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
                "SELECT * FROM Services";

            SqlCommand cmd =
                new SqlCommand(query, con);

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

                obj.CategoryId =
                    Convert.ToInt32(
                        dr["CategoryId"]);

                obj.ServiceName =
                    dr["ServiceName"].ToString();

                obj.Description =
                    dr["Description"].ToString();

                obj.Price =
                    Convert.ToDecimal(
                        dr["Price"]);

                obj.IsActive =
                    Convert.ToBoolean(
                        dr["IsActive"]);

                obj.CreatedDate =
                    Convert.ToDateTime(
                        dr["CreatedDate"]);

                list.Add(obj);
            }

            con.Close();

            return View(list);
        }

        public ActionResult AddService()
        {
            List<ServiceCategory> categories =
                new List<ServiceCategory>();

            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "SELECT * FROM ServiceCategories " +
                "WHERE IsActive=1";

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

                categories.Add(obj);
            }

            con.Close();

            ViewBag.Categories =
                categories;

            return View();
        }


        [HttpPost]
        public ActionResult AddService(Service model)
        {
            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "INSERT INTO Services " +
                "(CategoryId,ServiceName,Description,Price,IsActive,CreatedDate) " +
                "VALUES " +
                "(@CategoryId,@ServiceName,@Description,@Price,@IsActive,GETDATE())";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@CategoryId",
                model.CategoryId);

            cmd.Parameters.AddWithValue(
                "@ServiceName",
                model.ServiceName);

            cmd.Parameters.AddWithValue(
                "@Description",
                model.Description);

            cmd.Parameters.AddWithValue(
                "@Price",
                model.Price);

            cmd.Parameters.AddWithValue(
                "@IsActive",
                model.IsActive);

            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();

            return RedirectToAction(
                "Services");
        }


        public ActionResult DeleteService(int id)
        {
            string cs =
                ConfigurationManager
                .ConnectionStrings["MyConnection"]
                .ConnectionString;

            SqlConnection con =
                new SqlConnection(cs);

            string query =
                "UPDATE Services " +
                "SET IsActive=0 " +
                "WHERE ServiceId=@ServiceId";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@ServiceId",
                id);

            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();

            return RedirectToAction(
                "Services");
        }
    }


}