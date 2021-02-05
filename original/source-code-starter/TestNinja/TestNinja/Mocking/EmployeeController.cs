using System.Data.Entity;

namespace TestNinja.Mocking
{
    public class EmployeeController
    {
        private readonly IEmployeeStorage _employeeStorage;

        public EmployeeController(IEmployeeStorage employeeStorage)
        {
            //_db = new EmployeeContext();
            this._employeeStorage = employeeStorage;
        }

        public ActionResult DeleteEmployee(int id)
        {
            // Encapsulamos en una clase externa el código que toca un recurso externo. 
            // Esto con el fin de extraer una interface con el fin de mockearla en las pruebas unitarias
            _employeeStorage.DeleteEmployee(id);

            return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult { }
 
    public class RedirectResult : ActionResult { }
    
    public class EmployeeContext
    {
        public DbSet<Employee> Employees { get; set; }

        public void SaveChanges()
        {
        }
    }

    public class Employee
    {
    }
}