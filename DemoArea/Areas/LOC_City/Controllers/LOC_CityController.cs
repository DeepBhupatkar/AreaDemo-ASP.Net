using System;
using System.Data;
using System.Data.SqlClient;
using DemoArea.Areas.LOC_City.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoArea.Areas.LOC_City.Controllers
{
    [Area("LOC_City")]
    [Route("LOC_City/LOC_City/{Action}")]
    public class LOC_CityController : Controller
	{
        private readonly IConfiguration Configuration;
        public LOC_CityController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region CityList
        public IActionResult LOC_CityList()
        {
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_City_SelectAll";
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            conn.Close();
            return View(dt);
        }
        #endregion



        public IActionResult LOC_CityAdd()
        {
            return View();
        }
        #region CityAdd
        public IActionResult LOC_CityAddFormPage(LOC_CityModel model)
        {
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_City_Insert";
            cmd.Parameters.AddWithValue("CityName", model.CityName);
            cmd.Parameters.AddWithValue("CityCode", model.CityCode);
            cmd.Parameters.AddWithValue("StateID", model.StateID);
            cmd.Parameters.AddWithValue("CountryID", model.CountryID);
            cmd.ExecuteNonQuery();
            return RedirectToAction("LOC_CityList");
        }

        #endregion

        #region CityEdit
        public IActionResult LOC_CityEdit(int CityID)
        {
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_City_SelectByPK";
            cmd.Parameters.AddWithValue("CityID", CityID);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            conn.Close();
            string CityName = dt.Rows[0]["CityName"].ToString();
            string CityCode = dt.Rows[0]["CityCode"].ToString();
            string StateID = dt.Rows[0]["StateID"].ToString();
            string CountryID = dt.Rows[0]["CountryID"].ToString();
            ViewBag.CityID = CityID;
            ViewBag.CityName = CityName;
            ViewBag.CityCode = CityCode;
            ViewBag.StateID = StateID;
            ViewBag.CountryID = CountryID;
            return View();
        }
        #endregion
        #region CityUpdForm
        public IActionResult LOC_CityUpdateFillFormData(LOC_CityModel model)
        {
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_City_UpdateByPK";
            cmd.Parameters.AddWithValue("CityID", model.CityID);
            cmd.Parameters.AddWithValue("CityName", model.CityName);
            cmd.Parameters.AddWithValue("StateID", model.StateID);
            cmd.Parameters.AddWithValue("CityCode", model.CityCode);
            cmd.Parameters.AddWithValue("CountryID", model.CountryID);
            cmd.ExecuteNonQuery();
            return RedirectToAction("LOC_CityList");
        }
        #endregion
        #region CityDelete
        public IActionResult LOC_CityDelete(int CityID)
        {
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_City_DeleteByPK";
            cmd.Parameters.AddWithValue("CityID", CityID);
            cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("LOC_CityList");
        }

        #endregion
        #region CitySearch
        public IActionResult LOC_CitySearch(LOC_CityModel modal)
        {
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_Search";
            cmd.Parameters.AddWithValue("CityName", modal.CityName);
            cmd.Parameters.AddWithValue("CityCode", modal.CityCode);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            conn.Close();
            return View("LOC_CityList", dt);
        }
        #endregion

    }
}

