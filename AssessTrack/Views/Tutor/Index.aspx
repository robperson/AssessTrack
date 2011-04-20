<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AssessTrack.Models.ViewModels.TagViewModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Tutoring
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Tutoring</h2>
    <h3>Topics you need to review:</h3>
    
    <ul>
    <% foreach (var tag in Model)
       {%>
       <li>
       <p><strong><%= tag.Tag.DescriptiveName ?? tag.Tag.Name %></strong> - <%= tag.Tag.Description %></p>
       <p>You scored <%= tag.Score %>% on questions about <%= tag.Tag.DescriptiveName ?? tag.Tag.Name %>.</p>
       </li>
       
    <% } %>
    </ul>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ExtraContent" runat="server">
</asp:Content>
