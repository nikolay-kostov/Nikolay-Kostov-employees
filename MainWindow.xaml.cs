using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Windows;
using Employees.Models;
using Employees.Services;

namespace Employees;

public partial class MainWindow : Window
{
    public ObservableCollection<EmployeePair> EmployeePairs { get; set; }


    public MainWindow()
    {
        InitializeComponent();
        EmployeePairs = new ObservableCollection<EmployeePair>();
        DataContext = this;
    }
    

    private void LoadCsv_OnClick(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog { Filter = "CSV Files|*.csv" };

        if (openFileDialog.ShowDialog() != true)
        {
            return;
        }

        try
        {
            var projects = CsvService.LoadFromCsv(openFileDialog.FileName);
            var pairs = EmployeeService.FindEmployeePairs(projects);

            EmployeePairs.Clear();

            foreach (var pair in pairs)
            {
                EmployeePairs.Add(pair);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}