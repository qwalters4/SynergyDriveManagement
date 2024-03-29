﻿using IM.Common;
using IM.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IM.Views
{
    /// <summary>
    /// Interaction logic for InventoryView.xaml
    /// </summary>
    public partial class InventoryView : UserControl
    {
        private InventoryViewModel viewModel { get { return (InventoryViewModel)DataContext; } }
        public InventoryView()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            viewModel.Load();
            viewModel.CapLowerString = "0";
            viewModel.CapUpperString = "20000";
            dg.ItemsSource = viewModel.SearchResults;
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Filter();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.SetSaveRequired();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveData();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            viewModel.CloseConnection();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewModel.UpdateResults();
        }

        private void Export_Data(object sender, RoutedEventArgs e)
        {
            viewModel.exportData();
        }
    }
}
