namespace Employees.Models;

public class EmployeePair(int employee1Id, int employee2Id, int projectId, int daysWorked)
{
    public int Employee1Id { get; set; } = employee1Id;

    public int Employee2Id { get; set; } = employee2Id;

    public int ProjectId { get; set; } = projectId;

    public int DaysWorked { get; set; } = daysWorked;
}