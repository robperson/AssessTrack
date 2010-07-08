<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.CourseTermMemberViewModel>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
    <h3>Students:</h3>
    <table>
        <tr>
            <th></th>
            <th>
                User
            </th>
            <th>
                AccessLevel
            </th>
        </tr>

    <% foreach (var item in Model.Students) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id = item.CourseTermMemberID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%> |
                <%= Html.ActionLink("Details", "Details", new { id = item.CourseTermMemberID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%> |
                <a href="mailto:<%= item.Profile.EmailAddress %>" title="Send this member an email.">Send Email</a>
            </td>
            <td>
                <%= Html.Encode(AssessTrack.Helpers.UserHelpers.GetFullNameForID(item.MembershipID))%>
            </td>
            <td>
                <%= Html.Encode(item.AccessLevel) %>
            </td>
        </tr>
    
    <% } %>

    </table>
    
    <h3>Power Users (TA):</h3>
    <table>
        <tr>
            <th></th>
            <th>
                User
            </th>
            <th>
                AccessLevel
            </th>
        </tr>

    <% foreach (var item in Model.PowerUsers) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id = item.CourseTermMemberID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%> |
                <%= Html.ActionLink("Details", "Details", new { id = item.CourseTermMemberID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%> |
                <a href="mailto:<%= item.Profile.EmailAddress %>" title="Send this member an email.">Send Email</a>
            </td>
            <td>
                <%= Html.Encode(AssessTrack.Helpers.UserHelpers.GetFullNameForID(item.MembershipID))%>
            </td>
            <td>
                <%= Html.Encode(item.AccessLevel) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <h3>Super Users (Instructors):</h3>
    <table>
        <tr>
            <th></th>
            <th>
                User
            </th>
            <th>
                AccessLevel
            </th>
        </tr>

    <% foreach (var item in Model.SuperUsers) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id = item.CourseTermMemberID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%> |
                <%= Html.ActionLink("Details", "Details", new { id = item.CourseTermMemberID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%> |
                <a href="mailto:<%= item.Profile.EmailAddress %>" title="Send this member an email.">Send Email</a>
            </td>
            <td>
                <%= Html.Encode(AssessTrack.Helpers.UserHelpers.GetFullNameForID(item.MembershipID))%>
            </td>
            <td>
                <%= Html.Encode(item.AccessLevel) %>
            </td>
        </tr>
    
    <% } %>

    </table>
    
    <h3>Owners:</h3>
    <table>
        <tr>
            <th></th>
            <th>
                User
            </th>
            <th>
                AccessLevel
            </th>
        </tr>

    <% foreach (var item in Model.Owners) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id = item.CourseTermMemberID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%> |
                <%= Html.ActionLink("Details", "Details", new { id = item.CourseTermMemberID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%> |
                <a href="mailto:<%= item.Profile.EmailAddress %>" title="Send this member an email.">Send Email</a>
            </td>
            <td>
                <%= Html.Encode(AssessTrack.Helpers.UserHelpers.GetFullNameForID(item.MembershipID))%>
            </td>
            <td>
                <%= Html.Encode(item.AccessLevel) %>
            </td>
        </tr>
    
    <% } %>

    </table>
    
    <h3>Excluded Users:</h3>
    <table>
        <tr>
            <th></th>
            <th>
                User
            </th>
            <th>
                AccessLevel
            </th>
        </tr>

    <% foreach (var item in Model.Excluded) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id = item.CourseTermMemberID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%> |
                <%= Html.ActionLink("Details", "Details", new { id = item.CourseTermMemberID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%> |
                <a href="mailto:<%= item.Profile.EmailAddress %>" title="Send this member an email.">Send Email</a>
            </td>
            <td>
                <%= Html.Encode(AssessTrack.Helpers.UserHelpers.GetFullNameForID(item.MembershipID))%>
            </td>
            <td>
                <%= Html.Encode(item.AccessLevel) %>
            </td>
        </tr>
    
    <% } %>

    </table>
</asp:Content>

