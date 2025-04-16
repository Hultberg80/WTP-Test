Feature: Delete user
    Testing deleting a user from the system

    Scenario: Delete a user
        Given I am at the Admin dashboard and logged in as an admin
        When I click on the delete user button where mail equals "hultberg123@gmail.com"
        Then the user should be deleted from the system