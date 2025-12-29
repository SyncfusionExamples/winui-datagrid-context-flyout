using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Syncfusion.UI.Xaml.Data;
using Syncfusion.UI.Xaml.DataGrid;
using System.Collections.ObjectModel;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Context_Flyout
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Adding Table Summary Row
            sfDataGrid.TableSummaryRows.Add(new GridTableSummaryRow()
            {
                ShowSummaryInRow = true,
                Title = "Total Number Of Customers : {CustomerName}",
                SummaryColumns = new ObservableCollection<ISummaryColumn>()
                {
                    new GridSummaryColumn()
                    {
                        Name = "CustomerName",
                        MappingName = "CustomerName",
                        SummaryType = SummaryType.CountAggregate,
                        Format = "{Count:d}"
                    },
                }
             });

            //Adding Group Summary Row
            sfDataGrid.GroupSummaryRows.Add(new GridSummaryRow()
            {
                Title = "Total Number Of Customers : {CustomerName}",
                ShowSummaryInRow = true,
                SummaryColumns = new ObservableCollection<ISummaryColumn>()
                {
                    new GridSummaryColumn()
                    {
                        Name = "CustomerName",
                        MappingName = "CustomerName",
                        SummaryType = SummaryType.DoubleAggregate,
                        Format = "{Count:d}"
                    }
                }
            });

            //Setting the Context Flyouts

            // -------- Record Context Flyout --------
            var recordFlyout = new MenuFlyout();

            var cutItem = new MenuFlyoutItem { Text = "Cut", Icon = new SymbolIcon(Symbol.Cut) };
            cutItem.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.GridCutCommand"),
                Source = sfDataGrid
            });
            cutItem.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            recordFlyout.Items.Add(cutItem);

            var copyItem = new MenuFlyoutItem { Text = "Copy", Icon = new SymbolIcon(Symbol.Copy) };
            copyItem.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.GridCopyCommand"),
                Source = sfDataGrid
            });
            copyItem.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            recordFlyout.Items.Add(copyItem);

            var pasteItem = new MenuFlyoutItem { Text = "Paste", Icon = new SymbolIcon(Symbol.Paste) };
            pasteItem.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.GridPasteCommand"),
                Source = sfDataGrid
            });
            pasteItem.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            recordFlyout.Items.Add(pasteItem);

            var deleteItem = new MenuFlyoutItem { Text = "Delete", Icon = new SymbolIcon(Symbol.Delete) };
            deleteItem.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.DeleteCommand"),
                Source = sfDataGrid
            });
            deleteItem.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            recordFlyout.Items.Add(deleteItem);

            sfDataGrid.RecordContextFlyout = recordFlyout;


            // -------- Header Context Flyout --------
            var headerFlyout = new MenuFlyout();

            var sortAscItem = new MenuFlyoutItem { Text = "Sort Ascending"};
            sortAscItem.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.SortAscendingCommand"),
                Source = sfDataGrid
            });
            sortAscItem.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            headerFlyout.Items.Add(sortAscItem);

            var sortDescItem = new MenuFlyoutItem { Text = "Sort Descending" };
            sortDescItem.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.SortDescendingCommand"),
                Source = sfDataGrid
            });
            sortDescItem.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            headerFlyout.Items.Add(sortDescItem);

            headerFlyout.Items.Add(new MenuFlyoutSeparator());

            var groupColumnItem = new MenuFlyoutItem { Text = "Group by this Column" };
            groupColumnItem.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.GroupColumnCommand"),
                Source = sfDataGrid
            });
            groupColumnItem.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            headerFlyout.Items.Add(groupColumnItem);

            var toggleGroupAreaItem = new MenuFlyoutItem { Text = "Show/Hide Group Drop Area" };
            toggleGroupAreaItem.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.ShowGroupDropAreaCommand"),
                Source = sfDataGrid
            });
            toggleGroupAreaItem.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            headerFlyout.Items.Add(toggleGroupAreaItem);

            headerFlyout.Items.Add(new MenuFlyoutSeparator());

            var clearSortingItem = new MenuFlyoutItem { Text = "Clear Sorting" };
            clearSortingItem.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.ClearSortingCommand"),
                Source = sfDataGrid
            });
            clearSortingItem.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            headerFlyout.Items.Add(clearSortingItem);

            var clearFilteringItem = new MenuFlyoutItem { Text = "Clear Filtering" };
            clearFilteringItem.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.ClearFilteringCommand"),
                Source = sfDataGrid
            });
            clearFilteringItem.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            headerFlyout.Items.Add(clearFilteringItem);

            sfDataGrid.HeaderContextFlyout = headerFlyout;


            // -------- Group Drop Item Context Flyout --------
            var groupDropItemFlyout = new MenuFlyout();

            var expandAllItem = new MenuFlyoutItem { Text = "Expand all" };
            expandAllItem.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.ExpandAllCommand"),
                Source = sfDataGrid
            });
            expandAllItem.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            groupDropItemFlyout.Items.Add(expandAllItem);

            var collapseAllItem = new MenuFlyoutItem { Text = "Collapse all" };
            collapseAllItem.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.CollapseAllCommand"),
                Source = sfDataGrid
            });
            collapseAllItem.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            groupDropItemFlyout.Items.Add(collapseAllItem);

            var groupSortAscItem = new MenuFlyoutItem { Text = "Sort Ascending" };
            groupSortAscItem.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.SortAscendingCommand"),
                Source = sfDataGrid
            });
            groupSortAscItem.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            groupDropItemFlyout.Items.Add(groupSortAscItem);

            var groupSortDescItem = new MenuFlyoutItem { Text = "Sort Descending" };
            groupSortDescItem.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.SortDescendingCommand"),
                Source = sfDataGrid
            });
            groupSortDescItem.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            groupDropItemFlyout.Items.Add(groupSortDescItem);

            var groupClearSortingItem = new MenuFlyoutItem { Text = "Clear Sorting" };
            groupClearSortingItem.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.ClearSortingCommand"),
                Source = sfDataGrid
            });
            groupClearSortingItem.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            groupDropItemFlyout.Items.Add(groupClearSortingItem);

            var groupClearGroupsItem = new MenuFlyoutItem { Text = "Clear Grouping" };
            groupClearGroupsItem.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.ClearGroupingCommand"),
                Source = sfDataGrid
            });
            groupClearGroupsItem.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            groupDropItemFlyout.Items.Add(groupClearGroupsItem);

            sfDataGrid.GroupDropItemContextFlyout = groupDropItemFlyout;


            // -------- Group Drop Area Context Flyout --------
            var groupDropAreaFlyout = new MenuFlyout();

            var clearGroupsItem = new MenuFlyoutItem { Text = "Clear Groups" };
            clearGroupsItem.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.ClearGroupingCommand"),
                Source = sfDataGrid
            });
            clearGroupsItem.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            groupDropAreaFlyout.Items.Add(clearGroupsItem);

            sfDataGrid.GroupDropAreaContextFlyout = groupDropAreaFlyout;


            // -------- Caption Summary Context Flyout --------
            var captionSummaryFlyout = new MenuFlyout();
            var captionExpandAllItem = new MenuFlyoutItem { Text = "Expand all" };
            captionExpandAllItem.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.ExpandAllCommand"),
                Source = sfDataGrid
            });
            captionExpandAllItem.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            captionSummaryFlyout.Items.Add(captionExpandAllItem);

            var captionCollapseAllItem = new MenuFlyoutItem { Text = "Collapse all" };
            captionCollapseAllItem.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.CollapseAllCommand"),
                Source = sfDataGrid
            });
            captionCollapseAllItem.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            captionSummaryFlyout.Items.Add(captionCollapseAllItem);
            sfDataGrid.GroupCaptionContextFlyout = captionSummaryFlyout;


            // -------- Group Summary Context Flyout --------
            var groupSummaryFlyout = new MenuFlyout();

            var clearSummaryItem = new MenuFlyoutItem { Text = "Clear Summary" };
            clearSummaryItem.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.ClearSummaryCommand"),
                Source = sfDataGrid
            });
            clearSummaryItem.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            groupSummaryFlyout.Items.Add(clearSummaryItem);

            sfDataGrid.GroupSummaryContextFlyout = groupSummaryFlyout;


            // -------- Table Summary Context Flyout --------
            var tableSummaryFlyout = new MenuFlyout();

            var count = new MenuFlyoutItem { Text = "Count" };
            count.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.SummaryCountCommand"),
                Source = sfDataGrid
            });
            count.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            tableSummaryFlyout.Items.Add(count);

            var min = new MenuFlyoutItem { Text = "Min" };
            min.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.SummaryMinCommand"),
                Source = sfDataGrid
            });
            min.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            tableSummaryFlyout.Items.Add(min);

            var max = new MenuFlyoutItem { Text = "Max" };
            max.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.SummaryMaxCommand"),
                Source = sfDataGrid
            });
            max.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            tableSummaryFlyout.Items.Add(max);

            var average = new MenuFlyoutItem { Text = "Average" };
            average.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.TableAverageCommand"),
                Source = sfDataGrid
            });
            average.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            tableSummaryFlyout.Items.Add(average);

            var sum = new MenuFlyoutItem { Text = "Sum" };
            sum.SetBinding(MenuFlyoutItem.CommandProperty, new Binding
            {
                Path = new PropertyPath("DataContext.TableSumCommand"),
                Source = sfDataGrid
            });
            sum.SetBinding(MenuFlyoutItem.CommandParameterProperty, new Binding());
            tableSummaryFlyout.Items.Add(sum);

            sfDataGrid.TableSummaryContextFlyout = tableSummaryFlyout;
        }
    }
}
