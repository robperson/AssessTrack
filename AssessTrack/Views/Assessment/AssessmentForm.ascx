<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Controllers.AssessmentFormViewModel>" %>

<% using (Html.BeginForm()) {%>

        
            <p>
                <label for="Name">Name:</label>
                <%= Html.TextBox("Name", Model.Assessment.Name) %>
                <%= Html.ValidationMessage("Name", "*") %>
            </p>
            <p>
                <label for="DueDate">Due Date:</label>
                <%= Html.TextBox("DueDate", String.Format("{0:g}", Model.Assessment.DueDate))%>
                <%= Html.ValidationMessage("DueDate", "*") %>
            </p>
            <p>
                <label for="IsExtraCredit">Is Extra Credit:</label>
                <%= Html.CheckBox("IsExtraCredit", Model.Assessment.IsExtraCredit) %>
                <%= Html.ValidationMessage("IsExtraCredit", "*") %>
            </p>
            <p>
                <label for="AllowMultipleSubmissions">Allow Multiple Submissions:</label>
                <%= Html.CheckBox("AllowMultipleSubmissions", Model.Assessment.AllowMultipleSubmissions) %>
                <%= Html.ValidationMessage("AllowMultipleSubmissions", "*") %>
            </p>
            <p>
                <label for="AssessmentTypeID">Assessment Type :</label>
                <%= Html.DropDownList("AssessmentTypeID", Model.AssessmentTypes) %>
                <%= Html.ValidationMessage("AssessmentTypeID", "*") %>
            </p>
            
            <p>
                <label for="CreatedDate">Created Date:</label>
                <%= Html.TextBox("CreatedDate", String.Format("{0:g}", Model.Assessment.CreatedDate))%>
                <%= Html.ValidationMessage("CreatedDate", "*") %>
            </p>
            <p>
                <label for="IsVisible">Is Visible:</label>
                <%= Html.CheckBox("IsVisible", Model.Assessment.IsVisible)%>
                <%= Html.ValidationMessage("IsVisible", "*") %>
            </p>
            <p>
                <label for="IsOpen">Is Open:</label>
                <%= Html.CheckBox("IsOpen", Model.Assessment.IsOpen) %>
                <%= Html.ValidationMessage("IsOpen", "*") %>
            </p>
            <p>
                <label for="IsGradable">Is Gradable:</label>
                <%= Html.CheckBox("IsGradable", Model.Assessment.IsGradable)%>
                <%= Html.ValidationMessage("IsGradable", "*") %>
            </p>
            <div class="designer" id="<%= Html.Encode((Model.Assessment.AssessmentID != Guid.Empty)? Model.Assessment.AssessmentID.ToString() : "") %>">	
		    
			    <h2>DRAG QUESTIONS ONTO THE FORM, THEN POPULATE THE QUESTIONS WITH DATA</h2>
			    <ul id="questions">
			        <%= Model.DesignerData %>
			    </ul>
		    
	        </div>
	        <button id="build-button">Build Quiz</button>
            <p>
                <label for="Data">Data:</label>
                <%= Html.TextArea("Data", Model.Assessment.Data, 5, 50, new { id = "Data" })%>
                <%= Html.ValidationMessage("Data", "*") %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
       

    <% } %>