using FakeXrmEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;
using Microsoft.Xrm.Sdk.Query;
using Plugin;

namespace TestPluginRegistration
{
    [TestClass]
    public class TestBasicPlugin
    {
        [TestMethod]
        public void OnContactCreate_CreateTask()
        {
            // Prepare context
            var context = new XrmFakedContext();
            var service = context.GetOrganizationService();

            // Arrange 
            var target_id = Guid.NewGuid();
            var target = new Entity("contact") { Id = target_id };

            var inputParameters = new ParameterCollection();
            inputParameters.Add("Target", target);

            var outputParameters = new ParameterCollection();
            outputParameters.Add("id", target_id);

            // Act 
            context.ExecutePluginWith<BasicPlugin>(inputParameters, outputParameters, null, null);

            var query = new QueryExpression("task");
            query.Criteria.AddCondition("regardingobjectid", ConditionOperator.Equal, target_id);
            var tasks = service.RetrieveMultiple(query);

            var columns = new ColumnSet(new String[] { "description", "subject", "scheduledstart", "scheduledend" });
            var task = service.Retrieve("task", tasks[0].Id, columns);


            // Assert 
            var count = tasks.Entities.Count;
            Assert.AreEqual(count, 1);
            var check_description = task["description"];
            const string description = "Follow up with the customer. Check if there are any new issues that need resolution.";
            Assert.AreEqual(check_description, description);
        }
    }
}
