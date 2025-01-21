using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace explicacio
{
    public class Employee
    {
        #region Atributs
        //atributs de la classe Employee
        private static int idGeneral = 0;
        private const string CODI_ACCES = "1234";
        private int id;
        private string firstName;
        private string lastName;
        private double salary;
        private double commission;
        private DateTime hireDate;
        #endregion

        #region Constructors
        //Constructors
        public Employee()
        {
            idGeneral++;
            id = idGeneral;
            salary = 0;
            commission = 0;
            DiaSetmana(DateTime.Today);
        }
        public Employee(string firstName) : this()
        {
            this.firstName = firstName;
        }
        public Employee(int id, string nom, string lastName) : this(nom)
        {
            this.id = id;
            this.lastName = lastName;
        }
        public Employee(int id, string firstName, string lastName, double salary, double commission, DateTime hireDate)
        {
            idGeneral++;
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.salary = salary;
            this.commission = commission;
            //while((int)hireDate.DayOfWeek == 0 || (int)hireDate.DayOfWeek == 6) // Diumenge
            //    hireDate = hireDate.AddDays(1) ;
            //this.hireDate = hireDate;
            DiaSetmana(hireDate);
        }
        public Employee(int id, string firstName, string lastName, double salary) : this(id, firstName, lastName, salary, 0, DateTime.Today)
        {
        }
        #endregion

        #region Propietats
        #region Getters/Setters
        public string GetLastName() { return this.lastName; }
        public void SetCommission(double value) { this.commission = value; }
        public double GetSalary(string codi)
        {
            if (codi == CODI_ACCES)
                return salary;
            else
                throw new NotImplementedException();
        }
        #endregion
        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                if (value == null || value == "")
                    throw new NotImplementedException();
                else
                    //this.firstName = value.ToUpper();
                    this.firstName = value.Substring(0, 1).ToUpper() + value.Substring(1).ToLower();
            }
        }
        //propietat no necessaria
        public double TotalSalary
        {
            get { return SalariTotal(); }
        }
        public string LastName
        {
            get { return this.lastName; }
            set
            {
                if (value == null || value == "")
                    throw new NotImplementedException();
                else
                    //this.lastName = value.ToUpper();
                    this.lastName = value.Substring(0, 1).ToUpper() + value.Substring(1).ToLower();
            }
        }
        public double Commission
        {
            set { commission = value; }
        }
        public double Salary
        {
            get { return salary; }
            set
            {
                salary = MajorSMI(value);
            }
        }
        #endregion

        #region Metodes Instancia
        private double MajorSMI(double valor)
        {
            if (valor > 1148)
                return valor;
            else
                return 0;
        }
        public double SalariTotal()
        {
            return salary * (1 + commission);
        }
        public double AnysTreballats()
        {
            //throw new NotImplementedException(); 
            return DateTime.Now.Year - hireDate.Year;
        }
        private void DiaSetmana(DateTime hireDate)
        {
            if (hireDate.DayOfWeek == DayOfWeek.Sunday)
                this.hireDate = hireDate.AddDays(1);
            else if (hireDate.DayOfWeek == DayOfWeek.Saturday)
                this.hireDate = hireDate.AddDays(2);
            else
                this.hireDate = hireDate;
            //la resta de dies de la setmana
        }
        public override string ToString()
        {
            return $"id: {id}\nnom: {firstName}\ncongom: {lastName}\nSalary: {salary}\nComissió: {commission}\ndata contractació: {hireDate.ToString("D")}";
            //return $"{id};{firstName};{lastName};{salary};{commission};{hireDate.ToString("d")}"; 
        }
        public string ToString(string tipus)
        {
            if (tipus == "csv") //return $"id: {id};nom: {firstName};congom: {lastName};salari: {salary};Comissió: {commission};data contractació: {hireDate.ToString("D")}";
                return $"{id};{firstName};{lastName};{salary};{commission};{hireDate.ToString("d")}";
            return null;
        }
        #endregion

        #region Metodes Classe
        public static double MaxTotalSalary(Employee emp1, Employee emp2)
        {
            return Math.Max(emp1.SalariTotal(), emp2.SalariTotal());
        }
        #endregion

    }
}
