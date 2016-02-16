using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBagService.UserService
{
    public class BaseUserService
    {
        MkmEntities entity = new MkmEntities();

        public User LoginUser(string username, string password)
        {
            var user = entity.User.FirstOrDefault(p => p.AccountName == username);
            return user;
        }
    }
}
