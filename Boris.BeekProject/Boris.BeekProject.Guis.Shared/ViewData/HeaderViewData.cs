using Boris.Utils.Mvc.Attributes;

namespace Boris.BeekProject.Guis.Shared.ViewData
{
    public class HeaderViewData: BaseBeekViewData
    {
        public HeaderViewData()
        {

            CurrentNavBlock = NavBlocks.Home;
        }
        public NavBlocks CurrentNavBlock { get; set; }
    }
    public enum NavBlocks
    {
        Home, Beek, Search, MyStuff
    }
}