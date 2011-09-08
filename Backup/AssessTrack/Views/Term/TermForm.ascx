<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Models.Term>" %>

<% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="StartDate">StartDate:</label>
                <%= Html.TextBox("StartDate",Model.StartDate.ToShortDateString()) %>
                <%= Html.ValidationMessage("StartDate", "*") %>
            </p>
            <p>
                <label for="EndDate">EndDate:</label>
                <%= Html.TextBox("EndDate",Model.EndDate.ToShortDateString()) %>
                <%= Html.ValidationMessage("EndDate", "*") %>
            </p>
            <p>
                <label for="Name">Name:</label>
                <%= Html.TextBox("Name",Model.Name) %>
                <%= Html.ValidationMessage("Name", "*") %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>