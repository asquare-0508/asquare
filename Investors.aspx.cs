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
    public partial class Investors : System.Web.UI.Page
    {

        SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                drpRelationship.Items.Add("Father");
                drpRelationship.Items.Add("Mother");
                drpRelationship.Items.Add("Wife");
                drpRelationship.Items.Add("Husband");
                drpRelationship.Items.Add("Daughter");
                drpRelationship.Items.Add("Son");
                drpRelationship.Items.Add("Brother");

                Load_client();

            }
           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string joindt, maturedt;
            try
            {
                DateTime theDate = DateTime.ParseExact(txtJionDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                joindt = theDate.ToString("MM/dd/yyyy");

                DateTime theDate1 = DateTime.ParseExact(txtMatureDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                maturedt = theDate1.ToString("MM/dd/yyyy");
            }
            catch (Exception ex)
            {
                lblrtnMsg.Text = "Wrong Date formate in Join date / Mature date use 'dd/mm/yyyy' format";
                return;
            }

            myCon.Open();
           
            using (SqlCommand myCom = new SqlCommand("dbo.create_investors", myCon))
            {
                if (ValidateFileds()==0)
                {
                    show_msg("Error", "Fill All mandatory Fields");
                    return;
                }
                myCom.Connection = myCon;
                myCom.CommandType = CommandType.StoredProcedure;
                myCom.Parameters.Add("@Investor_clt_id", SqlDbType.VarChar).Value = drpClintId.SelectedItem.Value;
                myCom.Parameters.Add("@nominee_clt_id", SqlDbType.VarChar).Value = drpNomineeId.SelectedItem.Value;
                myCom.Parameters.Add("@relationship", SqlDbType.VarChar).Value = drpRelationship.SelectedItem.Text;
                myCom.Parameters.Add("@joindate", SqlDbType.VarChar).Value = joindt;
                myCom.Parameters.Add("@maturedate", SqlDbType.VarChar).Value = maturedt;

                SqlDataReader myDr = myCom.ExecuteReader();

                while (myDr.Read())
                {
                    string sts = myDr["Status"].ToString();
                    string msg = myDr["msg"].ToString();
                    show_msg(sts, msg);
                }
            }
            myCon.Close();

            Load_client();
        }

        void Load_client()
        {
            myCon.Open();
            using (SqlCommand myCom = new SqlCommand("select clt_id, FirstName + ' - ' + MobileNumber As 'FirstName' from clt_mst where clt_type = 'Investor'", myCon))
            {
                SqlDataAdapter da = new SqlDataAdapter(myCom);
                DataTable dt = new DataTable();
                da.Fill(dt);
                drpClintId.DataSource = dt;
                drpClintId.DataTextField = "FirstName";
                drpClintId.DataValueField = "clt_id";
                drpClintId.DataBind();
                ListItem li0 = new ListItem("Select Investor", "-1");
                drpClintId.Items.Insert(0, li0);
            }
            myCon.Close();

            myCon.Open();
            using (SqlCommand myCom = new SqlCommand("select clt_id, FirstName + ' - ' + MobileNumber As 'FirstName' from clt_mst where clt_type = 'Nominee'", myCon))
            {
                SqlDataAdapter da = new SqlDataAdapter(myCom);
                DataTable dt = new DataTable();
                da.Fill(dt);
                drpNomineeId.DataSource = dt;
                drpNomineeId.DataTextField = "FirstName";
                drpNomineeId.DataValueField = "clt_id";
                drpNomineeId.DataBind();
                ListItem li1 = new ListItem("Select Nominee", "-1");
                drpNomineeId.Items.Insert(0, li1);
            }
            myCon.Close();

            myCon.Open();
            using (SqlCommand myCom = new SqlCommand("exec get_investor_info 0 , 1", myCon))
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
            if(txtJionDate.Text.Length==0 || txtMatureDate.Text.Length == 0)
            {
                return 0;
            }

            return 1;
        }

        void show_msg(string msg_type, string msg)
        {
            lblrtnMsg.Text = msg;
        }
    }
}