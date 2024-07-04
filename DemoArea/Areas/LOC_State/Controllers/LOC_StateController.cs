using System.Data;
using System.Data.SqlClient;
using DemoArea.Areas.LOC_Country.Models;
using DemoArea.Areas.LOC_State.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoArea.Areas.LOC_State.Controllers
{
    public class LOC_StateController : Controller
    {
        private readonly IConfiguration Configuration;
        public LOC_StateController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        [Area("LOC_State")]
        [Route("LOC_State/LOC_State/{Action}")]
        public IActionResult LOC_StateList()
        {
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_State_SelectAll";
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            conn.Close();
            return View(dt);
        }


        [Area("LOC_State")]
        [Route("LOC_State/LOC_State/{Action}")]
        public IActionResult LOC_StateAdd()
        {
            
            return View();
        }

        [Area("LOC_State")]
        [Route("LOC_State/LOC_State/{Action}")]
        public IActionResult LOC_StateAddFormPage(LOC_StateModel model)
        {
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_State_Insert";
            cmd.Parameters.AddWithValue("CountryID", model.CountryID);
            cmd.Parameters.AddWithValue("StateName", model.StateName);
            cmd.Parameters.AddWithValue("StateCode", model.StateCode);
            cmd.ExecuteNonQuery();
            return RedirectToAction("LOC_StateList");
        }

        [Area("LOC_State")]
        [Route("LOC_State/LOC_State/{Action}")]
        public IActionResult LOC_StateEdit(int StateID)
        {
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_State_SelectByPK";
            cmd.Parameters.AddWithValue("StateID", StateID);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            conn.Close();
            string StateName = dt.Rows[0]["StateName"].ToString();
            string CountryID = dt.Rows[0]["CountryID"].ToString();
            string StateCode = dt.Rows[0]["StateCode"].ToString();
            ViewBag.StateID = StateID;
            ViewBag.StateName = StateName;
            ViewBag.CountryID = CountryID;
            ViewBag.StateCode = StateCode;
            return View();
        }

        [Area("LOC_State")]
        [Route("LOC_State/LOC_State/{Action}")]
        public IActionResult LOC_StateEditFormPage(LOC_StateModel model)
        {
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_State_UpdateByPK";
            cmd.Parameters.AddWithValue("StateID", model.StateID);
            cmd.Parameters.AddWithValue("StateName", model.StateName);
            cmd.Parameters.AddWithValue("StateCode", model.StateCode);
            cmd.Parameters.AddWithValue("CountryID", model.CountryID);
            cmd.ExecuteNonQuery();
            return RedirectToAction("LOC_StateList");
        }

        [Area("LOC_State")]
        [Route("LOC_State/LOC_State/{Action}")]
        public IActionResult LOC_StateDelete(int StateID)
        {
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_State_DeleteByPK";
            cmd.Parameters.AddWithValue("StateID", StateID);
            cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("LOC_StateList");
        }
        [Area("LOC_State")]
        [Route("LOC_State/LOC_State/{Action}")]
        public IActionResult LOC_StateSearch(LOC_StateModel model)
        {
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_State_Search";
            cmd.Parameters.AddWithValue("StateName", model.StateName);
            cmd.Parameters.AddWithValue("StateCode", model.StateCode);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            conn.Close();
            return View("LOC_StateList", dt);
        }
        



    }

}


