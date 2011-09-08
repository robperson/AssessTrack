<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Controllers.ProfileTable>" %>
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
                Email
            </th>
            <th>
                Registration Date
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
                    <%= Html.ActionLink("Edit", "Edit", new { id = item.MembershipID })%> |
                    <%= Html.ActionLink("Details", "Details", new { id = item.MembershipID })%> |
                    <a href="mailto:<%= item.EmailAddress %>" title="Send this member an email.">Send Email</a>
                    <% if (UserHelpers.IsAccountLocked(item.MembershipID))
                       { %>
                        | <%= Html.ActionLink("Unlock", "Unlock", new { id = item.MembershipID })%>
                    <%} %>
                </div>
            </td>
            <td>
                <%= Html.Encode(item.EmailAddress) %>
            </td>
            <td>
                <%= UserHelpers.GetUserRegistrationDate(item.MembershipID) %>
            </td>
            <td>
                <%= Html.Encode(item.AccessLevel) %>
            </td>
        </tr>
    
    <% } %>

    </table>