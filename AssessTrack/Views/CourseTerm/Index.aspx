<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AssessTrack.Controllers.CourseTermIndexModel>>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Course Offerings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Course Offerings</h2>

    <table>
        <tr>
            <th>Name</th>
           <th>Information</th>
            <th>Term</th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <strong><%= Html.ActionLink(item.CourseTerm.Name, "Details", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = item.CourseTerm.ShortName})%></strong>
                <div class="row-actions">
                    <% if (item.UserEnrolled)
                       { %>
                    <span>You are enrolled in this Course Offering</span>
                    <% }
                       else
                       { %>
                       <%= Html.ActionLink("Enroll", "Join", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = item.CourseTerm.ShortName })%>
                    <% } %>
                </div>
            </td>
            <td>
                <%= Html.Encode(item.CourseTerm.Information) %>
            </td>
            <td>
                <%= Html.Encode(item.CourseTerm.Term.Name) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ATAuthLink("Create New Course Offering", new { action = "Create", siteShortName = Html.CurrentSiteShortName() }, AssessTrack.Filters.AuthScope.Site, 5, 10)%>
        <%= Html.ActionLink("Enroll in a Course Offering", "Join", new { siteShortName = Html.CurrentSiteShortName() })%>
    </p>

</asp:Content>

