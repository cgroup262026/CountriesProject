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


        //===============================================GAME===============================================

        //--------------------------------------------------------------------------------------------------
        // This method inserts a student to the students table 
        // the model CCEC - Connect, Create Command, Execute, Close
        //--------------------------------------------------------------------------------------------------
        public int InsertCountriesToDB(List<Country> countries) //לשנות
        {
            SqlConnection con = null;
            int totalEffected = 0;

            try
            {
                con = connect("myProjDB"); // פתיחת החיבור פעם אחת בלבד!

                foreach (Country country in countries)
                {
                    // 1. שמירת המדינה
                    Dictionary<string, object> paramDic = new Dictionary<string, object>();
                    paramDic.Add("@Alpha3Code", country.Alpha3Code ?? (object)DBNull.Value);
                    paramDic.Add("@Alpha2Code", country.Alpha2Code ?? (object)DBNull.Value);
                    paramDic.Add("@CountryName", country.CountryName ?? (object)DBNull.Value);
                    paramDic.Add("@Capital", country.Capital ?? (object)DBNull.Value);
                    paramDic.Add("@RegionName", country.Region?.RegionName ?? (object)DBNull.Value);
                    paramDic.Add("@SubRegion", country.SubRegion ?? (object)DBNull.Value);
                    paramDic.Add("@Population", country.Population);
                    paramDic.Add("@Area", country.Area);
                    paramDic.Add("@FlagUrl", country.FlagUrl ?? (object)DBNull.Value);

                    SqlCommand cmd = CreateCommandWithStoredProcedureGeneral("SP_INSERT_COUNTRY", con, paramDic);
                    totalEffected += cmd.ExecuteNonQuery();

                    // 2. שמירת השפות של אותה מדינה
                    if (country.Languages != null)
                    {
                        foreach (Language lang in country.Languages)
                        {
                            Dictionary<string, object> langParams = new Dictionary<string, object>();
                            langParams.Add("@Alpha3Code", country.Alpha3Code);
                            langParams.Add("@LanguageName", lang.LanguageName);

                            SqlCommand cmdLang = CreateCommandWithStoredProcedureGeneral("SP_ADD_COUNTRY_LANGUAGE", con, langParams);
                            cmdLang.ExecuteNonQuery();
                        }
                    }

                    // 3. שמירת המטבעות של אותה מדינה
                    if (country.Currencies != null)
                    {
                        foreach (Currency curr in country.Currencies)
                        {
                            Dictionary<string, object> currParams = new Dictionary<string, object>();
                            currParams.Add("@Alpha3Code", country.Alpha3Code);
                            currParams.Add("@CurrencyCode", curr.CurrencyCode);
                            currParams.Add("@CurrencyName", curr.CurrencyName ?? (object)DBNull.Value);
                            currParams.Add("@CurrencySymbol", curr.Symbol ?? (object)DBNull.Value);

                            SqlCommand cmdCurr = CreateCommandWithStoredProcedureGeneral("SP_ADD_COUNTRY_CURRENCY", con, currParams);
                            cmdCurr.ExecuteNonQuery();
                        }
                    }

                    // 4. שמירת הגבולות של אותה מדינה
                    if (country.Borders != null)
                    {
                        foreach (Country border in country.Borders)
                        {
                            Dictionary<string, object> borderParams = new Dictionary<string, object>();
                            borderParams.Add("@Alpha3Code", country.Alpha3Code);
                            borderParams.Add("@BorderAlpha3Code", border.Alpha3Code);

                            SqlCommand cmdBorder = CreateCommandWithStoredProcedureGeneral("SP_ADD_COUNTRY_BORDER", con, borderParams);
                            cmdBorder.ExecuteNonQuery();
                        }
                    }
                }

                return totalEffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close(); // סגירת החיבור בסיום כל הלולאה
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

