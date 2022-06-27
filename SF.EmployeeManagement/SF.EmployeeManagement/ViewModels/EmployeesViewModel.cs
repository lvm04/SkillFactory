using SF.EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.EmployeeManagement.ViewModels
{
    public class EmployeesViewModel
    {
        private EmployeeRepository _employeeRepository { get; }
        public EmployeesViewModel()
        {
            _employeeRepository = new EmployeeRepository();
        }

        public ObservableCollection<Employee> Employees
        {
            get
            {
                return new ObservableCollection<Employee>
                    (this._employeeRepository.GetAll());
            }
        }
    }
}
