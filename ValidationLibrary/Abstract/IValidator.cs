using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationLibrary
{
    public interface IValidator
    {
        bool isAvailableLogin(string login);
        bool isValidLogin(string login);
        bool isValidPassword(string password);
        bool isEqualPasswordAndConfirm(string password, string confirm);
        bool isValidName(string name);
        bool isValidMobile(string mobile);
        bool isValidEmail(string email);
        bool isValidBirth(string birth);
        bool isValidCountry(string country);
        bool isValidText(string text);
        bool isValidTextWithDigits(string text);
    }
}
