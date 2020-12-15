using System;
using System.Linq;
using System.Reflection;

namespace LibraryIS.CrossCutting.Extensions
{
    public static class AssemblyExtensions
    {
        public static Assembly GetAssemblyByName(string name)
        {
            return Assembly.GetCallingAssembly()
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Union(AppDomain.CurrentDomain.GetAssemblies())
                .SingleOrDefault(assembly => assembly.GetName().Name.Contains(name));
        }
    }
}