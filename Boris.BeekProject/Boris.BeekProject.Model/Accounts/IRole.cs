using System;

namespace Boris.BeekProject.Model.Accounts
{
    public interface IRole: IEquatable<IRole>, IEquatable<string>
	{
        Guid Id { get; set; }
        string Name { get; set; }
	}
}
