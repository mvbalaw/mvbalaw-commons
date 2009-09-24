using StructureMap.Configuration.DSL;

namespace StarterProject_StructureMap.Core
{
    public class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            var helloWorld = new HelloWorld("StructureMap says hello!");
              
            ForRequestedType<HelloWorld>().AsSingletons().TheDefault.IsThis(helloWorld);
          
        }

    }
}