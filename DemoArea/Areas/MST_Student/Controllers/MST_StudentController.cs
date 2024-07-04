using System;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using DemoArea.Areas.MST_Student.Models;

namespace DemoArea.Areas.MST_Student.Controllers
{
	public class MST_StudentController : Controller
	{
        private readonly IConfiguration Configuration;
        public MST_StudentController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        [Area("MST_Student")]
        [Route("MST_Student/MST_Student/{Action}")]
        public IActionResult MST_StudentList()
        {
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SelectAll_Student";
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            conn.Close();
            return View(dt);
        }

        [Area("MST_Student")]
        [Route("MST_Student/MST_Student/{Action}")]
        public IActionResult MST_StudentAdd()
        {
            return View();
        }

        [Area("MST_Student")]
        [Route("MST_Student/MST_Student/{Action}")]
        public IActionResult MST_StudentAddFormPage(MST_StudentModel model)
        {
            DateTime dateTime = DateTime.Now;
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "Insert_Student";

            objcmd.Parameters.AddWithValue("StudentName", model.StudentName);
            objcmd.Parameters.AddWithValue("MobileNoStudent", model.MobileNoStudent);
            objcmd.Parameters.AddWithValue("Email", model.Email);
            objcmd.Parameters.AddWithValue("MobileNoFather", model.MobileNoFather);
            objcmd.Parameters.AddWithValue("Address", model.Address);
            objcmd.Parameters.AddWithValue("BirthDate", model.BirthDate);
            objcmd.Parameters.AddWithValue("Age", model.Age);
            objcmd.Parameters.AddWithValue("IsActive", model.IsActive);
            objcmd.Parameters.AddWithValue("Gender", model.Gender);
            objcmd.Parameters.AddWithValue("Password", model.Password);
            objcmd.Parameters.AddWithValue("CityID", model.CityID);
            objcmd.Parameters.AddWithValue("BranchID", model.BranchID);
        
           

            objcmd.ExecuteNonQuery();
            return RedirectToAction("MST_StudentList");
        }

        [Area("MST_Student")]
        [Route("MST_Student/MST_Student/{Action}")]
        public IActionResult MST_StudentEdit(int StudentID)
        {
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SelectByPk_Student";
            cmd.Parameters.AddWithValue("studentid", StudentID);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            conn.Close();
            string studentID = dt.Rows[0]["StudentID"].ToString();
            string StudentName = dt.Rows[0]["StudentName"].ToString();
            string MobileNoStudent = dt.Rows[0]["MobileNoStudent"].ToString();
            string Email = dt.Rows[0]["Email"].ToString();
            string MobileNoFather = dt.Rows[0]["MobileNoFather"].ToString();
            string Address = dt.Rows[0]["Address"].ToString();
            string BirthDate = dt.Rows[0]["BirthDate"].ToString();
            string Age = dt.Rows[0]["Age"].ToString();
            string IsActive = dt.Rows[0]["IsActive"].ToString();
            string Gender = dt.Rows[0]["Gender"].ToString();
            string Password = dt.Rows[0]["Password"].ToString();

            ViewBag.StudentID = studentID;
            ViewBag.StudentName = StudentName;
            ViewBag.MobileNoStudent = MobileNoStudent;
            ViewBag.Email = Email;
            ViewBag.MobileNoFather = MobileNoFather;
            ViewBag.Address = Address;
            ViewBag.BirthDate = BirthDate;
            ViewBag.Age = Age;
            ViewBag.IsActive = IsActive;
            ViewBag.Gender = Gender;
            ViewBag.Password = Password;
            ViewBag.StudentID = StudentID;
            return View();
        }

        [Area("MST_Student")]
        [Route("MST_Student/MST_Student/{Action}")]
        public IActionResult MST_StudentUpdateFillFormData(MST_StudentModel model)
        {
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Update_Student";
            cmd.Parameters.AddWithValue("branchID", model.BranchID);
            cmd.Parameters.AddWithValue("cityID", model.CityID);

            cmd.Parameters.AddWithValue("studentid", model.StudentID);
            cmd.Parameters.AddWithValue("studentname", model.StudentName);
            cmd.Parameters.AddWithValue("mobileno_student", model.MobileNoStudent);
            cmd.Parameters.AddWithValue("email", model.Email);
            cmd.Parameters.AddWithValue("mobileno_father", model.MobileNoFather);
            cmd.Parameters.AddWithValue("address", model.Address);
            cmd.Parameters.AddWithValue("dob", model.BirthDate);
            cmd.Parameters.AddWithValue("age", model.Age);
            cmd.Parameters.AddWithValue("isActive", model.IsActive);
            cmd.Parameters.AddWithValue("gender", model.Gender);
            cmd.Parameters.AddWithValue("password", model.Password);
            cmd.ExecuteNonQuery();
            return RedirectToAction("MST_StudentList");
        }


        [Area("MST_Student")]
        [Route("MST_Student/MST_Student/{Action}")]
        public IActionResult MST_StudentDelete(int StudentID)
        {
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Delete_Student";
            cmd.Parameters.AddWithValue("studentid", StudentID);
            cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("MST_StudentList");
        }
        [Area("MST_Student")]
        [Route("MST_Student/MST_Student/{Action}")]
        public IActionResult MST_StudentSearch(MST_StudentModel model)
        {
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Student_Search";
            cmd.Parameters.AddWithValue("StudentName", model.StudentName);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            conn.Close();
            return View("MST_StudentList", dt);
        }


    }
}

