Feature: Create User
    Testing creating a new customer support
    
    Scenario: Create a user
        Given I am at the WTP page and logged in as an admin
        When I click on the create user button
        And fill out the form with "fordon" and "user"
        And click on the skapa användare button
        Then a new user should be created