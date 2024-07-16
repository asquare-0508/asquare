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
using System.Net.NetworkInformation;

namespace WebApplication1
{
    public partial class AddProjects : System.Web.UI.Page
    {

        SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString);
        int projectId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitialLoad();
                drpdwn_projstatus.SelectedValue = "Open";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(btnSave.Text=="Create Project")
            {
                try
                {
                    DateTime theDate = DateTime.ParseExact(txtProjectStartDt.Text, "d/M/yyyy", CultureInfo.InvariantCulture);
                    string startDt = theDate.ToString("M/d/yyyy");
                }
                catch (Exception ex)
                {
                    lblrtnMsg.Text = "Wrong Date formate in Start date use 'dd/mm/yyyy' format";
                    return;
                }

                create_project();
            }
            else if (btnSave.Text ==  "Update Project")
            {
                update_project();
            }
            else if (btnSave.Text == "Close Project")
            {
                update_project();
            }

            InitialLoad();
        }

        private void update_project()
        {
            string project_start_dt = null;
            string project_closing_dt= "01/01/1990";

            try
            {
                if (txtProjectStartDt.Text.Length > 0)
                { 
                    DateTime theDate = DateTime.ParseExact(txtProjectStartDt.Text, "d/M/yyyy", CultureInfo.InvariantCulture);
                    project_start_dt = theDate.ToString("M/d/yyyy");
                }

                if (txtClosingDt.Text.Length > 0 )
                {
                    DateTime theDate1 = DateTime.ParseExact(txtClosingDt.Text, "d/M/yyyy", CultureInfo.InvariantCulture);
                    project_closing_dt = theDate1.ToString("M/dd/yyyy");
                }
            }
            catch (Exception ex)
            {
                lblrtnMsg.Text = "Wrong Date formate in DOB use 'd/m/yyyy' format";
                return;
            }

            myCon.Open();

            using (SqlCommand myCom = new SqlCommand("dbo.[update_project]", myCon))
            {
                myCom.Connection = myCon;
                myCom.CommandType = CommandType.StoredProcedure;
                myCom.Parameters.Add("@projectId", SqlDbType.VarChar).Value = txtProjectId.Text;
                myCom.Parameters.Add("@projectName", SqlDbType.VarChar).Value = txtProjectName.Text;
                myCom.Parameters.Add("@projectduration", SqlDbType.VarChar).Value = txtProjectDuration.Text;
                myCom.Parameters.Add("@entityId", SqlDbType.VarChar).Value = drpCompanyId.SelectedItem.Value;
                myCom.Parameters.Add("@amount_requested", SqlDbType.VarChar).Value = txtAmountRequested.Text;
                myCom.Parameters.Add("@amount_approved", SqlDbType.VarChar).Value = txtAmountApproved.Text;
                myCom.Parameters.Add("@introducer_investor", SqlDbType.VarChar).Value = drpInroducer.SelectedItem.Value;
                myCom.Parameters.Add("@project_start_dt", SqlDbType.VarChar).Value = project_start_dt;
                myCom.Parameters.Add("@project_status", SqlDbType.VarChar).Value = drpdwn_projstatus.SelectedItem.Text;
                myCom.Parameters.Add("@closing_percentage", SqlDbType.VarChar).Value = txtClosingpercentage.Text;
                myCom.Parameters.Add("@investor_bonus", SqlDbType.VarChar).Value = txtIntroducerBonus.Text;
                myCom.Parameters.Add("@AdminCharge", SqlDbType.VarChar).Value = txtAdminCharge.Text;
                myCom.Parameters.Add("@OtherCharge", SqlDbType.VarChar).Value = txtOtherCharge.Text;
                myCom.Parameters.Add("@ClosingDate", SqlDbType.VarChar).Value = project_closing_dt;
                myCom.Parameters.Add("@ClosureNote", SqlDbType.VarChar).Value = txtClosureNote.Text;


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

        void create_project()
        {
            myCon.Open();

            using (SqlCommand myCom = new SqlCommand("dbo.[create_project]", myCon))
            {
                if (ValidateFileds() == 0)
                {
                    show_msg("Error", "Fill All mandatory Fields");
                    return;
                }
                myCom.Connection = myCon;
                myCom.CommandType = CommandType.StoredProcedure;
                myCom.Parameters.Add("@projectName", SqlDbType.VarChar).Value = txtProjectName.Text;
                myCom.Parameters.Add("@projectduration", SqlDbType.VarChar).Value = txtProjectDuration.Text;
                myCom.Parameters.Add("@entityId", SqlDbType.VarChar).Value = drpCompanyId.SelectedItem.Value;
                myCom.Parameters.Add("@amount_requested", SqlDbType.VarChar).Value = txtAmountRequested.Text;
                myCom.Parameters.Add("@amount_approved", SqlDbType.VarChar).Value = txtAmountApproved.Text;
                myCom.Parameters.Add("@introducer_investor", SqlDbType.VarChar).Value = drpInroducer.SelectedItem.Value;

                DateTime theDate = DateTime.ParseExact(txtProjectStartDt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string startDt = theDate.ToString("MM/dd/yyyy");
                myCom.Parameters.Add("@project_start_dt", SqlDbType.VarChar).Value = startDt;

                myCom.Parameters.Add("@ExpecingProfitPercentage", SqlDbType.VarChar).Value = txtClosingpercentage.Text;
                myCom.Parameters.Add("@IntroducerBonus", SqlDbType.VarChar).Value = txtIntroducerBonus.Text;
                myCom.Parameters.Add("@AdminCharges", SqlDbType.VarChar).Value = txtAdminCharge.Text;
                myCom.Parameters.Add("@OtherCharges", SqlDbType.VarChar).Value = txtOtherCharge.Text;

                DateTime theDate1 = DateTime.ParseExact(txtClosingDt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string endDate = theDate.ToString("MM/dd/yyyy");

                myCom.Parameters.Add("@ExpectingClosingDate", SqlDbType.VarChar).Value = endDate;
                myCom.Parameters.Add("@Remark", SqlDbType.VarChar).Value = startDt;

                SqlDataReader myDr = myCom.ExecuteReader();

                while (myDr.Read())
                {
                    string sts = myDr["Status"].ToString();
                    string msg = myDr["msg"].ToString();
                    projectId = int.Parse(myDr["Id"].ToString());
                    txtProjectId.Text = projectId.ToString();
                    show_msg(sts, msg);
                }
            }
            myCon.Close();
            InitialLoad();
        }

        void InitialLoad()
        {
            myCon.Open();
            using (SqlCommand myCom = new SqlCommand("exec get_investor_info 0", myCon))
            {
                SqlDataAdapter da = new SqlDataAdapter(myCom);
                DataTable dt = new DataTable();
                da.Fill(dt);

                drpInroducer.DataSource = dt;
                drpInroducer.DataTextField = "Name";
                drpInroducer.DataValueField = "InvestorId";
                drpInroducer.DataBind();
            }
            myCon.Close();

            myCon.Open();
            using (SqlCommand myCom = new SqlCommand("exec get_company_info", myCon))
            {
                SqlDataAdapter da = new SqlDataAdapter(myCom);
                DataTable dt = new DataTable();
                da.Fill(dt);
                drpCompanyId.DataSource = dt;
                drpCompanyId.DataTextField = "Name";
                drpCompanyId.DataValueField = "BusinessId";
                drpCompanyId.DataBind();
            }
            myCon.Close();

            myCon.Open();
            using (SqlCommand myCom = new SqlCommand("select project_id, project_name, amount_approved Approved, investors_contributed Allocated, project_status Status from project_mst where project_status <> 'Close'", myCon))
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

        int ValidateFileds()
        {
            try { string a = drpCompanyId.SelectedItem.Value; }
            catch { return 0; }

            if (txtAmountRequested.Text.Length == 0 || txtAmountApproved.Text.Length == 0 ||
               txtProjectDuration.Text.Length == 0 || txtProjectName.Text.Length == 0
              )
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
                        txtProjectDuration.Text = myDr["project_duration"].ToString();
                        drpCompanyId.SelectedValue = myDr["BusinessId"].ToString();
                        txtAmountRequested.Text = myDr["amount_requested"].ToString().Split(' ')[0];
                        txtAmountApproved.Text = myDr["amount_approved"].ToString();
                        drpInroducer.SelectedValue = myDr["introducer_investor"].ToString();
                        txtProjectStartDt.Text = myDr["project_start_dt"].ToString().Split(' ')[0];

                        txtInvestorContibution.Text = myDr["investors_contributed"].ToString();
                        noofInvestor.Text = myDr["number_of_investors"].ToString();
                        drpdwn_projstatus.SelectedValue = myDr["project_status"].ToString();

                        txtClosingpercentage.Text = myDr["profit_percentage"].ToString();
                        txtIntroducerBonus.Text = myDr["introducer_bonus"].ToString();
                        txtAdminCharge.Text = myDr["AdminCharge"].ToString();
                        txtOtherCharge.Text = myDr["OtherCharge"].ToString();
                    }
                }
                myCon.Close();
                btnSave.Text = "Update Project";
                load_project_allocation();
            }
        }

        protected void RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[1].Visible = false;
            }
        }

        protected void drpdwn_projstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpdwn_projstatus.SelectedItem.Text=="Close")
            {
                btnSave.Text = "Close Project";
            }
            if (drpdwn_projstatus.SelectedItem.Text == "Update")
            {
                btnSave.Text = "Update Project";
            }
        }
    }
}