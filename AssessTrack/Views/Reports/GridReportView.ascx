<%@ Import Namespace="AssessTrack.Models" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Models.ReportsAndTools.GridReport<Profile,Assessment>>" %>
<div class="tablediv">
<table>
    <tr>
        <th colspan="2"></th>
        <% foreach (var item in Model.XItems)
           { %>
            <th><%= Model.PrintXLabel(item) %>   </th>
          <% } %>
    </tr>
    <%foreach (var yval in Model.YItems)
      { %>
      <tr>
        <th><%= Model.PrintYLabel(yval) %></th>
        <td><strong><%= Model.PrintYDetail(yval) %></strong></td>
        <%foreach (var xval in Model.XItems)
          { %>
            <td><%= Model.PrintCellValue(xval,yval) %></td>
        <%} %>
      </tr>
    <% } %>
</table>
</div>