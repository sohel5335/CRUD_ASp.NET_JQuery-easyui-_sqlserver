using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using System.Web;

namespace WebApplicationTest.Models
{
    public class UserReg
    {
        
        SqlConnection cn;
        public UserReg()
        {
            id = 0;
            firstname = "";
            lastname = "";
            UserName = "";
            email = "";
            phone = "";
            Gender = "";
            PresentAddress = "";
            LastEduQua = "";
            Occupation = "";
            DateOfBirth = "";
            ids = "0";
            erro ="";


        }
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string PresentAddress { get; set; }
        public string LastEduQua { get; set; }
        public string Occupation { get; set; }
        public string DateOfBirth { get; set; }
        public string ids { get; set; }
        public string erro { get; set; }
        public UserReg save(UserReg userReg)
        {
            List<UserReg> users = new List<UserReg>();
            try
            {
                if (userReg.id > 0)
                {
                    parsData(2, userReg);
                    string sqlquery = "Select * from userInfo  where id=" + userReg.id;
                    users = map(sqlquery, users);


                }
                else
                {
                    parsData(1, userReg);
                    string sqlquery = "SELECT TOP 1 * FROM userInfo ORDER BY id DESC";
                    users = map(sqlquery, users);
                }

            }
            catch (Exception ex)
            {
                UserReg usrReg = new UserReg();

                usrReg.erro = ex.Message;
                users.Add(usrReg);
                return users[0];
            }

            
            return users[0];

        }
        public void delete(UserReg userReg)
        {
            parsData(3, userReg);
        }
        public List<UserReg> gets()
        {
            

            string sqlquery = "Select * from userInfo";
            List<UserReg> users = new List<UserReg>();
            users = map(sqlquery, users);
           

            return users;
        }
        public List<UserReg> map(string sqlquery , List<UserReg> users)
        {

            try
            {

            SqlCommand cmd = sqlConnection(sqlquery);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                UserReg usrReg = new UserReg();
                usrReg.id = (int)dr["id"];
                usrReg.firstname = dr["firstname"].ToString();
                usrReg.lastname = dr["lastname"].ToString();
                usrReg.UserName = dr["UserName"].ToString();
                usrReg.email = dr["email"].ToString();
                usrReg.phone = dr["phone"].ToString();
                usrReg.Gender = dr["Gender"].ToString();
                usrReg.PresentAddress = dr["PresentAddress"].ToString();
                usrReg.LastEduQua = dr["LastEduQua"].ToString();
                usrReg.Occupation = dr["Occupation"].ToString();
                usrReg.DateOfBirth = dr["DateOfBirth"].ToString();
                users.Add(usrReg);

              }
            }
            catch(Exception ex)
            {
                UserReg usrReg = new UserReg();

                usrReg.erro = ex.Message;
                users.Add(usrReg);
                return users;
            }
            finally
            {
                cn.Close();
            }
            
            return users;
        }

        public void parsData(int opernation , UserReg userReg)
        {
            SqlCommand cmd = sqlConnection("Pro_userinfo");

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", userReg.id);
            cmd.Parameters.AddWithValue("@firstname", userReg.firstname);
            cmd.Parameters.AddWithValue("@lastname", userReg.lastname);
            cmd.Parameters.AddWithValue("@UserName", userReg.UserName);
            cmd.Parameters.AddWithValue("@email", userReg.email);
            cmd.Parameters.AddWithValue("@phone", userReg.phone);
            cmd.Parameters.AddWithValue("@Gender", userReg.Gender);
            cmd.Parameters.AddWithValue("@PresentAddress", userReg.PresentAddress);

            cmd.Parameters.AddWithValue("@LastEduQua", userReg.LastEduQua);
            cmd.Parameters.AddWithValue("@Occupation", userReg.Occupation);
            cmd.Parameters.AddWithValue("@DateOfBirth", userReg.DateOfBirth);
            cmd.Parameters.AddWithValue("@ids", userReg.ids);
            cmd.Parameters.AddWithValue("@opetation", opernation);



            cmd.ExecuteNonQuery();
            
            cn.Close();
        }
        public SqlCommand sqlConnection(string query)
        {
            string str = "Data Source=DESKTOP-67LBEUF\\RANA;Initial Catalog=Test;Integrated Security=True";
            cn = new SqlConnection(str);
            cn.Open();
            SqlCommand cmd = new SqlCommand(query, cn);

            return cmd;
        }

        ~UserReg()
        {
            cn.Close();
        }
           
    }
}