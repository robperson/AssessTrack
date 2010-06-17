<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Models.Grade>" %>
<span><%= Html.Encode(Model.Assessment.Name) %></span>
<span>Due: <%= Html.Encode(Model.Assessment.DueDate) %></span>
<span><%= Html.Encode(Model.Points) %>/<%= Html.Encode(Model.Assessment.Weight) %> (<%= Html.Encode(Model.Percentage) %>%)</span>
