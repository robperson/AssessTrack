<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.Assessment>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Prerequisite Not Satisfied
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Prerequisite Not Satisfied</h2>
    <p>You cannot submit <%= Model.Name %> because you have not submitted and passed <%= Model.PreReq.Name %></p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>
