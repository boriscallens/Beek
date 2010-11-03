using System.Text;
using System.Web.Mvc;
using Boris.BeekProject.Model.Accounts;

namespace Boris.BeekProject.Guis.Web
{
    public static class BeekHtmlHelper
    {
        public static string GetConcatenatedUsers(this HtmlHelper htmlHelper, Contributions contribution, string formatString)
        {
            StringBuilder sb = new StringBuilder();
            //foreach (IUser user in  GetInvolvedUsersForContribution(contribution))
            //{
            //    sb.AppendFormat(formatString, user.Name);
            //}
            return string.Empty;
        }
    }
}