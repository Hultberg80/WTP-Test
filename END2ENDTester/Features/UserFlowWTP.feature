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
        And I see the field companyType
        And I see the field firstName
        And I see the field email
        And I see the registrationNumber field
        And I see the field issueType
        And I see the textarea message
        When I click on the field companyType
        And I select Fordonsservice
        And I click on the field firstName
        And I enter the name John Doe
        And I click on the field email
        And I enter my email address
        And I enter my registration number
        And I click on the field issueType
        And I select the issue type "Övrigt"
        And I click on the textarea message
        And I enter the message "I have a question about my order."
        And I click on the submit button
        Then I should see the success message
        