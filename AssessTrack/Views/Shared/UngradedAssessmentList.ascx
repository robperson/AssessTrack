<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<AssessTrack.Models.Assessment>>" %>

     <ul>
    <% foreach (var assessment in Model)
       {%>
           <li><%= Html.ActionLink(assessment.Name, "Index", new { controller = "SubmissionRecord", id = assessment.AssessmentID, siteShortName = assessment.CourseTerm.Site.ShortName, courseTermShortName = assessment.CourseTerm.ShortName })%>
            </li>
       <%} %>
       </ul>


