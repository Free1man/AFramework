Feature: ToptalSmokeTestExamples
		This Feature provides an example for somke Rest Api test.
		Focus to verify that Anonymous user can access some data and Unauthorized can not.
		Difference between 'Anonymous' and 'Unauthorized' user:
		Anonymous - user that access data from an authorized client (have a valid token based on valid client id)
		Unauthorized - user that have no rights to access id data from API (no any token)

#Categories
Scenario: Get categories as Anonymous
	When I send a request
		| Method | Endpoint           | ExpectedStatusCode | UserType  |
		| GET    | recipes/categories | 200                | Anonymous |
	Then response should contain
        | Assert description                           | JsonPath           | ExpectedValue |
        | Verify that lunch is presented in categories | $.categories[0].id | lunch         |

Scenario: An unauthorized user can not get categories
	When I send a request
		| Method | Endpoint           | ExpectedStatusCode | UserType     |
		| GET    | recipes/categories | 401                | Unauthorized |

#Feed
Scenario: Get Feed as Anonymous
	When I send a request
		| Method | Endpoint | ExpectedStatusCode | UserType  |
		| GET    | feed     | 200                | Anonymous |
	
Scenario: An unauthorized user can not get Feed
	When I send a request
		| Method | Endpoint | ExpectedStatusCode | UserType     |
		| GET    | feed     | 401                | Unauthorized |

#collections
Scenario: Get collections as Anonymous
	When I send a request
		| Method | Endpoint    | ExpectedStatusCode | UserType  |
		| GET    | collections | 200                | Anonymous |
	Then response should contain
        | Assert description   | JsonPath            | ExpectedValue |
        | Verify Recipes Count | $.totalRecipesCount | 0             |

Scenario:  An unauthorized user can not get collections
	When I send a request
		| Method | Endpoint    | ExpectedStatusCode | UserType     |
		| GET    | collections | 401                | Unauthorized |

	





