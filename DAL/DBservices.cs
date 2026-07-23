using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using CountriesProject.Models;

namespace CountriesProject.DAL
{
    /// <summary>
    /// DBServices is a class created by me to provides some DataBase Services
    /// </summary>
    public class DBservices
    {
        public DBservices()
        {
        }
        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(string conString)
        {
            // read the connection string from the configuration file
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            string cStr = configuration.GetConnectionString(conString);
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        public int InsertUserToDB(User user)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();

            paramDic.Add("@Email", user.Email);
            paramDic.Add("@PasswordHash", user.PasswordHash);
            paramDic.Add("@FullName", user.FullName);
            paramDic.Add("@BirthDate", user.BirthDate);
            paramDic.Add("@Gender", user.Gender);
            paramDic.Add("@ImageUrl", string.IsNullOrEmpty(user.ImageUrl) ? DBNull.Value: user.ImageUrl);
                   
            cmd = CreateCommandWithStoredProcedureGeneral("SP_INSERT_USER_P",con,paramDic);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();

                return numEffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public int InsertUserLoginToDB(int userId)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@UserID", userId);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_INSERT_USER_LOGIN_P", con, paramDic);

            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        public User GetUserByEmail(string email)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Email", email);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_GET_USER_BY_EMAIL_P", con, paramDic);
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            try
            {
                if (dataReader.Read())
                {
                    User u = new User();
                    u.UserId = Convert.ToInt32(dataReader["UserID"]);
                    u.Email = dataReader["Email"].ToString();
                    u.PasswordHash = dataReader["PasswordHash"].ToString();
                    u.FullName = dataReader["FullName"].ToString();
                    u.BirthDate = Convert.ToDateTime(dataReader["BirthDate"]);
                    u.Gender = dataReader["Gender"].ToString();
                    u.ImageUrl = dataReader["ImageUrl"] == DBNull.Value ? "" : dataReader["ImageUrl"].ToString();
                    u.IsAdmin = Convert.ToBoolean(dataReader["IsAdmin"]);
                    u.IsLocked = Convert.ToBoolean(dataReader["IsLocked"]);
                    u.RegistrationDate = Convert.ToDateTime(dataReader["RegistrationDate"]);
                    return u;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        //---------------------------------------------------------------------------------
        // Create the SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateCommandWithStoredProcedureGeneral(String spName, SqlConnection con, Dictionary<string, object> paramDic)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            if (paramDic != null)
                foreach (KeyValuePair<string, object> param in paramDic)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);

                }

            return cmd;
        }
    }
}

