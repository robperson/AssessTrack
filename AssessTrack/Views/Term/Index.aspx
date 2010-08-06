<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<AssessTrack.Models.Term>>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Terms (Semesters)
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Terms (Semesters)</h2>

    <table>
        <tr>
            <th>Name</th>
            <th>
                Start Date
            </th>
            <th>
                End Date
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <strong><%= Html.ATAuthLink(item.Name, new { action = "Details", id = item.TermID, siteShortName = Html.CurrentSiteShortName() }, AssessTrack.Filters.AuthScope.Site, 1, 10)%></strong>
                <div class="row-actions"><%= Html.ATAuthLink("Edit", new { action = "Edit", id = item.TermID, siteShortName = Html.CurrentSiteShortName() }, AssessTrack.Filters.AuthScope.Site, 5, 10)%></div>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.StartDate.ToShortDateString())) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.EndDate.ToShortDateString())) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create", new { siteShortName = Html.CurrentSiteShortName() })%>
    </p>

</asp:Content>

