using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;

namespace WebApplication1
{
    public partial class ProjectList : System.Web.UI.Page
    {
        decimal allocated = 0;
        decimal approved = 0;
        decimal adminCharge = 0;
        decimal totalCharge = 0;
        decimal totalProfit = 0;
        decimal totalProfitPercent = 0;

        SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString);
        int projectId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            load_projects();
        }

        private void load_projects()
        {
            myCon.Open();
            using (SqlCommand myCom = new SqlCommand("select *, CONVERT(varchar,project_start_dt,107) StartDate, CONVERT(varchar,project_end_dt,107) EndDate from project_mst ", myCon))
            {
                SqlDataAdapter da = new SqlDataAdapter(myCom);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grd_projects.DataSource = dt;
                grd_projects.DataBind();
            }
            myCon.Close();
        }

        protected void grd_projects_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#ceedfc'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle");
                e.Row.Attributes.Add("style", "cursor:pointer;");

                string project_id = e.Row.Cells[0].Text;
                string sQuery = "exec get_project_allocation  " + project_id;
                GridView SC = (GridView) e.Row.FindControl("grd_inner");
                SC.DataSource = getData(sQuery);
                SC.ShowHeader = false;
                SC.DataBind();

                approved = approved + decimal.Parse(e.Row.Cells[2].Text);
                allocated = allocated + decimal.Parse(e.Row.Cells[3].Text);
                adminCharge = adminCharge + decimal.Parse(e.Row.Cells[5].Text);
                totalCharge = totalCharge + decimal.Parse(e.Row.Cells[6].Text);
                totalProfit = totalProfit + decimal.Parse(e.Row.Cells[7].Text);
                totalProfitPercent = totalProfitPercent + decimal.Parse(e.Row.Cells[8].Text);
            }

            lblAllocated.Text = allocated.ToString();
            lblApproved.Text = approved.ToString();
            lblAdminCharge.Text = adminCharge.ToString();
            lblOtherCharge.Text = totalCharge.ToString();
            lblProfit.Text = totalProfit.ToString();
            totProfitPercent.Text = totalProfitPercent.ToString();

        }

        DataTable getData(string sQuery)
        {
            using (SqlCommand myCom = new SqlCommand(sQuery, myCon))
            {
                SqlDataAdapter da = new SqlDataAdapter(myCom);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}