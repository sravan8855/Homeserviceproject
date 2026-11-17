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
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User model)
        {
            
                string cs =
                    ConfigurationManager
                    .ConnectionStrings["MyConnection"]
                    .ConnectionString;

            model.RoleId = 2;

            SqlConnection con =
                    new SqlConnection(cs);

                string query =
                    "INSERT INTO Users " +
                    "(FullName,MobileNumber,Email,Password,RoleId) " +
                    "VALUES " +
                    "(@FullName,@Mobile,@Email,@Password,@RoleId)";

                SqlCommand cmd =
                    new SqlCommand(query, con);

                cmd.Parameters.AddWithValue(
                    "@FullName",
                    model.FullName);

                cmd.Parameters.AddWithValue(
                    "@Mobile",
                    model.MobileNumber);

                cmd.Parameters.AddWithValue(
                    "@Email",
                    model.Email);

                cmd.Parameters.AddWithValue(
                    "@Password",
                    model.Password);

                cmd.Parameters.AddWithValue(
                    "@RoleId",
                    model.RoleId);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();

            return RedirectToAction("Login");


        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model)
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

            cmd.Parameters.AddWithValue("@Email", model.Email);

            cmd.Parameters.AddWithValue("@Password", model.Password);

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Session["UserId"] =
                    dr["UserId"].ToString();

                Session["FullName"] =
                    dr["FullName"].ToString();

                Session["RoleId"] =
                    dr["RoleId"].ToString();


                int roleId = Convert.ToInt32(dr["RoleId"]);

                if (roleId == 1)
                {
                    return RedirectToAction(
                        "Dashboard",
                        "Admin");
                }
                else
                {
                    return RedirectToAction(
                        "Index",
                        "Home");
                }

               
            }

            ViewBag.Message =
                "Invalid Email or Password";

            con.Close();

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();

            Session.Abandon();

            return RedirectToAction(
                "Login");
        }
    }
}