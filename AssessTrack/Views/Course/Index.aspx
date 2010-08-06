<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AssessTrack.Models.Course>>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Courses
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Courses</h2>

    <table>
        <tr>
            <th>
                Name
            </th>
            <th>
                Description
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <strong><%= Html.ActionLink(item.Name, "Details", new { id = item.CourseID, siteShortName = Html.CurrentSiteShortName() })%></strong>
                <div class="row-actions"><%= Html.ATAuthLink("Edit", new { action = "Edit", id = item.CourseID, siteShortName = Html.CurrentSiteShortName() }, AssessTrack.Filters.AuthScope.Site, 5, 10)%></div>
                
            </td>
            <td>
                <%= Html.Encode(item.Description) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ATAuthLink("Create New Course", new { action = "Create", siteShortName = Html.CurrentSiteShortName() }, AssessTrack.Filters.AuthScope.Site, 5, 10)%>
    </p>

</asp:Content>

