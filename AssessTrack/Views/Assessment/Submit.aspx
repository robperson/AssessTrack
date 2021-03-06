<%@ Import Namespace="AssessTrack.Helpers" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AssessTrack.Models.Assessment>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Submit
</asp:Content>
<asp:Content ID="Head" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
 $(document).ready(function () {
            // This will prevent the enter key from submitting any forms
            $("input").bind("keypress", function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
        }); 
 </script>
<link href="http://alexgorbatchev.com/pub/sh/current/styles/shThemeDefault.css" rel="stylesheet" type="text/css" />
    <script src="http://alexgorbatchev.com/pub/sh/current/scripts/shCore.js" type="text/javascript"></script>
    <script src="http://alexgorbatchev.com/pub/sh/current/scripts/shAutoloader.js" type="text/javascript"></script>
    <script src="http://alexgorbatchev.com/pub/sh/current/scripts/shBrushCpp.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Submit <%= Model.Name %></h2>
    <h3>This is due at <%= Model.DueDate.ToString() %></h3>
    <%  using (Html.BeginForm("Submit", "Assessment", FormMethod.Post, new { enctype = "multipart/form-data" }))
        { %>
           
           <%= Html.RenderAssessmentSubmissionForm(Model)%>
           
           <input type="submit" value="Submit" />
           
    <% } %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ExtraContent" runat="server">
<script type="text/javascript">
    SyntaxHighlighter.all();
    
</script>
</asp:Content>