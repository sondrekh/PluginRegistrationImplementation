using PluginRegistration.Interfaces;
using PluginRegistration.Models;
using System.Collections.Generic;

namespace PluginRegistration_setup
{
    public class PluginSetup_DummyPlugin : IPluginSetup
    {
        public PluginType plugin { get; set; }
        public List<SdkMessageProcessingStep> steps { get; set; }

        public IPluginSetup Setup(string assemblyId)
        {
            steps = new List<SdkMessageProcessingStep>();

            plugin = new PluginType()
            {
                PluginAssemblyId = assemblyId,
                TypeName = "Plugin.DummyPlugin"
            };
            return this;
        }

        public void AddSteps(string pluginId)
        {
            steps.Add(new DeleteStep().Setup(pluginId).Step);
        }
    }

    public class DeleteStep : IStep
    {
        public SdkMessageProcessingStep Step { get; set; }
        public List<SdkMessageProcessingStepImage> Images { get; set; }

        public IStep Setup(string pluginId)
        {
            Step = new SdkMessageProcessingStep()
            {
                PluginTypeId = pluginId,
                Message = "create",
                Entity = "contact",
                Mode = (int)Mode.Async,
                Stage = (int)Stage.postOperation,
            };

            return this;
        }
    }
}
