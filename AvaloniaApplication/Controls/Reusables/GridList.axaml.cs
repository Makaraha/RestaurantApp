using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml.Templates;
using AvaloniaApplication.ViewModels.Tabs.Orders;
using Domain;

namespace AvaloniaApplication.Controls.Reusables
{
    public partial class GridList : UserControl
    {
        private int _componentWidth = 0;

        public GridList()
        {
            InitializeComponent();

            var bound = this.GetObservable(UserControl.BoundsProperty);
            bound.Subscribe(new AnonymousObserver<Rect>(x => 
            { 
                _componentWidth = (int)(x.Right - x.Left); 
                HandleListUpdate();
            }, (_) => { }, () => { }));
        }


        public static readonly DirectProperty<GridList, ObservableCollection<OrderViewModel>> ItemSourceProperty =
            AvaloniaProperty.RegisterDirect<GridList, ObservableCollection<OrderViewModel>>(nameof(ItemSource), o => o.ItemSource, (o, v) => o.ItemSource = v);

        private ObservableCollection<OrderViewModel> _itemSource;
        public ObservableCollection<OrderViewModel> ItemSource
        {
            get => _itemSource;
            set
            {
                SetAndRaise(ItemSourceProperty, ref _itemSource, value);
                HandleListUpdate();
            }
        }

        public static readonly DirectProperty<GridList, DataTemplate> DataTemplateProperty =
            AvaloniaProperty.RegisterDirect<GridList, DataTemplate>(nameof(DataTemplate), o => o.DataTemplate, (o, v) => o.DataTemplate = v);

        private DataTemplate _dataTemplate;
        public DataTemplate DataTemplate
        {
            get => _dataTemplate;
            set
            {
                SetAndRaise(DataTemplateProperty, ref _dataTemplate, value);
                HandleListUpdate();
            }
        }

        public static readonly DirectProperty<GridList, int> CellWidthProperty =
            AvaloniaProperty.RegisterDirect<GridList, int>(nameof(CellWidth), o => o.CellWidth, (o, v) => o.CellWidth = v);

        private int _cellWidth;
        public int CellWidth
        {
            get => _cellWidth;
            set
            {
                SetAndRaise(CellWidthProperty, ref _cellWidth, value);
                HandleListUpdate();
            }
        }

        public static readonly DirectProperty<GridList, int> CellHeightProperty =
            AvaloniaProperty.RegisterDirect<GridList, int>(nameof(CellHeight), o => o.CellHeight, (o, v) => o.CellHeight = v);

        private int _cellHeight;
        public int CellHeight
        {
            get => _cellHeight;
            set
            {
                SetAndRaise(CellHeightProperty, ref _cellHeight, value);
                HandleListUpdate();
            }
        }

        public static readonly DirectProperty<GridList, int> CellSpacingProperty =
            AvaloniaProperty.RegisterDirect<GridList, int>(nameof(CellSpacing), o => o.CellSpacing, (o, v) => o.CellSpacing = v);

        private int _cellSpacing;
        public int CellSpacing
        {
            get => _cellSpacing;
            set
            {
                SetAndRaise(CellSpacingProperty, ref _cellSpacing, value);
            }
        }

        private void HandleListUpdate()
        {
            if (ItemSource == null || DataTemplate == null || CellWidth == 0 || CellHeight == 0 || CellSpacing == 0 || _componentWidth == 0)
                return;

            MainGrid.Children.Clear();

            var columnsCount = (_componentWidth + CellSpacing) / (CellWidth + CellSpacing);
            MainGrid.Width = (CellWidth + CellSpacing) * columnsCount - CellSpacing;

            if(columnsCount == 0)
                return;

            var rowsCount = (int)Math.Ceiling((decimal)ItemSource.Count / columnsCount);
            UpdateDefinitions(rowsCount, columnsCount);

            int row = 0;
            int column = 0;

            foreach (var item in ItemSource)
            {
                var control = DataTemplate.Build(item);
                control[Grid.RowProperty] = row;
                control[Grid.ColumnProperty] = column;
                control.DataContext = item;
                control.Width = CellWidth;
                control.Height = CellHeight;

                var marginRight = column + 1 == columnsCount ? 0 : CellSpacing;
                var marginBottom = row + 1 == rowsCount ? 0 : CellSpacing;
                control.Margin = new Thickness(0, 0, marginRight, marginBottom);

                MainGrid.Children.Add(control);

                column++;
                if(column >= columnsCount)
                {
                    column = 0;
                    row++;
                }    
            }
        }

        private void UpdateDefinitions(int rowsCount, int columnsCount)
        {
            MainGrid.ColumnDefinitions.Clear();
            MainGrid.RowDefinitions.Clear();

            for (int i = 0; i < rowsCount; i++)
                MainGrid.RowDefinitions.Add(new RowDefinition());

            for (int i = 0; i < columnsCount; i++)
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
        }
    }
}
