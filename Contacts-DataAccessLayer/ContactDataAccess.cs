using Microsoft.Data.SqlClient;
using System.Data;

namespace Contacts_DataAccessLayer
{
    public class clsContactDataAccess
    {
        public static bool GetContactInfoById(int ID, ref string FirstName, ref string LastName, ref string Email, ref string Phone,
                ref string Address, ref DateTime DateOfBirth, ref int CountryID, ref string ImagePath)
        {
            bool isFound = false;


            string query = "SELECT * FROM Contacts WHERE ContactID = @ContactId ";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ContactId", ID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            isFound = true;
                            FirstName = (string)reader["FirstName"];
                            LastName = (string)reader["LastName"];
                            Email = (string)reader["Email"];
                            Phone = (string)reader["Phone"];
                            Address = (string)reader["Address"];
                            DateOfBirth = (DateTime)reader["DateOfBirth"];
                            CountryID = (int)reader["CountryID"];
                            ImagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : ImagePath = "";

                        }
                        else
                        {
                            isFound = false;
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        isFound = false;
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }
                }
            }
            return isFound;
        }
        public static int AddNewContact(string FirstName, string LastName, string Email
            , string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            int ContactID = -1;
            string query = @"INSERT INTO Contacts
                            (FirstName, LastName, Email, Phone, Address, DateOfBirth, CountryID, ImagePath)
                            VALUES
                            (@FirstName, @LastName, @Email, @Phone, @Address, @DateOfBirth, @CountryID, @ImagePath);
                            SELECT CAST(SCOPE_IDENTITY() AS INT);";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100).Value = (object)FirstName ?? DBNull.Value;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar, 100).Value = (object)LastName ?? DBNull.Value;
                command.Parameters.Add("@Email", SqlDbType.NVarChar, 200).Value = (object)Email ?? DBNull.Value;
                command.Parameters.Add("@Phone", SqlDbType.NVarChar, 50).Value = (object)Phone ?? DBNull.Value;
                command.Parameters.Add("@Address", SqlDbType.NVarChar, 500).Value = (object)Address ?? DBNull.Value;
                command.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = DateOfBirth;
                command.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
                command.Parameters.Add("@ImagePath", SqlDbType.NVarChar, 500).Value = (object)ImagePath ?? DBNull.Value;
                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    {
                        ContactID = InsertedID;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Issue happened");
                }
            }
            return ContactID;
        }

        public static bool UpdateContact(int ID, string FirstName, string LastName, string Email
            , string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            int rowsAffected = 0;
            
            string query = @"Update Contacts
                            Set FirstName = @FirstName,
                                LastName = @LastName,
                                Email = @Email,
                                Phone = @Phone,
                                Address = @Address,
                                DateOfBirth = @DateOfBirth,
                                CountryID = @CountryID,
                                ImagePath = @ImagePath
                            Where ContactID = @ContactId";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection)) 
                {
                    command.Parameters.AddWithValue("@ContactId", ID);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@CountryID", CountryID);
                    if (ImagePath != "")
                    {
                        command.Parameters.AddWithValue("@ImagePath", ImagePath);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
                    }
                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Issue happened");
                        return false;
                    }

                }
                    
            }
            return (rowsAffected > 0);
        }


        public static bool DeleteContactById(int Id) 
        {
            int rowsAffected = 0;
           
            string query = "DELETE FROM Contacts WHERE ContactID = @ContactId";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) {
                using (SqlCommand command = new SqlCommand(query, connection)) 
                {
                    command.Parameters.AddWithValue("@ContactId", Id);
                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Issue happened");
                        return false;
                    }
                }
            }
            return (rowsAffected > 0);
        }

        public static DataTable GetAllContacts()
        {
            DataTable dt = new DataTable();
            
            string query = "SELECT * FROM Contacts";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) 
            {
                using (SqlCommand command = new SqlCommand(query, connection)) 
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Issue happened");
                        return dt;
                    }
                }
            }
            return dt;
        }
        public static bool IsContactExist(int ID) 
        {
            bool exists = false;
            
            string query = "SELECT Found = 1 FROM Contacts where ContactID = @CONTACTID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CONTACTID", ID);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            exists = true;
                        }
                        else
                        {
                            exists = false;
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Issue happened");
                        return false;
                    }
                }
            }
            return exists;
        }
    }
}
