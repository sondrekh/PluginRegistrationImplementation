using Dynamics.Basic;
using PluginRegistration;
using PluginRegistrationSetup;
using System.IO;
using System.Threading.Tasks;

namespace PluginRegistration_setup
{
    class Program
    {
        async static Task Main(string[] args)
        {
            // Get variables for local- or external run
            string pathToCredentials = @"C:\Users\SondreKværneHansen\Documents\GitHub\Programmatisk registrering av plugins\Plugin Registration Implementation\ids.json";
            string pathToAssembly = @"C:\Users\SondreKværneHansen\Documents\GitHub\Programmatisk registrering av plugins\Plugin Registration Implementation\Plugin\bin\Debug\Plugin.dll";
            CrmAuthentication authentication;

            if (File.Exists(pathToCredentials))
                authentication = CrmAuthentication.GetIdVariablesFromFile(pathToCredentials);

            else
            {
                pathToAssembly = args[4];
                authentication = new CrmAuthentication
                {
                    clientId = args[0],
                    clientSecret = args[1],
                    tenantId = args[2],
                    resourceUrl = args[3]
                };
            }

            // Establish crm connection 
            var crm = new Crm(authentication);
            await crm.GetAccessToken();

            // Get Assembly- and plugin-setup 
            var setup = new AssemblySetup().Setup(pathToAssembly);

            // Run registration setup
            await Registration.FullRegistration(crm, setup);
        }
    }
}
