<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.DownloadAnswerCodeViewModel>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Download Answer Code
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Download Answer Code</h2>
    <div>
    <% using (Html.BeginForm())
       { %>
       <p>
        <label for="AssessmentID">Select An Assessment:</label>
        <%= Html.DropDownList("AssessmentID",Model.Assessments) %>
       </p>
       <p>
        <input type="submit" value="Show Code Answers for Assessment" />
       </p>
    <% } %>
    </div>
    
    <% if (Model.Answers != null && Model.Answers.Count > 0)
       { %>
        <h3>Code Answers in <%= Html.Encode(Model.Assessment.Name)%></h3>
        <table>
            <tr>
                <th>Question and Answer Number</th>
                <th>Download</th>
            </tr>
            <% foreach (var answer in Model.Answers)
               { %>
              <tr>
                <td>Question #<%= answer.Question.Number%>, Answer #<%= answer.Number%></td>
                <td>
                    <% using (Html.BeginForm("GetCodeAnswerDownloadLink", "CourseTermTools", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }))
                       { %>
                       <input type="hidden" value="<%= answer.AnswerID %>" name="AnswerID" />
                       <input type="submit" value="Download Code for this Answer" />
                    <% } %>
                </td>
              </tr>     
            <% } %>
        </table>
    <% }
       else
       { %>
       <p>There are no Code Answers in the selected Assessment.</p>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>
