using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace JDA.Entities.Constants
{
    public static class Validation
    {
        public static bool ValidateEmail(string emailId)
        {
            if (String.IsNullOrEmpty(emailId))
            {
                return false;
            }

            emailId = Regex.Replace(emailId, "[^\x0d\x0a\x20-\x7e\t]", "A");

            try
            {
                return Regex.IsMatch(emailId,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public static bool ValidateNotMandatoryEmail(string emailId)
        {
            if (String.IsNullOrEmpty(emailId))
            {
                return true;
            }

            emailId = Regex.Replace(emailId, "[^\x0d\x0a\x20-\x7e\t]", "A");

            try
            {
                return Regex.IsMatch(emailId,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public static bool ValidateMobile(string mobileNo)
        {
            if (String.IsNullOrEmpty(mobileNo))
            {
                return false;
            }
            try
            {
                return Regex.IsMatch(mobileNo, @"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})?\s*$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public static bool ValidateNotMandatoryMobile(string mobileNo)
        {
            if (String.IsNullOrEmpty(mobileNo))
            {
                return true;
            }
            try
            {
                return Regex.IsMatch(mobileNo, @"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})?\s*$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public static bool ValidateNumber(string number)
        {
            if (String.IsNullOrEmpty(number))
            {
                return false;
            }
            try
            {
                return Regex.IsMatch(number, @"^[0-9]\d*$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool ValidateDateTimeFormat(string dateTime)
        {
            string format = "yyyy-MM-ddTHH:mm";
            if (string.IsNullOrWhiteSpace(dateTime))
            {
                return false;
            }
            else
            {
                DateTime dateValue = new DateTime();
                if (DateTime.TryParseExact(dateTime, format, new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static bool ValidateTimeWithSecondFormat(string dateTime)
        {
            string format = "yyyy-MM-ddTHH:mm:ss";
            if (string.IsNullOrWhiteSpace(dateTime))
            {
                return false;
            }
            else
            {
                DateTime dateValue = new DateTime();
                if (DateTime.TryParseExact(dateTime, format, new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static bool ValidateTimeWithSecondNmFormat(string dateTime)
        {
            string format = "yyyy-MM-ddTHH:mm:ss";
            if (string.IsNullOrWhiteSpace(dateTime))
            {
                return true;
            }
            else
            {
                DateTime dateValue = new DateTime();
                if (DateTime.TryParseExact(dateTime, format, new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static bool ValidateCardNumber(string cardNumber)
        {
            try
            {
                return Regex.IsMatch(cardNumber, @"^[0-9-]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public static bool ValidateGuid(string guid)
        {
            if (!string.IsNullOrWhiteSpace(guid))
            {
                try
                {
                    var aa = Regex.Split(guid, @"(\.)");
                    return Regex.IsMatch(aa[0], @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        public static bool ValidateNumerics(List<int> numbers)
        {
            foreach (var number in numbers)
            {
                if (String.IsNullOrEmpty(number.ToString()))
                {
                    return false;
                }
                try
                {
                    return Regex.IsMatch(number.ToString(), @"^[0-9]\d*$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
