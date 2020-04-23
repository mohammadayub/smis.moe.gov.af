using Clean.API.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.API.Results
{
    public class LoginResponse
    {
        public bool Valid { get; set; }
        public string Token { get; set; }
        public UserInfoModel UserInfo { get; set; }
    }
}
