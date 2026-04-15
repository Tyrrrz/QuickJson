#nullable enable
// ExcludeFromCodeCoverageAttribute is not available on netstandard1.0 or net35, so we
// define a minimal stub to allow the attribute to be used on older targets.
#if !QUICKJSON_INCLUDE_COVERAGE && (NETSTANDARD1_0 || NET35)
namespace System.Diagnostics.CodeAnalysis
{
    [AttributeUsage(
        AttributeTargets.Class
            | AttributeTargets.Constructor
            | AttributeTargets.Method
            | AttributeTargets.Property
            | AttributeTargets.Struct,
        Inherited = false
    )]
    internal sealed class ExcludeFromCodeCoverageAttribute : Attribute { }
}
#endif
