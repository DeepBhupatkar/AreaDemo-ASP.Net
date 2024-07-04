using System;
using DemoArea.Areas.LOC_Country.Models;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Reflection;

namespace DemoArea.Areas.LOC_Country.Controllers
{
    [Area("LOC_Country")]
    [Route("LOC_Country/LOC_Country/{Action}")]
    public class LOC_CountryController:Controller

	{

        public IConfiguration Configuration;
        public LOC_CountryController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #region CountryList
        public IActionResult LOC_CountryList()
        {
            string connectionstr = this.Configuration.GetConnectionString("ConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Country_SelectAll";

            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View(dt);
        }
        #endregion

        #region DeleteCountry 
        public IActionResult DeleteCountry(int CountryID)
        {
            string connectionstr = this.Configuration.GetConnectionString("ConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Country_DeleteByPK";
            ObjCmd.Parameters.AddWithValue("CountryId", CountryID);
            ObjCmd.ExecuteNonQuery();
            return RedirectToAction("LOC_CountryList");
        }
        #endregion

        #region CountryInsert
        public IActionResult LOC_CountryInsert(LOC_CountryModel modelLOC_Country)
        {
            string connectionstr = this.Configuration.GetConnectionString("ConnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Country_Insert";
            ObjCmd.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = modelLOC_Country.CountryName;
            ObjCmd.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = modelLOC_Country.CountryCode;
            ObjCmd.ExecuteNonQuery();


            return RedirectToAction("LOC_CountryList");
        }
        #endregion

        public IActionResult AddEdit()
        {
            return View("LOC_CountryInsert");
        }

        #region CountryAddEdit
        public IActionResult LOC_CountryAddEdit(int CountryId)
        {
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Country_SelectByPK";
            cmd.Parameters.AddWithValue("CountryID", CountryId);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            conn.Close();

            string CountryName = dt.Rows[0]["CountryName"].ToString();
            string CountryCode = dt.Rows[0]["CountryCode"].ToString();
            ViewBag.CountryId = CountryId;
            ViewBag.CountryName = CountryName;
            ViewBag.CountryCode = CountryCode;

            return View();
        }
        #endregion
        #region CountryUpdate
        public IActionResult CountryUpdate(LOC_CountryModel model)
        {
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Country_UpdateByPK";
            cmd.Parameters.AddWithValue("CountryID", model.CountryId);
            cmd.Parameters.AddWithValue("CountryCode", model.CountryCode);
            cmd.Parameters.AddWithValue("CountryName", model.CountryName);
            cmd.ExecuteNonQuery();
            return RedirectToAction("LOC_CountryList");
        }
        #endregion

        #region CountrySearch

        public IActionResult LOC_CountrySearch(LOC_CountryModel model)
        {
            string str = this.Configuration.GetConnectionString("connectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_Country_Search";
            cmd.Parameters.AddWithValue("CountryName", model.CountryName);
            cmd.Parameters.AddWithValue("CountryCode", model.CountryCode);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            conn.Close();
            return View("LOC_CountryList", dt);
        }
        #endregion
    }
}

