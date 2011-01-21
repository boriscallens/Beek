<%@ Control Language="C#" Inherits="ViewUserControl<HeaderViewData>" %>
<%@ Import Namespace="Boris.BeekProject.Services.Search.SearchBags" %>
<div id="header">
  <a href="#" id="logo"></a>
  <div id="navigation" class="<%=ViewData.Model.CurrentNavBlock%>IsActive">

    <div id="homeNavBlock" class="navBlock">
      <a href="<%=Url.Action("Index", "Home")%>" class="navTitle">home</a>
    </div>

    <div id="beekNavblock" class="navBlock">
      <a href="<%=Url.Action("Index", "Search")%>" class="navTitle">beek</a>
      <a href="<%=Url.Action("Index", "Search")%>" class="navSubLink">Search</a>
      <a href="<%=Url.Action("Create", "Beek")%>" class="navSubLink">Add</a>
    </div>

    <div id="myStuffNavBlock" class="navBlock">
      <%using (Html.BeginForm<AccountController>(a => a.LogIn(string.Empty, string.Empty, string.Empty), FormMethod.Post, new { id = "navLoginForm", @class="navSubLink"})){%>
        <table id="navLoginInputs">
          <tr>
            <td>
              <input type="text" name="username" value="Username" tabindex="998" id="navLoginUsername">
            </td>
            <td rowspan="2">
              <input type="image" value="Log In" src="<%=Url.Content("~/content/pics/icons/key.png") %>">
            </td>
          </tr>
          <tr>
            <td>
              <input type="password" name="password" id="loginpassword" value="" tabindex="999">
            </td>
          </tr>
        </table>
      <%}%>
      <%=Html.ActionLink<AccountController>(a=>a.Register(Model.User.Id), "Register", new{@class="navSubLink"})%>
    </div>
  </div>
</div>