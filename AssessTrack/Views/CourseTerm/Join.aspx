<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.CourseTermJoinModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Join
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Enroll in a Course Offering</h2>
    <% using (Html.BeginForm()) { %>
    <p>
        <%= Html.DropDownList("id",Model.CourseTermsList) %>
        <input type="submit" value="Enroll!" />
    </p>    
    <% } %>
</asp:Content>
