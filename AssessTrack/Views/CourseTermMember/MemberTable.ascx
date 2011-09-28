<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Controllers.CourseTermMemberTable>" %>
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
                Major
            </th>
            <th>
                Section
            </th>
        </tr>

    <% foreach (var item in Model.Members) { %>
    
        <tr>
            <td>
                <strong><%= Html.Encode(AssessTrack.Helpers.UserHelpers.GetFullNameForID(item.MembershipID))%></strong>
                <div class="row-actions">
                    <%= Html.ActionLink("Edit", "Edit", new { id = item.CourseTermMemberID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%> |
                    <%= Html.ActionLink("Details", "Details", new { id = item.CourseTermMemberID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%> |
                    <a href="mailto:<%= item.Profile.EmailAddress %>" title="Send this member an email.">Send Email</a>
                    <% if (UserHelpers.IsAccountLocked(item.MembershipID))
                       { %>
                        | <%= Html.ActionLink("Unlock", "Unlock", new { id = item.MembershipID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%>
                    <%} %>
                </div>
            </td>
            <td>
                <%= Html.Encode(item.Profile.Major) %>
            </td>
            <td>
                <%= (item.Section == null)? "N/A" : item.Section.ToString() %>
            </td>
        </tr>
    
    <% } %>

    </table>