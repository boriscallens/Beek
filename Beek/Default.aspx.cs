using System;
using System.Web.Security;
using System.Web.UI;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("Hello, " + Server.HtmlEncode(User.Identity.Name));

        FormsIdentity id = (FormsIdentity) User.Identity;
        FormsAuthenticationTicket ticket = id.Ticket;

        Response.Write("<p/>TicketName: " + ticket.Name);
        Response.Write("<br/>Cookie Path: " + ticket.CookiePath);
        Response.Write("<br/>Ticket Expiration: " +
                       ticket.Expiration);
        Response.Write("<br/>Expired: " + ticket.Expired);
        Response.Write("<br/>Persistent: " + ticket.IsPersistent);
        Response.Write("<br/>IssueDate: " + ticket.IssueDate);
        Response.Write("<br/>UserData: " + ticket.UserData);
        Response.Write("<br/>Version: " + ticket.Version);
    }
}