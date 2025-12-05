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
        
        public static int AddCountry(string countryname, string countrycode,string phonecode)
        {
            int CountryID = -1;
            string query = "Insert into Countries (CountryName, Code, PhoneCode)" +
                "values (@CountryName, @Code, @PhoneCode)" +
                "SELECT SCOPE_IDENTITY()";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CountryName", countryname);
                    command.Parameters.AddWithValue("@Code", countrycode);
                    command.Parameters.AddWithValue("@PhoneCode", phonecode);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                            CountryID = InsertedID;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error {ex.Message}");
                        CountryID = -1;
                    }
                }
            }
            return CountryID;

        }
        public static bool UpdateCountry(int Id, string countryname, string CountryCode, string phoneCode)
        {
            int rowsAffected = 0;
            string query = "Update Countries " +
                "set CountryName=@CountryName," +
                " Code=@Code," +
                " PhoneCode=@PhoneCode" +
                " where CountryID=@CountryID";
            using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CountryID", Id);
                    command.Parameters.AddWithValue("@CountryName", countryname);
                    command.Parameters.AddWithValue("@Code", CountryCode);
                    command.Parameters.AddWithValue("@PhoneCode", phoneCode);
                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error {ex.Message}");
                        rowsAffected = 0;
                    }
                }
            }
            return rowsAffected > 0;
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

        public static bool IsCountryExistById(int Id) 
        {
            bool IsFound = false;
            string query = "Select 1 from Countries where CountryID = @Id";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) { 
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    try {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        IsFound = (result != null);
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine($"Error, {ex.Message}");
                        IsFound = false;
                    }
                    return IsFound;
                }
            }
        }
        public static bool DeleteCountry(int Id)
        {
            int rowsAffected = 0;
            string query = "Delete from Countries where CountryID=@CountryId";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CountryId", Id);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error {ex.Message}");
                        rowsAffected = 0;
                    }
                }
            }
            return rowsAffected > 0;
        }   
            
    }
}
