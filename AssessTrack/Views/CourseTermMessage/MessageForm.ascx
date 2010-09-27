<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Models.CourseTermMessage>" %>
<% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="Subject">Subject:</label>
                <%= Html.TextBox("Subject", Model.Subject) %>
                <%= Html.ValidationMessage("Subject", "*") %>
            </p>
            <p>
                <label for="Body">Body:</label>
                <%= Html.TextArea("Body", Model.Body) %>
                <%= Html.ValidationMessage("Body", "*") %>
            </p>
            
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>
