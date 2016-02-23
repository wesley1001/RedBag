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

        public User GetUserByUserName(string username)
        {
            return entity.User.FirstOrDefault(p => p.AccountName == username);
        }

        public void SaveUserSignInLog(User user,string ipAddress)
        {
            entity.LoginHistory.Add(new LoginHistory() { CreateAt = DateTime.Now, IP = ipAddress, UserId = user.UserId });
            UpdateUser(user);
        }

        public IList<User> AllUser()
        {
            return entity.User.Where(p => !p.IsAdmin).ToList();
        }

        public int AddUser(User user)
        {
            entity.User.Add(user);
            return entity.SaveChanges();
        }

        public int UpdateUser(User user)
        {
            entity.User.Attach(user);
            entity.Entry(user).State = System.Data.Entity.EntityState.Modified;
            return entity.SaveChanges();
        }

    }
}
