<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.PerformanceReportModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Student Performance
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.Encode(Model.Profile.FirstName) %> <%= Html.Encode(Model.Profile.LastName) %>'s Performance</h2>
    <div id="StudentPerformancePagingLinks">
        <span class="prev"><%= Html.ActionLink("Previous: " + Model.PreviousStudent.FullName, "StudentPerformance", new {id = Model.PreviousStudent.MembershipID}) %></span>
        <span class="next"><%= Html.ActionLink("Next: " + Model.NextStudent.FullName, "StudentPerformance", new {id = Model.NextStudent.MembershipID}) %></span>
    </div>
    <h3>Final Grade: <%= Html.Encode(Model.FinalLetterGrade) %> (<%= Html.Encode(string.Format("{0:0.00}%",Model.FinalGrade)) %>)</h3>
    
    <% foreach (var section in Model.GradeSections)
       {
           %>
           <h3><%=Html.Encode(section.AssessmentType.Name) %> <span><%= Html.Encode(section.AssessmentType.Weight) %>%</span></h3>
           <ul>
           <%foreach (var grade in section.Grades)
             {
                 %>
                 <li>
                    <% Html.RenderPartial("GradeLink", grade, ViewData); %>
                 </li>
                 <%
             } %>
           </ul>
           <div>
                <%= Html.Encode(section.TotalPoints) %>/<%= Html.Encode(section.MaxPoints) %> (<%= Html.Encode(section.Percentage) %> %)
           </div>
           <%
           
       } %>

</asp:Content>
