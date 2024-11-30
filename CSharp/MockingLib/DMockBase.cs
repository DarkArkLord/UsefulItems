using System.Reflection;

namespace MockingLib
{
    public abstract class DMockBase
    {
        private readonly Dictionary<MethodBase, Delegate> implementations;

        protected DMockBase(Dictionary<MethodBase, Delegate> implementations)
        {
            this.implementations = implementations;
        }

        protected bool IsImplemented(MethodBase method, out Delegate implementation)
        {
            return implementations.TryGetValue(method, out implementation);
        }
    }
}
