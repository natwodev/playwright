using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Xspire.PlaywrightTests.Pages;

public class TaxesCategories
{
    private readonly IPage _page;

    public TaxesCategories(IPage page)
    {
        _page = page;
    }

    /// <summary>Nút "New" / vùng thao tác trên màn Taxes Categories (trong lpx-wrapper).</summary>
    public ILocator ButtonByIdEb43 => _page.Locator("xpath=//*[@id=\"lpx-wrapper\"]/div[2]/div/div/div[2]/div[3]/div[1]/div[2]/div/div[3]/div");

    public ILocator NeTxtCode => _page.Locator("#ne_Txt_Code");
    public ILocator NeTxtDescription => _page.Locator("#ne_Txt_Description");
    public ILocator CodeError => _page.Locator("#ne_Txt_Code-error");
    public ILocator DescriptionError => _page.Locator("#ne_Txt_Description-error");
    public ILocator LpxWrapperSaveButton => _page.Locator("xpath=//*[@id=\"lpx-wrapper\"]/div[2]/div/div/div[2]/div[4]/div[1]/div[2]/div/div[3]/button");

    public async Task FillCodeAndDescriptionAsync(string code, string description)
    {
        await NeTxtCode.FillAsync(code);
        await NeTxtDescription.FillAsync(description);
    }

    public async Task FillCodeOnlyAsync(string code)
    {
        await NeTxtCode.FillAsync(code);
    }

    public async Task FillDescriptionOnlyAsync(string description)
    {
        await NeTxtDescription.FillAsync(description);
    }

    public async Task ClickSaveAsync()
    {
        await LpxWrapperSaveButton.ClickAsync();
    }
}
