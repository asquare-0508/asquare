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

namespace WebApplication1
{
    public partial class AddNewCompany : System.Web.UI.Page
    {

        SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            Load_client();
            Load_Companys();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            myCon.Open();
            using (SqlCommand myCom = new SqlCommand("dbo.insert_entity_mst", myCon))
            {
                if (ValidateFileds()==0)
                {
                    show_msg("Error", "Fill All mandatory Fields");
                    return;
                }
                myCom.Connection = myCon;
                myCom.CommandType = CommandType.StoredProcedure;
                myCom.Parameters.Add("@Business_name", SqlDbType.VarChar).Value = txtBusinessName.Text;
                myCom.Parameters.Add("@Business_nature", SqlDbType.VarChar).Value = txtBusinessNature.Text;
                myCom.Parameters.Add("@add1", SqlDbType.VarChar).Value = txtAddress1.Text;
                myCom.Parameters.Add("@add2", SqlDbType.VarChar).Value = txtAddress2.Text;
                myCom.Parameters.Add("@add3", SqlDbType.VarChar).Value = txtAddress3.Text;
                myCom.Parameters.Add("@city", SqlDbType.VarChar).Value = txtCity.Text;
                myCom.Parameters.Add("@state", SqlDbType.VarChar).Value = txtState.Text;
                myCom.Parameters.Add("@pincode", SqlDbType.VarChar).Value = txtPincode.Text;
                myCom.Parameters.Add("@gst_no", SqlDbType.VarChar).Value = txtGstNo.Text;
                myCom.Parameters.Add("@tin", SqlDbType.VarChar).Value = txtTIN.Text;
                myCom.Parameters.Add("@proprietor_id", SqlDbType.VarChar).Value = drpOwnerName.SelectedItem.Value;

                SqlDataReader myDr = myCom.ExecuteReader();

                while (myDr.Read())
                {
                    string sts = myDr["Status"].ToString();
                    string msg = myDr["msg"].ToString();
                    show_msg(sts, msg);
                }
            }
            myCon.Close();
            Load_Companys();
        }

        void Load_client()
        {
            myCon.Open();
            using (SqlCommand myCom = new SqlCommand("select clt_id, FirstName + ' - ' + MobileNumber As 'FirstName' from clt_mst where clt_type = 'Proprietor'", myCon))
            {
                SqlDataAdapter da = new SqlDataAdapter(myCom);
                DataTable dt = new DataTable();
                da.Fill(dt);
                drpOwnerName.DataSource = dt;
                drpOwnerName.DataTextField = "FirstName";
                drpOwnerName.DataValueField = "clt_id";
                drpOwnerName.DataBind();
            }
            myCon.Close();
        }

        void Load_Companys()
        {
            myCon.Open();
            using (SqlCommand myCom = new SqlCommand("select BusinessId, BusinessName, BusinessNature from entity_mst", myCon))
            {
                SqlDataAdapter da = new SqlDataAdapter(myCom);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grd_compnay_info.DataSource = dt;
                grd_compnay_info.DataBind();

                if (dt.Rows.Count != 0)
                {
                    grd_compnay_info.HeaderRow.Cells.RemoveAt(1);
                }
            }
            myCon.Close();

            

        }

        int ValidateFileds()
        {
            if(txtBusinessName.Text.Length==0 || txtBusinessNature.Text.Length == 0 || txtCity.Text.Length == 0 || txtAddress2.Text.Length == 0)
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

        protected void Entity_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Visible = false;
            }
        }

        protected void Entity_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Select")
            {
                txtGstNo.Text = e.CommandName;

                GridViewRow selectedRow = grd_compnay_info.Rows[index];

                if (selectedRow != null)
                {
                    txtEntityId.Text = selectedRow.Cells[1].Text;
                }
            }

        }
    }
}