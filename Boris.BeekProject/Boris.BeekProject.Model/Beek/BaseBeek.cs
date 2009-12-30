using System;
using System.Collections.Generic;
using System.Linq;
using Boris.BeekProject.Model.Accounts;

namespace Boris.BeekProject.Model.Beek
{
    public class BaseBeek
    {
        private List<Genre> genres;
        private List<WritingStyle> writingStyles;
        private IList<KeyValuePair<IUser, Roles>> involvements;
        //private IList<KeyValuePair<BaseBeek, BeekRelationType>> relations;

        public int Id { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public BeekTypes Type { get; set; }
        public bool IsFiction { get; set; }
        public IEnumerable<KeyValuePair<IUser, Roles>> Involvements { get { return involvements; } }
        public IEnumerable<Genre> Genres { get{ return genres;} }
        public IEnumerable<WritingStyle> WritingStyles { get { return writingStyles; } }

        public BaseBeek(BeekTypes type)
        {
            involvements = new List<KeyValuePair<IUser, Roles>>();
            genres = new List<Genre>();
            Type = type;
        }

        public void AddGenre(Genre genre)
        {
            lock (genres)
            {
                if (!genres.Contains(genre))
                {
                    genres.Add(genre);
                }
            }
        }
        public void AddGenre(IEnumerable<Genre> newGenres)
        {
            lock (genres)
            {
                genres = genres.Union(newGenres).ToList();
            }
        }
        public void RemoveGenre(Genre genre)
        {
            lock (genres)
            {
                genres.Remove(genre);
            }
        }
        public void RemoveGenre(IEnumerable<Genre> genresToRemove)
        {
            lock (genres)
            {
                genres = genres.Where(g => !genresToRemove.Contains(g)).ToList();
            }
        }

        public void AddWritingStyle(WritingStyle writingStyle)
        {
            lock (writingStyles)
            {
                if (!writingStyles.Contains(writingStyle))
                {
                    writingStyles.Add(writingStyle);
                }
            }
        }
        public void AddWritingStyle(IEnumerable<WritingStyle> newWritingStyles)
        {
            lock (writingStyles)
            {
                writingStyles = writingStyles.Union(newWritingStyles).ToList();
            }
        }
        public void RemoveWritingStyle(WritingStyle writingStyle)
        {
            lock (writingStyles)
            {
                writingStyles.Remove(writingStyle);
            }
        }
        public void RemoveWritingStyle(IEnumerable<WritingStyle> writingStylesToRemove)
        {
            lock (writingStyles)
            {
                writingStyles = writingStyles.Where(w => !writingStylesToRemove.Contains(w)).ToList();
            }
        }
        
        public void InvolveUser(IUser user, Roles role)
        {
            if(!user.IsInRole(role))
            {
                throw new ArgumentException(String.Format("User {0} is not a {1}.", user, role), "role");
            }
            lock (involvements)
            {
                if (!IsUserInvolvedAs(user, role))
                {
                    involvements.Add(new KeyValuePair<IUser, Roles>(user, role));
                }
            }
        }
        public void InvolveUsers(IEnumerable<IUser>users, Roles role)
        {
            if(users.Any(u=>!u.IsInRole(role)))
            {
                throw new ArgumentException(String.Format("At least one use is not a {0}.", role), "users");
            }
            lock (involvements)
            {
                involvements = involvements.Union(
                        users.Where(u => !GetInvolvedUsersForRole(role).Contains(u))
                        .Select(u => new KeyValuePair<IUser, Roles>(u, role))
                    ).ToList();
            }
        }
        public void DisInvolveUser(IUser user, Roles role)
        {
            lock (involvements)
            {
                involvements = involvements.Where(i => !(i.Key.Equals(user) && i.Value.Equals(role))).ToList();
            }
        }
        public IEnumerable<IUser> GetInvolvedUsersForRole(Roles role)
        {
            return involvements
                .Where(i => i.Value.Equals(role))
                .Select(i => i.Key);
        }
        public bool IsUserInvolvedAs(IUser user, Roles role)
        {
            return involvements.Any(i => i.Value.Equals(role) && i.Key.Equals(user));
        }
    }

    public enum BeekTypes
    {
        ShortStory,
        LongStory,
        Comic,
        Poem
    }
}
