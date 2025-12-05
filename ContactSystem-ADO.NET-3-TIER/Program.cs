using Microsoft.Data.SqlClient;
using System;
using System.Data;
using Contacts_BusinessLayer;
using System.Xml.Serialization;
using Contacts_DataAccessLayer;
public class Program
{
    //---------------------Contact Tests-------------------------
    static void testFindContact(int Id) {
        clsContact contact = clsContact.FindContact(Id);
        
        if (contact != null)
        {
            Console.WriteLine("Contact Found: ");
            Console.WriteLine($"ID: {contact.ID}\n, Name: {contact.FirstName}{contact.LastName}\n," +
                $" Email: {contact.Email}\n, Phone: {contact.Phone}\n" +
                $"Address: {contact.Address},\n DateOfBirth:{contact.DateOfBirth},\n" +
                $"CountryId:{contact.CountryID},\n ImagePath:{contact.ImagePath}\n" +
                $"-------------------------------------------------------------------");
        }
        else
        {
            Console.WriteLine("Contact not found.");
        }
    }
    static void testAddContact() 
    {
        clsContact newContact = new clsContact();
        newContact.FirstName = "John";
        newContact.LastName = "Doe";
        newContact.Email = "jkdsds@test.com";
        newContact.Phone = "123-456-7890";
        newContact.Address = "123 Main St, Anytown, USA";
        newContact.DateOfBirth = new DateTime(1990, 1, 1);
        newContact.CountryID = 1;
        newContact.ImagePath = "path/to/image.jpg";
        if (newContact.Save())
        {
            Console.WriteLine("Contact Added Successfully");
        }
        else
        {
                       Console.WriteLine("Failed to add contact.");
        }
    }
    static void testUpdateContact(int Id) {
        clsContact contact1 = clsContact.FindContact(Id);
        if (contact1 != null)
        {
            contact1.FirstName = "Jaden";
            contact1.LastName = "Daniell";
            contact1.Email = "jade.carters@test.com";
            contact1.Phone = "123-456-7890";
            contact1.Address = "123 Main St, Anytown, USA";
            contact1.DateOfBirth = new DateTime(1990, 1, 1);
            contact1.CountryID = 1;
            contact1.ImagePath = "path/to/image.jpg";
            if (contact1.Save())
            {
                Console.WriteLine("Contact Updated Successfully");
            }
            else
            {
                Console.WriteLine("Failed to update contact.");
            }
        }
    }
    static void testListContacts() { }
    static void testDeleteContact(int Id) 
    {
        if (clsContact.IsContactExist(Id))
        {
            if (clsContact.DeleteContact(Id))
            {
                Console.WriteLine("Contact Deleted Successfully");
            }
            else
            {
                Console.WriteLine("Failed to delete contact.");
            }
        }
        else
        {
            Console.WriteLine("Contact Does Not Exist");
        }
    }

    static void ListContacts() { 
        DataTable dataTable = clsContact.GetAllContacts();
        Console.WriteLine("-------------Contacts List--------------");
        foreach(DataRow row in dataTable.Rows)
        {
            Console.WriteLine($"ID: {row["ContactID"]}, Name: {row["FirstName"]} {row["LastName"]}, " +
                $"Email: {row["Email"]}, Phone: {row["Phone"]}, Address: {row["Address"]}, " +
                $"DateOfBirth: {row["DateOfBirth"]}, CountryID: {row["CountryID"]}, ImagePath: {row["ImagePath"]}\n");
        }
    }
    static void testIsContactExist(int Id) { 
        if(clsContact.IsContactExist(Id))
        {
            Console.WriteLine("Contact Exists");
        }
        else
        {
            Console.WriteLine("Contact Does Not Exist");
        }
    }
    //-------------------------------------------------------------------

    //-------------------Country Tests-------------------------

    public static void testFindCountryById(int Id) {
        clsCountry country1 = clsCountry.FindCountryByID(Id);
        if (country1 != null)
        {
            Console.WriteLine("--------------Country details--------------");
            Console.WriteLine($"Country ID: {country1.CountryID},\nCountry Name: {country1.CountryName},\nCountry Code: {country1.CountryCode},\n" +
                $"Phone Code: {country1.PhoneCode}\n");
        }
        else
        {
            Console.WriteLine("Country Does Not Exist");
        }
    }
    
    public static void testFindCountryByName(string CountryName)
    {
        clsCountry country1 = clsCountry.FindCountryByName(CountryName);
        if (country1 != null)
        {
            Console.WriteLine("--------------Country details--------------");
            Console.WriteLine($"Country ID: {country1.CountryID},\nCountry Name: {country1.CountryName},\nCountry Code: {country1.CountryCode},\n" +
                $"Phone Code: {country1.PhoneCode}\n");
        }
        else
        {
            Console.WriteLine("Country Does Not Exist");
        }
    }
    
    public static void testAddCountry()
    {
        clsCountry newCountry = new clsCountry();
        newCountry.CountryName = "Egypt";
        newCountry.CountryCode = "Egy";
        newCountry.PhoneCode = "+20";
        if (newCountry.Save())
        {
            Console.WriteLine("Country Added Successfully");
        }
        else
        {
            Console.WriteLine("Failed to add country.");
        }
    }
    public static void testUpdateCountry(int Id) 
    {
        clsCountry country1 = clsCountry.FindCountryByID(Id);
        if (country1 != null)
        {
            country1.CountryName = "Germany";
            country1.CountryCode = "DE";
            country1.PhoneCode = "+49";
            if (country1.Save())
            {
                Console.WriteLine("Country Updated Successfully");
            }
            else
            {
                Console.WriteLine("Failed to update country.");
            }
        }
    }
    public static void testIsCountryExistByName(string countryName)
    {
        if (clsCountry.IsCountryExistByName(countryName))
        {
            Console.WriteLine($"Country: {countryName} Exists");
        }
        else
        {
            Console.WriteLine($"Country: {countryName} Does Not Exist");
        }
    }

    public static void testDeleteCountry(int Id)
    {
        if (clsCountry.IsCountryExistById(Id))
        {
            if(clsCountry.DeleteCountry(Id))
            {
                Console.WriteLine($"Country no {Id} deleted Successfully");
            }
            else
            {
                Console.WriteLine($"Country no {Id} Failed to be deleted");
            }
        }
    }
    public static void Main(string[] args)
    {
        //testFindContact(11);
        //testAddContact();
        //testUpdateContact(11);
        //testDeleteContact(11);
        //ListContacts();
        //testFindCountryById(7);
        //testFindCountryByName("Egypt");
        //testIsCountryExistByName("Germany");
        //testAddCountry();
        //testUpdateCountry(5);
        testDeleteCountry(9);
    }
}