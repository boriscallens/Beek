using System;

namespace Boris.BeekProject.Model.Beek
{
    public interface IBeekRelationType: IEquatable<IBeekRelationType>
    {
        string Label { get; }
        string Description { get; }
    }
}