using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.WebMVC.Models.Users
{
    public class UserRepository
    {
        private static UserRepository single_instance = null;

        public List<User> users;

        private UserRepository()
        {
            users = new List<User>()
            {
                new User(){ Id = "1", Username = "admin", Password = "123456", IsAdmin = true},
                new User(){ Id = "2", Username = "user", Password = "123456", IsAdmin = false},
            };
        }

        public static UserRepository GetInstance() {
            if(single_instance == null)
            {
                single_instance = new UserRepository();
            }
            return single_instance;
        }

        public User GetUserById(string Id) {
            var user = users.FirstOrDefault(k => k.Id == Id);
            return user;
        }

        public User GetUserByUsername(string username) {
            var user = users.FirstOrDefault(k => k.Username == username);
            return user;
        }

        public void Insert(User user) {
            users.Add(user);
        }

        public void Update(User user) {
            var found = users.FirstOrDefault(k => k.Id == user.Id);
            if (found != null) {
                found.Username = user.Username;
                found.Password = user.Password;
                found.IsAdmin = user.IsAdmin;
            }
        }

        public RegisterModel RegisterUser(RegisterModel model) {
            if (string.IsNullOrEmpty(model.Username))
            {
                model.Error = "Username cannot be empty.";
                return model;
            }
            if (string.IsNullOrEmpty(model.Password)) {
                model.Error = "Password cannot be empty";
                return model;
            }
            if (model.Password.Length < 8)
            {
                model.Error = "Password must be at least 8 characters";
                return model;
            }
            if (model.Password != model.PasswordAgain)
            {
                model.Error = "Password must be at least 8 characters";
                return model;
            }

            var user = GetUserByUsername(model.Username.ToLower());
            if (user != null)
            {
                model.Error = "User already exists!";
                return model;
            }

            user = new User();
            user.Id = generateId();
            user.Username = model.Username;
            user.Password = model.Password;
            Insert(user);

            return new RegisterModel();
        }

        public User LoginUser(LoginModel model)
        {
            var user = GetUserByUsername(model.Username.ToLower());
            if (user == null)
            {
                model.Error = "User not found!";
                return model;
            }
            if (user.Password != model.Password)
            {
                model.Error = "Wrong password!";
                return model;
            }
            model.Id = user.Id;
            model.IsAdmin = user.IsAdmin;
            return model;
        }

        public string generateId() {
            List<User> allUsers = UserRepository.GetInstance().users;
            int maxId = int.MinValue;
            foreach (User type in allUsers)
            {
                int currentId = Int32.Parse(type.Id);
                if (currentId > maxId)
                {
                    maxId = currentId;
                }
            }
            return (maxId+1).ToString();
        }

        public static implicit operator List<object>(UserRepository v)
        {
            throw new NotImplementedException();
        }
    }
}
