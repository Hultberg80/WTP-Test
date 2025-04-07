Feature: User flow
    Testing user flow on WTP

    Scenario: Clicking the faq button
        Given I am at the WTP page
        And I see the faq button
        When I click on the faq button
        AND I see the Yes button
        Then I should see the register form