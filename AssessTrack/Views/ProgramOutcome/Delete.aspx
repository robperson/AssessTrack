<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.ProgramOutcome>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <h2>Are you sure you want to delete "<%= Model.Label + " - " + Model.Description %>"?</h2>
    <p>Deleting this Outcome will delete all data associated with it. This action <strong>cannot</strong> be undone.</p>
    <p>If you arrived here by accident, press the back button or click on a different page.</p>
    <% using (Html.BeginForm())
       { %>
            <%= Html.Hidden("id", Model.ProgramOutcomeID)%>
            <p>
                <input type="submit" value="Yes. Delete it!" />
            </p>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>
