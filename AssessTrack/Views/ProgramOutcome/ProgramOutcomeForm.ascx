<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Models.ProgramOutcome>" %>

    

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="Label">Label:</label>
                <%= Html.TextBox("Label", Model.Label)%>
                <%= Html.ValidationMessage("Label", "*")%>
            </p>
            <p>
                <label for="Description">Description:</label>
                <%= Html.TextBox("Description", Model.Description) %>
                <%= Html.ValidationMessage("Description", "*") %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    


