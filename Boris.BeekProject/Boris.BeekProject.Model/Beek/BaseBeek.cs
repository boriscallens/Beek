using System;
using System.Linq;
using System.Collections.Generic;
using Boris.BeekProject.Model.Accounts;

namespace Boris.BeekProject.Model.Beek
{
    public class BaseBeek
    {
        private List<BaseGenre> genres;
        private List<WritingStyle> writingStyles;
        private IList<KeyValuePair<IUser, Contributions>> involvements;
        private IList<KeyValuePair<BaseBeek, BeekRelationTypes>> relations;

        public int Id { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public BeekTypes Type { get; set; }
        public bool IsFiction { get; set; }
        public BeekCollection Collection { get; private set; }
        public int VolumeNumber { get; set; }
        public char? SubVolume { get; set; }
        public int TotalVolumes { 
            get
            {
                return Collection.GroupBy(b => String.Format("{0}{1}", b.VolumeNumber, b.SubVolume)).Count();
            }
        }
        public IEnumerable<KeyValuePair<IUser, Contributions>> Involvements { get { return involvements; } }
        public IEnumerable<BaseGenre> Genres { get { return genres; } }
        public IEnumerable<WritingStyle> WritingStyles { get { return writingStyles; } }
        public IEnumerable<KeyValuePair<BaseBeek, BeekRelationTypes>> Relations { get { return relations; } }
        public DateTime DateCreated { get; private set; }
        
        public BaseBeek(BeekTypes type)
        {
            involvements = new List<KeyValuePair<IUser, Contributions>>();
            relations = new List<KeyValuePair<BaseBeek, BeekRelationTypes>>();
            writingStyles = new List<WritingStyle>();
            genres = new List<BaseGenre>();
            Type = type;
            Collection = new BeekCollection();
            AddToCollection(new BeekCollection(), 1, null);
            DateCreated = DateTime.UtcNow;
        }

        public void AddGenre(BaseGenre genre)
        {
            lock (genres)
            {
                if (!genres.Contains(genre))
                {
                    genres.Add(genre);
                }
            }
        }
        public void AddGenre(IEnumerable<BaseGenre> newGenres)
        {
            lock (genres)
            {
                genres = genres.Union(newGenres).ToList();
            }
        }
        public void RemoveGenre(BaseGenre genre)
        {
            lock (genres)
            {
                genres.Remove(genre);
            }
        }
        public void RemoveGenre(IEnumerable<BaseGenre> genresToRemove)
        {
            lock (genres)
            {
                genres = genres.Where(g => !genresToRemove.Contains(g)).ToList();
            }
        }
        public bool IsGenre(BaseGenre genre)
        {
            bool isGenre = genres.Contains(genre);
            if(!isGenre)
            {
                int i = 0;
                lock (genres)
                {
                    while (!isGenre && i < genres.Count)
                    {
                        isGenre = genres[i].IsChildOf(genre);
                        i++;
                    }    
                }
            }
            return isGenre;
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

        public void RelateTo(BaseBeek relatedBeek, BeekRelationTypes otherIsWhatOfMe)
        {
            if (relatedBeek.Equals(this))
            {
                throw new ArgumentException("Cannot relate a beek to itself", "relatedBeek");
            }
            lock (relations)
            {
                if (!IsBeekRelatedToMeAs(relatedBeek, otherIsWhatOfMe))
                {
                    relations.Add(new KeyValuePair<BaseBeek, BeekRelationTypes>(relatedBeek, otherIsWhatOfMe));
                }
            }
        }
        public void UnrelateTo(BaseBeek relatedBeek, BeekRelationTypes relationType)
        {
            lock (relations)
            {
                relations = relations.Where(r => !(r.Key.Equals(relatedBeek) && r.Value.Equals(relationType))).ToList();
            }
        }
        public IEnumerable<BaseBeek> GetRelatedBeekForRelationType(BeekRelationTypes relationType)
        {
            return relations.Where(r => r.Value.GetType().Equals(relationType.GetType()))
                .Select(r=>r.Key);
        }
        public bool IsBeekRelatedToMeAs(BaseBeek relatedBeek, BeekRelationTypes relationType)
        {
            return relations.Any(r => r.Key.Equals(relatedBeek) && r.Value.Equals(relationType));
        }

        public void InvolveUser(IUser user, Contributions contribution)
        {
            if(!user.IsContributingAs(contribution))
            {
                throw new ArgumentException(String.Format("User {0} is not a {1}.", user, contribution), "contribution");
            }
            lock (involvements)
            {
                if (!IsUserInvolvedAs(user, contribution))
                {
                    involvements.Add(new KeyValuePair<IUser, Contributions>(user, contribution));
                }
            }
        }
        public void InvolveUsers(IEnumerable<IUser>users, Contributions contribution)
        {
            if(users.Any(u=>!u.IsContributingAs(contribution)))
            {
                throw new ArgumentException(String.Format("At least one use is not a {0}.", contribution), "users");
            }
            lock (involvements)
            {
                involvements = involvements.Union(
                        users.Where(u => !GetInvolvedUsersForContribution(contribution).Contains(u))
                        .Select(u => new KeyValuePair<IUser, Contributions>(u, contribution))
                    ).ToList();
            }
        }
        public void DisInvolveUser(IUser user, Contributions contribution)
        {
            lock (involvements)
            {
                involvements = involvements.Where(i => !(i.Key.Equals(user) && i.Value.Equals(contribution))).ToList();
            }
        }
        public IEnumerable<IUser> GetInvolvedUsersForContribution(Contributions contribution)
        {
            return involvements
                .Where(i => i.Value.Equals(contribution))
                .Select(i => i.Key);
        }
        public bool IsUserInvolvedAs(IUser user, Contributions contribution)
        {
            return involvements.Any(i => i.Key.Equals(user) && i.Value.Equals(contribution));
        }

        public void AddToCollection(BeekCollection beekCollection, int volumeNumber, char? subVolume)
        {
            lock (Collection)
            {
                if (!beekCollection.Any(b => b.Equals(this)))
                {
                    beekCollection.Add(this);
                }
                Collection = beekCollection;
                VolumeNumber = volumeNumber;
                SubVolume = subVolume;
            }
        }
        public void RemoveFromCollection()
        {
            Collection.Remove(this);
            Collection = new BeekCollection();
            VolumeNumber = 0;
            SubVolume = null;
        }
    }

    public enum BeekTypes
    {
        ShortStory,
        LongStory,
        Comic,
        Poem,
        Omnibus
    }
    public enum BeekRelationTypes
    {
        Original,
        Republishment,
        Translation,
        Adaptation,
        Update,
        Complement
    }
}
