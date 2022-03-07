using PluginRegistration.Interfaces;
using PluginRegistration.Models;
using PluginRegistration_setup;
using System.Collections.Generic;

namespace PluginRegistrationSetup
{
    public class AssemblySetup : IAssembly
    {
        public PluginAssembly Assembly { get; set; }
        public List<IPluginSetup> Plugins { get; set; }

        public void AddAssemblyIdToPlugins(string assemblyId)
        {
            Plugins = new List<IPluginSetup>();
            Plugins.Add(new PluginSetup_BasicPlugin().Setup(assemblyId));
            Plugins.Add(new PluginSetup_DummyPlugin().Setup(assemblyId));
        }

        public IAssembly Setup(string PathToAssembly)
        {
            Assembly = new PluginAssembly(PathToAssembly);
            return this;
        }
    }
}
