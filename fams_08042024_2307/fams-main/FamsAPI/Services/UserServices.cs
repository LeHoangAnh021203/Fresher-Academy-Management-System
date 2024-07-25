using AutoMapper;
using DataLayer.Entities;
using DataLayer.Repositories;
using FamsAPI.IServices;
using FamsAPI.ViewModel;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static DataLayer.Entities.User;
#pragma warning disable 
namespace FamsAPI.Services
{
    public class UserServices : IUser
    {
        private readonly IConfiguration _configuration;
        private readonly UserRepository _userRepository;

        public UserServices(UserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }
        public UserServices(IConfiguration config, UserRepository userRepository)
        {
            _configuration = config;
            _userRepository = userRepository;
            
        }

        public List<User> GetAllUsers()
        {
            try
            {
                return (List<User>)_userRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User GetUserById(Guid id)
        {
            try
            {
                var user = _userRepository.Get(x => x.UserId == id);

                if (user == null)
                {
                    // Handle the case where the user is not found, e.g., return null or throw an exception
                    return null;
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public User UpdateUserStatus(Guid userId)
        {
            try
            {
                var user = _userRepository.Get(x => x.UserId == userId);

                if (user == null)
                {
                    // Handle the case where the user is not found, e.g., return null or throw an exception
                    return null;
                }
                if (user.Status == UserStatus.Deactive)
                {
                    user.Status = UserStatus.Active;
                }
                else
                {
                    user.Status = UserStatus.Deactive;
                }
                _userRepository.Update(user);
                _userRepository.SaveChanges();
                return user;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region Update User

        private int checkNameAndEmailv2(string name, string email, Guid userId)
        {
            var users = _userRepository.GetAll();
            if (users.FirstOrDefault(c => c.Name == name && c.UserId != userId) != null)
            {
                return 0;
            }
            else if (users.FirstOrDefault(d => d.Email == email && d.UserId != userId) != null)
            {
                return -1;
            }
            return 1;
        }


        private int checkNameAndEmail(string name, string email)
        {
            var users = _userRepository.GetAll();
            if (users.FirstOrDefault(c => c.Name == name)!= null)
            {
                return 0;
            }
            else if (users.FirstOrDefault(d => d.Email == email) != null)
            {
                return -1;
            }
            return 1;


       
        }

        public string UpdateUser(UpdateUserViewModel user)
        {
            var existedUser = _userRepository.Get( x => x.UserId.Equals(user.UserId) );
            var check = checkNameAndEmailv2(user.Name, user.Email, user.UserId);
            if (existedUser == null)
            {
                return "Can not find user!";
            }
            else if (check == 1)
            {
                existedUser.Name = user.Name;
                existedUser.Email = user.Email;
                existedUser.Phone = user.Phone;
                if (user.Password != "")
                {
                    existedUser.Password = HashAndTruncatePassword(user.Password);
                }
                existedUser.DOB = user.DOB;
                existedUser.Gender = (Genders)user.Gender;
                existedUser.ModifiedBy = user.ModifiedBy;
                existedUser.ModifiedTime = DateTime.Now;
                existedUser.Status = user.Status;
                existedUser.PermissionId = user.permissionId;
                _userRepository.Update(existedUser);
                _userRepository.SaveChanges();
            }
            else if (check == 0) return "User Name already existed!";
            else if (check == -1) return "Email already exited!";

            return "Update successfully!!!";
        }

        #endregion


        #region Grant Permission
        public User GrantPermission(User user)
        {
            var User = new User();
            var data = _userRepository.Get(x => x.UserId == user.UserId);
            if (data == null)
            {
                return null;
            }
            else
            {
                // Cập nhật các trường cần thiết
                data.PermissionId = user.PermissionId;
                data.ModifiedTime = DateTime.UtcNow;
                // Lưu thay đổi
                _userRepository.Update(data);
                _userRepository.SaveChanges();
            }
            return data;
        }
        #endregion

        #region Get User By Keyword
        /// <summary>
        /// Get Users By keywword
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsersByKeyword(string key, string filter)
        {
            try
            {
                List<User> users = new List<User>();
                Guid parsedKey;
                Guid.TryParse(key, out parsedKey);

                if (filter.ToUpper().Equals("UID"))
                {
                    var userList = GetAllUsers().Where(x => x.UserId == parsedKey).ToList();
                    users = userList;
                }
                else if (filter.ToUpper().Equals("NAME"))
                {
                    var userList = GetAllUsers().Where(x => x.Name.ToUpper().Contains(key.ToUpper())).ToList();
                    users = userList;
                }
                else if (filter.ToUpper().Equals("PHONE"))
                {
                    var userList = GetAllUsers().Where(x => x.Phone.ToUpper().Contains(key.ToUpper())).ToList();
                    users = userList;
                }
                else if (filter.ToUpper().Equals("EMAIL"))
                {
                    var userList = GetAllUsers().Where(x => x.Email.ToUpper().Contains(key.ToUpper())).ToList();
                    users = userList;
                }
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Sort Users
        /// <summary>
        /// Sort users
        /// </summary>
        /// <returns></returns> 
        public List<User> SortUsers(string sortBy, string sortDir)
        {
            var users = _userRepository.GetAll();

            switch (sortBy)
            {
                case "name":
                    return sortDir == "asc" ? users.OrderBy(u => u.Name).ToList() : users.OrderByDescending(u => u.Name).ToList();
                case "status":
                    return sortDir == "asc" ? users.OrderBy(u => u.Status).ToList() : users.OrderByDescending(u => u.Status).ToList();
                default:
                    throw new ArgumentException("Invalid sort parameter");
            }
        }
        #endregion
        
        #region Login
        /// <summary>
        /// Login
        /// </summary>
        /// <returns></returns>
        public User UserLogin(string email, string password)
        {
            var user = _userRepository.Get(u => u.Email == email && u.Password == password);
            return user;
        }
        #endregion

        #region AddNewUser
        /// <summary>
        /// Add new user 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// 


        public static string HashAndTruncatePassword(string password)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
                password = BitConverter.ToString(result).Replace("-", "").ToLowerInvariant();
            }

            // Truncate hash to 16 characters
            password = password.Substring(0, 16);

            return password;
        }


        public virtual async Task<string> AddNewUser(string name, string mail, Genders genders, string phone, string Dob, UserStatus status , int permissionId, ClaimsPrincipal user)
        {
            try
            {
                int checkResult = checkNameAndEmail(name, mail);
                if (checkResult == 0)
                {
                    return "Name already existed!!!!";
                }
                else if (checkResult == -1)
                {
                    return "Mail already existed!!!!";
                }
                

                // Set password to "123@"
                string password = "123@";

                // Hash password using MD5
                password = HashAndTruncatePassword(password);


                User newUser = new User
                {
                    Name = name,
                    Email = mail,
                    Gender = genders,
                    Phone = phone,
                    DOB = DateTime.Parse(Dob), // Parse DOB from string to DateTime without specific format
                    Password = password,
                    PermissionId = permissionId,
                    CreateDate = DateTime.Now,
                    Status = status, // Assuming the status is Active for a new user
                    CreateBy = user.Identity.Name
                };
                _userRepository.Add(newUser);
                await _userRepository.SaveChangesAsync();
                SendEmailAddingAccount(newUser.Name ,newUser.Email, "123@");
                return "Add User Success!!!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        #endregion
        public User GetUserByEmail(string email)
        {
            var user = _userRepository.Get(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        #region DeleteUser
        /// <summary>
        /// delete a user with UserID
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        public Boolean DeleteUser(Guid key)
        {
            var DeletedUser = new User();

            try
            {
                DeletedUser = _userRepository.Get(x => x.UserId == key);
                if (key != Guid.Empty)
                {
                    if (DeletedUser != null)
                    {
                        _userRepository.Delete(key);
                        _userRepository.SaveChanges();
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region Send Email With Password
        private void SendEmailAddingAccount(string username, string emailTo, string password)
        {

            string body = "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Document</title>\r\n    <style>\r\n        table, th, td{\r\n            border: 1px solid  ;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <h3>Hi,  "+ username +"</h3>\r\nHere is the information using for login into our system.\r\n\r\n<table>\r\n    <tr>\r\n        <th>Email</th>\r\n        <th>Password</th>\r\n    </tr>\r\n    <tr>\r\n        <th>\r\n           "+ emailTo +"\r\n        </th>\r\n        <th>\r\n            123@\r\n        </th>\r\n    </tr>\r\n</table>\r\n</body>\r\n</html>";
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = "Account use for login into FAMS system";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = body
            };


            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
        #endregion


    }
}



