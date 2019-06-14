#if INTERACTIVE
#load "../ProvidedTypes/ProvidedTypes.fsi ../ProvidedTypes/ProvidedTypes.fs"
#endif

namespace FSharp.Fix.Helpers

type SomeRuntimeHelper() = 
    static member Help() = "help"

#if !IS_DESIGNTIME
// Put the TypeProviderAssemblyAttribute in the runtime DLL, pointing to the design-time DLL
[<assembly:CompilerServices.TypeProviderAssembly("FSharp.Fix.Repository.DesignTime.dll")>]
do ()
#endif

