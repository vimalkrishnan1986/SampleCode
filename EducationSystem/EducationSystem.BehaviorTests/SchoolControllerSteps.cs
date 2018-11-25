using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;

namespace EducationSystem.BehaviorTests
{
    [Binding]
    public class SchoolControllerSteps
    {
        [Given(@"I have entered name ""(.*)""  into the model")]
        public void GivenIHaveEnteredNameIntoTheModel(string p0)
        {
            ScenarioContext.Current.Add("name", p0);
        }

        [Given(@"I have entered  address ""(.*)"" into the model")]
        public void GivenIHaveEnteredAddressIntoTheModel(string p0)
        {
            ScenarioContext.Current.Add("address", p0);
        }

        [Given(@"When I press submit")]
        public void GivenWhenIPressSubmit()
        {
            var name = Convert.ToString(ScenarioContext.Current["name"]);
            string address = Convert.ToString(ScenarioContext.Current["address"]);
            //
            bool res = false;

            //make call to controller and assign the value accordingly..

            //Since specflow is not stable enough to support .net core,we can not include any of .net core projects over here..

            Assert.IsTrue(res);
        }
    }
}
