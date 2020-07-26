Feature:
	Using the TradeMe Sandbox web site write automation code which does the following:
	Check that the brand ‘Kia’ exists and return the current number of Kia cars listed.
	Check that the brand ‘Hispano Suiza’ does not exist.
	
	
Scenario: TradeMe used cars test 
	Given I send a request
		| Method | Endpoint                                  | ExpectedStatusCode | UserType   |
		| GET    | Categories/UsedCars.json?with_counts=true | 200                | Authorized |
	Then response should contain node
		| Assert description                | JsonPath                          |
		| Check that the brand ‘Kia’ exists | $.Subcategories[?(@.Name=='Kia')] |
	And response should contain 
		| Assert description                         | JsonPath                                | ExpectedValue |
		| Verify current number of 'Kia' cars listed | $.Subcategories[?(@.Name=='Kia')].Count | 0             |
	And response should not contain node
		| Assert description                                   | JsonPath                                    |
		| Check that the brand ‘Hispano Suiza’ does not exist. | $.Subcategories[?(@.Name=='Hispano Suiza')] |


		 
