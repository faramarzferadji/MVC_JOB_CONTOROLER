using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Tornadu.Models;

namespace Tornadu.Controllers
{
    public class JobMController : Controller
    {
        string connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TornaduDB.sql;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        // GET: JobM
        //MalTable db = new MalTable();

        public ActionResult Index()
        {
            DataTable MalTable = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                string query = "Select * from MalTable order by Date desc";
                SqlDataAdapter sqlData = new SqlDataAdapter(query, con);
                sqlData.Fill(MalTable);
            }
            return View(MalTable);
            
        }

        

        // GET: JobM/Create
        public ActionResult Create()
        {
            var lista = new List<string>() { "Linkedin", "Indeed", "Glassdoor", "Jobboom", "Talent", "Jobillico", "Skrill", "Emploia Quebec", "Emploia Canada", "Other" };
            ViewBag.lista = lista;
            var listb = new List<string>() { "Sante Public", "Assistant Recheche", "Control de Qualite","Saise de Donne","Archive Medical" ,"Other" };
            ViewBag.listb = listb;
            return View(new Job());
            
           
        }

        // POST: JobM/Create
        [HttpPost]
        public ActionResult Create(Job jo)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                string query = "Insert into MalTable values(@JobNum,@JobNum,@Name,@Jsearch,@Position,@DOA,@Status)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@JobNum", jo.JobNum);
                cmd.Parameters.AddWithValue("@Name", jo.Name);
                cmd.Parameters.AddWithValue("@Jsearch", jo.Jsearch);
                cmd.Parameters.AddWithValue("@Position", jo.Position);
                cmd.Parameters.AddWithValue("@DOA", jo.DOA);
                cmd.Parameters.AddWithValue("@Status", jo.Status);
                cmd.ExecuteNonQuery();

            }
            return RedirectToAction("Index");
        }

        // GET: JobM/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: JobM/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: JobM/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                string query = "Delete From MalTable where id=@JobNum";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@JobNum", id);
                cmd.ExecuteNonQuery();
            }
                return RedirectToAction("Index");
        }

        
    }
}
