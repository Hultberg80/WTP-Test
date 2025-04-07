Feature: Login user
    Login a user at Shoptester

    Scenario: Fill out register form
        Given I am at the register form
        And I see the email field
        And I see the username field
        And I see the password field
            When I enter my email
            And I enter my username
            And I enter my password
            And I see the requiredpassword field
                When I enter my requiredpassword
                And I see the submit button
                    When I click on the submit button
                    Then a new user should be created
         