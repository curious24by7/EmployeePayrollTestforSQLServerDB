using System;

namespace EmployeePayrollService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Payroll!");
            EmployeeRepo repo = new EmployeeRepo();
            EmployeeModel employee = new EmployeeModel();
            //employee.EmployeeName = "Shyam";
            //employee.Department = "Sales";
            //employee.PhoneNumber = "9863907678";
            //employee.Address = "9865 KY Street";
            //employee.Gender = "M";
            //employee.BasicPay = 10000.00M;
            //employee.Deductions = 1500.00;
            //employee.StartDate = Convert.ToDateTime("2020-11-03");
            //if (repo.AddEmployee(employee))
            //    Console.WriteLine("Records added successfully");

            employee.EmployeeID = 2;
            employee.BasicPay = 3000000;
            if (repo.UpdateEmployeeSalary(employee))
            {
                Console.WriteLine("Salary Updated Successfully");
            }
            //repo.GetAllEmployee();
            Console.ReadKey();
        }
    }
}
