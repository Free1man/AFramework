# How to run tests:
1. Windwos PC.
2. Visual Studio 2019. [install](https://visualstudio.microsoft.com/vs/community/)
	-  With specflow plugin -> [install](https://marketplace.visualstudio.com/items?itemName=TechTalkSpecFlowTeam.SpecFlow)
3. Build solution and run tests with Test Explorer -> [How to run](https://docs.microsoft.com/en-us/visualstudio/test/run-unit-tests-with-test-explorer?view=vs-2019 "How to run")

# Tests description

Original requirements:
1) Return how many named brands of used car are available in the TradeMe UsedCars
category.
2) Check that the brand ‘Kia’ exists and return the current number of Kia cars listed.
3) Check that the brand ‘Hispano Suiza’ does not exist.

Note: return replaced with Assert.

There are two examples of tests presented in this solution: UnitTest style and Specflow style.
Both of them are sharing the same small codebase to demonstrate the flexibility to choose any other preferred framework.




