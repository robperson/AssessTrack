<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Models.AssessmentType>" %>
<% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="Name">Name:</label>
                <%= Html.TextBox("Name", Model.Name) %>
                <%= Html.ValidationMessage("Name", "*") %>
            </p>
            <p>
                <label for="Weight">Weight:</label>
                <%= Html.TextBox("Weight", String.Format("{0:F}", Model.Weight)) %>
                <%= Html.ValidationMessage("Weight", "*") %>
            </p>
            <p>
                <label for="IsExtraCredit">Is Question Bank:</label>
                <%= Html.CheckBox("QuestionBank", Model.QuestionBank) %>
                <%= Html.ValidationMessage("QuestionBank", "*") %>
            </p>
            <p>
                <label for="CourseID">Course:</label>
                <span id="CourseID"><%= Html.Encode(Model.CourseTerm.Name) %></span>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>
