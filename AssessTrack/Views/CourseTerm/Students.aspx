<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.CourseTerm>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Students
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Students in <%= Html.Encode(Model.Name) %></h2>
    <ul>
    <% foreach (var student in Model.GetMembers(0,2))
       {
           %>
           <li><%= Html.RouteLink(student.Profile.FirstName + " " + student.Profile.LastName, new { controller = "Reports", id = student.MembershipID, action = "StudentPerformance" })%></li>
           <%
       } %>
    </ul>
</asp:Content>
