<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	NotFound
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <h3>
            Page Not Found.</h3>
        <h3>
            The page you requested no longer exists.</h3>
        
        <p>You could go <a href="javascript:history.go(-1)">back</a> or return to the
            <a id="hlHomePage" href="/">Home Page</a>.</p>
        <p>Again, we apologize for any inconvenience.</p>

</asp:Content>
