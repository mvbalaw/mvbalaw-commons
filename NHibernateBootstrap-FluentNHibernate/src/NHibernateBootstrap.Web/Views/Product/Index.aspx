<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<Product>>" %>
<%@ Import Namespace="NHibernateBootstrap.Core.Domain"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
    
       <table>
        <tr>
            <th></th>
            <th>
                Name
            </th>
            <th>
                Category
            </th>
            <th>
                Discontinued
            </th>
        </tr>
        <% foreach (var product in Model)
           { %>
        
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id = product.Id })%>
                <%= Html.ActionLink("Delete", "Delete", new { id = product.Id })%>
            </td>
            <td>
                <%= Html.Encode(product.Name)%>
            </td>
            <td>
                <%= Html.Encode(product.Category)%>
            </td>
            <td>
                <%= Html.Encode(product.Discontinued)%>
            </td>
        </tr>
        <% } %>
    </table>
    <p> 
    <%= Html.ActionLink("Create New", "New") %>
    </p>
    
    

</asp:Content>
