Feature: User request weather forecas

Scenario: Call forecast endpoint
	Given the user enter int index page
	And write the address '4600 Silver Hill Rd, Washington, DC 20233'
	When the user submit the address
	Then the forecast endpoint is called and return the result