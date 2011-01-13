<%@ Import Namespace="AssessTrack.Helpers"%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.SubmissionRecord>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Grade
</asp:Content>

<asp:Content ID="Head" ContentPlaceHolderID="HeadContent" runat="server">
<link href="http://alexgorbatchev.com/pub/sh/current/styles/shThemeDefault.css" rel="stylesheet" type="text/css" />
    <script src="http://alexgorbatchev.com/pub/sh/current/scripts/shCore.js" type="text/javascript"></script>
    <script src="http://alexgorbatchev.com/pub/sh/current/scripts/shAutoloader.js" type="text/javascript"></script>
    <script src="http://alexgorbatchev.com/pub/sh/current/scripts/shBrushCpp.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.autogrowtextarea.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Grade Submission</h2>
    <h3><%= Model.Assessment.Name %></h3>
    <p>
    <%= Membership.GetUser(Model.StudentID).UserName %> submitted this on
    <%= Model.SubmissionDate.ToString() %>.
    </p>
    <div><button id="InvokeCompilerButton" onclick="InvokeCompiler('<%= Model.SubmissionRecordID.ToString() %>');">Click to Compile All Code Answers</button></div>
    <p id="CompilerOutput"></p>
    <% using (Html.BeginForm()) { %>
    
        <%= Html.RenderAssessmentGradingForm(Model) %>
        <hr />
        <h3>Comments</h3>
        <%= Html.TextArea("Comments", Model.Comments, new { rows = "10", cols = "50" })%>
        <input type="submit" value="Save Scores" />
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ExtraContent" runat="server">
<script type="text/javascript">
    SyntaxHighlighter.all();
    $('.response-comment, #Comments').autogrow();
</script>
</asp:Content>