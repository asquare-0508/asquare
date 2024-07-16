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
    public partial class ReceiptingMovement : System.Web.UI.Page
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
            if (!IsPostBack)
            { 
                LoadInvestors();
            }
        }

        void LoadInvestors()
        {
            myCon.Open();
            using (SqlCommand myCom = new SqlCommand("exec get_investor_info 0, 0", myCon))
            {
                SqlDataAdapter da = new SqlDataAdapter(myCom);
                DataTable dt = new DataTable();
                da.Fill(dt);
                drpdwnInvestor.DataSource = dt;
                drpdwnInvestor.DataTextField = "Name";
                drpdwnInvestor.DataValueField = "InvestorId";
                drpdwnInvestor.DataBind();
            }
            myCon.Close();
        }
        protected void drpdwnInvestor_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_receipts();
        }

        private void load_receipts()
        {
            string investorId = drpdwnInvestor.SelectedValue;

            string qry = "SELECT pay_id, c.FirstName + ' '+ c.FathersName name, convert(varchar(15),a.pay_date,107) pay_date, isnull(d.project_name,'External') money_source, a.Pay_type, a.pay_mode, a.Amount, (a.Amount - a.Allocated_amount) Unallocated FROM acc_pay a inner join Investors b on a.InvestorId = b.InvestorId inner join clt_mst c on c.clt_id = b.Investor_clt_id left join project_mst d on a.project_id = d.project_id where a.InvestorId = "+ investorId;
            myCon.Open();
            using (SqlCommand myCom = new SqlCommand(qry, myCon))
            {
                SqlDataAdapter da = new SqlDataAdapter(myCom);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grd_receipts.DataSource = dt;
                grd_receipts.DataBind();
            }
            myCon.Close();
        }

        protected void grd_receipts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#ceedfc'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle");
                e.Row.Attributes.Add("style", "cursor:pointer;");

                string payid = e.Row.Cells[0].Text;
                string sQuery = "select isnull(b.project_name, a.project_status) MoneyAllocated, a.amount_allocated AmountUsed, a.project_status Status, profit_amount Profit FROM allocation_mst a left join project_mst b on a.project_id = b.project_id \r\nWHERE a.pay_id = " + payid;
                GridView SC = (GridView)e.Row.FindControl("grd_allocation");
                SC.DataSource = getData(sQuery);
                SC.ShowHeader = false;
                SC.DataBind();
            }
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