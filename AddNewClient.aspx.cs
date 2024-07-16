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

namespace WebApplication1
{
    public partial class AddNewClient : System.Web.UI.Page
    {

        SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            Load_client();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtCltId.Text=="")
            {
                lblrtnMsg.Text = "Select one client to edit & update";
                return;
            }
            create_update_client("Update");
            Load_client();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            create_update_client("Create");
            Load_client();
        }

        void create_update_client(string SPtype)
        {
            string dateToInsert = "";

            try
            {
                DateTime theDate = DateTime.ParseExact(txtDOB.Text, "dd/M/yyyy", CultureInfo.InvariantCulture);
                dateToInsert = theDate.ToString("M/dd/yyyy");
            }
            catch (Exception ex)
            {
                lblrtnMsg.Text = "Wrong Date formate in DOB use 'dd/mm/yyyy' format";
                return;
            }

            myCon.Open();
            using (SqlCommand myCom = new SqlCommand("dbo.insert_clt_mst", myCon))
            {
                if (ValidateFileds() == 0)
                {
                    show_msg("Error", "Fill All mandatory Fields");
                    return;
                }
                myCom.Connection = myCon;
                myCom.CommandType = CommandType.StoredProcedure;
                myCom.Parameters.Add("@SPType", SqlDbType.VarChar).Value = SPtype;
                myCom.Parameters.Add("@clt_id", SqlDbType.VarChar).Value = txtCltId.Text;
                myCom.Parameters.Add("@clt_name", SqlDbType.VarChar).Value = txtFirstName.Text;
                myCom.Parameters.Add("@fathersname", SqlDbType.VarChar).Value = txtFathersName.Text;
                myCom.Parameters.Add("@gender", SqlDbType.VarChar).Value = drpdwnGender.SelectedItem.Text;
                myCom.Parameters.Add("@dob", SqlDbType.VarChar).Value = dateToInsert;
                myCom.Parameters.Add("@aadharid", SqlDbType.VarChar).Value = txtAadharId.Text;
                myCom.Parameters.Add("@mobile_number", SqlDbType.VarChar).Value = txtMobileNumber.Text;
                myCom.Parameters.Add("@email_id", SqlDbType.VarChar).Value = txtEmailId.Text;
                myCom.Parameters.Add("@add1", SqlDbType.VarChar).Value = txtAddress1.Text;
                myCom.Parameters.Add("@add2", SqlDbType.VarChar).Value = txtAddress2.Text;
                myCom.Parameters.Add("@city", SqlDbType.VarChar).Value = txtCity.Text;
                myCom.Parameters.Add("@state", SqlDbType.VarChar).Value = txtState.Text;
                myCom.Parameters.Add("@pincode", SqlDbType.VarChar).Value = txtPincode.Text;
                myCom.Parameters.Add("@clt_type", SqlDbType.VarChar).Value = drpdwm_clt_type.SelectedItem.Text;

                SqlDataReader myDr = myCom.ExecuteReader();

                while (myDr.Read())
                {
                    string sts = myDr["Status"].ToString();
                    string msg = myDr["msg"].ToString();
                    show_msg(sts, msg);
                }
            }
            myCon.Close();
            txtCltId.Text = "";
        }

        void Load_client()
        {
            myCon.Open();
            using (SqlCommand myCom = new SqlCommand("select clt_id , clt_type, FirstName+' '+ isnull(Fathersname, ' ') Name, MobileNumber from clt_mst", myCon))
            {
                SqlDataAdapter da = new SqlDataAdapter(myCom);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grd_clint_info.DataSource = dt;
                grd_clint_info.DataBind();

                if (dt.Rows.Count != 0)
                {
                    grd_clint_info.HeaderRow.Cells.RemoveAt(1);
                }
            }
            myCon.Close();
        }

        int ValidateFileds()
        {
            if(txtAadharId.Text.Length==0 || txtFirstName.Text.Length == 0 || txtCity.Text.Length == 0 || 
               txtFathersName.Text.Length ==0 || txtFathersName.Text.Length == 0)
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

        protected void grd_clint_info_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Visible = false;
            }
        }

        protected void RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Select")
            {
                GridViewRow selectedRow = grd_clint_info.Rows[index];
                string clt_id = selectedRow.Cells[1].Text;
                txtCltId.Text = clt_id;

                myCon.Open();
                using (SqlCommand myCom = new SqlCommand("select * from clt_mst where clt_id = "+ clt_id, myCon))
                {
                    SqlDataReader myDr = myCom.ExecuteReader();
                    while (myDr.Read())
                    {
                        txtFirstName.Text   = myDr["FirstName"].ToString();
                        txtFathersName.Text = myDr["FathersName"].ToString();
                        drpdwnGender.SelectedValue = myDr["Gender"].ToString();

                        CultureInfo provider = CultureInfo.InvariantCulture;
                        txtDOB.Text = myDr["DOB"].ToString().Split(' ')[0];
                        txtEmailId.Text = myDr["EmailId"].ToString();
                        txtAddress1.Text = myDr["Address1"].ToString();
                        txtAddress2.Text = myDr["Address2"].ToString();
                        txtCity.Text = myDr["City"].ToString();
                        txtPincode.Text = myDr["pincode"].ToString();
                        txtState.Text = myDr["State"].ToString();
                        txtMobileNumber.Text = myDr["MobileNumber"].ToString();
                        txtAadharId.Text = myDr["AadharId"].ToString();
                        drpdwm_clt_type.SelectedValue = myDr["clt_type"].ToString();
                    }
                }
                myCon.Close();
            }
        }
    }
}