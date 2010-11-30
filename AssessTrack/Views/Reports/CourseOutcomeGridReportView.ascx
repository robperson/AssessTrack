<%@ Import Namespace="AssessTrack.Models" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Models.ReportsAndTools.GridReport<Profile,Tag>>" %>
<div class="tablediv">
<table>
    <thead>
        <tr>
            <th colspan="2"></th>
            <% foreach (var item in Model.XItems)
               { %>
                <th><%= Model.PrintXLabel(item) %>   </th>
              <% } %>
        </tr>
    </thead>
    <tbody>
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
    <tr>
    
        <td colspan="<%= (Model.XItems.Count + 2).ToString() %>">&nbsp;</td>
    </tr>
    <%if (Model.ShowColumnTotals)
      { %>
      <tr>
        <th>Totals</th>
        <td><strong><%= Model.PrintDetailColumnTotal() %></strong></td>
        <%foreach (var xitem in Model.XItems)
          {%>
                <td><%= Model.PrintColumnTotal(xitem) %></td>  
          <%} %>
      </tr>
      <tr>
        <th>Percentage</th>
        <td><strong>100</strong></td>
        <%foreach (var xitem in Model.XItems)
          {%>
                <td><%= Model.PrintColumnPercentage(xitem) %></td>  
          <%} %>
      </tr>
    <%} %>
    <%if (Model.ShowColumnAverages)
      { %>
      <tr>
        <th>Average</th>
        <td>&nbsp;</td>
        <%foreach (var xitem in Model.XItems)
          {%>
                <td><%= Model.PrintColumnAverage(xitem) %></td>  
          <%} %>
      </tr>
      
    <%} %>
    </tbody>
</table>
</div>