using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public interface IEmployeeStorage
    {
        void DeleteEmployee(int id);
    }

    //Esta clase no se llamo EmployeeRepository  porque los repositorios no deberían tener un método Save
    // https://programmingwithmosh.com/net/common-mistakes-with-the-repository-pattern/
    // https://www.youtube.com/watch?v=rtXpYpZdOzM&feature=youtu.be
    // Si queremos usar el patrón repositorio, también deberíamos traer una unidad de trabajo (Unit of Work)
    public class EmployeeStorage : IEmployeeStorage
    {
        private EmployeeContext _db;

        public EmployeeStorage()
        {
            _db = new EmployeeContext();
        }

        public void DeleteEmployee(int id)
        {
            var employee = _db.Employees.Find(id);

            if (employee == null) return;

            _db.Employees.Remove(employee);
            _db.SaveChanges();
        }
    }
}
