using JinHu.Visualization.Plotter2D;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Markup;

// General Information about an assembly is controlled through the following set of attributes. 
// Change these attribute values to modify the information associated with an assembly.
[assembly: AssemblyTitle("WPF Plotter2D")]
[assembly: AssemblyDescription("WPF Control for Scientific Data Visualization")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Jin Hu")]
[assembly: AssemblyProduct("JinHu.Visualization.Plotter2D")]
[assembly: AssemblyCopyright("Copyright © Jin Hu 2009 - 2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: SatelliteContractVersion("1.0.0.0")]
[assembly: AssemblyFileVersionAttribute("1.0.0.0")]
[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
[assembly: XmlnsDefinition(AssemblyConstants.DefaultXmlNamespace, "JinHu.Visualization.Plotter2D")]
[assembly: XmlnsDefinition(AssemblyConstants.DefaultXmlNamespace, "JinHu.Visualization.Plotter2D.Charts")]
[assembly: XmlnsDefinition(AssemblyConstants.DefaultXmlNamespace, "JinHu.Visualization.Plotter2D.Graphs")]
[assembly: XmlnsDefinition(AssemblyConstants.DefaultXmlNamespace, "JinHu.Visualization.Plotter2D.DataSources")]
[assembly: XmlnsDefinition(AssemblyConstants.DefaultXmlNamespace, "JinHu.Visualization.Plotter2D.Common")]
[assembly: XmlnsPrefix(AssemblyConstants.DefaultXmlNamespace, "Plotter2D")]
[assembly: CLSCompliant(true)]
[assembly: AllowPartiallyTrustedCallers]
[module: SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]

namespace JinHu.Visualization.Plotter2D
{
  public static class AssemblyConstants
  {
    public const string DefaultXmlNamespace = "http://jinhuca.visualization.com/Plotter2D/1.0";
  }
}
