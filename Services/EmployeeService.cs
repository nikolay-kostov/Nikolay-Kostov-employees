using Employees.Models;
using System.Windows;

namespace Employees.Services;

public static class EmployeeService
{
    public static List<EmployeePair> FindEmployeePairs(List<Project> projects)
    {
        var pairs = new List<EmployeePair>();

        var groupedByProject = projects.GroupBy(p => p.ProjectId);
        try
        {
            foreach (var group in groupedByProject)
            {
                var employees = group.OrderBy(p => p.DateFrom)
                    .ToList();

                for (var i = 0; i < employees.Count; i++)
                {
                    for (var j = i + 1; j < employees.Count; j++)
                    {
                        var emp1 = employees[i];
                        var emp2 = employees[j];

                        var overlapStart = emp1.DateFrom > emp2.DateFrom
                            ? emp1.DateFrom
                            : emp2.DateFrom;
                        var overlapEnd = emp1.DateTo < emp2.DateTo
                            ? emp1.DateTo
                            : emp2.DateTo;

                        if (overlapStart >= overlapEnd)
                        {
                            continue;
                        }

                        var daysWorked = (overlapEnd - overlapStart).Days;
                        pairs.Add(new EmployeePair(emp1.EmployeeId, emp2.EmployeeId, emp1.ProjectId, daysWorked));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            pairs = new List<EmployeePair>();
            MessageBox.Show($"Error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        } 

        return pairs.OrderByDescending(p => p.DaysWorked).ToList();
    }
}