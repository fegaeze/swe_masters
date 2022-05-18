Feature: Create Booking
    As a customer
    I want to book a taxi ride
    So that I can reach my destination

    Scenario: Authenticated customers can view booking form
    Given that I am in the login page
    And I have signed up using the following credentials
        | full_name  | email              | password   | age | role     |
        | Hugo Liiv  | hugoliiv@gmail.com | #3ll0wOrlD | 26  | customer |
    And I enter my username "hugoliiv@gmail.com" and password "#3ll0wOrlD"
    And I click the Log In button
    And I am logged in and directed to my user page
    When I click the Book A Taxi button
    Then I should be directed to the booking form


    Scenario: A taxi is available for booking
    Given the following taxis on duty
        | location  | number_of_seats | rate | status    | user_id |
        | Kaubamaja | 4               | 0.79 | busy      | 3       |
        | Kesklinn  | 6               | 1.32 | available | 4       |
    And I want to go from "Tasku" to "Eesti Rahva Muuseum" with distance "3.0" km
    And I am in the login page
    And I enter my username "fred@abc.com" and password "parool"
    And I click the Log In button
    And I am logged in and directed to my user page
    And I click the Book A Taxi button
    And I input the trip information
    When I submit the booking request
    Then I should receive a confirmation message

    Scenario: No taxi is available for booking
    Given the following taxis on duty
        | location  | number_of_seats | rate | status    | user_id |
        | Kaubamaja | 4               | 0.79 | busy      | 3       |
        | Eeden     | 6               | 1.32 | busy | 4       |
    And I want to go from "Tasku" to "Eesti Rahva Muuseum" with distance "3.0" km
    And I am in the login page
    And I enter my username "fred@abc.com" and password "parool"
    And I click the Log In button
    And I am logged in and directed to my user page
    And I click the Book A Taxi button
    And I input the trip information
    When I submit the booking request
    Then I should receive a rejection message