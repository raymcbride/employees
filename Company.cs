using System;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace Employees
{
	//Custom interface for EmpID indexer
	interface IMyInterface
	{
		object this[int index]
		{
				get;
		}
	}

	public class Company : IList, ICollection, IEnumerable, ICloneable, IMyInterface
	{
		private ArrayList empArray;
		private FileInfo payFile;
		private float payrollTotal;

		#region Constructors
		//default constructor
		public Company()
		{
			empArray = new ArrayList();
			payFile  = new FileInfo("pay.txt");
			payrollTotal = 0;
		}
		#endregion

		#region Methods and properties
		//add an employee
		public void AddEmployee(Employee emp)
		{
			this.Add(emp);
		}

		//delete an employee based on their id
		public void RemoveEmployee(int EmpID)
		{
			for(int i = 0; i < empArray.Count; i++)
			{
				if (((Employee)empArray[i]).EmpID == EmpID)
					this.Remove((Employee)empArray[i]);
			}
		}

		//delete all employees of a particular type
		public void DeleteEmployeeType(Type T)
		{
			for(int i = 0; i < empArray.Count; i++)
			{
				if(T == ((empArray[i]).GetType()))
					this.Remove((Employee)empArray[i]);
			}
		}

		//determine the employee type, give them the appropriate bonus
		//then add their total pay to the payroll
		public void CalculatePayroll(int ManagerBonus, int SalesPersonBonus, 
			int PTSalesPersonBonus, int HumanRsourcesBonus, int ConsultantBonus)
		{
			payrollTotal = 0;
			foreach(Employee emp in empArray)
			{
				if (emp is Manager)
					emp.GiveBonus(ManagerBonus);
				if (emp is SalesPerson)
				{
					if (emp is PTSalesPerson)
						emp.GiveBonus(PTSalesPersonBonus);
					else emp.GiveBonus(SalesPersonBonus);
				}
				if (emp is HumanResources)
					emp.GiveBonus(HumanRsourcesBonus);
				if (emp is Consultant)
					emp.GiveBonus(ConsultantBonus);
				payrollTotal += emp.Pay;
			}
		}

		//print the payroll to file and display the total on screen
		public void DisplayPayroll()
		{
			StreamWriter writer = new StreamWriter("pay.txt", true);
			foreach(Employee emp in empArray)
			{
				writer.WriteLine("Name: {0} Pay: {1}\n", emp.GetFullName(), emp.Pay);
			}
			writer.Close();
			Console.WriteLine("The total Payroll is: {0}\n", payrollTotal);
		}

		//get rid of all the employees
		public void ClearAll()
		{
			this.Clear();
		}

		//display a particular employee's details
		public void Display(Employee emp)
		{
			if(emp == null)
				Console.WriteLine("Not a valid Employee\n");
			else if(!(this.Present(emp)))
				Console.WriteLine("Employee does not exist\n");
			else 
			{
				emp.DisplayStats();
				Console.WriteLine();
			}
		}

		//determine whether an employee exists
		public bool Present(Employee emp)
		{
			return this.Contains(emp);
		}

		//find an employee based on their id
		public Employee WithID(int EmpID)
		{
			foreach(Employee emp in empArray)
			{
				if(((Employee)emp).EmpID == EmpID)
					return emp;
			}
			return null;
			
		}

		//find an employee based on a name match
		public Employee FindName(string NamePattern)
		{
			foreach(Employee emp in empArray)
			{
				if(Regex.IsMatch(emp.GetFullName(), NamePattern))
					return emp;

			}
			return null;
		}

		//sort employees by name
		public void NameSort()
		{
			empArray.Sort();
		}

		//sort employees by id
		public void EmpIDSort()
		{
			Employee.EmpCompare c = Employee.GetComparer();
			empArray.Sort(c);
		}

		//index on name
		public object this[string FullName]
		{
			get
			{
				if(FullName.Length == 0)
				{
					throw new IndexOutOfRangeException("FullName");
				}
				foreach(Employee emp in empArray)
				{
					if(emp.GetFullName() == FullName)
						return emp;
				}
				return null;
			}
		}

		//explicit implementation of interface
		//I have chosen to explicitly implement the indexer for EmpID
		//rather than the IList indexer as I believe it is more intuitive
		//to implicitly use array-indexability for for loops etc
		object IMyInterface.this[int EmpID]
		{
			get
			{
				if(EmpID == 0)
				{
					throw new IndexOutOfRangeException("EmpID");
				}
				foreach(Employee emp in empArray)
				{
					if(emp.EmpID == EmpID)
						return emp;
				}
				return null;
			}
		}
		//Implementation of operator overload as discussed with Keith
		public static Company operator ++(Company company)
		{
			Company employees = new Company();
			return employees;
		}

		//Implementation of operator overload as discussed with Keith
		public static Company operator --(Company company)
		{
			Company employees = new Company();
			return employees;
		}
		#endregion

		#region IList Members

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		public int IndexOf(object employee)
		{
			Employee emp = employee as Employee;
			if(emp == null)
				return -1;
			for(int i = 0; i < empArray.Count; i++)
			{
				if(((Employee)empArray[i]).EmpID == emp.EmpID)
					return i;
			}
			return -1;
		}

		public void Insert(int index, object employee)
		{
			Employee emp = employee as Employee;
			if(emp == null)
				throw new InvalidCastException("You can only insert Employees");
			if(index < 0 || index > empArray.Count - 1)
				throw new IndexOutOfRangeException("index");
			empArray.Insert(index, emp);
		}

		public void RemoveAt(int index)
		{
			if(index < 0 || index > empArray.Count - 1)
				throw new IndexOutOfRangeException("You have specified an invalid index");
			else empArray.RemoveAt(index);
		}

		public void Remove(object employee)
		{
			Employee emp = employee as Employee;
			if(emp != null)
			{
				int idx = this.IndexOf(emp);
				if (idx >= 0)
					this.RemoveAt(idx);
			}
		}

		public void Clear()
		{
			empArray = new ArrayList();
		}

		public bool Contains(object employee)
		{
			Employee emp = employee as Employee;
			if(emp == null)
				return false;
			else
				return IndexOf(emp) != -1;
		}

		public object this[int index]
		{
			get
			{
				if(index < 0 || index > empArray.Count - 1)
					throw new IndexOutOfRangeException("index");
				else
					return empArray[index];
			}
			set
			{
				if(index < 0 || index > empArray.Count - 1)
					throw new IndexOutOfRangeException("index");
				else
					empArray[index] = value;
			}
		}

		public int Add(object employee)
		{
			Employee emp = employee as Employee;
			if (emp == null)
				throw new InvalidCastException("Can only add Employees");
			empArray.Add(emp);
			return empArray.Count - 1;
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return empArray.GetEnumerator();
		}

		#endregion

		#region ICloneable Members

		public object Clone()
		{
			Company temp = new Company();
			foreach(Employee emp in this.empArray)
				temp.Add(emp);
			return temp;
		}

		#endregion

		#region ICollection Members

		public bool IsSynchronized
		{
			get
			{
				return true;
			}
		}

		public int Count
		{
			get
			{
				return empArray.Count;
			}
		}

		public void CopyTo(Array array, int index)
		{
			if(index < 0)
				throw new ArgumentOutOfRangeException("Index cannot be negative.");
			if(array == null)
				throw new ArgumentNullException();
			if(array.GetLowerBound(0) != 0 || array.Rank > 1)
				throw new ArgumentException("Only zero-based, single-dimensioned arrays permitted.");
			if(array.Length >= index || array.Length - index < empArray.Count)
				throw new ArgumentException();
			empArray.CopyTo(array);
		}

		public object SyncRoot
		{
			get
			{
				return empArray.SyncRoot;
			}
		}
		#endregion

	}
}



