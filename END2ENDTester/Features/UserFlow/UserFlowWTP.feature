Feature: User flow
    Testing user flow on WTP

    Scenario: Clicking the faq button
        Given I am at the WTP page
        And I see the faq button
        When I click on the faq button
        Then I should see the FAQ page
        
    Scenario: Clicking Yes on the FAQ page
        Given I am at the FAQ page
        And I see the yes button
        When I click on the yes button
        Then I should see the form page
        
    Scenario: Filling out the form
        Given I am at the form page
        When I select the field companyType and enter "Fordonsservice"
        And I select the field firstName and enter "John"
        And I select the field email and enter my email address
        And I select the registration number field and enter "ABC123"
        And I see the field issueType and select the issue type "Övrigt"
        And I enter the message "I have a question about my order." in the message field
        And I click on the submit button
        Then I should see the success message
        