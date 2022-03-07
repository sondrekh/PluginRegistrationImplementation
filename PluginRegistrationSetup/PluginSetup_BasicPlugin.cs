using PluginRegistration.Interfaces;
using PluginRegistration.Models;
using System.Collections.Generic;

namespace PluginRegistration_setup
{
    public class PluginSetup_BasicPlugin : IPluginSetup
    {
        public PluginType plugin { get; set; }
        public List<SdkMessageProcessingStep> steps { get; set; }

        public IPluginSetup Setup(string assemblyId)
        {
            plugin = new PluginType()
            {
                PluginAssemblyId = assemblyId,
                TypeName = "Plugin.BasicPlugin"
            };

            return this;
        }

        public void AddSteps(string pluginId)
        {
            steps = new List<SdkMessageProcessingStep>();
            steps.Add(new CreateStep().Setup(pluginId).Step);
            steps.Add(new UpdateStep().Setup(pluginId).Step);
        }
    }

    public class CreateStep : IStep
    {
        public SdkMessageProcessingStep Step { get; set; }
        public List<SdkMessageProcessingStepImage> Images { get; set; }

        public IStep Setup(string pluginId)
        {
            Step = new SdkMessageProcessingStep()
            {
                Message = "create",
                Entity = "contact",
                Mode = (int)Mode.Sync,
                Stage = (int)Stage.postOperation,
                PluginTypeId = pluginId
            };

            Step.Images.Add(ImageA());

            return this;
        }

        private SdkMessageProcessingStepImage ImageA()
        {
            var image = new SdkMessageProcessingStepImage()
            {
                EntityAlias = "Image",
                Name = "Image A",
                ImageType = (int)ImageType.PostImage,
                MessagePropertyName = "Id"
            };

            return image;
        }
    }

    public class UpdateStep : IStep
    {
        public SdkMessageProcessingStep Step { get; set; }
        public List<SdkMessageProcessingStepImage> Images { get; set; }

        public IStep Setup(string pluginId)
        {
            Step = new SdkMessageProcessingStep()
            {
                Message = "update",
                Entity = "contact",
                Mode = (int)Mode.Async,
                Stage = (int)Stage.postOperation,
                PluginTypeId = pluginId,
                FilteringAttributes = "firstname,lastname"
            };

            Step.Images.Add(ImageB());
            Step.Images.Add(ImageC());

            return this;
        }

        private SdkMessageProcessingStepImage ImageB()
        {
            var image = new SdkMessageProcessingStepImage()
            {
                EntityAlias = "Image",
                Name = "Image B",
                ImageType = (int)ImageType.PostImage,
                Attributes = "firstname,lastname",
                MessagePropertyName = "Target"
            };

            return image;
        }

        private SdkMessageProcessingStepImage ImageC()
        {
            var image = new SdkMessageProcessingStepImage()
            {
                EntityAlias = "Image",
                Name = "Image C",
                ImageType = (int)ImageType.PostImage,
                Attributes = "firstname,lastname",
                MessagePropertyName = "Target"
            };

            return image;
        }
    }
}
