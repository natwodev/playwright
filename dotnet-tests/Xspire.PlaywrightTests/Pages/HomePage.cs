using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Xspire.PlaywrightTests.Pages;

public class HomePage
{
    private readonly IPage _page;

    public HomePage(IPage page)
    {
        _page = page;
    }

    public ILocator HeaderWelcome => _page.GetByText("Welcome to Xspire", new PageGetByTextOptions { Exact = false });
    public ILocator UserAvatar => _page.Locator("#userItem");
    public ILocator LogoutMenuItem => _page.Locator("xpath=//*[@id=\"MenuItem_Account_Logout\"]/a/span[2]");
    public ILocator SalesInvoiceMenuItem => _page.Locator("xpath=//*[@id=\"MenuItem_SI\"]/span[2]").First;
    public ILocator InventoryMenuItem => _page.Locator("xpath=//*[@id=\"MenuItem_IN\"]/span[2]").First;

    public async Task NavigateToAsync(string relativeUrl)
    {
        await _page.GotoAsync(relativeUrl);
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }
}

