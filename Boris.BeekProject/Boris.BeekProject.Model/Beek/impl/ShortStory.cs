using System.Linq;
using System.Collections.Generic;
using Boris.BeekProject.Model.Accounts;

namespace Boris.BeekProject.Model.Beek
{
    public class ShortStory: BaseBeek
    {
        private List<IUser> writers;

        public IEnumerable<IUser> Writers { get { return writers; } }

        public ShortStory()
        {
            writers = new List<IUser>();
        }

        public void AddWriter(IUser writer)
        {
            lock (writers)
            {
                if (!writers.Contains(writer))
                {
                    writers.Add(writer);
                }
            }
        }
        public void AddWriters(IEnumerable<IUser> newWriters)
        {
            lock (writers)
            {
                writers = writers.Union(newWriters).ToList();
            }
        }
        public void RemoveWriter(IUser writer)
        {
            lock (writers)
            {
                writers.Remove(writer);
            }
        }
        public void RemoveWriters(IEnumerable<IUser> writersToRemove)
        {
            lock (writers)
            {
                writers = writers.Where(w => !writersToRemove.Contains(w)).ToList();
            }
        }
    }
}