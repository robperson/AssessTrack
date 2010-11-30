<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Models.Tag>" %>
<% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="Name">Name:</label>
                <%= Html.TextBox("Name", Model.Name) %>
                <%= Html.ValidationMessage("Name", "*") %>
            </p>
            <p>
                <label for="DescriptiveName">Descriptive Name:</label>
                <%= Html.TextBox("DescriptiveName", Model.DescriptiveName)%>
                <%= Html.ValidationMessage("DescriptiveName", "*")%>
            </p>
            <p>
                <label for="Description">Description:</label>
                <%= Html.TextArea("Description", Model.Description) %>
                <%= Html.ValidationMessage("Description", "*") %>
            </p>
            <p>
                <label for="IsCourseOutcome">Is Course Outcome?:</label>
                <%= Html.CheckBox("IsCourseOutcome", Model.IsCourseOutcome)%>
                <%= Html.ValidationMessage("IsCourseOutcome", "*")%>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

