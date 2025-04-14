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
        await _page.Locator("#loggaIn").ClickAsync();
        await _page.FillAsync("[class='staff-field-input'][type='text']", username);
        await _page.FillAsync("[class='staff-field-input'][type='password']", password);
        await _page.ClickAsync("[class='staff-login-button'], [type='submit']");
    }
}