<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Controllers.SiteMemberTable>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<p>
    <a href="<%= Model.EmailAllLink %>">Email All <%= Model.Caption %></a>
</p>
 <table>
        <caption><%= Model.Caption %></caption>
        <tr>
            <th>
                Member Name
            </th>
            <th>
                Username
            </th>
            <th>
                Password
            </th>
            <th>
                Major
            </th>
            <th>
                Access Level
            </th>
        </tr>

    <% foreach (var item in Model.Members) { %>
    
        <tr>
            <td>
                <strong><%= Html.Encode(AssessTrack.Helpers.UserHelpers.GetFullNameForID(item.MembershipID))%></strong>
                <div class="row-actions">
                    <%= Html.ActionLink("Edit", "Edit", new { id = item.SiteMemberID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%> |
                    <%= Html.ActionLink("Details", "Details", new { id = item.SiteMemberID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%> |
                    <a href="mailto:<%= item.Profile.EmailAddress %>" title="Send this member an email.">Send Email</a>
                </div>
            </td>
            <td>
                <%= Html.Encode(UserHelpers.GetUsernameForID(item.MembershipID)) %>
            </td>
            <td>
                <%= Html.Encode(UserHelpers.GetPasswordForID(item.MembershipID)) %>
            </td>
            <td>
                <%= Html.Encode(item.Profile.Major) %>
            </td>
            <td>
                <%= Html.Encode(item.AccessLevel) %>
            </td>
        </tr>
    
    <% } %>

    </table>