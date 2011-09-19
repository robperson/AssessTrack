<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.SiteViews.SiteDetailsViewModel>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.Encode(Model.Site.Title) %></h2>

    
    <div class="site-description">
        <%= Html.Encode(Model.Site.Description) %>
    </div>
    
    <h3>Your Courses</h3>   
    <ul>
    <%foreach (var course in Model.UserCourseOfferings)
      { %>
        <li>
            <%= Html.CourseTermLink(course,"","") %>
        </li>

    <% } %>
    </ul>

    <p>Find your course(s) in the list below and click 'Enroll'.</p>
    <h3>All Current Courses in <%= Html.Encode(Model.Site.Title) %></h3>   
    <ul>
    <%foreach (var item in Model.AllCourseOfferings)
      { %>
        <li>
            <%= Html.CourseTermLink(item.CourseTerm,"","") %>
            <% if (item.StudentNotEnrolled)
               { %>
                <span class="courseterm-enroll-link">
                <%= Html.ActionLink("Enroll", "Join", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = item.CourseTerm.ShortName, id = item.CourseTerm.CourseTermID, controller = "CourseTerm" })%>
                </span>

            <% } %>
        </li>

    <% } %>
    </ul>
    <p>

        <%=Html.ATAuthLink("Edit", new { siteShortName = Model.Site.ShortName, action = "Edit" }, AssessTrack.Filters.AuthScope.Site, 5, 10)%> <br />
    </p>

</asp:Content>

