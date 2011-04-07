<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.Tag>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete Tag
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete Tag</h2>
    <p>Are you sure you want to delete "<%= Model.Name %> - <%= Model.Description %>"? Once it's
    deleted it cannot be restored. If you are not sure, do not delete it!</p>
    <% using (Html.BeginForm())
       { %>
       <%= Html.Hidden("id",Model.TagID) %>
       <input type="submit" value="Yes, delete it" />
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>
