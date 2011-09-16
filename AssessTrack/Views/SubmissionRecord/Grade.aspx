<%@ Import Namespace="AssessTrack.Helpers"%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.SingleSubmissionViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Grade
</asp:Content>

<asp:Content ID="Head" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        // This will prevent the enter key from submitting any forms
        $("input").bind("keypress", function (e) {
            if (e.keyCode == 13) {
                return false;
            }
        });
    }); 
 </script>
<link href="http://alexgorbatchev.com/pub/sh/current/styles/shThemeDefault.css" rel="stylesheet" type="text/css" />
    <script src="http://alexgorbatchev.com/pub/sh/current/scripts/shCore.js" type="text/javascript"></script>
    <script src="http://alexgorbatchev.com/pub/sh/current/scripts/shAutoloader.js" type="text/javascript"></script>
    <script src="http://alexgorbatchev.com/pub/sh/current/scripts/shBrushCpp.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Grade Submission</h2>
    <h3><%= Model.SubmissionRecord.Assessment.Name%></h3>
    <p>
    <%= AssessTrack.Helpers.UserHelpers.GetFullNameForID(Model.SubmissionRecord.StudentID)%> submitted this on
    <%= Model.SubmissionRecord.SubmissionDate.ToString()%>.
    </p>
    <% if (Model.OtherSubmissionRecords.Count > 0)
       { %>
    <div>
        <h4>Other Submissions for this Assessment (only the highest scoring submission will be counted):</h4>
        <ul>
            <% foreach (AssessTrack.Models.SubmissionRecord record in Model.OtherSubmissionRecords)
               {%>
            
            <li>
                <span>Submission on <%= record.SubmissionDate.ToString()%> (Score: <%= record.Score.ToString()%>)</span>
                <%= Html.ActionLink("Click here to grade", "Grade", new { id = record.SubmissionRecordID })%>
            </li>
            <% } %>
        </ul>
    </div>
    <%} %>
    <div><button id="InvokeCompilerButton" onclick="InvokeCompiler('<%= Model.SubmissionRecord.SubmissionRecordID.ToString() %>');">Click to Compile All Code Answers</button></div>
    <p id="CompilerOutput"></p>
    <% using (Html.BeginForm()) { %>
    
        <%= Html.RenderAssessmentGradingForm(Model.SubmissionRecord) %>
        <hr />
        <h3>Comments</h3>
        <%= Html.TextArea("Comments", Model.SubmissionRecord.Comments, new { rows = "10", cols = "50" })%>
        <input type="submit" value="Save Scores" />
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ExtraContent" runat="server">
<script type="text/javascript">
    SyntaxHighlighter.all();
</script>
</asp:Content>