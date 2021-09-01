' Developer Express Code Central Example:
' How to allow ComboBoxEdit to select multiple items with the autocomplete feature
' 
' By default, ComboBoxEdit allows you to select one item using the autocomplete
' feature.
' This example provides a way to enable the behavior when the
' autocomplete feature works for multiple item selection. Firstly, it is necessary
' to create a ComboBoxEdit descendant and override the CreateEditStrategy method,
' which should return your own ComboBoxEditStrategy class descendant.
' Then,
' override and implement the ProcessChangeText method, and some more methods to
' enable item auto-searching selection.
' For unimpeded search and selection of
' items without losing input field focus, the set FocusPopupOnOpen property to
' False.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E4226

' Developer Express Code Central Example:
' How to allow ComboBoxEdit to select multiple items with the autocomplete feature
' 
' By default, ComboBoxEdit allows you to select one item using the autocomplete
' feature.
' This example provides a way to enable the behavior when the
' autocomplete feature works for multiple item selection. Firstly, it is necessary
' to create a ComboBoxEdit descendant and override the CreateEditStrategy method,
' which should return your own ComboBoxEditStrategy class descendant.
' Then,
' override and implement the ProcessChangeText method, and some more methods to
' enable item auto-searching selection.
' For unimpeded search and selection of
' items without losing input field focus, the set FocusPopupOnOpen property to
' False.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E4226

Imports System.Reflection
Imports System.Resources
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Windows

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.
<Assembly: AssemblyTitle("CustomCheckComboBox")>
<Assembly: AssemblyDescription("")>
<Assembly: AssemblyConfiguration("")>
<Assembly: AssemblyCompany("")>
<Assembly: AssemblyProduct("CustomCheckComboBox")>
<Assembly: AssemblyCopyright("Copyright ©  2012")>
<Assembly: AssemblyTrademark("")>
<Assembly: AssemblyCulture("")>

' Setting ComVisible to false makes the types in this assembly not visible 
' to COM components.  If you need to access a type in this assembly from 
' COM, set the ComVisible attribute to true on that type.
<Assembly: ComVisible(False)>

'In order to begin building localizable applications, set 
'<UICulture>CultureYouAreCodingWith</UICulture> in your .csproj file
'inside a <PropertyGroup>.  For example, if you are using US english
'in your source files, set the <UICulture> to en-US.  Then uncomment
'the NeutralResourceLanguage attribute below.  Update the "en-US" in
'the line below to match the UICulture setting in the project file.

'[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


<Assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)>


' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Build and Revision Numbers 
' by using the '*' as shown below:
' [assembly: AssemblyVersion("1.0.*")]
<Assembly: AssemblyVersion("1.0.0.0")>
<Assembly: AssemblyFileVersion("1.0.0.0")>
