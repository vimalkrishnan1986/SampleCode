Feature: SchoolController
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Regsiter
	Given I have entered name "test"  into the model
	And I have entered  address "Address" into the model
	And When I press submit
	Then the result should be  "true" on the screen
