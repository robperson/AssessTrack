<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.DownloadAnswerCodeViewModel>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Download Answer Code
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Download Answer</h2>
    <div>
    <% using (Html.BeginForm())
       { %>
       <p>
        <label for="AssessmentID">Select An Assessment:</label>
        <%= Html.DropDownList("AssessmentID",Model.Assessments) %>
       </p>
       <p>
        <input type="submit" value="Show Answers for Assessment" />
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
            <% for(int i = 0; i < Model.Answers.Count; i++) //foreach (var answer in Model.Answers)
               {
                   AssessTrack.Models.Answer answer = Model.Answers[i];
                   String answerType = Model.AnswerTypes[i];
                   %>
              <tr>
                <td>Question #<%= answer.Question.Number%>, Answer #<%= answer.Number%></td>
                <td>
                    <% using (Html.BeginForm("GetCodeAnswerDownloadLink", "CourseTermTools", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }))
                       { %>
                       <input type="hidden" value="<%= answer.AnswerID %>" name="AnswerID" />
                       <input type="hidden" value="<%= answerType %>" name="AnswerType" />
                       <input type="submit" value="Download this Answer" />
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
