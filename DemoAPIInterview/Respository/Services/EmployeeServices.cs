using DemoAPIInterview.Models;
using DemoAPIInterview.Respository.Contract;

namespace DemoAPIInterview.Respository.Services
{
    public class EmployeeServices :IEmployee
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeServices(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public Employee DeleteEmployeeById(int id)
        {
            var emp = dbContext.Employees.SingleOrDefault(x => x.Id == id); 
            if (emp != null) { 
             dbContext.Employees.Remove(emp);
                return emp;
            }
            return emp;
        }

        public List<Employee> GetEmployee()
        {
           var employee = dbContext.Employees.ToList();
            return employee;
        }

        public Employee GetEmployeeById(int id)
        {
            var emp = dbContext.Employees.SingleOrDefault(e=> e.Id == id);
            return emp;
        }

        public Employee PostEmployee(Employee employee)
        {
            dbContext.Employees.Add(employee);
            dbContext.SaveChanges();
            return employee;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var emp = dbContext.Employees.SingleOrDefault(e => e.Id == employee.Id);
            if (emp != null) { 
                emp.Id = employee.Id;
                emp.FirstName = employee.FirstName;
                emp.LastName = employee.LastName;
                emp.Email = employee.Email;
                emp.Address = employee.Address;
                dbContext.Employees.Update(emp);
                dbContext.SaveChanges(true);
                return employee;
            }return null;
            
        }
    }
}
