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
    public class JobfController : Controller
    {
        string connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TornaduDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        // GET: Jobf
        public ActionResult Index()
        {
            DataTable FarTable = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                string query = "Select * from FarTable order by DOA desc ";
                SqlDataAdapter sqlData = new SqlDataAdapter(query, con);
                sqlData.Fill(FarTable);
            }
                return View(FarTable);
        }

        

        // GET: Jobf/Create
        public ActionResult Create()
        {
            var lista = new List<string>() { "Linkedin", "Indeed", "Glassdoor","Jobboom","Talent","Jobillico","Skrill", "Emploia Quebec","Emploia Canada","Other"};
            ViewBag.lista = lista;
            var listb = new List<string>() { "Web Developper", "Programmer", "Technition Informatic", "Other" };
            ViewBag.listb = listb;
            return View(new Job());

           
        }
        [HttpGet]
        public ActionResult Searching(string stsearch)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                string query = "Select * from FarTable where Name like '%" + stsearch + "%'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sqlData = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                sqlData.Fill(dataSet);
                List<Job> sm = new List<Job>();
                foreach (DataRow item in dataSet.Tables[0].Rows)
                {
                    sm.Add(new Job
                    {
                        JobNum = Convert.ToInt32(item["JobNum"]),
                        Name = Convert.ToString(item["Name"]),
                        Jsearch= Convert.ToString(item["Jsearch"]),
                        Position= Convert.ToString(item["Position"]),
                        DOA = Convert.ToDateTime(item["DOA"]),
                        Status = Convert.ToString(item["Status"]),

                    });
                    if ( (DateTime.Now.Day-5)> (Convert.ToDateTime(item["DOA"]).Day))
                    {
                        ViewBag.Message1 = "it need to follow";

                    }
                    else
                    {
                        ViewBag.Message2 = "you have the time";

                    }

                }
                
                ViewBag.Message = "Your application Searching page resulta.";
                con.Close();
                ModelState.Clear();
                return View(sm);

            }

        }
      

        // POST: Jobf/Create
        [HttpPost]
        public ActionResult Create(Job jo)
        {

            
            using(SqlConnection con =new SqlConnection(connectionstring))
            {
                con.Open();
                string query = "Insert into FarTable values(@JobNum,@Name,@Jsearch,@Position,@DOA,@Status)";
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

        // GET: Jobf/Edit/5
        public ActionResult Edit(int id)
        {
            var lista = new List<string>() { "Linkedin", "Indeed", "Glassdoor", "Jobboom", "Talent", "Jobillico", "Skrill", "Emploia Quebec", "Emploia Canada", "Other" };
            ViewBag.lista = lista;
            var listb = new List<string>() { "Web Developper", "Programmer", "Technition Informatic", "Other" };
            ViewBag.listb = listb;
            
            Job jo = new Job();
            DataTable FarTable = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                ViewBag.Massage = "This page is for editing";
                con.Open();
                string query = "Select * From FarTable where id=@JobNum ";
                SqlDataAdapter sqlData = new SqlDataAdapter(query, con);
                sqlData.SelectCommand.Parameters.AddWithValue("@JobNum", id);
                sqlData.Fill(FarTable);
                if (FarTable.Rows.Count == 1)
                {
                    jo.JobNum = Convert.ToInt32(FarTable.Rows[0][1].ToString());
                    jo.Name = FarTable.Rows[0][2].ToString();
                    jo.Jsearch = FarTable.Rows[0][3].ToString();
                    jo.Position = FarTable.Rows[0][4].ToString();
                    jo.DOA = Convert.ToDateTime(FarTable.Rows[0][5].ToString());
                    jo.Status = FarTable.Rows[0][6].ToString();
                    return View(jo);
                }
                else
                {
                    return RedirectToAction("Index");

                }

            }


                
        }

        // POST: Jobf/Edit/5
        [HttpPost]
        public ActionResult Edit(Job jo)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                string query = "UpDate FarTable Set JobNum=@JobNum, Name=@Name, Jsearch=@Jsearch, Position=@Position, DOA=@DOA,Status=@Status where JobNum=@JobNum ";
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

        // GET: Jobf/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                string query = "Delete From FarTable where id=@JobNum";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@JobNum", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

       
    }
}
