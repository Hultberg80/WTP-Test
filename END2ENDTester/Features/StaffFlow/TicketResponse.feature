Feature: Respond to a ticket
    Testing responding to a ticket

    Scenario: Respond to a ticket
        Given I click on a ticket on öppna chatt
        When I write a response in the chat
        And I click on the send button
        Then I should see my response in the chat