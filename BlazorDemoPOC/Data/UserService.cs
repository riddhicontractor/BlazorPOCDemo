using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDemoPOC.Data
{
    public class UserService
    {
        private readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }

        //get all users
        public List<UserInfo> GetUser()
        {
            var userList = _db.Users.ToList();
            return userList;
        }

        //insert user info
        public string Create(UserInfo objUser)
        {
            _db.Users.Add(objUser);
            _db.SaveChanges();
            return "User added successfully!";
        }
    }
}
