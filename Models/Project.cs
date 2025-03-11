namespace Employees.Models;

public class Project(int projectId, int employeeId, DateTime dateFrom, DateTime dateTo)
{
    public int ProjectId { get; set; } = projectId;

    public int EmployeeId { get; set; } = employeeId;

    public DateTime DateFrom { get; set; } = dateFrom;

    public DateTime DateTo { get; set; } = dateTo;
}