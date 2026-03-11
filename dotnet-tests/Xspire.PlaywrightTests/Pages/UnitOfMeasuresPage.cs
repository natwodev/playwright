using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Xspire.PlaywrightTests.Pages;

public class UnitOfMeasuresPage
{
    private readonly IPage _page;
    private const string BaseUrl = "https://xspire-test.hqsoft.vn/";

    public UnitOfMeasuresPage(IPage page)
    {
        _page = page;
    }

    public ILocator NewButtonContainer => _page.Locator("xpath=//*[@id=\"lpx-wrapper\"]/div[2]/div/div/div[2]/div[3]/div[1]/div[2]/div/div[3]/div");
    public ILocator FromUnitInput => _page.Locator("input[parent-id='ne_Txt_FromUnit']");
    public ILocator ToUnitInput => _page.Locator("input[parent-id='ne_Txt_ToUnit']");
    public ILocator SaveButton => _page.Locator("xpath=//*[@id=\"lpx-wrapper\"]/div[2]/div/div/div[2]/div[4]/div[1]/div[2]/div/div[3]/button");
    public ILocator FromUnitError => _page.Locator("dxbl-input-editor#ne_Txt_FromUnit + .validation-message");
    public ILocator ToUnitError => _page.Locator("dxbl-input-editor#ne_Txt_ToUnit + .validation-message");
    public ILocator HeaderUnitCodeTitle => _page.Locator("xpath=//*[@id=\"lpx-wrapper\"]/div[2]/div/div/div[2]/div[4]/div[1]/div[1]/h1");

    public async Task GotoAsync()
    {
        await _page.GotoAsync($"{BaseUrl}Inventory/UnitOfMeasures");
        await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }

    public async Task FillUnitsAsync(string fromUnit, string toUnit)
    {
        await FromUnitInput.FillAsync(fromUnit);
        await ToUnitInput.FillAsync(toUnit);
    }

    public async Task FillFromUnitOnlyAsync(string fromUnit)
    {
        await FromUnitInput.FillAsync(fromUnit);
    }

    public async Task FillToUnitOnlyAsync(string toUnit)
    {
        await ToUnitInput.FillAsync(toUnit);
    }

    public async Task ClickSaveAsync()
    {
        await SaveButton.ClickAsync();
    }
}

