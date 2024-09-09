using AuthenticationDemo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace AuthenticationDemo.DAL
{
    public class AuthenticateDAL
    {
        string connectionstring = ConfigurationManager.ConnectionStrings["constr"].ToString();
        public bool IsValidUser(User user)
        {
            bool isValid = false;
            string checkpass = null;
            try
            {
                

                using (SqlConnection con = new SqlConnection(connectionstring)) 
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand($"select password from userLogin where username = '{user.UserName}' and isvalid = '1' " , con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    SqlDataReader dr  = cmd.ExecuteReader();
                    if (dr.HasRows) 
                    {
                        while (dr.Read()) 
                        {
                            checkpass = dr["password"].ToString();
                        }
                    }
                    if (user.Password == checkpass) 
                    {
                        isValid = true;
                    }
                }

            }
            catch (Exception ex) {
                isValid = false;
            }

            return isValid;
        }

        public string GetUserEmail(string username)
        {
            string userEmail = "";
            try
            {
                using(SqlConnection con = new SqlConnection(username))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand($"select Email from  [Production1].[dbo].[UserLogin] where username =  '{username}'", con);
                    userEmail = cmd.ExecuteScalar().ToString();
                    return userEmail;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}