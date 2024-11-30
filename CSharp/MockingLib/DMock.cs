using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace MockingLib
{
    public class DMock<TMockType> where TMockType : class
    {
        private const string assemblyName = "DarkMockAssembly";
        private static readonly AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.Run);
        private static readonly ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName);

        private Dictionary<MethodBase, Delegate> implementations = new();
        private TypeBuilder typeBuilder;

        public TMockType Instance { get; }

        public DMock()
        {
            if (!typeof(TMockType).IsInterface)
            {
                throw new InvalidOperationException($"{typeof(TMockType)} must be interface.");
            }

            var typeName = $"Mock_{Guid.NewGuid().ToString().Replace('-', '_')}";
            typeBuilder = moduleBuilder.DefineType(typeName, TypeAttributes.Public | TypeAttributes.Class, typeof(DMockBase));
            typeBuilder.AddInterfaceImplementation(typeof(TMockType));

            CreateConstructor();
            ImplementInterface();

            Instance = (TMockType)Activator.CreateInstance(typeBuilder.CreateType(), implementations);
        }

        private void CreateConstructor()
        {
            var constructorInfo = typeof(DMockBase).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).First();

            var constructorParameters = constructorInfo.GetParameters().Select(info => info.ParameterType).ToArray();

            var constructor = typeBuilder.DefineConstructor(
                MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName,
                CallingConventions.Standard,
                constructorParameters);

            var generator = constructor.GetILGenerator();

            for (var i = 0; i < constructorParameters.Length + 1; i++)
            {
                generator.Emit(OpCodes.Ldarg, i);
            }

            generator.Emit(OpCodes.Call, constructorInfo); //call to base() constructor
            generator.Emit(OpCodes.Ret);
        }

        private void ImplementInterface()
        {
            var methods = typeof(TMockType).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod);

            var isImplementedMethod = typeof(DMockBase).GetMethod("IsImplemented", BindingFlags.NonPublic | BindingFlags.Instance);

            var dynamicInvoke = typeof(Delegate).GetMethod(nameof(Delegate.DynamicInvoke), new[] { typeof(object[]) });

            var getMethodFromHandle = typeof(MethodBase).GetMethod("GetMethodFromHandle", new[] { typeof(RuntimeMethodHandle) });

            var arrayEmptyCall = typeof(Array)
                .GetMethod("Empty", BindingFlags.Static | BindingFlags.Public)
                .MakeGenericMethod(typeof(object));

            foreach (var interfaceMethod in methods)
            {
                var parameters = interfaceMethod.GetParameters().Select(info => info.ParameterType).ToArray();
                var methodBuilder = typeBuilder.DefineMethod(interfaceMethod.Name,
                    MethodAttributes.Public
                    | MethodAttributes.Virtual
                    | MethodAttributes.HideBySig
                    | MethodAttributes.NewSlot
                    | MethodAttributes.Final,
                    interfaceMethod.ReturnType, parameters);

                var ilGenerator = methodBuilder.GetILGenerator();

                var local0 = ilGenerator.DeclareLocal(typeof(MethodBase)); //will contain current interface method
                var local1 = ilGenerator.DeclareLocal(typeof(Delegate)); //will contain current interface method
                var local2 = ilGenerator.DeclareLocal(interfaceMethod.ReturnType); // will contain default value for our return type.

                ilGenerator.Emit(OpCodes.Ldtoken, interfaceMethod);
                ilGenerator.Emit(OpCodes.Call, getMethodFromHandle);
                ilGenerator.Emit(OpCodes.Stloc_0);

                var fallbackWithDefault = ilGenerator.DefineLabel();

                ilGenerator.Emit(OpCodes.Ldarg_0); //this
                ilGenerator.Emit(OpCodes.Ldloc_0); //interface method
                ilGenerator.Emit(OpCodes.Ldloca_S, local1); //interface method local variable
                ilGenerator.Emit(OpCodes.Call, isImplementedMethod);
                ilGenerator.Emit(OpCodes.Brfalse_S, fallbackWithDefault);

                ilGenerator.Emit(OpCodes.Ldloc_1); //delegate
                ilGenerator.Emit(OpCodes.Call, arrayEmptyCall); //get empty array of objects
                ilGenerator.Emit(OpCodes.Callvirt, dynamicInvoke); //call dynamic invoke
                ilGenerator.Emit(OpCodes.Unbox_Any, interfaceMethod.ReturnType); //cast object to the type we need
                ilGenerator.Emit(OpCodes.Ret);

                //somewhere here is default fallback
                ilGenerator.MarkLabel(fallbackWithDefault);
                ilGenerator.Emit(OpCodes.Ldloca_S, local2);
                ilGenerator.Emit(OpCodes.Initobj, interfaceMethod.ReturnType);
                ilGenerator.Emit(OpCodes.Ldloc_2);
                ilGenerator.Emit(OpCodes.Ret);
            }
        }

        public void Setup<TResult>(Expression<Func<TMockType, TResult>> methodCall,
        Expression<Func<TResult>> implementation)
        {
            if (methodCall.Body is not MethodCallExpression methodCallExpression)
            {
                throw new InvalidOperationException("Only methods are supported in the Setup!");
            }

            var methodInfo = methodCallExpression.Method;

            implementations[methodInfo] = implementation.Compile();
        }
    }
}
