using Microsoft.Playwright;

namespace Xspire.PlaywrightTests.Pages;

public class Inventory
{
    private readonly IPage _page;

    public Inventory(IPage page)
    {
        _page = page;
    }

    public ILocator InventorySubMenuItem => _page.Locator("xpath=//*[@id=\"lpx-wrapper\"]/div[2]/div/div/div[2]/div[2]/div/div/div/div[2]/div[1]/div[3]/a/span");
}

