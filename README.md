# How to run tests:
1. Windwos PC.
2. Visual Studio 2019. [install](https://visualstudio.microsoft.com/vs/community/)
	-  With specflow plugin -> [install](https://marketplace.visualstudio.com/items?itemName=TechTalkSpecFlowTeam.SpecFlowForVisualStudioSpecFlowForVisualStudio "How to install Specflow")
	- With NUnit test adapter plugin (only for load tests)-> [install](https://marketplace.visualstudio.com/items?itemName=NUnitDevelopers.NUnit3TestAdapter)
- Run UI tests locally:
	- [Install Java](https://java.com/en/download/help/download_options.xml)
	- [Install Node.js](https://nodejs.org/en/download/)
	- With webdriver-manager -> [Install](https://www.npmjs.com/package/webdriver-manager "Install")
		Run in node.js console
		1. `npm install -g webdriver-manager`
		2. `webdriver-manager update`
		3. `webdriver-manager start` (it will start local selenium server)
- Run UI test in [Saucelabs](https://saucelabs.com/):
	- Modify: `AFramework/Ui.Test/appsettings.json`
	 -set `RemoteDriverHubUrl` to 
	 `https://IGTestuser1234:bb73c355-0085-47cc-bab3-dea355b86801@ondemand.saucelabs.com:443/wd/hub`

- Build solution and run tests with Test Explorer -> [How to run](https://docs.microsoft.com/en-us/visualstudio/test/run-unit-tests-with-test-explorer?view=vs-2019 "How to run")

Note: All tests are integrated with Jenkins pipeline on my local.

# About tests:
- Ui tests are written using Gherkin (Specflow), so they are self-explanatory,  located here: 
	- `Ui.Test/Specflow/ToptalExample.feature`
	- `Ui.Test/Specflow/WhiskExample.feature`
- API tests are written using Gherkin (Specflow), so they are self-explanatory,  located here: 
	- `RestApi.Test/ToptalSmokeTestExamples.feature`
	- `RestApi.Test/Example.feature`
- Load test is located here: 
	- `/Load.Test/LoadTestExample.cs`
	- Description: This test simulates 1000 users who will visit the https://gitter.im in a period of 15s.
	Report will be genearted here: `Load.Test\bin\Debug\netcoreapp3.1\reports`

-  JMeter Load test information is located [here](https://git.toptal.com/screening/ilia-galperin/blob/master/JMeterLoadTest/JMeterReportAnalysis.MD)
	- To run tests -> install Java and latest JMeter
	- Run in cmd to execute test`jmeter -n -t JMeterLoadTest.jmx -l log.jtl` 
	- Run in cmd to generate the report  `jmeter -g <pathtologfile>\log.jtl -o <folderfor report>\report`
