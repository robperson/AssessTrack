<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.Term>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

    <fieldset>
        <legend>Fields</legend>
        <p>
            StartDate:
            <%= Html.Encode(String.Format("{0:g}", Model.StartDate.ToShortDateString())) %>
        </p>
        <p>
            EndDate:
            <%= Html.Encode(String.Format("{0:g}", Model.EndDate.ToShortDateString())) %>
        </p>
        <p>
            Name:
            <%= Html.Encode(Model.Name) %>
        </p>
        
    </fieldset>
    <p>

        <%=Html.ActionLink("Edit", "Edit", new { id =Model.TermID }) %> |
        <%=Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

