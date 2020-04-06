Feature: Example
	
@mytag
Scenario: Verify user
	Given I send a request
		| Method | Payload         | Endpoint | ExpectedStatusCode | UserType  |
		| POST   | {"name":"Test"} | lists    | 200                | WhiskUser |


		 