using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Tornadu.Models;
using System.Configuration;

namespace Tornadu.Controllers
{
    public class UserController : Controller
    {
        string connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TornaduDB.sql;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User user)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                string query = "Select Password, UserName from AdminTable where Password = @Password and UserName=@UserName";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (user.UserName.Equals("Faramarz"))
                    {
                        Session["UPassword"] = user.Password.ToString();
                        Session["Username"] = user.UserName.ToString();
                        return RedirectToAction("Wellcome");
                    }
                    if (user.UserName.Equals("Faranak"))
                    {
                        Session["UPassword"] = user.Password.ToString();
                        Session["Username"] = user.UserName.ToString();
                        return RedirectToAction("Wellcomef");
                    }
                    if (user.UserName.Equals("Mali"))
                    {
                        Session["UPassword"] = user.Password.ToString();
                        Session["Username"] = user.UserName.ToString();
                        return RedirectToAction("WellcomeM");
                    }

                    else
                    {
                        ViewData["Massege1"] = "Page is under construction!! ";

                    }
                }
                else
                {
                    ViewData["Massege"] = "Please enter the valid data  ";
                }
            }
            return View();
        }
        public ActionResult Wellcome()
        {
            return View();
        }
        public ActionResult Wellcomef()
        {
            return View();
        }
        public ActionResult WellcomeM()
        {
            return View();
        }


    }
}
