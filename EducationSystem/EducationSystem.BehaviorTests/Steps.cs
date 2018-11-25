using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EducationSystem.BehaviorTests
{
    [Binding]
    public sealed class Steps
    {
        [TestInitialize]
        public void Initialize()
        {

        }
        [Given(@"Registration Name (.*)")]
        public void GivenRegistrationModel(string name)
        {
            ScenarioContext.Current.Add("Name", name);
        }

        [Given(@"Registration Address (.*)")]
        public void GivenRegistrationAddress(string address)
        {
            ScenarioContext.Current.Add("Address", address);
        }

        [When(@"method is called")]
        public void WhenMethodIsCalled()
        {
            var name = (string)ScenarioContext.Current["Name"];
            var address = (string)ScenarioContext.Current["Address"];

            // need to  controller here and get the  value out of controller
            ScenarioContext.Current.Add("result", false);
        }

        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(bool expectedResult)
        {
            var result = (bool)ScenarioContext.Current["result"];
            Assert.IsTrue(result == expectedResult);
        }


    }
}
