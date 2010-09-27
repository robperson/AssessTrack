<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<AssessTrack.Models.Assessment>>" %>
<li>
    <h3>Upcoming Due Dates</h3>
    <ul>
    <% foreach (var assessment in Model)
       {%>
           <li><%= Html.ActionLink(assessment.Name, "Submit", new { controller = "Assessment", id = assessment.AssessmentID, siteShortName = assessment.CourseTerm.Site.ShortName, courseTermShortName = assessment.CourseTerm.ShortName })%>
            from <%= assessment.CourseTerm.ShortName %> due <%= assessment.DueDate.ToString("M/d/yy h:mm tt") %>
           </li>
       <%} %>
       </ul>
</li>


