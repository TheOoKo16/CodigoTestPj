using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Infra.Helper;
using Data.ViewModels;

namespace Infra.Helper
{
   public class AccountApiRequestHelper
    {
        public static async Task<UserTokenViewModel> gettoken(user userdata)
        {
            string url = $"api/account/Authenticate";
            var token = await ApiRequest<user,UserTokenViewModel>.PostDiffRequest(url,userdata);

            return token;
        }
    }
}
