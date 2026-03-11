using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Xspire.PlaywrightTests.Pages;

namespace Xspire.PlaywrightTests.Tests;

public class UnitOfMeasuresValidationSuite : IClassFixture<PlaywrightFixture>
{
    private readonly IPage _page;
    private readonly LoginPage _loginPage;
    private readonly HomePage _homePage;
    private readonly Inventory _inventoryPage;
    private readonly UnitOfMeasuresPage _unitOfMeasuresPage;

    public UnitOfMeasuresValidationSuite(PlaywrightFixture fixture)
    {
        _page = fixture.Page;
        _loginPage = new LoginPage(_page);
        _homePage = new HomePage(_page);
        _inventoryPage = new Inventory(_page);
        _unitOfMeasuresPage = new UnitOfMeasuresPage(_page);
    }

    private async Task LogoutIfLoggedInAsync()
    {
        Console.WriteLine($"[LogoutIfLoggedInAsync] Current URL = {_page.Url}");
        var avatarVisible = await _homePage.UserAvatar.IsVisibleAsync();
        Console.WriteLine($"[LogoutIfLoggedInAsync] UserAvatar visible = {avatarVisible}");

        if (avatarVisible)
        {
            Console.WriteLine("[LogoutIfLoggedInAsync] Clicking UserAvatar...");
            await _homePage.UserAvatar.ClickAsync();
            Console.WriteLine("[LogoutIfLoggedInAsync] Clicking LogoutMenuItem...");
            await _homePage.LogoutMenuItem.ClickAsync();

            await _page.WaitForURLAsync(
                url => url.StartsWith("https://xspire-test.hqsoft.vn/", StringComparison.OrdinalIgnoreCase),
                new PageWaitForURLOptions { Timeout = 30000 });
            Console.WriteLine($"[LogoutIfLoggedInAsync] After logout, URL = {_page.Url}");
        }
    }

    private async Task GoToUnitOfMeasuresFormAsync()
    {
        Console.WriteLine($"[GoToUnitOfMeasuresFormAsync] START. Current URL = {_page.Url}");

        var isLoggedIn = await _homePage.UserAvatar.IsVisibleAsync();
        Console.WriteLine($"[GoToUnitOfMeasuresFormAsync] UserAvatar visible (isLoggedIn) = {isLoggedIn}");

        if (!isLoggedIn)
        {
            Console.WriteLine("[GoToUnitOfMeasuresFormAsync] Not logged in -> performing login...");
            await _loginPage.GotoAsync();
            Console.WriteLine($"[GoToUnitOfMeasuresFormAsync] After GotoAsync, URL = {_page.Url}");

            if (_page.Url.Contains("/Account/Login", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("[GoToUnitOfMeasuresFormAsync] On Login page -> filling credentials...");
                await _loginPage.FillSuccessAccountAsync(LoginTestData.SuccessUsername, LoginTestData.SuccessPassword);
                await _loginPage.ClickLoginAsync();
                Console.WriteLine("[GoToUnitOfMeasuresFormAsync] ClickLoginAsync done, waiting DOMContentLoaded...");
                await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            }
            else
            {
                Console.WriteLine("[GoToUnitOfMeasuresFormAsync] Already logged in (redirected to Home), skipping fill. Waiting DOMContentLoaded instead of NetworkIdle.");
                await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            }

            Console.WriteLine($"[GoToUnitOfMeasuresFormAsync] After login, URL = {_page.Url}");
        }
        else
        {
            Console.WriteLine("[GoToUnitOfMeasuresFormAsync] Already logged in, skipping login.");
        }

        Console.WriteLine("[GoToUnitOfMeasuresFormAsync] Clicking InventoryMenuItem...");
        await _homePage.InventoryMenuItem.ClickAsync();
        Console.WriteLine("[GoToUnitOfMeasuresFormAsync] Clicking InventorySubMenuItem...");
        await _inventoryPage.InventorySubMenuItem.ClickAsync();
        Console.WriteLine("[GoToUnitOfMeasuresFormAsync] Clicking NewButtonContainer...");
        await _unitOfMeasuresPage.NewButtonContainer.ClickAsync();
        Console.WriteLine("[GoToUnitOfMeasuresFormAsync] DONE. Form should be open.");
    }

    [Fact(DisplayName = "TC-UOM-001 - Validate Unit Of Measures form visibility")]
    public async Task ValidateUnitOfMeasuresFormVisibility()
    {
        Console.WriteLine("=== TC-UOM-001 START ===");
        await GoToUnitOfMeasuresFormAsync();

        var fromVisible = await _unitOfMeasuresPage.FromUnitInput.IsVisibleAsync();
        var toVisible = await _unitOfMeasuresPage.ToUnitInput.IsVisibleAsync();
        var saveVisible = await _unitOfMeasuresPage.SaveButton.IsVisibleAsync();
        Console.WriteLine($"[TC-UOM-001] FromUnitInput visible = {fromVisible}");
        Console.WriteLine($"[TC-UOM-001] ToUnitInput visible = {toVisible}");
        Console.WriteLine($"[TC-UOM-001] SaveButton visible = {saveVisible}");

        Assert.True(fromVisible);
        Assert.True(toVisible);
        Assert.True(saveVisible);

        await LogoutIfLoggedInAsync();
        Console.WriteLine("=== TC-UOM-001 END ===");
    }

    [Fact(DisplayName = "TC-UOM-002 - Should show validation errors for empty form submission")]
    public async Task ShouldShowValidationErrorsForEmptyFormSubmission()
    {
        Console.WriteLine("=== TC-UOM-002 START ===");
        await GoToUnitOfMeasuresFormAsync();

        Console.WriteLine("[TC-UOM-002] Clicking Save with empty form...");
        await _unitOfMeasuresPage.ClickSaveAsync();

        var fromErr = await _unitOfMeasuresPage.FromUnitError.IsVisibleAsync();
        var toErr = await _unitOfMeasuresPage.ToUnitError.IsVisibleAsync();
        Console.WriteLine($"[TC-UOM-002] FromUnitError visible = {fromErr}");
        Console.WriteLine($"[TC-UOM-002] ToUnitError visible = {toErr}");

        Assert.True(fromErr);
        Assert.True(toErr);

        await LogoutIfLoggedInAsync();
        Console.WriteLine("=== TC-UOM-002 END ===");
    }

    [Fact(DisplayName = "TC-UOM-003 - Should show validation error for empty FromUnit")]
    public async Task ShouldShowValidationErrorForEmptyFromUnit()
    {
        Console.WriteLine("=== TC-UOM-003 START ===");
        await GoToUnitOfMeasuresFormAsync();

        Console.WriteLine("[TC-UOM-003] Filling ToUnit only...");
        await _unitOfMeasuresPage.FillToUnitOnlyAsync(UnitOfMeasuresTestData.ValidToUnit);
        await _unitOfMeasuresPage.ClickSaveAsync();

        var fromErr = await _unitOfMeasuresPage.FromUnitError.IsVisibleAsync();
        var toErr = await _unitOfMeasuresPage.ToUnitError.IsVisibleAsync();
        Console.WriteLine($"[TC-UOM-003] FromUnitError visible = {fromErr}");
        Console.WriteLine($"[TC-UOM-003] ToUnitError visible = {toErr}");

        Assert.True(fromErr);
        Assert.False(toErr);

        await LogoutIfLoggedInAsync();
        Console.WriteLine("=== TC-UOM-003 END ===");
    }

    [Fact(DisplayName = "TC-UOM-004 - Should show validation error for empty ToUnit")]
    public async Task ShouldShowValidationErrorForEmptyToUnit()
    {
        Console.WriteLine("=== TC-UOM-004 START ===");
        await GoToUnitOfMeasuresFormAsync();

        Console.WriteLine("[TC-UOM-004] Filling FromUnit only...");
        await _unitOfMeasuresPage.FillFromUnitOnlyAsync(UnitOfMeasuresTestData.ValidFromUnit);
        await _unitOfMeasuresPage.ClickSaveAsync();

        var fromErr = await _unitOfMeasuresPage.FromUnitError.IsVisibleAsync();
        var toErr = await _unitOfMeasuresPage.ToUnitError.IsVisibleAsync();
        Console.WriteLine($"[TC-UOM-004] FromUnitError visible = {fromErr}");
        Console.WriteLine($"[TC-UOM-004] ToUnitError visible = {toErr}");

        // Kịch bản mong muốn: chỉ ToUnit (bắt buộc) báo lỗi, FromUnit không lỗi.
        Assert.False(fromErr);
        Assert.True(toErr);

        await LogoutIfLoggedInAsync();
        Console.WriteLine("=== TC-UOM-004 END ===");
    }

    [Fact(DisplayName = "TC-UOM-005 - Should successfully save with valid FromUnit and ToUnit")]
    public async Task ShouldSuccessfullySaveWithValidFromAndToUnit()
    {
        Console.WriteLine("=== TC-UOM-005 START ===");
        await GoToUnitOfMeasuresFormAsync();

        Console.WriteLine("[TC-UOM-005] Filling both FromUnit and ToUnit with valid data...");
        await _unitOfMeasuresPage.FillUnitsAsync(UnitOfMeasuresTestData.ValidFromUnit, UnitOfMeasuresTestData.ValidToUnit);
        Console.WriteLine("[TC-UOM-005] Clicking Save...");
        await _unitOfMeasuresPage.ClickSaveAsync();

        var fromErr = await _unitOfMeasuresPage.FromUnitError.IsVisibleAsync();
        var toErr = await _unitOfMeasuresPage.ToUnitError.IsVisibleAsync();
        Console.WriteLine($"[TC-UOM-005] FromUnitError visible = {fromErr}");
        Console.WriteLine($"[TC-UOM-005] ToUnitError visible = {toErr}");
        Console.WriteLine($"[TC-UOM-005] Current URL = {_page.Url}");

        // Kịch bản mong muốn: nhập dữ liệu hợp lệ thì không còn lỗi validation.
        Assert.False(fromErr);
        Assert.False(toErr);

        // Và header Unit Code phải hiển thị đúng giá trị FromUnit vừa tạo.
        var headerText = await _unitOfMeasuresPage.HeaderUnitCodeTitle.InnerTextAsync();
        Console.WriteLine($"[TC-UOM-005] HeaderUnitCodeTitle text = {headerText}");
        Assert.Equal(UnitOfMeasuresTestData.ValidFromUnit, headerText.Trim());

        await LogoutIfLoggedInAsync();
        Console.WriteLine("=== TC-UOM-005 END ===");
    }

    [Fact(DisplayName = "TC-UOM-007 - Should show both required and duplicate errors when FromUnit exists and ToUnit is empty")]
    public async Task ShouldShowBothErrorsWhenFromUnitExistsAndToUnitEmpty()
    {
        Console.WriteLine("=== TC-UOM-007 START ===");
        await GoToUnitOfMeasuresFormAsync();

        Console.WriteLine("[TC-UOM-007] Filling FromUnit with the same value as previous test, ToUnit empty...");
        await _unitOfMeasuresPage.FillFromUnitOnlyAsync(UnitOfMeasuresTestData.ValidFromUnit);
        await _unitOfMeasuresPage.ClickSaveAsync();

        var fromErr = await _unitOfMeasuresPage.FromUnitError.IsVisibleAsync();
        var toErr = await _unitOfMeasuresPage.ToUnitError.IsVisibleAsync();
        Console.WriteLine($"[TC-UOM-007] FromUnitError visible = {fromErr}");
        Console.WriteLine($"[TC-UOM-007] ToUnitError visible = {toErr}");

        // TC-005 đã tạo thành công với ValidFromUnit.
        // TC-007 chạy sau, dùng lại cùng ValidFromUnit + ToUnit rỗng
        // → FromUnit: duplicate, ToUnit: required → cả 2 đều lỗi.
        Assert.True(fromErr);
        Assert.True(toErr);

        await LogoutIfLoggedInAsync();
        Console.WriteLine("=== TC-UOM-007 END ===");
    }

    [Fact(DisplayName = "TC-UOM-006 - Should show duplicate error when Unit Code already exists", Skip = "Temporarily disabled until duplicate validation selector is confirmed")]
    public async Task ShouldShowDuplicateErrorWhenFromUnitAlreadyExists()
    {
        Console.WriteLine("=== TC-UOM-006 START ===");
        await GoToUnitOfMeasuresFormAsync();

        Console.WriteLine("[TC-UOM-006] Filling FromUnit with existing value and valid ToUnit...");
        await _unitOfMeasuresPage.FillUnitsAsync(UnitOfMeasuresTestData.ValidFromUnit, UnitOfMeasuresTestData.ValidToUnit);
        await _unitOfMeasuresPage.ClickSaveAsync();

        // Chờ message duplicate xuất hiện cho chắc, tránh timeout InnerText.
        await _unitOfMeasuresPage.FromUnitError.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 30000
        });

        var fromErrVisible = await _unitOfMeasuresPage.FromUnitError.IsVisibleAsync();
        var fromErrText = await _unitOfMeasuresPage.FromUnitError.InnerTextAsync();
        Console.WriteLine($"[TC-UOM-006] FromUnitError visible = {fromErrVisible}");
        Console.WriteLine($"[TC-UOM-006] FromUnitError text = {fromErrText}");

        Assert.True(fromErrVisible);
        Assert.Contains("This value already exists", fromErrText, StringComparison.OrdinalIgnoreCase);

        await LogoutIfLoggedInAsync();
        Console.WriteLine("=== TC-UOM-006 END ===");
    }
}

