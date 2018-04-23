# How to allow ComboBoxEdit to select multiple items with the autocomplete feature


<p>By default, ComboBoxEdit allows you to select one item using the autocomplete feature.</p>
<p>This example provides a way to enable the behavior when the autocomplete feature works for multiple item selection.</p>
<p>Firstly, it is necessary to create a ComboBoxEdit descendant and override the CreateEditStrategy method, which should return your own ComboBoxEditStrategy class descendant.</p>
<p>Then, override and implement the ProcessChangeText method, and some more methods to enable item auto-searching selection.</p>
<p>For unimpeded search and selection of items without losing input field focus, the set FocusPopupOnOpen property to False.<br /><br /></p>
<p><strong>UPDATE: </strong> In 15.1, we provided this feature out of the box. Please refer to this thread to get more information: <a href="https://www.devexpress.com/Support/Center/p/Q561698">Q561698: Provide ComboBoxEdit incremental search functionality </a></p>

<br/>


