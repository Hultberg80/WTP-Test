Feature: Select a ticket
    Testing selecting a ticket from the list
    
    Scenario: Select a ticket
        Given I am at the WTP page and logged in as a staff
        When I click on a ticket and drag it to my errands
        Then I should see the ticket in my errands