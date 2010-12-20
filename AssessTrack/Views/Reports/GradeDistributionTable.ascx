<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AssessTrack.Models.ReportsAndTools.GradeDistribution>" %>
<div class="barchart">
    <img src="http://chart.apis.google.com/chart?chxl=0:|A|B|C|D|F|Ungraded&chxr=1,-5,100&chxt=x,y&chbh=a&chs=300x225&cht=bvg&chco=C2D6EB&chd=t:<%= Model.ACount + "," + Model.BCount + "," + Model.CCount + "," + Model.DCount + "," + Model.FCount + "," + Model.UngradedCount%>&chtt=Grade+Distribution" width="300" height="225" alt="Grade Distribution" />

</div>
<table>
    <tr>
        <th>Grade</th>
        <th>Count</th>
    </tr>
    <tr>
        <td>A</td>
        <td>
            <p><%= Model.ACount %></p>
            <p>
                <% foreach (var profile in Model.AStudents)
                   {
                       Response.Write(profile.FullName + ", ");   
                   } %>
            </p>        
        </td>
    </tr>
    <tr>
        <td>B</td>
        <td>
            <p><%= Model.BCount %></p>
            <p>
                <% foreach (var profile in Model.BStudents)
                   {
                       Response.Write(profile.FullName + ", ");   
                   } %>
            </p>        
        </td>
    </tr>
    <tr>
        <td>C</td>
        <td>
            <p><%= Model.CCount %></p>
            <p>
                <% foreach (var profile in Model.CStudents)
                   {
                       Response.Write(profile.FullName + ", ");   
                   } %>
            </p>        
        </td>
    </tr>
    <tr>
        <td>D</td>
        <td>
            <p><%= Model.DCount %></p>
            <p>
                <% foreach (var profile in Model.DStudents)
                   {
                       Response.Write(profile.FullName + ", ");   
                   } %>
            </p>        
        </td>
    </tr>
    <tr>
        <td>F</td>
        <td>
            <p><%= Model.FCount %></p>
            <p>
                <% foreach (var profile in Model.FStudents)
                   {
                       Response.Write(profile.FullName + ", ");   
                   } %>
            </p>        
        </td>
    </tr>
    <tr>
        <td>Ungraded</td>
        <td>
            <p><%= Model.UngradedCount %></p>
            <p>
                <% foreach (var profile in Model.UngradedStudents)
                   {
                       Response.Write(profile.FullName + ", ");   
                   } %>
            </p>        
        </td>
    </tr>
</table>
<p>Total Students: <%= Model.TotalCount %></p>
