<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AssessTrack.Models.ProgramOutcome>>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Program Outcomes
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Program Outcomes</h2>

    <table>
        <tr>
            <th>
                Label
            </th>
            <th>
                Description
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <strong><%= item.Label%></strong>
                <div class="row-actions">
                <%= Html.ATAuthLink("Edit", new { action = "Edit", id = item.ProgramOutcomeID, siteShortName = Html.CurrentSiteShortName() }, AssessTrack.Filters.AuthScope.Site, 5, 10)%>
                <%= Html.ATAuthLink("Delete", " | ", "", new { action = "Delete", id = item.ProgramOutcomeID, siteShortName = Html.CurrentSiteShortName() }, AssessTrack.Filters.AuthScope.Site, 5, 10)%>
                </div>
                
            </td>
            <td>
                <%= Html.Encode(item.Description) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ATAuthLink("Create New Program Outcome", new { action = "Create", siteShortName = Html.CurrentSiteShortName() }, AssessTrack.Filters.AuthScope.Site, 5, 10)%>
    </p>

</asp:Content>
