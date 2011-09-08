<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.AssessmentIndexViewModel>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Assessments
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
    <h2>Assessments</h2>
    
    <p>
        <%= Html.ATAuthLink("Create A New Assessment", new { Action = "Create", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
    </p>
    
    
    <% foreach (var table in Model.AssessmentTables) 
       { 
           Html.RenderPartial("AssessmentTable", table); 
       } %>
    
    
    <p>
        <%= Html.ATAuthLink("Create A New Assessment", new { Action = "Create", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
    </p>

</asp:Content>

