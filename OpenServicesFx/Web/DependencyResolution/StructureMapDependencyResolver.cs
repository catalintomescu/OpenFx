using System.Web.Http.Dependencies;
using StructureMap;

namespace Web.DependencyResolution
{
    public class StructureMapDependencyResolver : StructureMapDependencyScope, IDependencyResolver
    {
        public StructureMapDependencyResolver(IContainer container)
            : base(container)
        {
        }

        public IDependencyScope BeginScope()
        {
            var child = this.Container.GetNestedContainer();
            return new StructureMapDependencyResolver(child);
        }
    }
}