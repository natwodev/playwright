using System.Threading.Tasks;
using Microsoft.Playwright;
using Xspire.PlaywrightTests.Pages;

namespace Xspire.PlaywrightTests.Tests;

public class LoginValidationSuite : IClassFixture<PlaywrightFixture>
{
    private readonly IPage _page;
    private readonly LoginPage _loginPage;
    private readonly HomePage _homePage;

    public LoginValidationSuite(PlaywrightFixture fixture)
    {
        _page = fixture.Page;
        _loginPage = new LoginPage(_page);
        _homePage = new HomePage(_page);
    }

    [Fact(DisplayName = "TC-LOGIN-001 - Validate login page visibility")]
    public async Task ValidateLoginPageVisibility()
    {
        await _loginPage.GotoAsync();

        Assert.Equal("Login | Xspire", await _page.TitleAsync());
        Assert.True(await _loginPage.BrandLogo.IsVisibleAsync());
        Assert.True(await _loginPage.BrandName.IsVisibleAsync());
        Assert.Equal("Xspire", await _loginPage.BrandName.InnerTextAsync());
        Assert.True(await _loginPage.FormHeader.IsVisibleAsync());
        Assert.Equal("Login", await _loginPage.FormHeader.InnerTextAsync());
        Assert.True(await _loginPage.LanguageCombo.IsVisibleAsync());
        Assert.Contains("English", await _loginPage.LanguageCombo.InnerTextAsync());
        Assert.True(await _loginPage.TenantLabel.IsVisibleAsync());
        var tenantLabel = (await _loginPage.TenantLabel.InnerTextAsync()).Trim();
        Assert.Equal("TENANT", tenantLabel, ignoreCase: true);
        Assert.True(await _loginPage.TenantName.IsVisibleAsync());
        Assert.Equal("Not selected", await _loginPage.TenantName.InnerTextAsync());
        Assert.True(await _loginPage.SwitchTenantButton.IsVisibleAsync());
        Assert.Contains("switch", await _loginPage.SwitchTenantButton.InnerTextAsync());
        Assert.True(await _loginPage.UserNameInput.IsVisibleAsync());
        Assert.True(await _loginPage.PasswordInput.IsVisibleAsync());
        Assert.True(await _loginPage.PasswordVisibilityButton.IsVisibleAsync());
        Assert.True(await _loginPage.RememberMeCheckbox.IsVisibleAsync());
        Assert.False(await _loginPage.RememberMeCheckbox.IsCheckedAsync());
        Assert.True(await _loginPage.RememberMeText.IsVisibleAsync());
        Assert.Equal("Remember me", await _loginPage.RememberMeText.InnerTextAsync());
        Assert.True(await _loginPage.ForgotPasswordLink.IsVisibleAsync());
        Assert.Equal("Forgot password?", await _loginPage.ForgotPasswordLink.InnerTextAsync());
        Assert.True(await _loginPage.LoginButton.IsVisibleAsync());
        Assert.Contains("Login", await _loginPage.LoginButton.InnerTextAsync());
        Assert.False(await _loginPage.ErrorAlert.IsVisibleAsync());
    }

    [Fact(DisplayName = "TC-LOGIN-002 - Should show validation errors for empty form submission")]
    public async Task ShouldShowValidationErrorsForEmptyFormSubmission()
    {
        await _loginPage.GotoAsync();

        await _loginPage.ClickLoginAsync();

        Assert.True(await _loginPage.UserNameError.IsVisibleAsync());
        Assert.Equal(
            "The User name or email address field is required.",
            await _loginPage.UserNameError.InnerTextAsync());

        Assert.True(await _loginPage.PasswordError.IsVisibleAsync());
        Assert.Equal(
            "The Password field is required.",
            await _loginPage.PasswordError.InnerTextAsync());
    }

    [Fact(DisplayName = "TC-LOGIN-003 - Should show validation error for empty username")]
    public async Task ShouldShowValidationErrorForEmptyUsername()
    {
        await _loginPage.GotoAsync();

        await _loginPage.FillEmptyUsernameAsync(LoginTestData.FailurePassword);
        await _loginPage.ClickLoginAsync();

        Assert.True(await _loginPage.UserNameError.IsVisibleAsync());
        Assert.Equal(
            "The User name or email address field is required.",
            await _loginPage.UserNameError.InnerTextAsync());
        Assert.False(await _loginPage.PasswordError.IsVisibleAsync());
    }

    [Fact(DisplayName = "TC-LOGIN-004 - Should show validation error for empty password")]
    public async Task ShouldShowValidationErrorForEmptyPassword()
    {
        await _loginPage.GotoAsync();

        await _loginPage.FillEmptyPasswordAsync(LoginTestData.FailureUsername);
        await _loginPage.ClickLoginAsync();

        Assert.True(await _loginPage.PasswordError.IsVisibleAsync());
        Assert.Equal(
            "The Password field is required.",
            await _loginPage.PasswordError.InnerTextAsync());
        Assert.False(await _loginPage.UserNameError.IsVisibleAsync());
    }

    [Fact(DisplayName = "TC-LOGIN-005 - Should show error for invalid credentials")]
    public async Task ShouldShowErrorForInvalidCredentials()
    {
        await _loginPage.GotoAsync();

        await _loginPage.FillFailedAccountAsync(LoginTestData.FailureUsername, LoginTestData.FailurePassword);
        await _loginPage.ClickLoginAsync();

        Assert.True(await _loginPage.ErrorAlert.IsVisibleAsync());
        Assert.Contains("Invalid username or password!", await _loginPage.ErrorAlert.InnerTextAsync());

        await _loginPage.CloseErrorAlertButton.ClickAsync();
        await _loginPage.ErrorAlert.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Hidden,
            Timeout = 5000
        });
        Assert.False(await _loginPage.ErrorAlert.IsVisibleAsync());
    }

    [Fact(DisplayName = "TC-LOGIN-006 - Should navigate to forgot password page")]
    public async Task ShouldNavigateToForgotPasswordPage()
    {
        await _loginPage.GotoAsync();

        await _loginPage.ForgotPasswordLink.ClickAsync();

        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        Assert.Contains("/Account/ForgotPassword", _page.Url);
        Assert.Equal("Forgot password? | Xspire", await _page.TitleAsync());
    }

    [Fact(DisplayName = "TC-LOGIN-007 - Should handle password field as sensitive input")]
    public async Task ShouldHandlePasswordFieldAsSensitiveInput()
    {
        await _loginPage.GotoAsync();

        Assert.True(await _loginPage.PasswordInput.IsVisibleAsync());
        var typeAttr = await _loginPage.PasswordInput.GetAttributeAsync("type");
        Assert.Equal("password", typeAttr);
    }

    [Fact(DisplayName = "TC-LOGIN-008 - Should successfully login with valid credentials")]
    public async Task ShouldSuccessfullyLoginWithValidCredentials()
    {
        await _loginPage.GotoAsync();

        await _loginPage.FillSuccessAccountAsync(LoginTestData.SuccessUsername, LoginTestData.SuccessPassword);
        await _loginPage.ClickLoginAsync();

        // Chờ load cơ bản thay vì NetworkIdle để tránh timeout do request nền
        await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        Assert.Equal("Xspire", await _page.TitleAsync());
        Assert.True(await _homePage.UserAvatar.IsVisibleAsync());

        await _homePage.UserAvatar.ClickAsync();
        await _homePage.LogoutMenuItem.ClickAsync();

        // Sau logout, chờ điều hướng về trang gốc theo URL
        await _page.WaitForURLAsync(
            url => url.StartsWith("https://xspire-test.hqsoft.vn/"),
            new PageWaitForURLOptions { Timeout = 30000 });

        Assert.Equal("Xspire", await _page.TitleAsync());
        Assert.StartsWith("https://xspire-test.hqsoft.vn/", _page.Url);
    }
}

