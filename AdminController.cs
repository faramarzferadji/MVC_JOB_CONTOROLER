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
    public class AdminController : Controller
    {
        string connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TornaduDB.sql;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        // GET: Admin
        public ActionResult Index()
        {
            DataTable AdminTable = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                string query = "Select * from AdminTable";
                SqlDataAdapter sqlData = new SqlDataAdapter(query, con);
                sqlData.Fill(AdminTable);
            }
            return View(AdminTable);
        }

        [HttpGet]
        public ActionResult Searching(string sstsearch)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                string query = "Select * from AdminTable  where UserName like '%" + sstsearch + "%'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sqlData = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                sqlData.Fill(dataSet);
                List<Admin> sm = new List<Admin>();
                foreach (DataRow item in dataSet.Tables[0].Rows)
                {
                    sm.Add(new Admin
                    {
                        Password = Convert.ToInt32(item["Password"]),
                        UserName = Convert.ToString(item["UserName"])
                       

                    });

                }
                ViewBag.Message = "Your application Searching page resulta.";
                con.Close();
                ModelState.Clear();
                return View(sm);

            }

        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            Admin model = new Admin();
            return View(model);
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(Admin admin)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                string query = "Insert into AdminTable  Values(@Password,@UserName)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Password", admin.Password);
                cmd.Parameters.AddWithValue("@UserName",admin.UserName);

                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            Admin admin = new Admin();
            DataTable AdminTable = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                ViewBag.Massage = "This page is for editing";
                con.Open();
                string query = "Select * From AdminTable where id=@Password ";
                SqlDataAdapter sqlData = new SqlDataAdapter(query, con);
                sqlData.SelectCommand.Parameters.AddWithValue("@Password", id);
                sqlData.Fill(AdminTable);
                if (AdminTable.Rows.Count == 1)
                {
                    admin.Password= Convert.ToInt32(AdminTable.Rows[0][1].ToString());
                    admin.UserName = AdminTable.Rows[0][2].ToString();
                   
                    return View(admin);
                }
                else
                {
                    return RedirectToAction("Index");

                }

            }

        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(Admin admin)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                string query = "UpDate AdminTable Set Password=@Password,UserName=@UserName where Password=@Password ";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Password", admin.Password);
                cmd.Parameters.AddWithValue("@UserName", admin.UserName);
               
                cmd.ExecuteNonQuery();

            }
            return RedirectToAction("Index");
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                string query = "Delete From AdminTable where id =@Password";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Password", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
