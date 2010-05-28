<%@ Import Namespace="AssessTrack.Helpers" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.Assessment>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Submit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Submit</h2>
    <% using (Html.BeginForm()) { %>
           
           <%= Html.RenderAssessmentSubmissionForm(Model)%>
           
           <input type="submit" value="Submit" />
           
    <% } %>
</asp:Content>
