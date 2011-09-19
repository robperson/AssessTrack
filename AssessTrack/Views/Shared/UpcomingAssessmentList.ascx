<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<AssessTrack.Models.Assessment>>" %>

    <h3>Upcoming Due Dates</h3>
    <ul>
    <% foreach (var assessment in Model)
       {%>
           <li><%= Html.ActionLink(assessment.Name, "Submit", new { controller = "Assessment", id = assessment.AssessmentID, siteShortName = assessment.CourseTerm.Site.ShortName, courseTermShortName = assessment.CourseTerm.ShortName })%>
            from <%= assessment.CourseTerm.Name %> due <%= assessment.DueDate.ToString("M/d/yy h:mm tt") %>
           </li>
       <%} %>
       </ul>



