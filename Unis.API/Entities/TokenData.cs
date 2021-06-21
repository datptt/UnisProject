using System;
namespace Unis.API
{
    public class TokenData
    {
        public string UserName { get; set; }

        public string UserID { get; set; }

        public string SessionID { get; set; }

        public string RoleName { get; set; }

        public bool IsAdministrator { get; set; }
    }
}
