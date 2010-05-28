<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AssessTrack.Models.Site>>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Your Sites
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Your Sites</h2>

    <table>
        <tr>
            <th></th>
            <th>
                Site Title
            </th>
            <th>
                Description
            </th>
            
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ATAuthLink("Edit", new { id=item.SiteID, action="Edit" },AssessTrack.Filters.AuthScope.Site,5,10) %> |
            </td>
            <td>
                <%= Html.ATAuthLink(item.Title , new { siteShortName = item.ShortName, action = "Details" }, AssessTrack.Filters.AuthScope.Site, 0, 10)%>
            </td>
            <td>
                <%= Html.Encode(item.Description) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ATAuthLink("Create New Site", new { Action = "Create" }, AssessTrack.Filters.AuthScope.Application, 9, 10)%><br />
        <%= Html.ActionLink("Join a site", "Join") %>
    </p>

</asp:Content>

