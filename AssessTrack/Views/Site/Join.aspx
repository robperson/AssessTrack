<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.SiteViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Join
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Join a Site</h2>
    <% using (Html.BeginForm()) { %>
    <p>
        <%= Html.DropDownList("id",Model.SiteList) %>
        <input type="submit" value="Join this Site!" />
    </p>    
    <% } %>

</asp:Content>
