<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Controllers.AssessmentFormViewModel>" %>
<%= Html.ValidationSummary() %>
<div class="designer" id="<%= Html.Encode((Model.Assessment.AssessmentID != Guid.Empty)? Model.Assessment.AssessmentID.ToString() : "") %>">	
		    
			    <h2>DRAG QUESTIONS ONTO THE FORM, THEN POPULATE THE QUESTIONS WITH DATA</h2>
			    <ul id="questions">
			        <%= Model.DesignerData %>
			    </ul>
		    
	        </div>
	        
    