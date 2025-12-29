# winui-datagrid-context-flyout
DataGrid provides an entirely customizable context flyout to commonly expose functionality on the user interface. Context flyouts can be created for different rows in an efficient manner.

**XAML**
```xml
xmlns:dataGrid="using:Syncfusion.UI.Xaml.DataGrid"

<dataGrid:SfDataGrid.RecordContextFlyout>
    <MenuFlyout>
        <MenuFlyoutItem Text="Cut" />
        <MenuFlyoutItem Text="Copy" />
        <MenuFlyoutItem Text="Paste" />
        <MenuFlyoutItem Text="Delete" />
    </MenuFlyout>
</dataGrid:SfDataGrid.RecordContextFlyout>
```

**C#**
```csharp
using Syncfusion.UI.Xaml.Core;
using Syncfusion.UI.Xaml.DataGrid;

public class OrderInfoViewModel
{
    public OrderInfoViewModel()
    {
        _gridCopyCommand = new DelegateCommand(Copy);
    }
        
    private DelegateCommand _gridCopyCommand;

    /// <summary>
    /// Gets and sets the copy command
    /// </summary>
    public DelegateCommand GridCopyCommand
    {
        get { return _gridCopyCommand; }
        set { _gridCopyCommand = value; }
    }
    
    /// <summary>
    /// Copy the specified record.
    /// </summary>
    /// <param name="param">
    /// Specifies the <see cref="Syncfusion.UI.Xaml.DataGrid.GridRecordContextFlyoutInfo"/>
    /// </param>
    public void Copy(object param)
    {
        if (param is GridRecordContextFlyoutInfo)
        {	
            var dataGrid = (param as GridRecordContextFlyoutInfo).DataGrid;
            dataGrid.ClipboardController.Copy();
        }
    }
}
```

![Context Flyout in WinUI DataGrid](Context_Flyout.gif)

Take a moment to peruse the [WinUI DataGrid - Context Flyout](https://help.syncfusion.com/winui/datagrid/context-flyout) documentation, to learn more about context flyout with examples.