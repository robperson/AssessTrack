<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Models.Grade>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<% if (Model.SubmissionRecord != null)
   { %>
        <% if (Html.CheckAuthorization(2,10,AssessTrack.Filters.AuthScope.CourseTerm)) { %>
        <a class="gradelink <%= (Model.SubmissionRecord.GradedBy == null)? "ungraded" : "graded" %>" href="<%= Url.Action("Grade", "SubmissionRecord", new { id = Model.SubmissionRecord.SubmissionRecordID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }) %>"
            title="Click to Grade this submission">
            <% Html.RenderPartial("GradeInfo"); %>
        </a>
        <% } %>
        <% if (Html.CheckAuthorization(1,1,AssessTrack.Filters.AuthScope.CourseTerm)) { %>
        <a class="gradelink <%= (Model.SubmissionRecord.GradedBy == null)? "ungraded" : "graded" %>" href="<%= Url.Action("View", "SubmissionRecord", new { id = Model.SubmissionRecord.SubmissionRecordID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }) %>"
            title="Click to View this submission">
            <% Html.RenderPartial("GradeInfo"); %>
        </a>
        <% } %>
<% }
   else
   { %>
        <% if (UserHelpers.GetCurrentUserID() == Model.Student.MembershipID)
                   { %>

            <a class="gradelink unsubmitted" href="<%= Url.Action("Submit", "Assessment", new { id = Model.Assessment.AssessmentID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }) %>"
                title="Click to Submit this Assessment">
                <% Html.RenderPartial("GradeInfo"); %>
            </a>

        <% }
           else
           { %>
                <span class="gradelink unsubmitted">
                    <% Html.RenderPartial("GradeInfo"); %>
                </span>
        <% } %>
<% } %>