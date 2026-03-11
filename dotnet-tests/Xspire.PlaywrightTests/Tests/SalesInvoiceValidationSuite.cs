using System.Threading.Tasks;
using Microsoft.Playwright;
using Xspire.PlaywrightTests.Pages;

namespace Xspire.PlaywrightTests.Tests;

public class SalesInvoiceValidationSuite : IClassFixture<PlaywrightFixture>
{
    private readonly IPage _page;
    private readonly LoginPage _loginPage;
    private readonly HomePage _homePage;
    private readonly ShareInformationPages _shareInformationPages;
    private readonly TaxesCategories _taxesCategories;

    public SalesInvoiceValidationSuite(PlaywrightFixture fixture)
    {
        _page = fixture.Page;
        _loginPage = new LoginPage(_page);
        _homePage = new HomePage(_page);
        _shareInformationPages = new ShareInformationPages(_page);
        _taxesCategories = new TaxesCategories(_page);
    }

    private async Task GoToTaxesCategoriesFormAsync()
    {
        await _loginPage.GotoAsync();
        await _loginPage.FillSuccessAccountAsync(LoginTestData.SuccessUsername, LoginTestData.SuccessPassword);
        await _loginPage.ClickLoginAsync();

        await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        await _homePage.SalesInvoiceMenuItem.ClickAsync();
        await _shareInformationPages.FirstSubLinkInLpxWrapper.ClickAsync();
        await _taxesCategories.ButtonByIdEb43.ClickAsync();
    }

    [Fact(DisplayName = "TC-SI-001 - Validate Taxes Categories form visibility")]
    public async Task ValidateTaxesCategoriesFormVisibility()
    {
        await GoToTaxesCategoriesFormAsync();

        Assert.True(await _taxesCategories.NeTxtCode.IsVisibleAsync());
        Assert.True(await _taxesCategories.NeTxtDescription.IsVisibleAsync());
        Assert.True(await _taxesCategories.LpxWrapperSaveButton.IsVisibleAsync());
    }

    [Fact(DisplayName = "TC-SI-002 - Should show validation errors for empty form submission")]
    public async Task ShouldShowValidationErrorsForEmptyFormSubmission()
    {
        await GoToTaxesCategoriesFormAsync();

        await _taxesCategories.ClickSaveAsync();

        Assert.True(await _taxesCategories.CodeError.IsVisibleAsync());
        Assert.True(await _taxesCategories.DescriptionError.IsVisibleAsync());
    }

    [Fact(DisplayName = "TC-SI-003 - Should show validation error for empty Code")]
    public async Task ShouldShowValidationErrorForEmptyCode()
    {
        await GoToTaxesCategoriesFormAsync();

        await _taxesCategories.FillDescriptionOnlyAsync(TaxesCategoriesTestData.ValidDescription);
        await _taxesCategories.ClickSaveAsync();

        Assert.True(await _taxesCategories.CodeError.IsVisibleAsync());
        Assert.False(await _taxesCategories.DescriptionError.IsVisibleAsync());
    }

    [Fact(DisplayName = "TC-SI-004 - Should show validation error for empty Description")]
    public async Task ShouldShowValidationErrorForEmptyDescription()
    {
        await GoToTaxesCategoriesFormAsync();

        await _taxesCategories.FillCodeOnlyAsync(TaxesCategoriesTestData.ValidCode);
        await _taxesCategories.ClickSaveAsync();

        Assert.True(await _taxesCategories.DescriptionError.IsVisibleAsync());
        Assert.False(await _taxesCategories.CodeError.IsVisibleAsync());
    }

    [Fact(DisplayName = "TC-SI-005 - Should successfully save with valid Code and Description")]
    public async Task ShouldSuccessfullySaveWithValidCodeAndDescription()
    {
        await GoToTaxesCategoriesFormAsync();

        await _taxesCategories.FillCodeAndDescriptionAsync(TaxesCategoriesTestData.ValidCode, TaxesCategoriesTestData.ValidDescription);
        await _taxesCategories.ClickSaveAsync();

        Assert.False(await _taxesCategories.CodeError.IsVisibleAsync());
        Assert.False(await _taxesCategories.DescriptionError.IsVisibleAsync());
    }
}
