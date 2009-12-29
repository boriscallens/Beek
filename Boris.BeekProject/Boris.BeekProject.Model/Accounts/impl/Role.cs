using System;

namespace Boris.BeekProject.Model.Accounts
{
    public class Role: IRole
    {
        public bool Equals(IRole other)
        {
            return Id.Equals(other.Id);
        }
        public bool Equals(string other)
        {
            return this.Name.Equals(other, StringComparison.InvariantCultureIgnoreCase);
        }

        public Guid Id { get; set;}
        public string Name { get; set;}
    }
}
