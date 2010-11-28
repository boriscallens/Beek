<%@ Control Language="C#" Inherits="ViewUserControl<BeekViewData>" %>

<div class="beekThumb">
  <img alt="<%=Model.Beek.Title%>" class="coverArt" width="100" height="100" src="<%=Model.Beek.CoverArtPath%>" >
  <div class="title"><%=Model.Beek.Title%></div>
  <div class="author">by <%=Model.Beek.Author%></div>
</div>