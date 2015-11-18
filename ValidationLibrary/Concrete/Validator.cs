using DataAccess.Abstract;
using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ValidationLibrary
{
    public class Validator : IValidator
    {
        Regex textRgx = new Regex(@"^[a-zA-Zа-яА-ЯЄєЇїІі\- ]+$");
        Regex mobileRgx = new Regex(@"^\+\d{12}$");
        Regex nameRgx = new Regex(@"^[A-ZА-ЯЄЇІ][a-zа-яєїі]*([\- ]+[A-ZА-ЯЄЇІ][a-zа-яєїі]+)*$");
        Regex textWithDigRgx = new Regex(@"^[\w\-_. ]+$");
        Regex emailRgx = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

        public bool isAvailableLogin(string login)
        {
            IUserManager manager = new DbUserManager();
            return manager.IsAvailableLogin(login);
        }

        public bool isValidLogin(string login)
        {
            return isValidTextWithDigits(login);
        }

        public bool isValidPassword(string password)
        {
            return !string.IsNullOrEmpty(password);
        }

        public bool isEqualPasswordAndConfirm(string password, string confirm)
        {
            return string.Equals(password, confirm);
        }

        public bool isValidName(string name)
        {
            return nameRgx.IsMatch(name);
        }

        public bool isValidMobile(string mobile)
        {
            return mobileRgx.IsMatch(mobile);
        }

        public bool isValidEmail(string email)
        {
            return emailRgx.IsMatch(email);
        }

        public bool isValidBirth(string birth)
        {
            DateTime birthday;
            if (DateTime.TryParse(birth, out birthday))
            {
                if (birthday.Year > 1940 &&
                    birthday.Year < (DateTime.Now.Year - 2))
                {
                    return true;
                }
            }
            return false;
        }

        public bool isValidText(string text)
        {
            return textRgx.IsMatch(text);
        }

        public bool isValidCountry(string country)
        {
            return isValidText(country);
        }

        public bool isValidTextWithDigits(string text)
        {
            return textWithDigRgx.IsMatch(text);
        }
    }
}
