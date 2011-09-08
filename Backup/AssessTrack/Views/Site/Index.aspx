<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AssessTrack.Models.Site>>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	My Sites
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>My Sites</h2>

    <table>
        <tr>
            <th>
                Site
            </th>
            <th>
                Description
            </th>
            
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
           <td>
                <strong><%= Html.ATAuthLink(item.Title , new { siteShortName = item.ShortName, action = "Details" }, AssessTrack.Filters.AuthScope.Site, 0, 10)%></strong>
                <div class="row-actions"><%= Html.ATAuthLink("Edit", new { siteShortName=item.ShortName, action="Edit" },AssessTrack.Filters.AuthScope.Site,5,10) %></div>
            </td>
            <td>
                <p><%= Html.Encode(item.Description) %></p>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ATAuthLink("Create New Site", new { Action = "Create" }, AssessTrack.Filters.AuthScope.Application, 9, 10)%><br />
        <%= Html.ActionLink("Join a site", "Join") %>
    </p>

</asp:Content>

