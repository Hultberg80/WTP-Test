namespace END2ENDTester.Steps;

using Microsoft.Playwright;
using TechTalk.SpecFlow;
using Xunit;

[Binding]
public class RegisterSteps
{
    private IPlaywright _playwright;
    private IBrowser _browser;
    private IBrowserContext _context;
    private IPage _page;

    [BeforeScenario]
    public async Task Setup()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = false, SlowMo = 1000 });
        _context = await _browser.NewContextAsync();
        _page = await _context.NewPageAsync();
    }

    [AfterScenario]
    public async Task Teardown()
    {
        await _browser.CloseAsync();
        _playwright.Dispose();
    }

    // Steps
    [Given(@"I am at the Shoptester page")]
    public async Task GivenIAmAtTheShoptesterPage()
    {
        await _page.GotoAsync("http://localhost:5000/");
    }

    [Given(@"I see the register button")]
    public async Task GivenISeeTheRegisterButton()
    {
        var element = await _page.QuerySelectorAsync("[id='register-button']");
        Assert.NotNull(element);
    }

    [When(@"I click on the register button")]
    public async Task WhenIClickTheRegisterButton()
    {
        await _page.ClickAsync("[id='register-button']");
    }

    [Then(@"I should see the register form")]
    public async Task ThenIShouldSeeTheRegisterForm()
    {
        var element = await _page.QuerySelectorAsync("[id='register-form']");
        Assert.NotNull(element);
    }

    [Given(@"I am at the register form")]
    public async Task GivenIAmAtTheRegisterForm()
    {
        await _page.QuerySelectorAsync("[id='register-form']");
    }

    [And(@"I see the email field")]
    public async Task GivenISeeTheEmailField()
    {
        var element = await _page.QuerySelectorAsync("input[type=email]");
        Assert.NotNull(element);
    }
    
    [And(@"I see the username field")]
    public async Task WhenISeeTheUsernameField()
    {
        var element = await _page.QuerySelectorAsync("input[name='username']");
        Assert.NotNull(element);
    }
    
    [And(@"I see the password field")]
    public async Task WhenISeeThePasswordField()
    {
        var element = await _page.QuerySelectorAsync("input[name='password']");
        Assert.NotNull(element);
    }

    [When(@"I enter my email")]
    public async Task WhenIEnterMyEmail()
    {
        await _page.FillAsync("input[type=email]" ,"nisse@gmail.com");
    }
    

    [And(@"I enter my username")]
    public async Task WhenIEnterMyUsername()
    {
        await _page.FillAsync("input[name='username']", "nisse1");
    }

    [And(@"I enter my password")]
    public async Task WhenIEnterMyPassword()
    {
        await _page.FillAsync("input[name='password']", "1234");
    }

    [And(@"I see the requiredpassword field")]
    public async Task WhenISeeTheRequiredpasswordField()
    {
        var element = await _page.QuerySelectorAsync("input[name='requiredpassword']");
        Assert.NotNull(element);
    }

    [When(@"I enter my requiredpassword")]
    public async Task WhenIEnterMyRequiredpassword()
    {
        await _page.FillAsync("input[name='requiredpassword']", "1234");
    }

    [And(@"I see the submit button")]
    public async Task WhenISeeTheSubmitButton()
    {
        var element = await _page.QuerySelectorAsync("input[name='submit']");
        Assert.NotNull(element);
    }

    [When(@"I click on the submit button")]
    public async Task WhenIClickOnTheSubmitButton()
    {
        await _page.ClickAsync("[id='submit']");
    }

    [Then(@"a new user should be created")]
    public async Task ThenANewUserShouldBeCreated()
    {
        var successMessage = _page.Locator("text=Registration successfull!");
        await Assertions.Expect(successMessage).ToBeVisibleAsync();
    }
}