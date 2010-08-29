<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.CourseTermJoinModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Join
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Enroll in a Course Offering</h2>
    <% using (Html.BeginForm()) { %>
    <%= Html.ValidationSummary("Enrollment was unsuccessful. Please correct the errors and try again.") %>
    
    <p>
        <label for="id">Course Offering:</label>
        <%= Html.DropDownList("id",Model.CourseTermsList) %>
        
    </p>    
    <p>
        <label for="Password">Password:</label>
        <%= Html.TextBox("Password") %>
    </p>
    <p>
        <input type="submit" value="Enroll!" />
    </p>
    <% } %>
</asp:Content>
