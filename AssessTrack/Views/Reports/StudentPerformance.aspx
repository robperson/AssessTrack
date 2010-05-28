<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.PerformanceReportModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	StudentPerformance
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.Encode(Model.Profile.FirstName) %> <%= Html.Encode(Model.Profile.LastName) %>'s Performance</h2>
    <% foreach (var section in Model.GradeSections)
       {
           %>
           <h3><%=Html.Encode(section.AssessmentType.Name) %> <span><%= Html.Encode(section.AssessmentType.Weight) %>%</span></h3>
           <ul>
           <%foreach (var grade in section.Grades)
             {
                 %>
                 <li><a href="#">
                    <span><%= Html.Encode(grade.Assessment.Name) %></span>
                    <span>D: <%= Html.Encode(grade.Assessment.DueDate) %></span>
                    <span><%= Html.Encode(grade.Points) %>/<%= Html.Encode(grade.Assessment.Weight) %> (<%= Html.Encode(grade.Percentage) %>%)</span>
                 </a></li>
                 <%
             } %>
           </ul>
           <div>
                <%= Html.Encode(section.TotalPoints) %>/<%= Html.Encode(section.MaxPoints) %> (<%= Html.Encode(section.Percentage) %> %)
           </div>
           <%
           
       } %>

</asp:Content>
