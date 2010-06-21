<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AssessTrack.Models.CourseTermMember>>" %>
<%@ Import Namespace="AssessTrack.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	FinalGrades
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Final Grades</h2>

    <table>
        <tr>
            <th></th>
            <th>
                Student Name
            </th>
            <th>
                Final Grade
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
              
            </td>
            <td>
                <%= Html.Encode(UserHelpers.GetFullNameForID(item.MembershipID)) %>
            </td>
            <td>
               
            </td>
            <td>
                <%= Html.Encode(item.GetFinalLetterGrade()) %>
                (<%= Html.Encode(string.Format("{0:0.00}%",item.GetFinalGrade())) %>)
            </td>
        </tr>
    
    <% } %>

    </table>

</asp:Content>

