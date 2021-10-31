﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Mono.Addins;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle ("Pinta.Tools")]
[assembly: AssemblyDescription ("")]
[assembly: AssemblyConfiguration ("")]
[assembly: AssemblyCompany ("Microsoft")]
[assembly: AssemblyProduct ("Pinta.Tools")]
[assembly: AssemblyCopyright ("Copyright © Microsoft 2010")]
[assembly: AssemblyTrademark ("")]
[assembly: AssemblyCulture ("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible (false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid ("411e5713-933e-475c-a051-ab5e63ce77f1")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion ("0.6.0.0")]
[assembly: AssemblyFileVersion ("0.6.0.0")]

[assembly: Addin ("DefaultTools", "0.7", Category = "Core")]
[assembly: AddinName ("Default Tools")]
[assembly: AddinDescription ("The default tools and brushes that ship with Pinta")]
[assembly: AddinDependency ("Pinta", "1.0")]
