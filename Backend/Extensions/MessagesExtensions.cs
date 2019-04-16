using Backend.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Backend.Extensions
{
    public static class MessageExtensions
    {
        public static IQueryable<Messages> OwnedByFriends(this IQueryable<Messages> query, DbSet<Relationship> relationships, string userId)
        {
            var myPosts = from m in query
                          let fr = (from f in relationships where f.FriendId == userId select f.UserId)
                          where m.UserId == userId || fr.Contains(m.UserId)
                          select m;
            return myPosts;
        }
    }
}