<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.PerformanceReportModel>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Student Performance
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.Encode(Model.Profile.FirstName) %> <%= Html.Encode(Model.Profile.LastName) %>'s Performance</h2>
    <div id="StudentPerformancePagingLinks" class="group">
        <%= Html.ATAuthLink("Previous: " + Model.PreviousStudent.FullName, @"<span class=""prev"">", "</span>", new { id = Model.PreviousStudent.MembershipID, action = "StudentPerformance" }, AssessTrack.Filters.AuthScope.CourseTerm, 2, 10)%>
        <%= Html.ATAuthLink("Next: " + Model.NextStudent.FullName, @"<span class=""next"">", "</span>", new { id = Model.NextStudent.MembershipID, action = "StudentPerformance" }, AssessTrack.Filters.AuthScope.CourseTerm, 2, 10)%>
    </div>
    <%= Html.ATAuthLink("Return to Student List", new { action = "Students", controller = "Reports", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 5, 10)%>
    <h3>Current Grade*: <%= Html.Encode(Model.FinalLetterGrade) %> (<%= Html.Encode(string.Format("{0:0.00}%",Model.FinalGrade)) %>)</h3>
    
    <div class="legend">
    <strong>Legend: </strong> <span class="gradelink">Graded Assessment</span>, <span class="gradelink ungraded">Ungraded Assessment</span>, <span class="gradelink unsubmitted">Unsubmitted Assessment</span>.
    </div>
    <% foreach (var section in Model.GradeSections)
       {
           %>
           <h3><%=Html.Encode(section.AssessmentType.Name) %> <span><%= Html.Encode(string.Format("{0:0.00}",section.Weight / Model.TotalWeight * 100)) %>% of total grade</span></h3>
           <ul class="gradesection group">
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
                <%= Html.Encode(section.TotalPoints) %>/<%= Html.Encode(section.MaxPoints) %> (<%= Html.Encode(string.Format("{0:0.00}",section.Percentage)) %> %)
           </div>
           <%
           
       } %>

       <% Html.RenderPartial("GradeSnapshot", Model); %>
       <p>*Note: Current grade only counts assessments that have been graded and assignments for which the due date has passed.
        Your grade will change if there 
       are many ungraded assessments.</p>

</asp:Content>
