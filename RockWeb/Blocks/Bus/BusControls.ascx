<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BusControls.ascx.cs" Inherits="RockWeb.Blocks.Bus.BusControls" %>

<asp:UpdatePanel ID="upDetail" runat="server">
    <ContentTemplate>
        <asp:LinkButton ID="btnPublishStreakRebuild" runat="server" Text="Streak Rebuild" CssClass="btn btn-primary" OnClick="btnPublishStreakRebuild_Click" />
        <asp:LinkButton ID="btnPublishAddInteraction" runat="server" Text="Add Interactions" CssClass="btn btn-primary" OnClick="btnPublishAddInteraction_Click" />
    </ContentTemplate>
</asp:UpdatePanel>
