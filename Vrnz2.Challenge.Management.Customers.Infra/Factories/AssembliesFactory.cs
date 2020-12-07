using System.Reflection;

namespace Vrnz2.Challenge.Management.Customers.Infra.Factories
{
    public static class AssembliesFactory
    {
        #region Methods

        public static Assembly GetAssemblies<T>() => typeof(T).GetTypeInfo().Assembly;

        #endregion
    }
}
