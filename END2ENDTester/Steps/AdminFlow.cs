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

    [BeforeScenario]
    public async Task Setup()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = false, SlowMo = 2000 });
        _context = await _browser.NewContextAsync();
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
        await _page.GotoAsync("http://localhost:3001/");
    }

    [WhenAttribute("I click on the {string} symbol")]
    public async Task WhenIClickOnTheSymbol(string login)
    {
        await _page.ClickAsync("[class='login-img']");
    }

    [WhenAttribute("I enter username and password")]
    public async Task WhenIEnterUsernameAndPassword()
    {
        await _page.FillAsync("[class='staff-field-input'][type='text']", "ville");
        await _page.FillAsync("[class='staff-field-input'][type='password']", "12345");

    }

    [WhenAttribute("I click on the login button")]
    public async Task WhenIClickOnTheLoginButton()
    {
        await _page.ClickAsync("[class='staff-login-button'], [type='submit']");
    }

    [ThenAttribute("I should see the admin dashboard view")]
    public async Task ThenIShouldSeeTheAdminDashboardView()
    {
        var element = await _page.GotoAsync("http://localhost:3001/admin/dashboard");
        Assert.NotNull(element);
    }

    [GivenAttribute("I am at the WTP page and logged in as an admin")]
    public async Task GivenIAmAtTheWtpPageAndLoggedInAsAnAdmin()
    {
        await _page.GotoAsync("http://localhost:3001/");
        await _loginHelper.LoginFiller("ville", "12345");
    }


    [WhenAttribute("I click on the create user button")]
    public async Task WhenIClickOnTheCreateUserButton()
    {
        await _page.ClickAsync("a[href='/admin/create-user'][data-discover='true']");
    }

    [WhenAttribute("fill out the form with {string} and {string}")]
    public async Task WhenFillOutTheFormWithAnd(string fordon, string user)
    {
        await _page.FillAsync("[name='email'][type='text']", "hultberg80@gmail.com");
        await _page.FillAsync("[name='firstName'][type='text']", "zunken123");
        await _page.FillAsync("[name='password'][type='password']", "abc123");
        await _page.SelectOptionAsync("[name='company'][class='login-bar']", new SelectOptionValue() { Value = "fordon" });
        await _page.SelectOptionAsync("[name='role'][class='login-bar']", new SelectOptionValue() { Value = "user" });
    }

    [WhenAttribute("click on the skapa användare button")]
    public async Task WhenClickOnTheSkapaAnvandareButton()
    {
        await _page.ClickAsync("[class='dynamisk-form-button'], [type='submit']");
    }

    [ThenAttribute("a new user should be created")]
    public async Task ThenANewUserShouldBeCreated()
    {
        var element = await _page.QuerySelectorAsync("[text='Användare skapad']");
        Assert.Null(element);
    }

   
}