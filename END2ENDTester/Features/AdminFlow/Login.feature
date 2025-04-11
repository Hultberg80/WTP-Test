Feature: Login as Admin
    
    Scenario: Login as Admin
        Given I am on the WTP page
        When I click on the "Login" symbol
        And I enter username and password
        And I click on the login button
        Then I should see the admin dashboard view