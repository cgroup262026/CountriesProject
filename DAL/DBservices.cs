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

        //===============================================USER===============================================//

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
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
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

        public List<User> GetAllUsersFromDB()
        {
            SqlConnection con;
            SqlCommand cmd;
            List<User> users = new List<User>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedureGeneral("SP_GET_ALL_USERS_P", con, null); // create the command

            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            try
            {
                while (dataReader.Read())
                {
                    User u = new User();
                    u.UserId = Convert.ToInt32(dataReader["UserID"]);
                    u.Email = dataReader["Email"].ToString();
                    u.FullName = dataReader["FullName"].ToString();
                    u.BirthDate = Convert.ToDateTime(dataReader["BirthDate"]);
                    u.Gender = dataReader["Gender"].ToString();
                    u.ImageUrl = dataReader["ImageUrl"] == DBNull.Value ? "" : dataReader["ImageUrl"].ToString();
                    u.IsAdmin = Convert.ToBoolean(dataReader["IsAdmin"]);
                    u.IsLocked = Convert.ToBoolean(dataReader["IsLocked"]);
                    u.RegistrationDate = Convert.ToDateTime(dataReader["RegistrationDate"]);

                    users.Add(u);
                }
                return users;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //--------------------------------------------------------------------------------------------------
        // This method Reads a single user by ID 
        //--------------------------------------------------------------------------------------------------
        public User GetUserByIdFromDB(int id)
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
            paramDic.Add("@UserID", id);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_GET_USER_BY_ID_P", con, paramDic);

            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            try
            {
                if (dataReader.Read())
                {
                    User u = new User();
                    u.UserId = Convert.ToInt32(dataReader["UserID"]);
                    u.Email = dataReader["Email"].ToString();
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
                if (con != null)
                    con.Close();
            }
        }

        //--------------------------------------------------------------------------------------------------
        // This method Updates a user in the database
        //--------------------------------------------------------------------------------------------------
        public int UpdateUserInDB(User user)
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

            paramDic.Add("@UserID", user.UserId);
            paramDic.Add("@FullName", user.FullName);
            paramDic.Add("@BirthDate", user.BirthDate);
            paramDic.Add("@Gender", user.Gender);
            paramDic.Add("@ImageUrl", string.IsNullOrEmpty(user.ImageUrl) ? DBNull.Value : user.ImageUrl);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_UPDATE_USER_P", con, paramDic);

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
                    con.Close();
            }
        }

        //--------------------------------------------------------------------------------------------------
        // This method Deletes (or Locks) a user in the database 
        //--------------------------------------------------------------------------------------------------
        public int DeleteOrLockUserInDB(int id)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@UserID", id);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_DELETE_USER_P", con, paramDic); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public int UpdateUserLockStatusInDB(int id, bool isLocked)
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
            paramDic.Add("@UserID", id);
            paramDic.Add("@IsLocked", isLocked);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_UPDATE_USER_LOCK_STATUS_P", con, paramDic);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public int UpdateUserHobbiesInDB(int userID, List<string> hobbies)
        {
            SqlConnection con = null;
            int numEffected = 0;

            try
            {
                con = connect("myProjDB");

                Dictionary<string, object> clearParamDic = new Dictionary<string, object>();
                clearParamDic.Add("@UserID", userID);

                SqlCommand clearCmd = CreateCommandWithStoredProcedureGeneral("SP_CLEAR_USER_HOBBIES_P", con, clearParamDic);
                clearCmd.ExecuteNonQuery();

                foreach (string hobby in hobbies)
                {
                    Dictionary<string, object> paramDic = new Dictionary<string, object>();
                    paramDic.Add("@UserID", userID);
                    paramDic.Add("@HobbyName", hobby);

                    SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_ADD_USER_HOBBY_P", con, paramDic);
                    numEffected += cmd.ExecuteNonQuery();
                }

                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //===============================================USER EXTRA===============================================//

        public List<string> GetUserHobbiesFromDB(int userId)
        {
            SqlConnection con;
            SqlCommand cmd;
            List<string> hobbies = new List<string>();

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
            cmd = CreateCommandWithStoredProcedureGeneral("SP_GET_USER_HOBBIES_P", con, paramDic);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    hobbies.Add(dataReader["HobbyName"].ToString());
                }
                return hobbies;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public List<UserLanguage> GetUserLanguagesFromDB(int userId)
        {
            SqlConnection con;
            SqlCommand cmd;
            List<UserLanguage> languages = new List<UserLanguage>();

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
            cmd = CreateCommandWithStoredProcedureGeneral("SP_GET_USER_LANGUAGES_P", con, paramDic);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    UserLanguage language = new UserLanguage();
                    language.LanguageName = dataReader["LanguageName"].ToString();
                    language.ProficiencyLevel = Convert.ToInt32(dataReader["ProficiencyLevel"]);
                    languages.Add(language);
                }
                return languages;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public List<string> GetUserRegionsFromDB(int userId)
        {
            SqlConnection con;
            SqlCommand cmd;
            List<string> regions = new List<string>();

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
            cmd = CreateCommandWithStoredProcedureGeneral("SP_GET_USER_REGIONS_P", con, paramDic);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    regions.Add(dataReader["RegionName"].ToString());
                }
                return regions;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public List<string> GetUserTravelPreferencesFromDB(int userId)
        {
            SqlConnection con;
            SqlCommand cmd;
            List<string> preferences = new List<string>();

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
            cmd = CreateCommandWithStoredProcedureGeneral("SP_GET_USER_TRAVEL_PREFERENCES_P", con, paramDic);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    preferences.Add(dataReader["PreferenceName"].ToString());
                }
                return preferences;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public int GetUserTotalScoreFromDB(int userId)
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
            cmd = CreateCommandWithStoredProcedureGeneral("SP_GET_USER_TOTAL_SCORE_P", con, paramDic);

            try
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //===============================================COUNTRY===============================================//

        public int InsertRegionToDB(string regionName)
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
            paramDic.Add("@RegionName", regionName);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_INSERT_REGION_P", con, paramDic);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public int InsertCurrencyToDB(string currencyName)
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
            paramDic.Add("@CurrencyName", currencyName);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_INSERT_CURRENCY_P", con, paramDic);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public int InsertLanguageToDB(string languageName)
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
            paramDic.Add("@LanguageName", languageName);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_INSERT_LANGUAGE_P", con, paramDic);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public int InsertCountryToDB(Country country)
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
            paramDic.Add("@Alpha3Code", country.Alpha3Code);
            paramDic.Add("@Alpha2Code", country.Alpha2Code);
            paramDic.Add("@CountryName", country.Name);
            paramDic.Add("@Capital", string.IsNullOrEmpty(country.Capital) ? DBNull.Value : country.Capital);
            paramDic.Add("@RegionName", country.Region);
            paramDic.Add("@Subregion", string.IsNullOrEmpty(country.SubRegion) ? DBNull.Value : country.SubRegion);
            paramDic.Add("@Population", country.Population);
            paramDic.Add("@Area", country.Area);
            paramDic.Add("@FlagUrl", string.IsNullOrEmpty(country.FlagUrl) ? DBNull.Value : country.FlagUrl);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_INSERT_COUNTRY_P", con, paramDic);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public int InsertCountryCurrencyToDB(string alpha3Code, string currencyName)
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
            paramDic.Add("@Alpha3Code", alpha3Code);
            paramDic.Add("@CurrencyName", currencyName);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_INSERT_COUNTRY_CURRENCY_P", con, paramDic);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public int InsertCountryLanguageToDB(string alpha3Code, string languageName)
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
            paramDic.Add("@Alpha3Code", alpha3Code);
            paramDic.Add("@LanguageName", languageName);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_INSERT_COUNTRY_LANGUAGE_P", con, paramDic);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public int InsertCountryBorderToDB(string alpha3Code, string borderAlpha3Code)
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
            paramDic.Add("@Alpha3Code", alpha3Code);
            paramDic.Add("@BorderAlpha3Code", borderAlpha3Code);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_INSERT_COUNTRY_BORDER_P", con, paramDic);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //===============================================HOBBIES===============================================//

        public List<string> GetAllHobbiesFromDB()
        {
            SqlConnection con;
            SqlCommand cmd;
            List<string> hobbies = new List<string>();

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            cmd = CreateCommandWithStoredProcedureGeneral("SP_GET_ALL_HOBBIES_P", con, null);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    hobbies.Add(dataReader["HobbyName"].ToString());
                }

                return hobbies;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //===============================================TRIVIA GAME===============================================//

        public List<TriviaQuestion> GetRandomTriviaQuestionsFromDB()
        {
            SqlConnection con;
            SqlCommand cmd;
            List<TriviaQuestion> questions = new List<TriviaQuestion>();

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            cmd = CreateCommandWithStoredProcedureGeneral("SP_GET_RANDOM_TRIVIA_QUESTIONS_P", con, null);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    TriviaQuestion question = new TriviaQuestion();
                    question.QuestionId = Convert.ToInt32(dataReader["QuestionID"]);
                    question.QuestionText = dataReader["QuestionText"].ToString();
                    question.CorrectAnswer = dataReader["CorrectAnswer"].ToString();
                    question.WrongAnswer1 = dataReader["WrongAnswer1"].ToString();
                    question.WrongAnswer2 = dataReader["WrongAnswer2"].ToString();
                    question.WrongAnswer3 = dataReader["WrongAnswer3"].ToString();
                    questions.Add(question);
                }
                return questions;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public int InsertTriviaResultToDB(TriviaResult result)
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
            paramDic.Add("@UserID", result.UserID);
            paramDic.Add("@Score", result.Score);
            paramDic.Add("@CorrectAnswers", result.CorrectAnswers);
            paramDic.Add("@TotalQuestions", result.TotalQuestions);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_INSERT_TRIVIA_RESULT_P", con, paramDic);

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

        //===============================================MEMORY GAME===============================================//

        public List<Country> GetMemoryGameCountriesFromDB()
        {
            SqlConnection con;
            SqlCommand cmd;
            List<Country> countries = new List<Country>();

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            cmd = CreateCommandWithStoredProcedureGeneral("SP_GET_MEMORY_GAME_COUNTRIES_P", con, null);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    Country country = new Country();
                    country.Alpha3Code = dataReader["Alpha3Code"].ToString();
                    country.Name = dataReader["CountryName"].ToString();
                    country.FlagUrl = dataReader["FlagUrl"].ToString();
                    countries.Add(country);
                }
                return countries;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertMemoryGameResultToDB(MemoryGameResult result)
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
            paramDic.Add("@UserID", result.UserId);
            paramDic.Add("@Points", result.Points);
            paramDic.Add("@Moves", result.Moves);
            paramDic.Add("@MatchedPairs", result.MatchedPairs);
            paramDic.Add("@IsCompleted", result.IsCompleted);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_INSERT_MEMORY_GAME_RESULT_P", con, paramDic);

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

        //===============================================USER SAVED COUNTRIES===============================================//

        public int AddUserSavedCountryToDB(int userId, string alpha3Code, string listType)
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
            paramDic.Add("@Alpha3Code", alpha3Code);
            paramDic.Add("@ListType", listType);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_ADD_USER_SAVED_COUNTRY_P", con, paramDic);

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

        public int DeleteUserSavedCountryFromDB(int userId, string alpha3Code)
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
            paramDic.Add("@Alpha3Code", alpha3Code);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_DELETE_USER_SAVED_COUNTRY_P", con, paramDic);

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

        public List<Country> GetUserSavedCountriesFromDB(int userId, string listType)
        {
            SqlConnection con;
            SqlCommand cmd;
            List<Country> countries = new List<Country>();

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
            paramDic.Add("@ListType", listType);

            cmd = CreateCommandWithStoredProcedureGeneral("SP_GET_USER_SAVED_COUNTRIES_P", con, paramDic);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Country country = new Country();
                    country.Alpha3Code = dataReader["Alpha3Code"].ToString();
                    country.Name = dataReader["CountryName"].ToString();
                    country.FlagUrl = dataReader["FlagUrl"].ToString();
                    countries.Add(country);
                }

                return countries;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
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

