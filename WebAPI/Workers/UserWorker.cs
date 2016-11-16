using System.Linq;
using System.Security.Principal;
using WebAPI.Models;
using WebAPI.Models.Entities;

namespace WebAPI.Workers
{
    public class UserWorker
    {
        private readonly Model _db;
        private readonly IPrincipal _user;

        public UserWorker(Model db, IPrincipal user)
        {
            _db = db;
            _user = user;
        }

        //Метод возвращающий пользователя по его имени
        public static AspNetUsers GetUserByName(string name, Model db)
        {
            return db.AspNetUsers.First(user => user.UserName.Equals(name));
        }
    }
}