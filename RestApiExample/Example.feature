Feature: Example
	
@mytag
Scenario: Verify user
	Given I send a request
		| Method | Endpoint | ExpectedStatusCode | 
		| GET    | users/2  | 200                | 
	And response should contain
		| Assert description     | JsonPath          | Value             |
		| Verify first name      | $.data.first_name | Janet             |
		| Verify ad company name | $.ad.company      | StatusCode Weekly |

		 