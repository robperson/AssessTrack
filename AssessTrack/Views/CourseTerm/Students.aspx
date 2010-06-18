<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.CourseTerm>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Students
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Students in <%= Html.Encode(Model.Name) %></h2>
    <ul>
    <% foreach (var student in Model.GetMembers(1,1))
       {
           %>
           <li><%= Html.RouteLink(student.Profile.FirstName + " " + student.Profile.LastName, new { controller = "Reports", id = student.MembershipID, action = "StudentPerformance", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%></li>
           <%
       } %>
    </ul>
</asp:Content>
