<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Models.ViewModels.TagEditViewModel>" %>
<% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="Name">Name:</label>
                <%= Html.TextBox("Name", Model.Tag.Name) %>
                <%= Html.ValidationMessage("Name", "*") %>
            </p>
            <p>
                <label for="DescriptiveName">Descriptive Name:</label>
                <%= Html.TextBox("DescriptiveName", Model.Tag.DescriptiveName)%>
                <%= Html.ValidationMessage("DescriptiveName", "*")%>
            </p>
            <p>
                <label for="Description">Description:</label>
                <%= Html.TextArea("Description", Model.Tag.Description) %>
                <%= Html.ValidationMessage("Description", "*") %>
            </p>
            <p>
                <label for="IsCourseOutcome">Is Course Outcome?:</label>
                <%= Html.CheckBox("IsCourseOutcome", Model.Tag.IsCourseOutcome)%>
                <%= Html.ValidationMessage("IsCourseOutcome", "*")%>
                
                
                
            </p>
            <div class="outcomes">
                <h3>Related Program Outcomes</h3>
                <% foreach (var outcome in Model.ProgramOutcomes)
                   { %>
                <p>
                     <label>
                        <%= Html.CheckBox(outcome.Outcome.ProgramOutcomeID.ToString(),outcome.AreRelated) %>
                        <%= outcome.Outcome.Label %> - <%= outcome.Outcome.Description %>
                     </label>  
                </p>
                 <%  } %>
            </div>
            <p>
                <label for="Tutorial">Tutorial (HTML):</label>
                <%= Html.TextArea("Tutorial", Model.Tag.Tutorial) %>
                <%= Html.ValidationMessage("Tutorial", "*") %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

