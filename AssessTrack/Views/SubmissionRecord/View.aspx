<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.SubmissionRecord>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Submission Details for 
	<%= AssessTrack.Helpers.UserHelpers.GetFullNameForID(Model.StudentID)%>'s 
	<%= Model.Assessment.Name %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>
    Submission Details for 
	<%= AssessTrack.Helpers.UserHelpers.GetFullNameForID(Model.StudentID)%>'s 
	<%= Model.Assessment.Name %>
    </h2>
    <h3>Submitted on <%= Model.SubmissionDate.ToShortDateString() %></h3>
    <h3>Score: <%= Model.Score.ToString() %></h3>
    
    <%= Html.RenderAssessmentViewForm(Model) %>

</asp:Content>
