<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.Site>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.Encode(Model.Title) %></h2>

    
        <p>
            Description:
            <%= Html.Encode(Model.Description) %>
        </p>
       
    
    <p>
        <%= Html.ATAuthLink("Manage Courses for this Site", new { controller = "Course", siteShortName = Model.ShortName, action = "" }, AssessTrack.Filters.AuthScope.Site, 3, 10)%> <br />
        <%= Html.ATAuthLink("Manage Terms(Semesters) for this Site", new { controller = "Term", siteShortName = Model.ShortName, action = "" }, AssessTrack.Filters.AuthScope.Site, 3, 10)%> <br />
        <%= Html.ATAuthLink("View Course Offerings", new { controller = "CourseTerm", siteShortName = Model.ShortName, action = "Index" }, AssessTrack.Filters.AuthScope.Site, 0, 10)%>
    </p>
    <p>

        <%=Html.ATAuthLink("Edit", new { siteShortName = Model.ShortName, action = "Edit" }, AssessTrack.Filters.AuthScope.Site, 5, 10)%> <br />
        <%=Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

