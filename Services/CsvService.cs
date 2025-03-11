using Employees.Models;
using System.Globalization;
using System.IO;
using System.Windows;

namespace Employees.Services;

public static class CsvService
{
    private static readonly string[] DateFormats = ["yyyy-MM-dd", "MM/dd/yyyy", "dd-MM-yyyy", "yyyy/MM/dd", "dd.MM.yyyy"];

    public static List<Project> LoadFromCsv(string filePath)
    {
        var projects = new List<Project>();

        try
        {
            
            foreach (var line in File.ReadLines(filePath).Skip(1)) // Skip header line
            {
                var row = line.Split(',');
                var employeeId = int.Parse(row[0].Trim());
                var projectId = int.Parse(row[1].Trim());
                var dateFrom = ParseDate(row[2].Trim());
                var dateTo = row[3].Trim().ToUpper() == "NULL" ? DateTime.Today : ParseDate(row[3].Trim());

                projects.Add(new Project(projectId, employeeId, dateFrom, dateTo));
            }
            
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            projects = new List<Project>();
        }

        return projects;
    }

    private static DateTime ParseDate(string dateStr)
    {
        return DateTime.TryParseExact(dateStr, DateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date)
            ? date
            : throw new FormatException($"Invalid date format: {dateStr}.");
    }

}