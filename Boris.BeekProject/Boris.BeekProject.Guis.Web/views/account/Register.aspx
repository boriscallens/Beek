<%@ Page Language="C#" MasterPageFile="~/views/shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AccountViewData>" %>

<asp:Content ContentPlaceHolderID="Title" runat="server">
  - Account - Register
</asp:Content>
<asp:Content ContentPlaceHolderID="Css" runat="server">
  <%=Html.PrintStyleSheet("register")%>
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
  <%=Html.ValidationSummary() %>
  <%using (Html.BeginForm<AccountController>(a=>a.Register(null), FormMethod.Post, new{id="registerForm"})){%>
    <div>
      <%=Html.HiddenFor(i=>Model.ViewUser.Id)%>
    </div>
    <div>
      <%=Html.LabelFor(n => Model.ViewUser.Name)%>
      <%=Html.TextBoxFor(n => Model.ViewUser.Name, new { tabindex = "1" })%>
      <%=Html.ValidationMessageFor(n => Model.ViewUser.Name, "*")%>
    </div>
    <div>
      <%=Html.LabelFor(n => Model.ViewUser.Password)%>
      <%=Html.PasswordFor(n => Model.ViewUser.Password, new { tabindex = "2" })%>
      <%=Html.ValidationMessageFor(n => Model.ViewUser.Password, "*")%>      
    </div>
    <div>
      <%=Html.LabelFor(n => Model.ViewUser.PasswordRepeat)%>
      <%=Html.PasswordFor(n => Model.ViewUser.PasswordRepeat, new { tabindex = "3" })%>
      <%=Html.ValidationMessageFor(n => Model.ViewUser.PasswordRepeat, "*")%>      
    </div>
    <div>
      <%=Html.LabelFor(n => Model.ViewUser.Email)%>
      <%=Html.TextBoxFor(n => Model.ViewUser.Email, new { tabindex = "3" })%>      
      <%=Html.ValidationMessageFor(n => Model.ViewUser.Email, "*")%>
    </div>
    <div>
	    <input type="submit" value="Register"/>
    </div>
  <%}%>
</asp:Content>
