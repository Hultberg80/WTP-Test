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
        // Try multiple selector strategies
        try {
            // Wait for page to be fully loaded
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
            // Try by ID
            var loginButton = await _page.QuerySelectorAsync("#loggaIn");
            if (loginButton != null) {
                await loginButton.ClickAsync();
            } else {
                // Try by text content
                await _page.ClickAsync("text=Login", new() { Timeout = 10000 });
            }
        
            // Continue with form filling
            await _page.FillAsync("[class='staff-field-input'][type='text']", username);
            await _page.FillAsync("[class='staff-field-input'][type='password']", password);
            await _page.ClickAsync("[class='staff-login-button'], [type='submit']");
        }
        catch (Exception ex) {
            Console.WriteLine($"Login failed: {ex.Message}");
            await _page.ScreenshotAsync(new() { Path = "login-failed.png" });
            throw;
        }
    }
}