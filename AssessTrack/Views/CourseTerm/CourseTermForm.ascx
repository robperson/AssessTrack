<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Controllers.CourseTermViewModel>" %>
<% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="Name">Name:</label>
                <%= Html.TextBox("Name",Model.CourseTerm.Name) %>
                <%= Html.ValidationMessage("Name", "*") %>
            </p>
            <p>
                <label for="ShortName">ShortName:</label>
                <%= Html.TextBox("ShortName", Model.CourseTerm.ShortName) %>
                <%= Html.ValidationMessage("ShortName", "*") %>
            </p>
            <p>
                <label for="Information">Information:</label>
                <%= Html.TextArea("Information",Model.CourseTerm.Information) %>
                <%= Html.ValidationMessage("Information", "*") %>
            </p>
            <p>
                <label for="CourseID">Course:</label>
                <%= Html.DropDownList("CourseID",Model.CourseList) %>
            </p>
            <p>
                <label for="TermID">Term:</label>
                <%= Html.DropDownList("TermID", Model.TermList) %>
            </p>
            <p>
                <label for="Password">Password:</label>
                <%= Html.TextBox("Password", Model.CourseTerm.Password) %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>
