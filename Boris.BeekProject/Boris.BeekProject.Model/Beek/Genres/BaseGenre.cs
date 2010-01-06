using System;
using System.Collections.Generic;
using System.Linq;

namespace Boris.BeekProject.Model.Beek
{
    public class BaseGenre: IEquatable<BaseGenre>
    {
        private IList<BaseGenre> subGenres;

        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<BaseGenre> SubGenres { get { return subGenres; } }

        protected BaseGenre(string name)
        {
            Name = name;
            subGenres = new List<BaseGenre>();
        }
        public void AddSubGenre(BaseGenre genre)
        {
            if (genre == this)
            {
                throw new ArgumentException("Cannot add self as subgenre");
            }
            if (genre.IsParentOf(this))
            {
                throw new ArgumentException("Cannot add parent genre as subgenre");
            }
            lock (subGenres)
            {
                if(!IsParentOf(genre))
                {
                    subGenres.Add(genre);
                }
            }
        }
        public void RemoveSubGenre(BaseGenre genre)
        {
            lock (subGenres)
            {
                subGenres = subGenres.Where(g => !g.Equals(genre)).ToList();
            }
        }
        public bool IsParentOf(BaseGenre genre)
        {
            bool isParent = subGenres.Contains(genre);
            int i = 0;
            if(!isParent)
            {
                lock (SubGenres)
                {
                    while (!isParent && i < subGenres.Count)
                    {
                        isParent = subGenres[i].IsParentOf(genre);
                        i++;
                    }
                }
            }
            return isParent;
        }
        public bool IsChildOf(BaseGenre genre)
        {
            return genre.IsParentOf(this);
        }

        public bool Equals(BaseGenre other)
        {
            return (other.GetType().Equals(GetType()));
        }
    }
}
