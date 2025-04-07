namespace END2ENDTester.Steps;

using Microsoft.Playwright;
using TechTalk.SpecFlow;
using Xunit;

[Binding]
public class UserFlow
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
    [GivenAttribute("I am at the WTP page")]
    public async Task GivenIAmAtTheWtpPage()
    {
        await _page.GotoAsync("http://localhost:3001/");
    }

    [GivenAttribute("I see the faq button")]
    public async Task GivenISeeTheFaqButton()
    {
        var element = await _page.QuerySelectorAsync("[id='faq-link']");
        Assert.NotNull(element);
    }

    [WhenAttribute("I click on the faq button")]
    public async Task WhenIClickOnTheFaqButton()
    {
        await _page.ClickAsync("[id='faq-link']");
    }

    [ThenAttribute("I should see the FAQ page")]
    public async Task ThenIShouldSeeTheFaqPage()
    {
        var element = await _page.QuerySelectorAsync("[text='Har du läst vår FAQ?']");
        Assert.Null(element);

    }

    [GivenAttribute("I am at the FAQ page")]
    public async Task GivenIAmAtTheFaqPage()
    {
        await _page.GotoAsync("http://localhost:3001/faq");
    }

    [GivenAttribute("I see the yes button")]
    public async Task GivenISeeTheYesButton()
    {
        var element = await _page.QuerySelectorAsync("[class='faq-buttons'], [text='Ja']");
        Assert.NotNull(element);
    }

    [WhenAttribute("I click on the yes button")]
    public async Task WhenIClickOnTheYesButton()
    {
        await _page.ClickAsync("[class='faq-buttons'], [text='Ja']");
    }

    [ThenAttribute("I should see the form page")]
    public async Task ThenIShouldSeeTheFormPage()
    {
        var element = await _page.QuerySelectorAsync("[text='Kontakta kundtjänst']");
        Assert.Null(element);
    }
    
    [GivenAttribute("I am at the form page")]
    public async Task GivenIAmAtTheFormPage()
    {
        await _page.GotoAsync("http://localhost:3001/dynamisk");
    }

    [GivenAttribute("I see the field companyType")]
    public async Task GivenISeeTheFieldCompanyType()
    {
        var element = await _page.QuerySelectorAsync("[name='companyType']");
        Assert.NotNull(element);
    }

    [GivenAttribute("I see the field firstName")]
    public async Task GivenISeeTheFieldFirstName()
    {
        var element = await _page.QuerySelectorAsync("[name='firstName']");
        Assert.NotNull(element);
    }

    [GivenAttribute("I see the field email")]
    public async Task GivenISeeTheFieldEmail()
    {
        var element = await _page.QuerySelectorAsync("[name='email']");
        Assert.NotNull(element);
    }
    
    [GivenAttribute("I see the registrationNumber field")]
    public async Task GivenISeeTheRegistrationNumberField()
    {
        var element = await _page.QuerySelectorAsync("[name='registrationNumber']");
        Assert.Null(element);
    }
    
    [GivenAttribute("I see the field issueType")]
    public async Task GivenISeeTheFieldIssueType()
    {
        var element = await _page.QuerySelectorAsync("[name='issueType']");
        Assert.Null(element);
    }

    [GivenAttribute("I see the textarea message")]
    public async Task GivenISeeTheTextareaMessage()
    {
        var element = await _page.QuerySelectorAsync("[name='message']");
        Assert.NotNull(element);
    }

    [WhenAttribute("I click on the field companyType")]
    public async Task WhenIClickOnTheFieldCompanyType()
    {
        await _page.ClickAsync("[name='companyType']");
    }

    [WhenAttribute("I select Fordonsservice")]
    public async Task WhenISelectFordonsservice()
    {
        await _page.SelectOptionAsync("[name='companyType']", new SelectOptionValue() { Label = "Fordonsservice" });
    }

    [WhenAttribute("I click on the field firstName")]
    public async Task WhenIClickOnTheFieldFirstName()
    {
        await _page.QuerySelectorAsync("[name='firstName']");
    }

    [WhenAttribute("I enter the name John Doe")]
    public async Task WhenIEnterTheNameJohnDoe()
    {
        await _page.FillAsync("[name='firstName']", "John Doe");
    }

    [WhenAttribute("I click on the field email")]
    public async Task WhenIClickOnTheFieldEmail()
    {
        await _page.QuerySelectorAsync("[name='email']");
    }

    [WhenAttribute("I enter my email address")]
    public async Task WhenIEnterMyEmailAddress()
    {
        await _page.FillAsync("[name='email']", "hultberg.80@gmail.com");
    }
    
    [WhenAttribute("I enter my registration number")]
    public async Task WhenIEnterMyRegistrationNumber()
    {
        await _page.FillAsync("[name='registrationNumber']", "ABC123");
    }
    
    [WhenAttribute("I click on the field issueType")]
    public async Task WhenIClickOnTheFieldIssueType()
    {
        await _page.ClickAsync("[name='issueType']");
    }
    
    [WhenAttribute("I select the issue type {string}")]
    public async Task WhenISelectTheIssueType(string Övrigt)
    {
        await _page.SelectOptionAsync("[name='issueType']", new SelectOptionValue() { Label = Övrigt });
    }

    [WhenAttribute("I click on the textarea message")]
    public async Task WhenIClickOnTheTextareaMessage()
    {
        await _page.ClickAsync("[name='message']");
    }

    [WhenAttribute("I enter the message {string}")]
    public async Task WhenIEnterTheMessage(string p0)
    {
        await _page.FillAsync("[name='message']", "Hej, jag har en fråga om min beställning.");
    }

    [WhenAttribute("I click on the submit button")]
    public async Task WhenIClickOnTheSubmitButton()
    {
        await _page.ClickAsync("[class='dynamisk-form-button'], [type='submit']");
    }

    [ThenAttribute("I should see the success message")]
    public async Task ThenIShouldSeeTheSuccessMessage()
    {
        var element = await _page.QuerySelectorAsync("[text='Formulär skickat! Kolla din e-post för chattlänken.']");
        Assert.Null(element);
    }
    
}