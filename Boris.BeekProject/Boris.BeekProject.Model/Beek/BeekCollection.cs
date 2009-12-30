using System.Collections.Generic;

namespace Boris.BeekProject.Model.Beek
{
    public class BeekCollection: LinkedList<BaseBeek>
    {
        public string Name { get; set; }
    }
}