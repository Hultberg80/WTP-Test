namespace END2ENDTester.Helpers;

using Microsoft.Playwright;
using System.Threading.Tasks;

public class LoginHelper
{
    private readonly IPage _page;


    public LoginHelper(IPage page)
    {
        _page = page;
    }

    public async Task LoginFiller(string username, string password)
    {
        try {
            // Wait for page to be fully loaded
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
            // Try multiple selectors to find the login button, starting with the most specific
            await _page.ClickAsync("#loggaIn, img[alt='Logga in'], a[href='/staff/login']", 
                new() { Timeout = 30000 });
        
            // Wait for login form to appear
            await _page.WaitForSelectorAsync("[class='staff-field-input'][type='text']", 
                new() { Timeout = 30000 });
            
            // Continue with form filling
            await _page.FillAsync("[class='staff-field-input'][type='text']", username);
            await _page.FillAsync("[class='staff-field-input'][type='password']", password);
            await _page.ClickAsync("[class='staff-login-button'], [type='submit']");
        }
        catch (Exception ex) {
            Console.WriteLine($"Login failed: {ex.Message}");
        
            // Take a screenshot to debug the issue
            await _page.ScreenshotAsync(new() { Path = "login-failed.png" });
        
            // Output the page HTML for debugging
            var html = await _page.ContentAsync();
            Console.WriteLine($"Page HTML at failure: {html.Substring(0, Math.Min(1000, html.Length))}");
        
            throw;
        }
    }
}