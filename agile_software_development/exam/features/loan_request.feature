Feature: Loan request
    As a registered user
    Such that I want to do capital investments
    I want to request a loan

    Scenario: Loan request with confirmation
    Given that I am one of several registered users with the following emails
        | full_name | email	         | monthly_income  |
        | Test 1    | test1@test.com | 1000            |
        | Test 2    | test2@test.com | 600             |
    And there is already a loan for one of the users in the system
        | loan_amount	   | loan_term  |
        | 205              | 2          |
    And I want to request for a new loan
    And I enter the my email "test2@test.com" and the loan amount "200" and the loan term "2"
    When I click the submit button
    Then I should receive a confirmation message including the interest ratio, the monthly amount to be paid, and the number of years to complete the payment