using System;
using System.Data;
using Contacts_DataAccessLayer;
namespace Contacts_BusinessLayer
{
    public class clsContact
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImagePath { get; set; }
        public int CountryID { get; set; }
        public enum enMode { AddNew, Update }
        public enMode Mode { get; set; }
        public clsContact() { 
            ID = -1;
            FirstName = "";
            LastName = "";
            Email = "";
            Phone = "";
            Address = "";
            DateOfBirth = DateTime.Now;
            CountryID = -1;
            ImagePath = "";
            Mode = enMode.AddNew;
        }
        private clsContact(int Id, string firstName, string lastName, string email, string phone,
            string address, DateTime dateOfBirth, int countryID, string imagePath)
        {
            ID = Id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
            DateOfBirth = dateOfBirth;
            CountryID = countryID;
            ImagePath = imagePath;
            Mode = enMode.Update;
        }
        public static clsContact FindContact(int ID)
        {
            string FirstName = "", LastName = "", Email="",Phone="", Address="", ImagePath="" ;
            DateTime DateOfBirth = DateTime.Now;
            int CountryID = -1;
            if (clsContactDataAccess.GetContactInfoById(ID,ref FirstName,ref LastName,ref Email,ref Phone,
                ref Address,ref DateOfBirth, ref CountryID, ref ImagePath))
            {
                return new clsContact(ID, FirstName,LastName,Email,Phone, Address,DateOfBirth,CountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }
        private bool _AddContact()
        {
            this.ID = clsContactDataAccess.AddNewContact(this.FirstName, this.LastName
                , this.Email, this.Phone, this.Address, this.DateOfBirth, this.CountryID, this.ImagePath);
            return (this.ID != -1);
        }
        
       private bool _UpdateContact()
        {
            return clsContactDataAccess.UpdateContact(this.ID, this.FirstName, this.LastName
                , this.Email, this.Phone, this.Address, this.DateOfBirth, this.CountryID, this.ImagePath);
        }
       public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddContact())
                    {
                        Mode = enMode.Update;
                        return true;
                    }else { return false; }
                case enMode.Update:
                    return _UpdateContact();
            }
            return false;
        }
       public static bool DeleteContact(int ID)
        {
            return clsContactDataAccess.DeleteContactById(ID);
        }

        public static DataTable GetAllContacts() {
            return clsContactDataAccess.GetAllContacts();
        }
        public static bool IsContactExist(int ID)
        {
            return clsContactDataAccess.IsContactExist(ID);
        }
    }
}
