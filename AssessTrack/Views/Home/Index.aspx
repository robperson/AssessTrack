<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.Home.HomePageViewModel>" %>
<%@ Import Namespace="AssessTrack.Models.Home" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>My Sites &amp; Courses:</h2>
    <p><%= Html.ATAuthLink("Join A Site", new { Action = "Join", controller = "Site" }, AssessTrack.Filters.AuthScope.Application, 1, 10)%></p>
    <ul class="site-sections">
    <% foreach (SiteSection siteSection in Model.SiteSections)
       {%>
       <li class="site-section">
           <h3><%= Html.SiteLink(siteSection.Site,"","") %></h3>
           <ul class="courseterm-sections">
                <% foreach (CourseTermSection courseTermSection in siteSection.CourseTermSections)
                   { %>
                  <li class="courseterm-section">
                        <strong><%= Html.CourseTermLink(courseTermSection.CourseTerm,"","") %></strong>
                        <% if (courseTermSection.DisplayGrade){ %><span><%= courseTermSection.Grade %></span> <% } %>
                  </li>     
                <% } %>
                <% if (siteSection.CourseTermSections.Count == 0)
                   { %>
                   <li class="courseterm-section">Not enrolled in any courses</li>
                <% } %>
           </ul>
       </li>
         <p><%= Html.ATAuthLink("Join A Site", new { Action = "Join", controller = "Site" }, AssessTrack.Filters.AuthScope.Application, 1, 10)%></p>  
    <% }%>
    </ul>
</asp:Content>
