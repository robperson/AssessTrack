<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.ViewModels.TagReviewViewModel>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Review for <%= Model.Tag.Name %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Review for <%= Model.Tag.Name %></h2>
    <h3>Assessments you should review:</h3>
    <ul>
        <% foreach (var assessment in Model.Assessments)
           { %>
           <li>
            You scored <%= assessment.Score() %>% on <%= assessment.Name %>. <%= Html.ATAuthLink("Click here to review.", "", "", new { controller = "Assessment", action="Preview", id = (assessment as AssessTrack.Models.Assessment).AssessmentID, siteShortName = Html.CurrentSiteShortName(), courseTermShortName = Html.CurrentCourseTermShortName() }, AssessTrack.Filters.AuthScope.CourseTerm, 1, 10)%>
           </li>
        <%} %>
    </ul>
    
    <h3>Specific Questions to review</h3>
    <% foreach (var question in Model.Questions)
       { %>
       <div>
       <p><strong><%= question.Name %></strong></p>
            <%= Html.RenderAssessmentFragment((question as AssessTrack.Models.Question).Data) %>
       </div>
    <%} %>
    
    <% foreach (var answer in Model.Answers)
       { %>
       <div>
            <p><strong><%= answer.Name %></strong></p>
            <%= Html.RenderAssessmentFragment((answer as AssessTrack.Models.Answer).Question.Data) %>
       </div>
    <%} %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>
