using MetalTheist.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalTheist.Data.Extensions
{
    public static class UserMapper
    {
        public static void Map(this User user, User otherUser)
        {
            if (otherUser.Username != null) user.Username = otherUser.Username;
            if (otherUser.Password != null) user.Password = otherUser.Password;
            if (otherUser.Email != null) user.Email = otherUser.Email;
            if (otherUser.DateOfRegistration != null) user.DateOfRegistration = otherUser.DateOfRegistration;
            if (otherUser.Bands != null) user.Bands = otherUser.Bands;
            if (otherUser.Articles != null) user.Articles = otherUser.Articles;
        }
    }
}
