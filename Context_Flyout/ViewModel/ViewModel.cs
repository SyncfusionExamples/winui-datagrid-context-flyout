using Microsoft.UI.Xaml;
using Syncfusion.UI.Xaml.Core;
using Syncfusion.UI.Xaml.Data;
using Syncfusion.UI.Xaml.DataGrid;
using Syncfusion.UI.Xaml.Grids;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using ListSortDirection = Syncfusion.UI.Xaml.Data.SortDirection;

namespace Context_Flyout
{
   public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {
            // Initialize Commands
            _gridCutCommand = new DelegateCommand(Cut);
            _gridCopyCommand = new DelegateCommand(Copy);
            _gridPasteCommand = new DelegateCommand(Paste);
            _gridDeleteCommand = new DelegateCommand(Delete);
            _expandAllCommand = new DelegateCommand(ExpandAll);
            _collapseAllCommand = new DelegateCommand(CollapseAll);
            _sortAscendingCommand = new DelegateCommand(SortAscending);
            _sortDescendingCommand = new DelegateCommand(SortDescending);
            _clearSummaryCommand = new DelegateCommand(ClearSummary);
            _groupColumnCommand = new DelegateCommand(GroupColumn);
            _showGroupDropAreaCommand = new DelegateCommand(ShowGroupDropArea);
            _clearSortingCommand = new DelegateCommand(ClearSorting);
            _clearFilteringCommand = new DelegateCommand(ClearFiltering);
            _clearGroupingCommand = new DelegateCommand(ClearGrouping);
            _countCommand = new DelegateCommand(SummaryCount);
            _maxCommand = new DelegateCommand(SummaryMax);
            _minCommand = new DelegateCommand(SummaryMin);
            _averageCommand = new DelegateCommand(SummaryAverage);
            _sumCommand = new DelegateCommand(SummarySum);

            // Generate orders collection
            _orders = new ObservableCollection<OrderInfo>();
            GenerateOrders();
        }

        private ObservableCollection<OrderInfo> _orders;
        public ObservableCollection<OrderInfo> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

        private void GenerateOrders()
        {
            _orders.Add(new OrderInfo(1001, "Maria Anders", 12990, "Germany", "ALFKI", "Berlin"));
            _orders.Add(new OrderInfo(1002, "Ana Trujilo",17822, "Mexico", "ANATR", "Mexico D.F."));
            _orders.Add(new OrderInfo(1003, "Antonio Moreno",22211, "Mexico", "ANTON", "Mexico D.F."));
            _orders.Add(new OrderInfo(1004, "Thomas Hardy", 17822, "UK", "AROUT", "London"));
            _orders.Add(new OrderInfo(1005, "Christina Berglund", 2311, "Sweden", "BERGS", "Lula"));
            _orders.Add(new OrderInfo(1006, "Hanna Moos", 2341, "Germany", "BLAUS", "Mannheim"));
            _orders.Add(new OrderInfo(1007, "Frederique Citeaux",4753, "France", "BLONP", "Strasbourg"));
            _orders.Add(new OrderInfo(1008, "Martin Sommer",773, "Spain", "BOLID", "Madrid"));
            _orders.Add(new OrderInfo(1009, "Laurence Lebihan",24533, "France", "BONAP", "Marseille"));
            _orders.Add(new OrderInfo(1010, "Elizabeth Lincoln",544, "Canada", "BOTTM", "Tsawassen"));
        }

        //Command Implementations for the ContextFlyout

        #region Cut

        private DelegateCommand _gridCutCommand;

        /// <summary>
        /// Gets and sets the cut command
        /// </summary>
        public DelegateCommand GridCutCommand
        {
            get { return _gridCutCommand; }
            set { _gridCutCommand = value; }
        }

        /// <summary>
        /// Cut the specified record.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="GridRecordContextFlyoutInfo"/>.
        /// </param>
        public void Cut(object param)
        {
            if (param is GridRecordContextFlyoutInfo)
            {
                var grid = (param as GridRecordContextFlyoutInfo).DataGrid;
                var copypasteoption = grid.CopyOption;
                grid.CopyOption = GridCopyOptions.CutData;
                grid.ClipboardController.Cut();
                grid.CopyOption = copypasteoption;
            }
        }

        #endregion Cut

        #region Copy

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
        /// Specifies the <see cref="GridRecordContextFlyoutInfo"/>.
        /// </param>
        public void Copy(object param)
        {
            if (param is GridRecordContextFlyoutInfo)
            {
                var grid = (param as GridRecordContextFlyoutInfo).DataGrid;
                grid.ClipboardController.Copy();
            }
        }

        #endregion Copy

        #region Paste

        private DelegateCommand _gridPasteCommand;

        /// <summary>
        /// Gets and sets the paste command
        /// </summary>
        public DelegateCommand GridPasteCommand
        {
            get
            {
                if (_gridPasteCommand != null)
                    _gridPasteCommand = new DelegateCommand(Paste, CanPaste);

                return _gridPasteCommand;
            }
            set { _gridPasteCommand = value; }
        }

        /// <summary>
        /// Paste the specified record.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="GridRecordContextFlyoutInfo"/>.
        /// </param>
        public void Paste(object param)
        {
            if (param is GridRecordContextFlyoutInfo)
            {
                var grid = (param as GridRecordContextFlyoutInfo).DataGrid;
                grid.ClipboardController.Paste();
            }
        }

        /// <summary>
        /// Determines whether the paste command can be executed.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.DataGrid.GridRecordContextMenuInfo"/>
        /// </param>
        /// <returns>
        /// <b>true</b> if the clipboard has content, Otherwise <b>false</b>
        /// </returns>
        public bool CanPaste(object param)
        {
            var content = Clipboard.GetContent();
            if (content != null && content.Contains(StandardDataFormats.Text))
                return true;
            return false;
        }

        #endregion Paste

        #region Delete

        private DelegateCommand _gridDeleteCommand;

        /// <summary>
        /// Gets and sets the expand command
        /// </summary>
        public DelegateCommand DeleteCommand
        {
            get
            {
                if (_gridDeleteCommand != null)
                    _gridDeleteCommand = new DelegateCommand(Delete);
                return _gridDeleteCommand;
            }
            set { _gridDeleteCommand = value; }
        }

        /// <summary>
        /// Expand the specified record in group caption menu.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="GridRecordContextFlyoutInfo"/>.
        /// </param>
        private void Delete(object param)
        {
            if (param is GridRecordContextFlyoutInfo)
            {
                var grid = (param as GridRecordContextFlyoutInfo).DataGrid;
                var record = (param as GridRecordContextFlyoutInfo).Record as OrderInfo;
                if (record != null)
                    (grid.DataContext as ViewModel).Orders.Remove(record);
                grid.View.Refresh();

            }
        }

        #endregion Delete

        #region ExpandAll

        private DelegateCommand _expandAllCommand;

        /// <summary>
        /// Gets and sets the expandall command
        /// </summary>
        public DelegateCommand ExpandAllCommand
        {
            get
            {
                if (_expandAllCommand != null)
                    _expandAllCommand = new DelegateCommand(ExpandAll, CanExpandAll);
                return _expandAllCommand;
            }
            set { _expandAllCommand = value; }
        }

        /// <summary>
        /// Expand all groups in datagrid.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.DataGrid.GridContextMenuInfo"/>.
        /// </param>
        public void ExpandAll(object param)
        {
            if (param is GridContextFlyoutInfo)
            {
                var grid = (param as GridContextFlyoutInfo).DataGrid;
                grid.ExpandAllGroup();
            }
        }

        /// <summary>
        /// Determines whether the Expand all command can be executed..
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="GridContextFlyoutInfo"/>.
        /// </param>
        /// <returns>
        /// <b>true</b> if the Specified datagrid can expand, Otherwise <b>false</b>
        /// </returns>
        public bool CanExpandAll(object param)
        {
            if (param is GridContextFlyoutInfo)
            {
                var grid = (param as GridContextFlyoutInfo).DataGrid;
                if (grid.View.TopLevelGroup != null && grid.View.TopLevelGroup.Groups.Count > 0)
                    return grid.View.TopLevelGroup.Groups.Any(x => !x.IsExpanded);
            }
            return false;
        }

        #endregion ExpandAll

        #region CollapseAll

        private DelegateCommand _collapseAllCommand;

        /// <summary>
        /// Gets and sets the remove command
        /// </summary>
        public DelegateCommand CollapseAllCommand
        {
            get
            {
                if (_collapseAllCommand != null)
                    _collapseAllCommand = new DelegateCommand(CollapseAll, CanCollapseAll);

                return _collapseAllCommand;
            }
            set { _collapseAllCommand = value; }
        }

        /// <summary>
        /// Collapse all groups in datagrid.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="GridContextFlyoutInfo"/>.
        /// </param>
        public void CollapseAll(object param)
        {
            if (param is GridContextFlyoutInfo)
            {
                var grid = (param as GridContextFlyoutInfo).DataGrid;
                grid.CollapseAllGroup();
            }
        }

        /// <summary>
        /// Determines whether the collapse all command can be executed.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.DataGrid.GridContextMenuInfo"/>.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specified datagrid can collapse,Otherwise <b>false</b>
        /// </returns>
        private bool CanCollapseAll(object param)
        {
            if (param is GridContextFlyoutInfo)
            {
                var grid = (param as GridContextFlyoutInfo).DataGrid;
                if (grid.View.TopLevelGroup != null && grid.View.TopLevelGroup.Groups.Count > 0)
                    return grid.View.TopLevelGroup.Groups.Any(x => x.IsExpanded);
            }
            return false;
        }

        #endregion CollapseAll

        #region SortAscending

        private DelegateCommand _sortAscendingCommand;

        /// <summary>
        /// Gets and sets the sort ascending command
        /// </summary>
        public DelegateCommand SortAscendingCommand
        {
            get
            {
                if (_sortAscendingCommand != null)
                    _sortAscendingCommand = new DelegateCommand(SortAscending, CanSortAscending);

                return _sortAscendingCommand;
            }
            set { _sortAscendingCommand = value; }
        }

        /// <summary>
        /// Sort the specific column in ascending order.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="GridColumnContextFlyoutInfo"/>.
        /// </param>
        private void SortAscending(object param)
        {
            if (param is GridColumnContextFlyoutInfo)
            {
                var grid = (param as GridContextFlyoutInfo).DataGrid;
                var column = (param as GridColumnContextFlyoutInfo).Column;
                grid.SortColumnDescriptions.Clear();
                grid.SortColumnDescriptions.Add(new SortColumnDescription() { ColumnName = column.MappingName, SortDirection = ListSortDirection.Ascending });
            }
        }

        /// <summary>
        /// Determines whether the sort ascending command can be executed.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="GridColumnContextFlyoutInfo"/>.
        /// </param>
        /// <returns>
        /// <b>true</b> if the specific column can be sort in ascending order, Otherwise<b>false</b>
        /// </returns>
        private static bool CanSortAscending(object param)
        {
            if (param is GridColumnContextFlyoutInfo)
            {
                var grid = (param as GridContextFlyoutInfo).DataGrid;
                var column = (param as GridColumnContextFlyoutInfo).Column;
                var sortColumn = grid.SortColumnDescriptions.FirstOrDefault(x => x.ColumnName == column.MappingName);
                if (sortColumn != null)
                {
                    if ((sortColumn as SortColumnDescription).SortDirection == ListSortDirection.Ascending)
                        return false;
                }
                return grid.AllowSorting;
            }
            return false;
        }

        #endregion SortAscending

        #region SortDescending

        private DelegateCommand _sortDescendingCommand;

        /// <summary>
        /// Gets and sets the sort descending command
        /// </summary>
        public DelegateCommand SortDescendingCommand
        {
            get
            {
                if (_sortDescendingCommand != null)
                    _sortDescendingCommand = new DelegateCommand(SortDescending, CanSortDescending);

                return _sortDescendingCommand;
            }
            set { _sortDescendingCommand = value; }
        }

        /// <summary>
        /// Sort the specified column in descending order.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="GridColumnContextFlyoutInfo"/>.
        /// </param>
        private void SortDescending(object param)
        {
            if (param is GridColumnContextFlyoutInfo)
            {
                var grid = (param as GridContextFlyoutInfo).DataGrid;
                var column = (param as GridColumnContextFlyoutInfo).Column;
                grid.SortColumnDescriptions.Clear();
                grid.SortColumnDescriptions.Add(new SortColumnDescription() { ColumnName = column.MappingName, SortDirection = ListSortDirection.Descending });
            }
        }

        /// <summary>
        /// Determines whether the sort descending command can be executed.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="GridColumnContextFlyoutInfo"/>.
        /// </param>
        /// <returns>
        ///  <b>true</b> if the specific column can be sort in descending order, Otherwise<b>false</b>
        /// </returns>
        private bool CanSortDescending(object param)
        {
            if (param is GridColumnContextFlyoutInfo)
            {
                var grid = (param as GridContextFlyoutInfo).DataGrid;
                var column = (param as GridColumnContextFlyoutInfo).Column;
                var sortColumn = grid.SortColumnDescriptions.FirstOrDefault(x => x.ColumnName == column.MappingName);
                if (sortColumn != null)
                {
                    if ((sortColumn as SortColumnDescription).SortDirection == ListSortDirection.Descending)
                        return false;
                }
                return grid.AllowSorting;
            }
            return false;
        }

        #endregion SortDescending

        #region clearSummary

        private DelegateCommand _clearSummaryCommand;

        /// <summary>
        /// Gets and sets the clear summary command
        /// </summary>
        public DelegateCommand ClearSummaryCommand
        {
            get { return _clearSummaryCommand; }
            set { _clearSummaryCommand = value; }
        }

        /// <summary>
        /// Clears group summary from each group.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="Syncfusion.UI.Xaml.DataGrid.GridRecordContextMenuInfo"/>.
        /// </param>
        private void ClearSummary(object param)
        {
            if (param is GridRecordContextFlyoutInfo)
            {
                var grid = (param as GridRecordContextFlyoutInfo).DataGrid;
                if (grid.GroupSummaryRows.Any())
                    grid.GroupSummaryRows.Clear();
            }
        }

        #endregion clearSummary

        #region GroupColumn

        private DelegateCommand _groupColumnCommand;

        /// <summary>
        /// Gets and sets the group column command
        /// </summary>
        public DelegateCommand GroupColumnCommand
        {
            get
            {
                if (_groupColumnCommand != null)
                    _groupColumnCommand = new DelegateCommand(GroupColumn, CanGroupColumn);
                return _groupColumnCommand;
            }
            set
            {
                _groupColumnCommand = value;
            }
        }

        /// <summary>
        /// Determines whether the group column command can be executed.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="GridColumnContextFlyoutInfo"/>.
        /// </param>
        /// <returns>
        ///  <b>true</b> if the specific column can group, Otherwise<b>false</b>
        /// </returns>
        public bool CanGroupColumn(object param)
        {
            if (param is GridColumnContextFlyoutInfo)
            {
                var grid = (param as GridColumnContextFlyoutInfo).DataGrid;
                var column = (param as GridColumnContextFlyoutInfo).Column;
                bool canGroup = false;
                if (!grid.GroupColumnDescriptions.Any(x => x.ColumnName == column.MappingName))
                {
                    var groupcolumn = column.ReadLocalValue(GridColumn.AllowGroupingProperty);
                    if (grid.AllowGrouping)
                        canGroup = true;
                    if (groupcolumn != DependencyProperty.UnsetValue || canGroup)
                        canGroup = column.AllowGrouping;
                }
                return canGroup;
            }
            return false;
        }

        /// <summary>
        /// Group the specified column.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="GridColumnContextFlyoutInfo"/>.
        /// </param>
        public void GroupColumn(object param)
        {
            if (param is GridColumnContextFlyoutInfo)
            {
                var grid = (param as GridColumnContextFlyoutInfo).DataGrid;
                var column = (param as GridColumnContextFlyoutInfo).Column;
                if (!grid.GroupColumnDescriptions.Any(x => x.ColumnName == column.MappingName))
                    grid.GroupColumnDescriptions.Add(new GroupColumnDescription() { ColumnName = column.MappingName });
            }
        }

        #endregion GroupColumn

        #region ShowGroupDropArea

        private DelegateCommand _showGroupDropAreaCommand;

        /// <summary>
        /// Gets and sets the show group drop area command
        /// </summary>
        public DelegateCommand ShowGroupDropAreaCommand
        {
            get { return _showGroupDropAreaCommand; }
            set { _showGroupDropAreaCommand = value; }
        }

        /// <summary>
        /// Show group drop area of the datagrid.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="GridColumnContextFlyoutInfo"/>.
        /// </param>
        private void ShowGroupDropArea(object param)
        {
            if (param is GridColumnContextFlyoutInfo)
            {
                var grid = (param as GridColumnContextFlyoutInfo).DataGrid;
                grid.IsGroupDropAreaExpanded = !grid.IsGroupDropAreaExpanded;
            }
        }

        #endregion ShowGroupDropArea

        #region ClearSorting

        private DelegateCommand _clearSortingCommand;

        /// <summary>
        /// Gets and sets the clear sorting command
        /// </summary>
        public DelegateCommand ClearSortingCommand
        {
            get
            {
                if (_clearSortingCommand != null)
                    _clearSortingCommand = new DelegateCommand(ClearSorting, CanClearSorting);
                return _clearSortingCommand;
            }
            set
            {
                _groupColumnCommand = value;
            }
        }

        /// <summary>
        /// Clears sorting from specified column.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="GridColumnContextFlyoutInfo"/>.
        /// </param>
        private void ClearSorting(object param)
        {
            if (param is GridColumnContextFlyoutInfo)
            {
                var grid = (param as GridContextFlyoutInfo).DataGrid;
                var column = (param as GridColumnContextFlyoutInfo).Column;
                if (grid.SortColumnDescriptions.Any(x => x.ColumnName == column.MappingName))
                    grid.SortColumnDescriptions.Remove(grid.SortColumnDescriptions.FirstOrDefault(x => x.ColumnName == column.MappingName));
            }
        }

        /// <summary>
        /// Determines whether the clear sorting command can be executed.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="GridColumnContextFlyoutInfo"/>.
        /// </param>
        /// <returns>
        ///  <b>true</b> if the specific column sorted, Otherwise<b>false</b>
        /// </returns>
        private bool CanClearSorting(object param)
        {
            if (param == null)
                return false;

            var grid = (param as GridColumnContextFlyoutInfo).DataGrid;
            var column = (param as GridColumnContextFlyoutInfo).Column;
            return grid.SortColumnDescriptions.Any(x => x.ColumnName == column.MappingName);
        }

        #endregion ClearSorting

        #region ClearFiltering

        private DelegateCommand _clearFilteringCommand;

        /// <summary>
        /// Gets and sets the clear filtering command
        /// </summary>
        public DelegateCommand ClearFilteringCommand
        {
            get
            {
                if (_clearFilteringCommand != null)
                    _clearFilteringCommand = new DelegateCommand(ClearFiltering, CanClearFiltering);
                return _clearFilteringCommand;
            }
            set
            {
                _clearFilteringCommand = value;
            }
        }

        /// <summary>
        /// Clears filter from specified column.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="GridColumnContextFlyoutInfo"/>.
        /// </param>
        private void ClearFiltering(object param)
        {
            if (param is GridColumnContextFlyoutInfo)
            {
                var column = (param as GridColumnContextFlyoutInfo).Column;
                if (column.FilterPredicates.Any())
                    column.FilterPredicates.Clear();
            }
        }

        /// <summary>
        /// Determines whether the clear filtering command can be executed.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="GridColumnContextFlyoutInfo"/>.
        /// </param>
        /// <returns>
        ///  <b>true</b> if the specific column filtered, Otherwise<b>false</b>
        /// </returns>
        private bool CanClearFiltering(object param)
        {
            if (param is GridColumnContextFlyoutInfo)
            {
                var column = (param as GridColumnContextFlyoutInfo).Column;
                return column.FilterPredicates.Any();
            }
            return false;
        }

        #endregion ClearFiltering

        #region Filtering

        internal delegate void FilterChanged();
        internal FilterChanged filterChanged;

        private bool MakeStringFilter(OrderInfo o, string option, string condition)
        {
            var value = o.GetType().GetTypeInfo().GetDeclaredProperty(option);
            var exactValue = value.GetValue(o);
            exactValue = exactValue.ToString().ToLower();
            string text = FilterText.ToLower();
            var methods = typeof(string).GetTypeInfo().GetDeclaredMethods(condition);
            if (methods.Count() != 0)
            {
                var methodInfo = methods.FirstOrDefault();
                bool result1 = (bool)methodInfo.Invoke(exactValue, new object[] { text });
                return result1;
            }
            else
                return false;
        }

        private bool MakeNumericFilter(OrderInfo o, string option, string condition)
        {
            var value = o.GetType().GetTypeInfo().GetDeclaredProperty(option);
            var exactValue = value.GetValue(o);
            double res;
            bool checkNumeric = double.TryParse(exactValue.ToString(), out res);
            if (checkNumeric)
            {
                switch (condition)
                {
                    case "Equals":
                        if (Convert.ToDouble(exactValue) == Convert.ToDouble(FilterText))
                            return true;
                        break;
                    case "Greater Than":
                        if (Convert.ToDouble(exactValue) > Convert.ToDouble(FilterText))
                            return true;
                        break;
                    case "Less Than":
                        if (Convert.ToDouble(exactValue) < Convert.ToDouble(FilterText))
                            return true;
                        break;
                    case "Not Equals":
                        if (Convert.ToDouble(FilterText) != Convert.ToDouble(exactValue))
                            return true;
                        break;
                }
            }
            return false;
        }

        public bool FilerRecords(object o)
        {
            double res;
            bool checkNumeric = double.TryParse(FilterText, out res);
            var item = o as OrderInfo;
            if (item != null && FilterText.Equals(""))
            {
                return true;
            }
            else
            {
                if (item != null)
                {
                    if (checkNumeric && !FilterOption.Equals("AllColumns"))
                    {
                        if (FilterCondition == null || FilterCondition.Equals("Contains") || FilterCondition.Equals("StartsWith") || FilterCondition.Equals("EndsWith"))
                            FilterCondition = "Equals";
                        bool result = MakeNumericFilter(item, FilterOption, FilterCondition);
                        return result;
                    }
                    else if (FilterOption.Equals("AllColumns"))
                    {
                        if (item.OrderID.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.CustomerID.ToLower().Contains(FilterText.ToLower()) ||
                            item.CustomerName.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.Country.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.ShipCity.ToString().ToLower().Contains(FilterText.ToLower()))
                            return true;
                        return false;
                    }
                    else
                    {
                        if (FilterCondition == null || FilterCondition.Equals("Equals") || FilterCondition.Equals("Less Than") || FilterCondition.Equals("Greater Than") || FilterCondition.Equals("Not Equals"))
                            FilterCondition = "Contains";
                        bool result = MakeStringFilter(item, FilterOption, FilterCondition);
                        return result;
                    }
                }
            }
            return false;
        }

        private string filterOption = "AllColumns";

        /// <summary>
        /// Get or set the FilterOption
        /// </summary>
        public string FilterOption
        {
            get { return filterOption; }
            set
            {
                filterOption = value.Replace(" ", "");
                if (filterChanged != null)
                    filterChanged();
                OnPropertyChanged("FilterOption");
            }
        }

        private string filterText = string.Empty;
        /// <summary>
        /// Get or set the FilterText
        /// </summary>
        public string FilterText
        {
            get { return filterText; }
            set
            {
                filterText = value;
                OnPropertyChanged("FilterText");
            }
        }

        private string filterCondition;
        /// <summary>
        /// Get or set the FilterCondition
        /// </summary>
        public string FilterCondition
        {
            get { return filterCondition; }
            set
            {
                filterCondition = value;
                OnPropertyChanged("FilterCondition");
            }
        }

        #endregion

        #region ClearGroups

        private DelegateCommand _clearGroupingCommand;

        /// <summary>
        /// Gets and sets the clear groups command
        /// </summary>
        public DelegateCommand ClearGroupingCommand
        {
            get
            {
                if (_clearGroupingCommand != null)
                    _clearGroupingCommand = new DelegateCommand(ClearGrouping, CanClearGrouping);
                return _clearGroupingCommand;
            }
            set
            {
                _clearGroupingCommand = value;
            }
        }

        /// <summary>
        /// Clear all groups from datagrid.
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="GridContextFlyoutInfo"/>.
        /// </param>
        private void ClearGrouping(object param)
        {
            if (param is GridContextFlyoutInfo)
            {
                var grid = (param as GridContextFlyoutInfo).DataGrid;
                grid.GroupColumnDescriptions.Clear();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="param">
        /// Specifies the <see cref="GridContextFlyoutInfo"/>.
        /// </param>
        /// <returns></returns>
        private bool CanClearGrouping(object param)
        {
            if (param is GridContextFlyoutInfo)
            {
                var grid = (param as GridContextFlyoutInfo).DataGrid;
                return grid.GroupColumnDescriptions.Any();
            }
            return false;
        }

        #endregion ClearGroups

        #region SummaryCount

        private DelegateCommand _countCommand;

        /// <summary>
        /// Gets or sets the command that applies a count summary.
        /// </summary>
        public DelegateCommand SummaryCountCommand
        {
            get { return _countCommand; }
            set { _countCommand = value; }
        }

        /// <summary>
        /// Applies a table summary row that shows the total number of customers.
        /// </summary>
        /// <param name="obj">Specifies the <see cref="GridRecordContextFlyoutInfo"/>.</param>
        private void SummaryCount(object obj)
        {
            if (obj is GridRecordContextFlyoutInfo)
            {
                var grid = (obj as GridRecordContextFlyoutInfo).DataGrid;
                var record = (obj as GridRecordContextFlyoutInfo).Record as SummaryRecordEntry;
                if (record != null)
                {
                    var summaryrow = new GridSummaryRow() { Name = "totalgroupsummaryrow", Title = "{totalSummary}", ShowSummaryInRow = true };
                    summaryrow.SummaryColumns.Add(new GridSummaryColumn() { Name = "totalSummary", MappingName = "CustomerID", SummaryType = SummaryType.CountAggregate, Format = "Total Number Of Customers : {Count}" });
                    grid.TableSummaryRows.Clear();
                    grid.TableSummaryRows.Add(summaryrow);

                }
            }
        }

        #endregion

        #region SummaryMax

        private DelegateCommand _maxCommand;

        /// <summary>
        /// Gets or sets the command that applies a maximum summary.
        /// </summary>
        public DelegateCommand SummaryMaxCommand
        {
            get { return _maxCommand; }
            set { _maxCommand = value; }
        }

        /// <summary>
        /// Applies a table summary row that shows the maximum UnitPrice.
        /// </summary>
        /// <param name="obj">Specifies the <see cref="GridRecordContextFlyoutInfo"/>.</param>
        private void SummaryMax(object obj)
        {
            if (obj is GridRecordContextFlyoutInfo)
            {
                var grid = (obj as GridRecordContextFlyoutInfo).DataGrid;
                var record = (obj as GridRecordContextFlyoutInfo).Record as SummaryRecordEntry;
                if (record != null)
                {
                    var summaryrow = new GridSummaryRow() { ShowSummaryInRow = true, Name = "totalgroupsummaryrow", Title = "{totalSummary}" };
                    summaryrow.SummaryColumns.Add(new GridSummaryColumn() { Name = "totalSummary", MappingName = "UnitPrice", SummaryType = SummaryType.DoubleAggregate, Format = "Maximum Of UnitPrice : {Max}" });
                    grid.TableSummaryRows.Clear();
                    grid.TableSummaryRows.Add(summaryrow);
                }


            }
        }

        #endregion

        #region SummaryMin

        private ICommand _minCommand;

        /// <summary>
        /// Gets or sets the command that applies a minimum summary.
        /// </summary>
        public ICommand SummaryMinCommand
        {
            get { return _minCommand; }
            set { _minCommand = value; }
        }

        /// <summary>
        /// Applies a table summary row that shows the minimum UnitPrice.
        /// </summary>
        /// <param name="obj">Specifies the <see cref="GridRecordContextFlyoutInfo"/>.</param>
        private void SummaryMin(object obj)
        {
            if (obj is GridRecordContextFlyoutInfo)
            {
                var grid = (obj as GridRecordContextFlyoutInfo).DataGrid;
                var record = (obj as GridRecordContextFlyoutInfo).Record as SummaryRecordEntry;
                if (record != null)
                {
                    var summaryrow = new GridSummaryRow() { ShowSummaryInRow = true, Name = "totalgroupsummaryrow", Title = "{totalSummary}" };
                    summaryrow.SummaryColumns.Add(new GridSummaryColumn() { Name = "totalSummary", MappingName = "UnitPrice", SummaryType = SummaryType.DoubleAggregate, Format = "Minimum Of UnitPrice : {Min}" });
                    grid.TableSummaryRows.Clear();
                    grid.TableSummaryRows.Add(summaryrow);
                }


            }
        }

        #endregion

        #region SummaryAverage

        private DelegateCommand _averageCommand;

        /// <summary>
        /// Gets or sets the command that applies an average summary.
        /// </summary>
        public DelegateCommand TableAverageCommand
        {
            get { return _averageCommand; }
            set { _averageCommand = value; }
        }


        /// <summary>
        /// Applies a table summary row that shows the average UnitPrice.
        /// </summary>
        /// <param name="obj">Specifies the <see cref="GridRecordContextFlyoutInfo"/>.</param>
        private void SummaryAverage(object obj)
        {
            if (obj is GridRecordContextFlyoutInfo)
            {
                var grid = (obj as GridRecordContextFlyoutInfo).DataGrid;
                var record = (obj as GridRecordContextFlyoutInfo).Record as SummaryRecordEntry;
                if (record != null)
                {
                    var summaryrow = new GridSummaryRow() { ShowSummaryInRow = true, Name = "totalgroupsummaryrow", Title = "{totalSummary}" };
                    summaryrow.SummaryColumns.Add(new GridSummaryColumn() { Name = "totalSummary", MappingName = "UnitPrice", SummaryType = SummaryType.DoubleAggregate, Format = "Average Of UnitPrice : {Average}" });
                    grid.TableSummaryRows.Clear();
                    grid.TableSummaryRows.Add(summaryrow);
                }


            }
        }

        #endregion

        #region SummarySum

        private DelegateCommand _sumCommand;

        /// <summary>
        /// Gets or sets the command that applies a sum summary.
        /// </summary>
        public DelegateCommand TableSumCommand
        {
            get { return _sumCommand; }
            set { _sumCommand = value; }
        }


        /// <summary>
        /// Applies a table summary row that shows the sum of UnitPrice.
        /// </summary>
        /// <param name="obj">Specifies the <see cref="GridRecordContextFlyoutInfo"/>.</param>
        private void SummarySum(object obj)
        {
            if (obj is GridRecordContextFlyoutInfo)
            {
                var grid = (obj as GridRecordContextFlyoutInfo).DataGrid;
                var record = (obj as GridRecordContextFlyoutInfo).Record as SummaryRecordEntry;
                if (record != null)
                {
                    var summaryrow = new GridSummaryRow() { ShowSummaryInRow = true, Name = "totalgroupsummaryrow", Title = "{totalSummary}" };
                    summaryrow.SummaryColumns.Add(new GridSummaryColumn() { Name = "totalSummary", MappingName = "UnitPrice", SummaryType = SummaryType.DoubleAggregate, Format = "Sum Of UnitPrice : {Sum}" });
                    grid.TableSummaryRows.Clear();
                    grid.TableSummaryRows.Add(summaryrow);
                }
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
    }
}
