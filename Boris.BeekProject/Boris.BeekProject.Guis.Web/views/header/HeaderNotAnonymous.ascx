<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<HeaderViewData>" %>
<div id="header">
  <a href="#" id="logo"></a>
  <div id="navigation" class="<%=Model.CurrentNavBlock%>IsActive">
    <a href="/" id="homeNavBlock" class="navBlock">          
      <span class="navTitle">home</span>    
    </a>
    <div id="beekNavBlock" class="navBlock">
      <span class="navTitle">beek</span>
      <%=Html.ActionLink<SearchController>(a=>a.Beek(), "Search", new{@class="navSubLink"})%>
      <%=Html.ActionLink<BeekController>(a=>a.Create(), "Add", new{@class="navSubLink"})%>
    </div>
    <div id="myStuffNavBlock" class="navBlock">
      <a href="#" class="navTitle">my stuff</a>
      <%=Html.ActionLink<AccountController>(a=>a.MyBeek(), "My Beek", new{@class="navSubLink"})%>
      <div class="navSubLink">My other stuff</div>
      <%=Html.ActionLink<AccountController>(a=>a.LogOut(), "Log Out", new{@class="navSubLink"})%>      
    </div>
  </div>
</div>
