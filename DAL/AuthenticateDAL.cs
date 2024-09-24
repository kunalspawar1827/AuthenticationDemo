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
            bool isValid = true;
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
                    if (user.Password.ToString() == checkpass.ToString()) 
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
                using(SqlConnection con = new SqlConnection(connectionstring))
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


        public bool ResetPass(string newpass , string username) 
        {
            bool isReset = false;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand($"Update UserLogin set password = '{newpass} 'Where Username = '{username}'", con);
                    int res = Convert.ToInt32(cmd.ExecuteScalar());
                    if (res == 0)
                    {
                        isReset = true;
                    }
                }
            }
            catch (Exception ex)
            {
                isReset = false;
            }
            return isReset;
        }

    }
}