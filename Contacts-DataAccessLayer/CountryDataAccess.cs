using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts_DataAccessLayer
{
    public class clsCountryDataAccess
    {
        public static bool GetCountryInfoById(int CountryID, ref string CountryName, ref string CountryCode, ref string PhoneCode)
        {
            bool isFound = false;
            string query = "Select * from Countries where CountryID=@CountryID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CountryID", CountryID);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                CountryName = (string)reader["CountryName"];
                                CountryCode = (string)reader["Code"];
                                PhoneCode = (string)reader["PhoneCode"];

                                isFound = true;
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Error {ex.Message}");
                        isFound = false;
                    }

                }
            }
            return isFound;
        }


        public static bool GetCountryInfoByName(ref int CountryID, string COUNTRYNAME, ref string CountryCode, ref string PhoneCode)
        {
            bool isFound = false;
            string query = "Select * from Countries where CountryName = @Name";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", COUNTRYNAME);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CountryID = (int)reader["CountryID"];
                                CountryCode = (string)reader["Code"];
                                PhoneCode = (string)reader["PhoneCode"];
                                isFound = true;
                            }
                        }
                    }
                    catch(Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
                }
            }
            return isFound;
        }
        
        public static bool IsCountryExistByName(String Name)
        {
            bool IsFound = false;
            string query = "Select 1 from Countries where CountryName=@CountryName";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CountryName", Name);
                   
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        IsFound = (result != null);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error {ex.Message}");
                        IsFound = false;
                    }
                    
                }
                
            }
            return IsFound;
        }
        
        
    
    }
}
