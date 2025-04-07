Feature: Register user
    Register a user at Shoptester

    Scenario: Open register form
        Given I am at the Shoptester page
        And I see the register button
        When I click on the register button
        Then I should see the register form