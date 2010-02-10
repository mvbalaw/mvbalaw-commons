<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ProductModel>" %>
<%@ Import Namespace="NHibernateBootstrap.Web.Controllers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>
    <% using (Html.BeginForm("Edit", "Product"))
       {%>
    <p>Name: <%= Html.TextBox("Name")%></p>    
    <p>Category: <%= Html.TextBox("Category")%></p>    
    <p>Discontinued: <%= Html.CheckBox("Discontinued")%></p>  
    <p><input type="submit" value="Edit Product" /></p>  
    <%} %>

</asp:Content>
