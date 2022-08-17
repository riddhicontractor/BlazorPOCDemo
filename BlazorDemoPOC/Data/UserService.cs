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

        public UserInfo GetUser(string id)
        {
            var user = (from u in _db.Users
                       where u.UserId==Convert.ToInt32(id)
                       select new UserInfo
                       {
                           UserId=u.UserId,
                           FirstName = u.FirstName,
                           LastName = u.LastName,
                           Gender = u.Gender,
                           Department = u.Department,
                           Username = u.Username,
                           Password = u.Password
                       }).First();
            return user;
        }
        //insert user info
        public string Create(UserInfo objUser)
        {
            _db.Users.Add(objUser);
            _db.SaveChanges();
            return "User added successfully!";
        }

        public string Edit(UserInfo objUser)
        {
            var user = _db.Users.SingleOrDefault(a => a.UserId == objUser.UserId);
            if(user != null)
            {
                user.FirstName = objUser.FirstName;
                user.LastName = objUser.LastName;
                user.Gender = objUser.Gender;
                user.Department = objUser.Department;
                user.Username = objUser.Username;
                user.Password = objUser.Password;
               // _db.Users.Update(user);
                _db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();
               
            }
            return "User updated successfully!";
        }

        //delete user
        public string Delete(UserInfo objUser)
        {
            var user = _db.Users.Find(objUser.UserId);
            if(user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
            return "User deleted successfully!";
        }

        //login user
        public bool UserLogin(UserInfo userInfo)
        {
            try
            {
                var query = from u in _db.Users
                            where userInfo.Username == u.Username &&
                            userInfo.Password == u.Password
                            select u;
                if (query.FirstOrDefault() != null)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
