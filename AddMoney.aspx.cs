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

namespace WebApplication1
{
    public partial class AddMoney : System.Web.UI.Page
    {

        SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                drpPayMode.Items.Add("Cash");
                //drpPayMode.Items.Add("Bank Transfer");
                //drpPayMode.Items.Add("Cheque");

                drpPayType.Items.Add("Receipt");
                drpPayType.Items.Add("Withdraw");
                //drpPayType.Items.Add("Profit");
                //drpPayType.Items.Add("Bonus");

                LoadInvestors();
                LoadReceipts();


            }
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string dateToInsert;
            try
            {
                DateTime theDate = DateTime.ParseExact(txtPayDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dateToInsert = theDate.ToString("MM/dd/yyyy");
            }
            catch (Exception ex)
            {
                lblrtnMsg.Text = "Wrong Date formate in Receipt date use 'dd/mm/yyyy' format";
                return;
            }


            myCon.Open();
           
            using (SqlCommand myCom = new SqlCommand("dbo.[insert_accpay]", myCon))
            {
                if (ValidateFileds()==0)
                {
                    show_msg("Error", "Fill All mandatory Fields");
                    return;
                }
                myCom.Connection = myCon;
                myCom.CommandType = CommandType.StoredProcedure;
                myCom.Parameters.Add("@investorID", SqlDbType.VarChar).Value = drpInvestorId.SelectedItem.Value;
                myCom.Parameters.Add("@projectID", SqlDbType.VarChar).Value = 0;
                myCom.Parameters.Add("@PayDate", SqlDbType.VarChar).Value = dateToInsert;
                myCom.Parameters.Add("@PayType", SqlDbType.VarChar).Value = drpPayType.SelectedItem.Text;
                myCom.Parameters.Add("@PayMode", SqlDbType.VarChar).Value = drpPayMode.SelectedItem.Text;
                myCom.Parameters.Add("@ReceiptAmount", SqlDbType.VarChar).Value = txtAmount.Text;
                myCom.Parameters.Add("@AllocatedAmount", SqlDbType.VarChar).Value = 0;
                myCom.Parameters.Add("@Remark", SqlDbType.VarChar).Value = txtRemark.Text;

                SqlDataReader myDr = myCom.ExecuteReader();

                while (myDr.Read())
                {

                    string sts = myDr["Status"].ToString();
                    string msg = myDr["msg"].ToString();
                    show_msg(sts, msg);

                }
            }
            myCon.Close();

            LoadInvestors();
            LoadReceipts();
        }

        void LoadInvestors()
        {
            myCon.Open();
            using (SqlCommand myCom = new SqlCommand("EXEC get_investor_info 0, 0", myCon))
            {
                SqlDataAdapter da = new SqlDataAdapter(myCom);
                DataTable dt = new DataTable();
                da.Fill(dt);
                drpInvestorId.DataSource = dt;
                drpInvestorId.DataTextField = "Name";
                drpInvestorId.DataValueField = "InvestorId";
                drpInvestorId.DataBind();
                ListItem li = new ListItem("Select Investor", "-1");
                drpInvestorId.Items.Insert(0, li);
            }
            myCon.Close();
        }

        void LoadReceipts()
        {
            myCon.Open();
            using (SqlCommand myCom = new SqlCommand("exec get_receipts", myCon))
            {
                SqlDataAdapter da = new SqlDataAdapter(myCom);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grd_investor_list.DataSource = dt;
                grd_investor_list.DataBind();
            }
            myCon.Close();

            
        }

        int ValidateFileds()
        {
            if(txtAmount.Text.Length==0 || txtPayDate.Text.Length == 0)
            {
                return 0;
            }

            return 1;
        }

        void show_msg(string msg_type, string msg)
        {
            lblrtnMsg.Text = msg;

            if (msg_type == "Success")
            {
                lblrtnMsg.ForeColor = Color.Green;
            }
            else
            {
                lblrtnMsg.ForeColor = Color.Red;
            }
        }
    }
}