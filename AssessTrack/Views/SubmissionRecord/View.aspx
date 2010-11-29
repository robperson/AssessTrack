<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.SingleSubmissionViewModel>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Submission Details for 
	<%= AssessTrack.Helpers.UserHelpers.GetFullNameForID(Model.SubmissionRecord.StudentID)%>'s 
	<%= Model.SubmissionRecord.Assessment.Name %>
</asp:Content>
<asp:Content ID="Head" ContentPlaceHolderID="HeadContent" runat="server">
<link href="http://alexgorbatchev.com/pub/sh/current/styles/shThemeDefault.css" rel="stylesheet" type="text/css" />
    <script src="http://alexgorbatchev.com/pub/sh/current/scripts/shCore.js" type="text/javascript"></script>
    <script src="http://alexgorbatchev.com/pub/sh/current/scripts/shAutoloader.js" type="text/javascript"></script>
    <script src="http://alexgorbatchev.com/pub/sh/current/scripts/shBrushCpp.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>
    Submission Details for 
	<%= AssessTrack.Helpers.UserHelpers.GetFullNameForID(Model.SubmissionRecord.StudentID)%>'s 
	<%= Model.SubmissionRecord.Assessment.Name %>
    </h2>
    
    <h3>Submitted on <%= Model.SubmissionRecord.SubmissionDate.ToString() %></h3>
    <h3>Score: <%= Model.SubmissionRecord.Score.ToString() %></h3>
    <h3>Graded By: <%= UserHelpers.GetFullNameForID(Model.SubmissionRecord.GradedBy) %></h3>
    
    <% if (Model.OtherSubmissionRecords.Count > 0)
       { %>
    <div>
        <h4>Other Submissions for this Assessment:</h4>
        <ul>
            <% foreach (AssessTrack.Models.SubmissionRecord record in Model.OtherSubmissionRecords)
               {%>
            
            <li>
                <span>Submission on <%= record.SubmissionDate.ToString()%> (Score: <%= record.Score.ToString()%>)</span>
                <%= Html.ActionLink("Click here to view", "View", new { id = record.SubmissionRecordID })%>
            </li>
            <% } %>
        </ul>
    </div>
    <%} %>
    
    <%= Html.RenderAssessmentViewForm(Model.SubmissionRecord) %>
    <h3>Comments</h3>
        <pre><%= Html.Encode(Model.SubmissionRecord.Comments)%></pre>
</asp:Content>
