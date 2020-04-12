Feature:User flow test -> Create/delete list
		This Feature file provides an example for user flow Rest Api test.
		Focus to verify that valid user can create and delete list.
	
Scenario: Create an empty list, delete it, then verify it deleted for sure
	Given I send a request
		| Method | Payload         | Endpoint | ExpectedStatusCode | UserType  |
		| POST   | {"name":"Test"} | lists    | 200                | Anonymous |
	When I send a request
		| Method | Endpoint | ExpectedStatusCode | UserType  |
		| GET    | lists    | 200                | Anonymous |
	Then response should contain 
		| Assert description                         | JsonPath                         | ExpectedValue |
		| Verify that list was created with no items | $.[?(@.name=='Test')].itemsCount | 0             |
	And I save value from json for future reuse
		| refrenceName | jsonPath |
		| listId       | $..id    |
	And I send a request
		| Method | Endpoint   | ExpectedStatusCode | UserType  |
		| DELETE | {{listId}} | 200                | Anonymous |
	And I send a request
		| Method | Endpoint   | ExpectedStatusCode | UserType  |
		| GET    | {{listId}} | 400                | Anonymous |


		 