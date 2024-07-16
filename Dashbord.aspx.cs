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
using System.Drawing;

namespace WebApplication1
{
    

    public partial class _Default : Page
    {
        SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                myCon.Open();
                using (SqlCommand myCom = new SqlCommand("dbo.insert_clt_mst", myCon))
                {
                    myCom.Connection = myCon;
                    myCom.CommandType = CommandType.StoredProcedure;
                    myCom.Parameters.Add("");
                    myCom.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = "1";
                    myCom.Parameters.Add("@LastName", SqlDbType.VarChar).Value = "2";


                    SqlDataReader myDr = myCom.ExecuteReader();


                }
            }
            catch (Exception ex) {  }
            finally { myCon.Close(); }
        }
    }
}