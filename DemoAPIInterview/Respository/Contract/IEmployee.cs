using DemoAPIInterview.Models;

namespace DemoAPIInterview.Respository.Contract
{
    public interface IEmployee
    {
        List<Employee> GetEmployee();
        Employee PostEmployee(Employee employee);
        Employee GetEmployeeById(int id);
        Employee DeleteEmployeeById(int id);
        Employee UpdateEmployee(Employee employee);
    }
}
