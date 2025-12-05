using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts_DataAccessLayer;

namespace Contacts_BusinessLayer
{
    public class clsCountry
    {
        //--properties
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string PhoneCode { get; set; }
        public enum enMode { AddNew, Update }
        public enMode Mode { get; set; }
        //--constructors
        public clsCountry() {
            CountryID = -1;
            CountryName = "";
            CountryCode = "";
            PhoneCode = "";
            Mode = enMode.AddNew;
        }
        private clsCountry(int countryID, string countryName, string countryCode, string phoneCode)
        {
            CountryID = countryID;
            CountryName = countryName;
            CountryCode = countryCode;
            PhoneCode = phoneCode;
            Mode = enMode.Update;
        }
        public static clsCountry FindCountryByID(int ID)
        {
            string CountryName = "", CountryCode ="", PhoneCode = "";
            if (Contacts_DataAccessLayer.clsCountryDataAccess.GetCountryInfoById(ID, ref CountryName, ref CountryCode, ref PhoneCode))
            {
                return new clsCountry(ID, CountryName, CountryCode, PhoneCode);
            }
            else
            {
                return null;
            }
        }
        
        public static clsCountry FindCountryByName(string countryName) {
            int CountryID = -1;
            string CountryCode = "", PhoneCode = "";
            if (clsCountryDataAccess.GetCountryInfoByName(ref CountryID, countryName, ref CountryCode, ref PhoneCode))
            {
                return new clsCountry(CountryID, countryName,CountryCode,PhoneCode);
            }
            else
            {
                return null;
            }
        }
        public static bool IsCountryExistByName(string CountryName)
        {
            return clsCountryDataAccess.IsCountryExistByName(CountryName);
        }
        public static bool IsCountryExistById(int Id)
        {
            return clsCountryDataAccess.IsCountryExistById(Id);
        }
        private bool _AddCountry()
        {
            this.CountryID = clsCountryDataAccess.AddCountry(this.CountryName, this.CountryCode,this.PhoneCode);
            return this.CountryID != -1;
        }
        private bool _UpdateCountry()
        {
            return clsCountryDataAccess.UpdateCountry(this.CountryID, this.CountryName, this.CountryCode, this.PhoneCode);
        }
        public bool Save()
        {
            switch (Mode) {
                case enMode.AddNew:

                    if (_AddCountry()) 
                       { 
                          Mode = enMode.Update;
                          return true;
                        }
                        return false;
                case enMode.Update:
                    return _UpdateCountry();
                    
            }
            return false;
        }
        
        public static bool DeleteCountry(int CountryID)
        {
            return clsCountryDataAccess.DeleteCountry(CountryID);
        }
    }
}
