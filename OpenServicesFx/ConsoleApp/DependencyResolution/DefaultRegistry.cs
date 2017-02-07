using CTOnline.OpenServicesFx.Reference;
using SampleServicesLibrary;
using StructureMap.Configuration.DSL;

namespace ConsoleApp.DependencyResolution
{
    public class DefaultRegistry : Registry
    {
        #region Constructors and Destructors

        public DefaultRegistry()
        {
            Bootstraper.InitializeIoCWithStructuremap(this);

            // App Services
            For<IValuesService>().Use<ValuesService>();
        }

        #endregion
    }
}
