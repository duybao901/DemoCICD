using System.Reflection;

namespace DemoCICD.API;

public static class AssemblyReference
{
    public static readonly Assembly assembly = typeof(AssemblyReference).Assembly;
}
