<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Controllers.CreateSubmissionViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create Submission
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create Submission</h2>
    <p>Complete the form below to create a new, graded submission for a student without any input from them.</p>
    <div>
        <%= Html.ValidationSummary() %>
    </div>
    <div>
        <% using (Html.BeginForm())
           { %>
            <p>
                <label for="AssessmentID">Select an Assessment:</label>
                <%= Html.DropDownList("AssessmentID",Model.Assessments) %>
            </p>
            <p>
                <label for="MembershipID">Select a Student:</label>
                <%= Html.DropDownList("MembershipID",Model.Students) %>
            </p>
            <p>
                <label for="SubmissionDate">Submission Date:</label>
                <%= Html.TextBox("SubmissionDate",DateTime.Now) %>
            </p>
            <p>
                <label for="Score">Score:</label>
                <%= Html.TextBox("Score","0.0") %>%
            </p>
            <p>
                <input type="submit" value="Create Submission" />
            </p>
        <%} %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>
