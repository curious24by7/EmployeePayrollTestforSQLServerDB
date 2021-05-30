using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayrollService
{
    public class EmployeeRepo
    {
        public static string connectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=payroll_service;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection connection = new SqlConnection(connectionString);
        EmployeeModel empModel = new EmployeeModel();
        public void GetAllEmployee()
        {
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();
                using (this.connection)
                {
                    string query = @"Select * from employee_payroll;";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            employeeModel.EmployeeID = dr.GetInt32(0);
                            employeeModel.EmployeeName = dr.GetString(1);
                            employeeModel.BasicPay = dr.GetDecimal(2);
                            employeeModel.StartDate = dr.GetDateTime(3);
                            employeeModel.Gender = dr.GetString(4);
                            employeeModel.PhoneNumber = dr.GetString(5);
                            employeeModel.Address = dr.GetString(6);
                            employeeModel.Department = dr.GetString(7);
                            employeeModel.Deductions = dr.GetDouble(8);
                            employeeModel.TaxablePay = dr.GetDecimal(9);
                            employeeModel.NetPay = dr.GetDecimal(10);
                            employeeModel.Tax = dr.GetDecimal(11);
                            System.Console.WriteLine(employeeModel.EmployeeName + " " + employeeModel.BasicPay + " " + employeeModel.StartDate + " " + employeeModel.Gender + " " + employeeModel.PhoneNumber + " " + employeeModel.Address + " " + employeeModel.Department + " " + employeeModel.Deductions + " " + employeeModel.TaxablePay + " " + employeeModel.Tax + " " + employeeModel.NetPay);
                            System.Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public bool AddEmployee(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("SpAddEmployeeDetails", this.connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                    command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@Department", model.Department);
                    command.Parameters.AddWithValue("@Gender", model.Gender);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    command.Parameters.AddWithValue("@Deductions", model.Deductions);
                    command.Parameters.AddWithValue("@TaxablePay", model.TaxablePay);
                    command.Parameters.AddWithValue("@Tax", model.Tax);
                    command.Parameters.AddWithValue("@NetPay", model.NetPay);
                    command.Parameters.AddWithValue("@StartDate", DateTime.Now);
                    this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
            return false;
        }

        public bool UpdateEmployeeSalary(EmployeeModel model)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using(connection)
                {
                    SqlCommand update = new SqlCommand("spUpdateEmployeeSalary", connection);
                    update.CommandType = System.Data.CommandType.StoredProcedure;
                    update.Parameters.AddWithValue("@id", model.EmployeeID);
                    update.Parameters.AddWithValue("@Basic_Pay", model.BasicPay);
                    connection.Open();
                    var result = update.ExecuteNonQuery();
                    connection.Close();
                    if(result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
        public void RetriveDateRange()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string query = "SELECT * from employee_payroll where start between '2015-01-03' and GETDATE()";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            empModel.EmployeeName = dr.GetString(1);
                            empModel.StartDate = dr.GetDateTime(3);
                            Console.WriteLine("[Name] {0}  [Date] {1}", empModel.EmployeeName, empModel.StartDate);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void Functions()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string SUM = @"select sum (basic_pay) from employee_payroll where gender='M'";
                    string AVG = @"select avg (basic_pay) from employee_payroll where gender='M'";
                    string MIN = @"select min (basic_pay) from employee_payroll where gender='M'";

                    SqlCommand Sumcmd = new SqlCommand(SUM, connection);
                    connection.Open();
                    SqlDataReader dr = Sumcmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            decimal Add = dr.GetDecimal(0);
                            Console.WriteLine("Salary Total = {0}", Add);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    connection.Close();

                    SqlCommand average = new SqlCommand(AVG, connection);
                    connection.Open();
                    SqlDataReader AvgDr = average.ExecuteReader();
                    if (AvgDr.HasRows)
                    {
                        while (AvgDr.Read())
                        {
                            decimal Avg = AvgDr.GetDecimal(0);
                            Console.WriteLine("Average Total Salary = {0}", Avg);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    connection.Close();

                    SqlCommand min = new SqlCommand(MIN, connection);
                    connection.Open();
                    SqlDataReader MinDr = min.ExecuteReader();
                    if (MinDr.HasRows)
                    {
                        while (MinDr.Read())
                        {
                            decimal Min = MinDr.GetDecimal(0);
                            Console.WriteLine("Minimum Salary is = {0}", Min);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    connection.Close();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);

            }
            finally
            {
                connection.Close();
            }

        }
    }

}
