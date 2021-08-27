<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128644302/14.1.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4226)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [CustomComboBoxEdit.cs](./CS/CustomCheckComboBox/CustomComboBoxEdit.cs) (VB: [CustomComboBoxEdit.vb](./VB/CustomCheckComboBox/CustomComboBoxEdit.vb))
* [CustomComboBoxEditSettings.cs](./CS/CustomCheckComboBox/CustomComboBoxEditSettings.cs) (VB: [CustomComboBoxEditSettings.vb](./VB/CustomCheckComboBox/CustomComboBoxEditSettings.vb))
* [CustomEditStrategy.cs](./CS/CustomCheckComboBox/CustomEditStrategy.cs) (VB: [CustomEditStrategy.vb](./VB/CustomCheckComboBox/CustomEditStrategy.vb))
* [ItemsIndexFinder.cs](./CS/CustomCheckComboBox/ItemsIndexFinder.cs) (VB: [ItemsIndexFinder.vb](./VB/CustomCheckComboBox/ItemsIndexFinder.vb))
* [MainWindow.xaml](./CS/CustomCheckComboBox/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/CustomCheckComboBox/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/CustomCheckComboBox/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/CustomCheckComboBox/MainWindow.xaml.vb))
<!-- default file list end -->
# How to allow ComboBoxEdit to select multiple items with the autocomplete feature


<p>By default, ComboBoxEdit allows you to select one item using the autocomplete feature.</p>
<p>This example provides a way to enable the behavior when the autocomplete feature works for multiple item selection.</p>
<p>Firstly, it is necessary to create a ComboBoxEdit descendant and override the CreateEditStrategy method, which should return your own ComboBoxEditStrategy class descendant.</p>
<p>Then, override and implement the ProcessChangeText method, and some more methods to enable item auto-searching selection.</p>
<p>For unimpeded search and selection of items without losing input field focus, the set FocusPopupOnOpen property to False.<br /><br /></p>
<p><strong>UPDATE: </strong> In 15.1, we provided this feature out of the box. Please refer to this thread to get more information: <a href="https://www.devexpress.com/Support/Center/p/Q561698">Q561698: Provide ComboBoxEdit incremental search functionality </a></p>

<br/>


