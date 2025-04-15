﻿namespace END2ENDTester.Steps;

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
        await _page.GotoAsync($"{BaseUrl}");
    }

    [GivenAttribute("I see the faq button")]
    public async Task GivenISeeTheFaqButton()
    {
        await _page.WaitForSelectorAsync("[id='faq-link']");
    }

    [WhenAttribute("I click on the faq button")]
    public async Task WhenIClickOnTheFaqButton()
    {
        await _page.WaitForSelectorAsync("[id='faq-link']", new() {
            Timeout = 10000,
            State = WaitForSelectorState.Visible
        });
        await _page.ClickAsync("[id='faq-link']");
    }

    [ThenAttribute("I should see the FAQ page")]
    public async Task ThenIShouldSeeTheFaqPage()
    {
        await _page.WaitForSelectorAsync("[text='Har du läst vår FAQ?']");
        
    }

    [GivenAttribute("I am at the FAQ page")]
    public async Task GivenIAmAtTheFaqPage()
    {
        await _page.GotoAsync($"{BaseUrl}faq");
    }

    [GivenAttribute("I see the yes button")]
    public async Task GivenISeeTheYesButton()
    {
        await _page.WaitForSelectorAsync("[class='faq-buttons'], [text='Ja']");
        
    }

    [WhenAttribute("I click on the yes button")]
    public async Task WhenIClickOnTheYesButton()
    {
        await _page.WaitForSelectorAsync("[class='faq-buttons']", new() {
            Timeout = 10000,
            State = WaitForSelectorState.Visible
        });
        await _page.ClickAsync("[class='faq-buttons'], [text='Ja']");
    }

    [ThenAttribute("I should see the form page")]
    public async Task ThenIShouldSeeTheFormPage()
    {
        await _page.WaitForSelectorAsync("[class='dynamisk-form-title']", new() { Timeout = 10000 });
       
    }
    

    [GivenAttribute("I am at the form page")]
    public async Task GivenIAmAtTheFormPage()
    {
        await _page.GotoAsync($"{BaseUrl}dynamisk");
    }

    [WhenAttribute("I select the field companyType and enter {string}")]
    public async Task WhenISelectTheFieldCompanyTypeAndEnter(string fordonsservice)
    {
        try
        {
            // Vänta på fältet
            await _page.WaitForSelectorAsync("[name='company']", new() { Timeout = 10000 });

            // Försök välja alternativet
            await _page.SelectOptionAsync("[name='company']", new SelectOptionValue() { Label = fordonsservice });
        }
        catch (Exception ex)
        {
            // Alltid dumpa skärmdump och HTML
            var path = Path.Combine(Directory.GetCurrentDirectory(), "company_field_debug.png");
            await _page.ScreenshotAsync(new() { Path = path, FullPage = true });

            var htmlPath = Path.Combine(Directory.GetCurrentDirectory(), "company_field_debug.html");
            var html = await _page.ContentAsync();
            File.WriteAllText(htmlPath, html);

        }

    }

    [WhenAttribute("I select the field firstName and enter {string}")]
    public async Task WhenISelectTheFieldFirstNameAndEnter(string john)
    {
        await _page.WaitForSelectorAsync("[name='firstName']", new() {
            Timeout = 10000,
            State = WaitForSelectorState.Visible
        });
        await _page.FillAsync("[name='firstName']", "John Doe");
    }

    [WhenAttribute("I select the field email and enter my email address")]
    public async Task WhenISelectTheFieldEmailAndEnterMyEmailAddress()
    {
        await _page.WaitForSelectorAsync("[name='email']", new() {
            Timeout = 10000,
            State = WaitForSelectorState.Visible
        });
        await _page.FillAsync("[name='email']", "hultberg.80@gmail.com");
    }

    [WhenAttribute("I select the registration number field and enter {string}")]
    public async Task WhenISelectTheRegistrationNumberFieldAndEnter(string p0)
    {
        await _page.WaitForSelectorAsync("[name='registrationNumber']", new() {
            Timeout = 10000,
            State = WaitForSelectorState.Visible
        });
        await _page.FillAsync("[name='registrationNumber']", "ABC123");
    }

    [WhenAttribute("I see the field issueType and select the issue type {string}")]
    public async Task WhenISeeTheFieldIssueTypeAndSelectTheIssueType(string Övrigt)
    {
        await _page.SelectOptionAsync("[name='issueType']", new SelectOptionValue() { Label = Övrigt });
    }

    [WhenAttribute("I enter the message {string} in the message field")]
    public async Task WhenIEnterTheMessageInTheMessageField(string p0)
    {
        await _page.WaitForSelectorAsync("[name='message']", new() {
            Timeout = 10000,
            State = WaitForSelectorState.Visible
        });
        await _page.FillAsync("[name='message']", "Hej, jag har en fråga om min beställning.");
    }

    [WhenAttribute("I click on the submit button")]
    public async Task WhenIClickOnTheSubmitButton()
    {
        await _page.WaitForSelectorAsync("[class='dynamisk-form-button']", new() {
            Timeout = 10000,
            State = WaitForSelectorState.Visible
        });
        await _page.ClickAsync("[class='dynamisk-form-button'], [type='submit']");
    }

    [ThenAttribute("I should see the success message")]
    public async Task ThenIShouldSeeTheSuccessMessage()
    {
        await _page.WaitForSelectorAsync("[text='Formulär skickat! Kolla din e-post för chattlänken.']");
    
    }
}