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
    public partial class InvestorList : System.Web.UI.Page
    {
        decimal Receipts = 0;
        decimal Profits = 0;
        decimal adminCharge = 0;
        decimal Unitilized = 0;
        decimal unallocated = 0;
        decimal totalWithdraw = 0;

        SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString);
        int projectId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            load_Investors();
        }

        private void load_Investors()
        {
            myCon.Open();
            using (SqlCommand myCom = new SqlCommand("exec dbo.get_investor_info 0, 1 ", myCon))
            {
                SqlDataAdapter da = new SqlDataAdapter(myCom);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grd_investor.DataSource = dt;
                grd_investor.DataBind();
            }
            myCon.Close();
        }

        protected void grd_investor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Receipts = Receipts + decimal.Parse(e.Row.Cells[3].Text);
                Profits = Profits + decimal.Parse(e.Row.Cells[4].Text);
                totalWithdraw = totalWithdraw + decimal.Parse(e.Row.Cells[5].Text);
                Unitilized = Unitilized + decimal.Parse(e.Row.Cells[6].Text);
                unallocated = unallocated + decimal.Parse(e.Row.Cells[7].Text);
            }

            lblInvestAmt.Text = Receipts.ToString();
            lblProfit.Text = Profits.ToString();
            lblWithdraw.Text = totalWithdraw.ToString();
            lblUtilized.Text = Unitilized.ToString();
            lblUnallocate.Text = unallocated.ToString();
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