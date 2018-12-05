using System.Collections.Generic;

namespace JDA.Common.Helpers
{
    public static class ServiceConstants
    {
        public const string DateFormat = "yyyy-MM-ddTHH:mm:ss";
        public const string DateTimeFormat = "yyyy-MM-ddTHH:mm";
        public const string LongDateFormat = "yyyy-MM-ddTHH:mm:ss.fff";
        public const string UserId = "UserId";
        public const string SecurityToken = "SecurityToken";
        public const int NolongerRegistered = 60134;
        public static readonly int ValidTempPinExists = 60131;
        private static Dictionary<int, string> _errorCodes;
        public const string ServiceRequestFolder = "ServiceRequestFolder";

        /// <summary>
        /// Gets the error codes.
        /// </summary>
        /// <value>
        /// The error codes.
        /// </value>
        public static Dictionary<int, string> ErrorCodes
        {
            get
            {
                if (_errorCodes == null)
                {
                    _errorCodes = new Dictionary<int, string>
                    {
                        {1001, ApiMessages.ValueNotProvided},
                        {1002, ApiMessages.InternalError},
                        {60134, ApiMessages.NoLongerRegistered},
                        {60080, ApiMessages.EmailNotExist },
                    };
                }
                return _errorCodes;
            }
        }
    }
}
