using Microsoft.Playwright;

namespace Xspire.PlaywrightTests.Pages;

public class ShareInformationPages
{
    private readonly IPage _page;

    public ShareInformationPages(IPage page)
    {
        _page = page;
    }

    public ILocator FirstSubLinkInLpxWrapper => _page.Locator("xpath=//*[@id=\"lpx-wrapper\"]/div[2]/div/div/div[2]/div[2]/div/div/div/div[2]/div[4]/div[1]/a/span");
}
