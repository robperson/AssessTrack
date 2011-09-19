<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<AssessTrack.Models.CourseTermMessage>>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>

    <h3>Recent Messages</h3>
    <ul>
    <% foreach (var message in Model)
       {%>
           <li><%= Html.ActionLink(message.Subject, "Details", new { controller = "CourseTermMessage", id = message.MessageID, siteShortName = message.CourseTerm.Site.ShortName, courseTermShortName = message.CourseTerm.ShortName })%>
            from <%= message.CourseTerm.Name %>
           </li>
       <%} %>
       </ul>

