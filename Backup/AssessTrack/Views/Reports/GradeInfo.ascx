<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Models.Grade>" %>
<span class="assessmentinfotitle"><strong><%= Html.Encode(Model.Assessment.Name) %></strong></span>
<span class="duedate" title="Due: <%= Html.Encode(Model.Assessment.DueDate) %>, Submitted: <%= (Model.SubmissionRecord != null)? Html.Encode(Model.SubmissionRecord.SubmissionDate) : "" %> ">
    Due: <%= Html.Encode(Model.Assessment.DueDate.ToString("M/dd h:mmt")) %> <%= (Model.IsLate)? "*" : "" %>
</span>
<span class="grade"><%= Html.Encode(Model.Points) %>/<%= Html.Encode(Model.Assessment.Weight) %> (<%= Html.Encode(Model.Percentage) %>%)</span>
