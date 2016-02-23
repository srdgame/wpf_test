﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ComboBoxTreeViewSample
{
    public class ComboBoxTreeView : ComboBox
    {
        private ExtendedTreeView _treeView;
        private ContentPresenter _contentPresenter;

        public ComboBoxTreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ComboBoxTreeView), new FrameworkPropertyMetadata(typeof(ComboBoxTreeView)));
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            //don't call the method of the base class
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            _treeView = (ExtendedTreeView)this.GetTemplateChild("treeView");
            _treeView.OnHierarchyMouseUp += new MouseEventHandler(OnTreeViewHierarchyMouseUp);
            _contentPresenter = (ContentPresenter)this.GetTemplateChild("ContentPresenter");

            this.SetSelectedItemToHeader();
        }

        protected override void OnDropDownClosed(EventArgs e)
        {
            base.OnDropDownClosed(e);
            this.SelectedItem = _treeView.SelectedItem;
            this.SetSelectedItemToHeader();
        }

        protected override void OnDropDownOpened(EventArgs e)
        {
            base.OnDropDownOpened(e);
            this.SetSelectedItemToHeader();
        }

        /// <summary>
        /// Handles clicks on any item in the tree view
        /// </summary>
        private void OnTreeViewHierarchyMouseUp(object sender, MouseEventArgs e)
        {
            //This line isn't obligatory because it is executed in the OnDropDownClosed method, but be it so
            this.SelectedItem = _treeView.SelectedItem;

            this.IsDropDownOpen = false;
        }

        /// <summary>
        /// Selected item of the TreeView
        /// </summary>
        public new object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public new static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(ComboBoxTreeView), new PropertyMetadata(null, new PropertyChangedCallback(OnSelectedItemChanged)));

        private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((ComboBoxTreeView)sender).UpdateSelectedItem();
        }

        /// <summary>
        /// Selected hierarchy of the treeview
        /// </summary>
        public IEnumerable<string> SelectedHierarchy
        {
            get { return (IEnumerable<string>)GetValue(SelectedHierarchyProperty); }
            set { SetValue(SelectedHierarchyProperty, value); }
        }

        public static readonly DependencyProperty SelectedHierarchyProperty =
            DependencyProperty.Register("SelectedHierarchy", typeof(IEnumerable<string>), typeof(ComboBoxTreeView), new PropertyMetadata(null, OnSelectedHierarchyChanged));

        private static void OnSelectedHierarchyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((ComboBoxTreeView)sender).UpdateSelectedHierarchy();
        }

        private void UpdateSelectedItem()
        {
            if (this.SelectedItem is TreeViewItem)
            {
                //I would rather use a correct object instead of TreeViewItem
                this.SelectedItem = ((TreeViewItem)this.SelectedItem).DataContext;
            }
            else
            {
                //Update the selected hierarchy and displays
                var model = this.SelectedItem as ITreeViewItemModel;
                if (model != null)
                {
                    this.SelectedHierarchy = model.GetHierarchy().Select(h => h.SelectedValuePath).ToList();
                }

                this.SetSelectedItemToHeader();
            }
        }

        private void UpdateSelectedHierarchy()
        {
            if (ItemsSource != null && this.SelectedHierarchy != null)
            {
                //Find corresponding items and expand or select them
                var source = this.ItemsSource.OfType<ITreeViewItemModel>();
                var item = SelectItem(source, this.SelectedHierarchy);
                this.SelectedItem = item;
            }
        }

        /// <summary>
        /// Searches the items of the hierarchy inside the items source and selects the last found item
        /// </summary>
        private static ITreeViewItemModel SelectItem(IEnumerable<ITreeViewItemModel> items, IEnumerable<string> selectedHierarchy)
        {
            if (items == null || selectedHierarchy == null || !items.Any() || !selectedHierarchy.Any())
            {
                return null;
            }

            var hierarchy = selectedHierarchy.ToList();
            var currentItems = items;
            ITreeViewItemModel selectedItem = null;

            for (int i = 0; i < hierarchy.Count; i++)
            {
                // get next item in the hierarchy from the collection of child items
                var currentItem = currentItems.FirstOrDefault(ci => ci.SelectedValuePath == hierarchy[i]);
                if (currentItem == null)
                {
                    break;
                }

                selectedItem = currentItem;

                // rewrite the current collection of child items
                currentItems = selectedItem.GetChildren();
                if (currentItems == null)
                {
                    break;
                }

                // the intermediate items will be expanded
                if (i != hierarchy.Count - 1)
                {
                    selectedItem.IsExpanded = true;
                }
            }

            if (selectedItem != null)
            {
                selectedItem.IsSelected = true;
            }

            return selectedItem;
        }

        /// <summary>
        /// Gets the hierarchy of the selected tree item and displays it at the combobox header
        /// </summary>
        private void SetSelectedItemToHeader()
        {
            string content = null;

            var item = this.SelectedItem as ITreeViewItemModel;
            if (item != null)
            {
                //Get hierarchy and display it as the selected item
                var hierarchy = item.GetHierarchy().Select(i => i.DisplayValuePath).ToArray();
                if (hierarchy.Length > 0)
                {
                    content = string.Join(" - ", hierarchy);
                }
            }

            this.SetContentAsTextBlock(content);
        }

        /// <summary>
        /// Gets the combobox header and displays the specified content there
        /// </summary>
        private void SetContentAsTextBlock(string content)
        {
            if (_contentPresenter == null)
            {
                return;
            }

            var tb = _contentPresenter.Content as TextBlock;
            if (tb == null)
            {
                _contentPresenter.Content = tb = new TextBlock();
            }
            tb.Text = content ?? ' '.ToString();

            _contentPresenter.ContentTemplate = null;
        }
    }

}
