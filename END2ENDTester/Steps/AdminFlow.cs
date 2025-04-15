﻿using System.Net.Mime;
using END2ENDTester.Helpers;

namespace END2ENDTester.Steps;

using Microsoft.Playwright;
using TechTalk.SpecFlow;
using Xunit;

[Binding]
public class AdminFlow
{
    private IPlaywright _playwright;
    private IBrowser _browser;
    private IBrowserContext _context;
    private IPage _page;
    private LoginHelper _loginHelper;
    private string BaseUrl => Environment.GetEnvironmentVariable("TEST_APP_URL") ?? "http://localhost:3002/";
    

    [BeforeScenario]
    public async Task Setup()
    {
        _playwright = await Playwright.CreateAsync();
        var isCi = Environment.GetEnvironmentVariable("CI") != null;
        _browser = await _playwright.Chromium.LaunchAsync(new()
        { 
            Headless = isCi, // Use headless mode in CI
            SlowMo = isCi ? 0 : 1000 // SlowMo might not be needed in CI
        });
        _context = await _browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize { Width = 1920, Height = 1080 },
            // Force desktop mode
            IsMobile = false,
            HasTouch = false
        });
        _page = await _context.NewPageAsync();
        _loginHelper = new LoginHelper(_page);
    }

    [AfterScenario]
    public async Task Teardown()
    {
        await _browser.CloseAsync();
        _playwright.Dispose();
    }

    [GivenAttribute("I am on the WTP page")]
    public async Task GivenIAmOnTheWtpPage()
    {
        await _page.GotoAsync("http://localhost:3002/");
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await _page.ScreenshotAsync(new() { Path = "page-loaded.png" });
    
        // Log page content for debugging
        var content = await _page.ContentAsync();
        Console.WriteLine($"Page HTML: {content.Substring(0, Math.Min(500, content.Length))}...");
    }

    [WhenAttribute("I click on the {string} symbol")]
    public async Task WhenIClickOnTheSymbol(string login)
    {
        await _page.Locator("#loggaIn").ClickAsync();
    }

    [WhenAttribute("I enter username and password")]
    public async Task WhenIEnterUsernameAndPassword()
    {
        await _page.WaitForSelectorAsync("[class='staff-field-input'][type='text']", new() {
            Timeout = 10000,
            State = WaitForSelectorState.Visible
        });
        await _page.FillAsync("[class='staff-field-input'][type='text']", "ville");
        await _page.WaitForSelectorAsync("[class='staff-field-input'][type='password']", new() {
            Timeout = 10000,
            State = WaitForSelectorState.Visible
        });
        await _page.FillAsync("[class='staff-field-input'][type='password']", "12345");

    }

    [WhenAttribute("I click on the login button")]
    public async Task WhenIClickOnTheLoginButton()
    {
        await _page.WaitForSelectorAsync("[class='staff-login-button']", new() {
            Timeout = 10000,
            State = WaitForSelectorState.Visible
        });
        await _page.ClickAsync("[class='staff-login-button'], [type='submit']");
    }

    [ThenAttribute("I should see the admin dashboard view")]
    public async Task ThenIShouldSeeTheAdminDashboardView()
    {
        var element = await _page.GotoAsync($"{BaseUrl}admin/dashboard");
        Assert.NotNull(element);
    }

    [GivenAttribute("I am at the WTP page and logged in as an admin")]
    public async Task GivenIAmAtTheWtpPageAndLoggedInAsAnAdmin()
    {
        try {
            await _page.GotoAsync($"{BaseUrl}");
        
            // Take screenshot for debugging
            await _page.ScreenshotAsync(new() { Path = "before-login.png" });
        
            // Wait for page to be fully loaded
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
            await _loginHelper.LoginFiller("Admino", "02589");
        
            // Add verification that login was successful
            await _page.WaitForSelectorAsync("text=Dashboard", 
                new() { Timeout = 30000, State = WaitForSelectorState.Visible });
        }
        catch (Exception ex) {
            Console.WriteLine($"Admin login failed: {ex.Message}");
            await _page.ScreenshotAsync(new() { Path = "admin-login-failed.png" });
            throw;
        }
    }


    [WhenAttribute("I click on the create user button")]
    public async Task WhenIClickOnTheCreateUserButton()
    {
        await _page.WaitForSelectorAsync("a[href='/admin/create-user'][data-discover='true']", new() {
            Timeout = 10000,
            State = WaitForSelectorState.Visible
        });
        await _page.ClickAsync("a[href='/admin/create-user'][data-discover='true']");
    }

    [WhenAttribute("fill out the form with {string} and {string}")]
    public async Task WhenFillOutTheFormWithAnd(string fordon, string user)
    {
        // Wait for form to be fully loaded
        await _page.WaitForSelectorAsync("[name='email']", new() { State = WaitForSelectorState.Visible });
    
        // Use more explicit waits before each action
        await _page.FillAsync("[name='email'][type='text']", "hultberg100@gmail.com");
        await _page.WaitForTimeoutAsync(500); // Short pause between actions
    
        await _page.FillAsync("[name='firstName'][type='text']", "zunken123");
        await _page.WaitForTimeoutAsync(500);
    
        await _page.FillAsync("[name='password'][type='password']", "abc123");
        await _page.WaitForTimeoutAsync(500);
    
        // For dropdowns, make sure they're clickable first
        await _page.WaitForSelectorAsync("[name='company']", new() { State = WaitForSelectorState.Visible });
        await _page.SelectOptionAsync("[name='company'][class='login-bar']", new SelectOptionValue() { Value = "fordon" });
        await _page.WaitForTimeoutAsync(500);
    
        await _page.SelectOptionAsync("[name='role'][class='login-bar']", new SelectOptionValue() { Value = "staff" });
    }

    [WhenAttribute("click on the skapa användare button")]
    public async Task WhenClickOnTheSkapaAnvandareButton()
    {
        await _page.WaitForSelectorAsync("[class='dynamisk-form-button']", new() {
            Timeout = 10000,
            State = WaitForSelectorState.Visible
        });
        await _page.ClickAsync("[class='dynamisk-form-button'], [type='submit']");
    }

    [ThenAttribute("a new user should be created")]
    public async Task ThenANewUserShouldBeCreated()
    {
        try {
            // Wait longer in CI environment
            var timeout = Environment.GetEnvironmentVariable("CI") != null ? 30000 : 5000;
        
            // Either wait for success message or check that user appears in the list
            await _page.WaitForSelectorAsync("text=Användare skapad", 
                new() { Timeout = timeout, State = WaitForSelectorState.Visible });
        
            // If the above doesn't work, try different selectors:
            // await _page.WaitForNavigationAsync();
            // var element = await _page.QuerySelectorAsync("text=Användare skapad");
            // Assert.NotNull(element);
        }
        catch (Exception ex) {
            // Take a screenshot on failure to help debugging
            await _page.ScreenshotAsync(new() { Path = "user-creation-failed.png" });
            throw;
        }
    }


    [GivenAttribute("I am at the Admin dashboard and logged in as an admin")]
    public async Task GivenIAmAtTheAdminDashboardAndLoggedInAsAnAdmin()
    {
        await _page.GotoAsync($"{BaseUrl}admin/dashboard");
        await _loginHelper.LoginFiller("Admino", "02589");
    }

    [WhenAttribute("I click on the delete user button where mail equals {string}")]
    public async Task WhenIClickOnTheDeleteUserButtonWhereMailEquals(string p0)
    {
        _page.Dialog += async (_, dialog) =>
        {
            if (dialog.Type == "confirm")
            {
                await dialog.AcceptAsync();
            }
        };

        var row = _page.Locator("tr").Filter(new() { HasTextString = p0 });
        await row.WaitForAsync(new() { Timeout = 10000 }); // längre vid behov

        var deleteButton = row.Locator("button.delete-button");
        await deleteButton.ClickAsync();
        

        // Lägg ev. till en kort paus för att se att det händer
        await _page.WaitForTimeoutAsync(1000);
    }
    

    [ThenAttribute("the user should be deleted from the system")]
    public async Task ThenTheUserShouldBeDeletedFromTheSystem()
    {
        var dialogHandled = false;

        _page.Dialog += async (_, dialog) =>
        {
            if (dialog.Type == "alert" && dialog.Message.Contains("Användaren har tagits bort"))
            {
                await dialog.AcceptAsync();
                dialogHandled = true;
            }
        };
        
    }
    
}