using System;
using System.Collections.Generic;
using System.Text;

namespace JDA.Common.Helpers
{
    public static class Constants
    {

        //Set validation TimeDuration for token
        public const int ValidTokenDuration = 87600;
        //Temp PIN validity (in minutes) generated for forgot PIN flow
        public const string PinValidityDuration = "15";
        /// <summary>
        /// The random no minimum value
        /// </summary>
        public const int RandomNoMinValue = 100000;
        /// <summary>
        /// The random no maximum value
        /// </summary>
        public const int RandomNoMaxValue = 999999;
      
        public const string EmailUpdatePin = "maspEmailUpdatePin";
        public const string SaveTempPin = "maspSaveTempPin";
        #region SP Input Parameters

        /// <summary>
        /// 
        /// </summary>
        public const string SpIpInstId = "@InstID";

        /// <summary>
        /// 
        /// </summary>
        public const string SpIpEnable = "@Enable";

        /// <summary>
        /// 
        /// </summary>
        public const string SpIpLastUpdatedBy = "@LastUpdateBy";

        /// <summary>
        /// The input parameter for company identifier
        /// </summary>
        public const string SpIpCompanyId = "@CompanyId";
        /// <summary>
        /// The input parameter for is ble enable
        /// </summary>
        public const string SpIpIsBleEnable = "@isBleEnable";
        /// <summary>
        /// The input parameter for filter registered
        /// </summary>
        public const string SpIpFilterRegistered = "@FilterRegistered";
        /// <summary>
        /// The input parameter for filter mail sent
        /// </summary>
        public const string SpIpFilterMailSent = "@FilterMailSent";
        /// <summary>
        /// The input parameter for update by
        /// </summary>
        public const string SpIpUpdateBy = "@UpdateBy";
        /// <summary>
        /// The input parameter for update on behalf
        /// </summary>
        public const string SpIpUpdateOnBehalf = "@UpdateOnBehalf";

        /// <summary>
        /// The input parameter for module name
        /// </summary>
        public const string SpIpModuleName = "@ModuleName";
        /// <summary>
        /// The input parameter for from email
        /// </summary>
        public const string SpIpFromEmail = "@FromEmail";
        /// <summary>
        /// The input parameter for email
        /// </summary>
        public const string SpIpEmail = "@Email";
        /// <summary>
        /// The input parameter for from
        /// </summary>
        public const string SpIpFrom = "@FROM";
        /// <summary>
        /// The input parameter for to
        /// </summary>
        public const string SpIpTo = "@TO";
        /// <summary>
        /// The input parameter for cc
        /// </summary>
        public const string SpIpCc = "@CC";
        /// <summary>
        /// The input parameter for b cc
        /// </summary>
        public const string SpIpBCc = "@BCC";
        /// <summary>
        /// The input parameter for subject
        /// </summary>
        public const string SpIpSubject = "@Subject";
        /// <summary>
        /// The input parameter for body
        /// </summary>
        public const string SpIpBody = "@Body";
        /// <summary>
        /// The input parameter for body format
        /// </summary>
        public const string SpIpBodyFormat = "@BodyFormat";
        /// <summary>
        /// The input parameter for message identifier
        /// </summary>
        public const string SpIpMessageId = "@MessageId";
        /// <summary>
        /// The input parameter for card holder identifier
        /// </summary>
        public const string SpIpCardHolderId = "@CardHolderId";
        /// <summary>
        /// The input parameter for pin
        /// </summary>
        public const string SpIpPin = "@PIN";
        /// <summary>
        /// The input parameter for is email changed
        /// </summary>
        public const string SpIpIsEmailChanged = "@IsEmailChanged";
        /// <summary>
        /// The input parameter for is company changed
        /// </summary>
        public const string SpIpIsCompanyChanged = "@IsCompanyChanged";
        /// <summary>
        /// The attachment file name
        /// </summary>
        public const string AttachmentFileName = "@AttachmentFileName";
        /// <summary>
        /// The validity
        /// </summary>
        public const string Validity = "@Validity";
        /// <summary>
        /// The maximum try count
        /// </summary>
        public const string MaxTryCount = "@MaxTryCount";
        /// <summary>
        /// The do not send e mail
        /// </summary>
        public const string DoNotSendEMail = "@DoNotSendEMail";

        /// <summary>
        /// The sp ip is status
        /// </summary>
        public const string SpIpIsStatus = "@Status";


        #endregion
    }
}
