using System.Reflection;

namespace DemoCICD.Contract;
public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
