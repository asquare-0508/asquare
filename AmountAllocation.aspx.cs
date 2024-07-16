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
using System.CodeDom;
using System.Globalization;

namespace WebApplication1
{
    public partial class AmountAllocation : System.Web.UI.Page
    {

        SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString);
        int projectId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInvestors();
                LoadProjects();
            }
        }

        void LoadProjects()
        {
            myCon.Open();
            using (SqlCommand myCom = new SqlCommand("select project_id, project_name, amount_approved Approved, investors_contributed Allocated, project_status Status from project_mst where project_status <> 'Closed'", myCon))
            {
                SqlDataAdapter da = new SqlDataAdapter(myCom);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grd_projects.DataSource = dt;
                grd_projects.DataBind();

                if (dt.Rows.Count != 0)
                {
                    grd_projects.HeaderRow.Cells.RemoveAt(1);
                }
            }
            myCon.Close();
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

        protected void drpdwnInvestor_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAvailableBalance.Text = drpdwnInvestor.SelectedItem.Text.Split('|')[3];
        }

        protected void btn_execute_Click(object sender, EventArgs e)
        {
            if (txtProjectId.Text.Length==0)
            {
                lblrtnMsg.Text = "Select one project before execute";
                return;
            }
            myCon.Open();

            using (SqlCommand myCom = new SqlCommand("dbo.[update_project]", myCon))
            {
                myCom.Connection = myCon;
                myCom.CommandType = CommandType.StoredProcedure;
                myCom.Parameters.Add("@projectId", SqlDbType.VarChar).Value = txtProjectId.Text;
                myCom.Parameters.Add("@projectName", SqlDbType.VarChar).Value = txtProjectName.Text;
                myCom.Parameters.Add("@projectduration", SqlDbType.VarChar).Value = "0";
                myCom.Parameters.Add("@entityId", SqlDbType.VarChar).Value = "0";
                myCom.Parameters.Add("@amount_requested", SqlDbType.VarChar).Value = "0";
                myCom.Parameters.Add("@amount_approved", SqlDbType.VarChar).Value = "0";
                myCom.Parameters.Add("@introducer_investor", SqlDbType.VarChar).Value = "0";
                myCom.Parameters.Add("@project_start_dt", SqlDbType.VarChar).Value = "01/01/2000";
                myCom.Parameters.Add("@project_status", SqlDbType.VarChar).Value = "Execute";
                myCom.Parameters.Add("@closing_percentage", SqlDbType.VarChar).Value = "0";
                myCom.Parameters.Add("@investor_bonus", SqlDbType.VarChar).Value = "0";
                myCom.Parameters.Add("@AdminCharge", SqlDbType.VarChar).Value = "0";
                myCom.Parameters.Add("@OtherCharge", SqlDbType.VarChar).Value = "0";
                myCom.Parameters.Add("@ClosingDate", SqlDbType.VarChar).Value = "01/01/2000";
                myCom.Parameters.Add("@ClosureNote", SqlDbType.VarChar).Value = "";

                SqlDataReader myDr = myCom.ExecuteReader();

                while (myDr.Read())
                {
                    string sts = myDr["Status"].ToString();
                    string msg = myDr["msg"].ToString();
                    projectId = int.Parse(myDr["Id"].ToString());
                    show_msg(sts, msg);
                }
            }
            myCon.Close();
        }


        protected void btn_finish_Click(object sender, EventArgs e)
        {
            if(txtAmountAllocate.Text=="")
            {
                lblrtnMsg.Text = "Fill All required fields first";
                return;
            }

            decimal available_amt = 0;
            available_amt = decimal.Parse(drpdwnInvestor.SelectedItem.Text.Trim().Split('|')[3]);

            if (available_amt < decimal.Parse(txtAmountAllocate.Text))
            {
                lblrtnMsg.Text = "Dont have enough balance to cover";
                return;
            }

            myCon.Open();

            using (SqlCommand myCom = new SqlCommand("create_project_allocation", myCon))
            {
                myCom.Connection = myCon;
                myCom.CommandType = CommandType.StoredProcedure;
                myCom.Parameters.Add("@project_id", SqlDbType.VarChar).Value = txtProjectId.Text;
                myCom.Parameters.Add("@investor_id", SqlDbType.VarChar).Value = drpdwnInvestor.SelectedItem.Value;

                DateTime theDate = DateTime.ParseExact(txtAllocateDt.Text, "dd/M/yyyy", CultureInfo.InvariantCulture);
                string dt = theDate.ToString("M/dd/yyyy");

                myCom.Parameters.Add("@allocation_date", SqlDbType.VarChar).Value = dt;
                myCom.Parameters.Add("@amount_to_allocate", SqlDbType.VarChar).Value = txtAmountAllocate.Text;

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
            LoadProjects();
            load_project_allocation();
        }

        void load_project_allocation()
        {
            myCon.Open();
            using (SqlCommand myCom = new SqlCommand("get_project_allocation", myCon))
            {
                myCom.Parameters.AddWithValue("@project_id", txtProjectId.Text);
                myCom.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(myCom);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grd_project_allocation.DataSource = dt;
                grd_project_allocation.DataBind();
            }
            myCon.Close();
        }

        protected void RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Select")
            {
                GridViewRow selectedRow = grd_projects.Rows[index];
                txtProjectId.Text = selectedRow.Cells[1].Text;

                myCon.Open();
                using (SqlCommand myCom = new SqlCommand("select * from project_mst where project_id = " + txtProjectId.Text, myCon))
                {
                    SqlDataReader myDr = myCom.ExecuteReader();
                    while (myDr.Read())
                    {
                        txtProjectName.Text = myDr["project_name"].ToString();
                    }
                }
                myCon.Close();

                load_project_allocation();
            }
        }

        protected void RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Visible = false;
            }
        }

    }


}