using System.Reflection;

namespace DemoCICD.Contract;
public class AssemblyReference
{
    public static readonly Assembly assembly = typeof(AssemblyReference).Assembly;
}
