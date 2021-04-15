using RaaLabs.Edge.Testing;
using TechTalk.SpecFlow;

namespace RaaLabs.Edge.Connectors.Modbus.Specs.Hooks
{
    [Binding]
    class AssemblyRegistration
    {
        private readonly ComponentAssemblies _assemblies;

        public AssemblyRegistration(ComponentAssemblies assemblies)
        {
            _assemblies = assemblies;
        }

        [BeforeScenario]
        private void RegisterAssembly()
        {
            _assemblies.Add(GetType().Assembly);
        }
    }
}
