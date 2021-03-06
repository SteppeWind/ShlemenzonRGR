﻿using Model.UserTypes;
using ModelDataBase.DBUserTypes;
using ServerApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase;

namespace ServerApp.CRUD
{
    class UserCRUD
    {
        private static NewsForumContext CurrentNewsForumContext = NewsForumContext.GetNewsForumContext;

        public static User Authorization(string login, string password)
        {
            var user = CurrentNewsForumContext.Users.FirstOrDefault(u => u.EMail.Equals(login) || u.PhoneNumber.Equals(login));

            if (user != null && user.Password.Equals(password))
            {
                if (!user.IsBunned)
                {
                    User res = new User();
                    res.Convert(user);
                    return res;
                }
                else
                    return new User() { UserId = -1 };
            }
            
            return null;
        }

        public static bool CreateUser(User user)
        {
            DBUser dbuser = new DBUser();
            dbuser.Convert(user);

            CurrentNewsForumContext.Users.Add(dbuser);
            CurrentNewsForumContext.SaveChanges();

            return true;
        }

        public static bool UpdateUser(User user)
        {
            var dbUser = GetDBUserFromId(user.UserId);
            if (dbUser != null || user.AccessLevel >= UserAccessLevel.Admin)
            {
                dbUser.Convert(user);
                CurrentNewsForumContext.SaveChanges();
                return true;
            }

            return false;
        }

        public static List<VMUser> GetUsers => CurrentNewsForumContext.Users
            .ToList()
            .Where(u => u.AccessLevel != UserAccessLevel.God)
            .Select(u =>
        {
            VMUser user = new VMUser();
            user.Convert(u);
            return user;
        }).ToList();

        public static UserAccessLevel SetAdminAccess(int userId, int godId)
        {
            var godUser = GetDBUserFromId(godId);
            var dbUser = GetDBUserFromId(userId);

            if (godUser != null && dbUser != null)
            {
                if (godUser.AccessLevel >= UserAccessLevel.God)
                {
                    dbUser.AccessLevel = UserAccessLevel.Admin;
                    CurrentNewsForumContext.SaveChanges();
                    return dbUser.AccessLevel;
                }
            }

            return UserAccessLevel.Goest;
        }

        public static UserAccessLevel RemoveAdminAccess(int userId, int godId)
        {
            var godUser = GetDBUserFromId(godId);
            var dbUser = GetDBUserFromId(userId);

            if (godUser != null && dbUser != null)
            {
                if (godUser.AccessLevel >= UserAccessLevel.God)
                {
                    dbUser.AccessLevel = UserAccessLevel.User;
                    CurrentNewsForumContext.SaveChanges();
                    return dbUser.AccessLevel;
                }
            }

            return UserAccessLevel.Goest;
        }


        public static bool IsAdmin(int id)
        {
            var user = GetDBUserFromId(id);

            if (user != null && user.AccessLevel == UserAccessLevel.God)
                return true;

            return false;
        }

        public static DBUser GetDBUserFromId(int id) => CurrentNewsForumContext.Users.FirstOrDefault(u => u.UserId == id);

        public static User GetUserFromId(int id)
        {
            User res = new User();
            res.Convert(GetDBUserFromId(id));
            return res;
        }
        public static bool RegistrationUser(User user)
        {
            DBUser dbUser = new DBUser();
            if (user.EMail != null)
            {
                if (CurrentNewsForumContext.Users.FirstOrDefault(u => u.EMail.Contains(user.EMail)) != null)
                {
                    return false;
                }
            }

            if (user.PhoneNumber != null)
            {
                if (CurrentNewsForumContext.Users.FirstOrDefault(u => u.PhoneNumber.Contains(user.PhoneNumber)) != null)
                {
                    return false;
                }
            }

            dbUser.Convert(user);
            CurrentNewsForumContext.Users.Add(dbUser);
            CurrentNewsForumContext.SaveChanges();
            return true;
        }

        public static bool BanUser(int bunnedUserId, int adminId)
        {
            var dbBannedUser = GetDBUserFromId(bunnedUserId);
            var dbAdmin = GetDBUserFromId(adminId);

            if (dbBannedUser != null)
            {
                if (dbAdmin != null && dbAdmin.AccessLevel >= UserAccessLevel.Admin)
                {
                    dbBannedUser.IsBunned = true;
                    CurrentNewsForumContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public static bool UnbanUser(int bunnedUserId, int adminId)
        {
            var dbBannedUser = GetDBUserFromId(bunnedUserId);
            var dbAdmin = GetDBUserFromId(adminId);

            if (dbBannedUser != null)
            {
                if (dbAdmin != null && dbAdmin.AccessLevel >= UserAccessLevel.Admin)
                {
                    dbBannedUser.IsBunned = false;
                    CurrentNewsForumContext.SaveChanges();
                    return true;
                }
            }

            return false;
        }
    }
}