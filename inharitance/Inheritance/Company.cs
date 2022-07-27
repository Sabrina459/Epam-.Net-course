using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace InheritanceTask
{
    public class Company
    {
        private readonly Employee[] employees;
        public Company(Employee[] employees) 
        {
            if (employees == null) return;

            this.employees = new Employee[employees.Length];
            for (int i = 0; i < employees.Length; i++)
            {
                if(employees[i] !=null)
                    this.employees[i] = employees[i];
            }
        }

        public void GiveEverybodyBonus(decimal companyBonus) 
        {
            if (employees == null) return;
            foreach(Employee employee in employees)
            {
                if (employee!= null)
                {
                    employee.SetBonus(companyBonus);

                }
            }
        }
        public decimal TotalToPay()
        {
            decimal total = 0;
            foreach (Employee employee in employees)
            {
                if (employee != null)
                {
                    total += employee.ToPay();
                }
            }
            return total;
        }

        public string NameMaxSalary()
        {
            string nameMax = employees[0].Name;
            decimal salaryMax = employees[0].ToPay();
            foreach (Employee employee in employees[1..])
            {
                if (employee.ToPay() > salaryMax && employee!=null)
                {
                    salaryMax = employee.ToPay();
                    nameMax = employee.Name;
                }
            }
            return nameMax;
        }
    }
}
