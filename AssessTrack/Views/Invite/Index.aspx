<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AssessTrack.Models.Invitation>>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Invitations
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Invitations</h2>
    <table>
        <tr>
            <th>
                Email
            </th>
            <th>
                Site (Access Level)
            </th>
            <th>
                Course Offering (Access Level)
            </th>
            <th>
                Accepted
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <strong><%= item.Email%></strong>
                <!--
                <div class="row-actions">
                <%= Html.ATAuthLink("Edit", new { action = "Edit", id = item.InvitationID, siteShortName = Html.CurrentSiteShortName() }, AssessTrack.Filters.AuthScope.Site, 5, 10)%>
                <%= Html.ATAuthLink("Delete", " | ", "", new { action = "Delete", id = item.InvitationID, siteShortName = Html.CurrentSiteShortName() }, AssessTrack.Filters.AuthScope.Site, 5, 10)%>
                </div>
                -->
            </td>
            <td>
                <%= Html.Encode(item.Site.Title) %> (<%= Html.Encode(item.SiteAccessLevel) %>)
            </td>
            <td>
                <%= Html.Encode((item.CourseTerm != null)? string.Format("{0} ({1})",item.CourseTerm.Name, item.CourseTermAccessLevel) : "N/A") %>
            </td>
            <td>
                <%= (item.Accepted)? "Yes" : "No" %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ATAuthLink("Create New Invitation", new { action = "Create", siteShortName = Html.CurrentSiteShortName() }, AssessTrack.Filters.AuthScope.Site, 5, 10)%>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>
