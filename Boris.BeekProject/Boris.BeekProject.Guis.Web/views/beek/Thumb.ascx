<%@ Control Language="C#" Inherits="ViewUserControl<BeekViewData>" %>

<div class="beekThumb">
  <img alt="<%=Model.Beek.Title%>" class="coverArt" width="100" height="100" src="/content/pics/placeholders/coverArt.png" />
  <div><%=Model.Beek.Title%></div>
</div>