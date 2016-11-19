using System.Linq;
using System.Security.Principal;
using WebAPI.Models;
using WebAPI.Models.Entities;

namespace WebAPI.Workers
{
    public class UserWorker
    {
        private readonly ModelDB _db;
        private readonly IPrincipal _user;

        public UserWorker(ModelDB db, IPrincipal user)
        {
            _db = db;
            _user = user;
        }

        //Метод возвращающий пользователя по его имени
        public static AspNetUsers GetUserByName(string name, ModelDB db)
        {
            return db.AspNetUsers.First(user => user.UserName.Equals(name));
        }
    }
}