<%@ Import Namespace="AssessTrack.Helpers" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.SubmissionRecordViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Submission Records
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Submission Records</h2>
    <div>
        <% using (Html.BeginForm()) { %>
           <%=Html.DropDownList("id", Model.AssessmentList)%>
           <input type="submit" value="Get Submission Records"/>
        <% } %>
        
    </div>
    
    
        <% if (Model.AssessmentList.SelectedValue != null)
           { %>
           <div>
                <% using (Html.BeginForm(new { action = "GradeAll", controller = "SubmissionRecord", siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName(), id = new Guid(Model.AssessmentList.SelectedValue.ToString()) }))
                   {%>
                   <input type="submit" value="Grade All These Submissions" />
                <%}%>
           </div>
           <%}%>
    
    
    <div>
        <%= Html.ActionLink("Create A Submission", "CreateSubmission", new { siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%>
    </div>
    <table>
        <tr>
            <th>
                Student
            </th>
            <th>
                SubmissionDate
            </th>
            <th>
                Grade
            </th>
            <th>
                GradedOn
            </th>
            <th>
                GradedBy
            </th>
        </tr>

    <% foreach (var item in Model.Submissions) { %>
    
        <tr>
            <td>
                <strong><%= Html.Encode(UserHelpers.GetFullNameForID(item.StudentID))%></strong>
                <div class="row-actions">
                    <%= Html.ActionLink("Grade", "Grade", new { id = item.SubmissionRecordID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%>
                    |
                    <%= Html.ActionLink("View", "View", new { id = item.SubmissionRecordID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() })%>
                </div>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.SubmissionDate)) %>
            </td>
            <td>
                <%= Html.Encode(item.Score) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.GradedOn)) %>
            </td>
            <td>
                <%= Html.Encode(UserHelpers.GetFullNameForID(item.GradedBy))%>
            </td>
        </tr>
    
    <% } %>

    </table>

</asp:Content>

