using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Xspire.PlaywrightTests.Pages;

public class LoginPage
{
    private readonly IPage _page;
    private const string BaseUrl = "https://xspire-test.hqsoft.vn/";

    public LoginPage(IPage page)
    {
        _page = page;
    }

    public ILocator BrandLogo => _page.Locator("xpath=//html/body/div/div/div/div/div/div/div[1]/div[1]");
    public ILocator BrandName => _page.Locator("xpath=//html/body/div/div/div/div/div/div/div[1]/div[2]");
    public ILocator FormHeader => _page.Locator("xpath=//html/body/div/div/div/div/div/div/div[2]/div/div[1]/h2");
    public ILocator LanguageCombo => _page.Locator("xpath=//html/body/div/div/div/div/div/div/div[2]/div/div[1]/div/button");
    public ILocator TenantLabel => _page.Locator("xpath=//html/body/div/div/div/div/div/div/div[2]/div/div[2]/div/div[1]/span");
    public ILocator TenantName => _page.Locator("xpath=//html/body/div/div/div/div/div/div/div[2]/div/div[2]/div/div[1]/h6/span");
    public ILocator SwitchTenantButton => _page.Locator("#AbpTenantSwitchLink");
    public ILocator UserNameInput => _page.Locator("#LoginInput_UserNameOrEmailAddress");
    public ILocator PasswordInput => _page.Locator("#password-input");
    public ILocator PasswordVisibilityButton => _page.Locator("#PasswordVisibilityButton");
    public ILocator RememberMeCheckbox => _page.Locator("#LoginInput_RememberMe");
    public ILocator RememberMeText => _page.GetByText("Remember me", new PageGetByTextOptions { Exact = true });
    public ILocator ForgotPasswordLink => _page.Locator("xpath=//*[@id=\"loginForm\"]/div[2]/div[2]/a");
    public ILocator LoginButton => _page.Locator("xpath=//*[@id=\"loginForm\"]/div[3]/button");
    public ILocator ErrorAlert => _page.Locator("xpath=//*[@id=\"AbpPageAlerts\"]/div");
    public ILocator CloseErrorAlertButton => _page.Locator("xpath=//*[@id=\"AbpPageAlerts\"]/div/button");
    public ILocator UserNameError => _page.Locator("#LoginInput_UserNameOrEmailAddress-error");
    public ILocator PasswordError => _page.Locator("#password-input-error");

    public async Task GotoAsync()
    {
        await _page.GotoAsync($"{BaseUrl}Account/Login");
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    public async Task FillSuccessAccountAsync(string username, string password)
    {
        await UserNameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
    }

    public async Task FillFailedAccountAsync(string username, string password)
    {
        await UserNameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
    }

    public async Task FillEmptyUsernameAsync(string password)
    {
        await PasswordInput.FillAsync(password);
    }

    public async Task FillEmptyPasswordAsync(string username)
    {
        await UserNameInput.FillAsync(username);
    }

    public async Task ClickLoginAsync()
    {
        await LoginButton.ClickAsync();
    }
}

