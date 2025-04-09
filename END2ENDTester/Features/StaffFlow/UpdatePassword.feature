Feature: Update Password
    Testing updating the password for a user

    @UpdatePasswordTest
    Scenario: Update password staff
        Given I am at the WTP page and logged in as a staff
        When I click on the update password button
        And I enter the new password "password" and confirm it
        And I click on the update button
        Then I should see a success message