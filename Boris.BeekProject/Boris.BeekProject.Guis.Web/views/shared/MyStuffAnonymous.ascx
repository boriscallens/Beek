<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<BaseBeekViewData>" %>
  <%using (Html.BeginForm<AccountController>(a => a.LogIn(string.Empty, string.Empty, string.Empty), FormMethod.Post, new { id = "navLoginForm", @class="navSubLink"})){%>
    <table id="navLoginInputs">
      <tr>
        <td>
          <input type="text" name="username" value="Username" tabindex="998" id="navLoginUsername"/>
        </td>
        <td rowspan="2">
          <input type="image" value="Log In" src="/content/pics/icons/key.png"/>
        </td>
      </tr>
      <tr>
        <td>
          <input type="password" name="password" id="password" value="" tabindex="999" />  
        </td>
      </tr>
    </table>
  <%}%>
<%=Html.ActionLink<AccountController>(a=>a.Register(Model.User.Id), "Register", new{@class="navSubLink"})%>