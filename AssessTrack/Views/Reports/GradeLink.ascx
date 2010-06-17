<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Models.Grade>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<% if (Model.SubmissionRecord != null)
   { %>
<% if (Html.CheckAuthorization(5,10,AssessTrack.Filters.AuthScope.CourseTerm)) { %>
<a href="<%= Url.Action("Grade", "SubmissionRecord", new { id = Model.SubmissionRecord.SubmissionRecordID }) %>"
    title="Click to Grade this submission">
    <% Html.RenderPartial("GradeInfo"); %>
</a>
<% } %>
<% if (Html.CheckAuthorization(0,4,AssessTrack.Filters.AuthScope.CourseTerm)) { %>
<a href="<%= Url.Action("View", "SubmissionRecord", new { id = Model.SubmissionRecord.SubmissionRecordID }) %>"
    title="Click to View this submission">
    <% Html.RenderPartial("GradeInfo"); %>
</a>
<% } %>
<% }
   else
   { %>
<% if (UserHelpers.GetCurrentUserID() == Model.Student.MembershipID)
           { %>

<a href="<%= Url.Action("Submit", "Assessment", new { id = Model.Assessment.AssessmentID }) %>"
    title="Click to Submit this Assessment">
    <% Html.RenderPartial("GradeInfo"); %>
</a>

<% }
           else
           { %>
<% Html.RenderPartial("GradeInfo"); %>
<% } %>
<% } %>