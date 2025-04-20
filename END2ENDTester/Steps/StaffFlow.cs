using END2ENDTester.Helpers;

namespace END2ENDTester.Steps;

using Microsoft.Playwright;
using TechTalk.SpecFlow;
using Xunit;

[Binding]
public class StaffFlow
{
    private IPlaywright _playwright;
    private IBrowser _browser;
    private IBrowserContext _context;
    private IPage _page;
    private LoginHelper _loginHelper;
    private string BaseUrl => Environment.GetEnvironmentVariable("TEST_APP_URL") ?? "http://localhost:5000/";

    
    [BeforeScenario]
    public async Task Setup()
    {
        _playwright = await Playwright.CreateAsync();
        var isCi = Environment.GetEnvironmentVariable("CI") != null;
        _browser = await _playwright.Chromium.LaunchAsync(new() { 
            Headless = isCi, // Use headless mode in CI
            SlowMo = isCi ? 0 : 1000 // SlowMo might not be needed in CI
        });
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

    [GivenAttribute("I am at the WTP page and logged in as a staff")]
    public async Task GivenIAmAtTheWtpPageAndLoggedInAsAStaff()
    {
        await _page.GotoAsync($"{BaseUrl}");
        await _loginHelper.LoginFiller("zunken123", "abc123");
    }

    [WhenAttribute("I click on the update password button")]
    public async Task WhenIClickOnTheUpdatePasswordButton()
    {
        await _page.ClickAsync("a[href='/staff/update-user'][data-discover='true']");
    }

    [WhenAttribute("I enter the new password {string} and confirm it")]
    public async Task WhenIEnterTheNewPasswordAndConfirmIt(string password)
    {
        await _page.FillAsync("[class='login-bar'][type='password']", "abc111");
        await _page.FillAsync("[class='login-bar'][type='password'][name='confirmPassword']", "abc111");
    }

    [WhenAttribute("I click on the update button")]
    public async Task WhenIClickOnTheUpdateButton()
    {
        await _page.ClickAsync("[class='bla'], [type='submit']");
    }

    [ThenAttribute("I should see a success message")]
    public async Task ThenIShouldSeeASuccessMessage()
    {
        var element = await _page.QuerySelectorAsync("[text='Uppgifterna uppdaterades framgångsrikt']");
        Assert.Null(element);
    }

    
    
    [GivenAttribute("I am at the staff dashboard")]
    public async Task GivenIAmAtTheStaffDashboard()
    {
        await _page.GotoAsync($"{BaseUrl}staff/dashboard");
    }


    [WhenAttribute("I click on a ticket and drag it to my errands")]
    public async Task WhenIClickOnATicketAndDragItToMyErrands()
    {
        var sourceSelector = "a[href='/chat/2885815c-1181-4101-b473-54947e6cb33c']:has-text('Öppna chatt')";
        var targetSelector = ".ticket-items-container"; // Uppdatera detta med rätt selector för målet
        await _page.DragAndDropAsync(sourceSelector, targetSelector);
    }

    [ThenAttribute("I should see the ticket in my errands")]
    public async Task ThenIShouldSeeTheTicketInMyErrands()
    {
        await _page.QuerySelectorAsync("a[href='/chat/2885815c-1181-4101-b473-54947e6cb33c']:has-text('Öppna chatt')");
    }


    [GivenAttribute("I click on a ticket on öppna chatt")]
    public async Task GivenIClickOnATicketOnOppnaChatt()
    {
        await _page.GotoAsync($"{BaseUrl}staff/dashboard");
        await _loginHelper.LoginFiller("zunken123", "abc123");
        await _page.ClickAsync("div.ticket-task-token a[href='/chat/2885815c-1181-4101-b473-54947e6cb33c']:has-text('Öppna chatt')");
    }

    [WhenAttribute("I write a response in the chat")]
    public async Task WhenIWriteAResponseInTheChat()
    {
        await _page.FillAsync("[class='chat-modal__input-field']", "Vad kan jag hjälpa dig med?");
    }

    [WhenAttribute("I click on the send button")]
    public async Task WhenIClickOnTheSendButton()
    {
        await _page.ClickAsync("[class='chat-modal__send-button']");
    }

    [ThenAttribute("I should see my response in the chat")]
    public async Task ThenIShouldSeeMyResponseInTheChat()
    {
        await _page.QuerySelectorAsync("[class='chat-modal__message-text']:has-text('Vad kan jag hjälpa dig med?')");
    }
}

    