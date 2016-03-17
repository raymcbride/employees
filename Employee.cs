using System;
using System.Collections;

namespace Employees
{
	// Employee is the base class in this hierarchy.
	// It serves to hold data & functionality
	// common to all employee types.
	abstract public class Employee : IComparable, ICloneable
	{
		// protected data for kids.
		protected string fullName;
		protected int	 empID;
		protected float  currPay;
		protected string empSSN;

		private static String companyName;

		private readonly String SSNField;

		#region Constructors
		// A static ctor (to assign static field)
		static Employee(){companyName = "Birkbeck, Inc";}

		// Default ctor.
		public Employee(){}

		// If the user calls this ctor, forward to the 4-arg version
		// using arbitrary values...
		public Employee(string fullName) : this(fullName, 3333, 0.0F, ""){}

		// Custom ctor
		public Employee(string FullName, int empID, float currPay, string ssn)
		{
			// Assign internal state data.
			// Note use of 'this' keyword
			// to avoid name clashes.
			this.fullName = FullName;
			this.empID = empID;
			this.currPay = currPay;
			this.empSSN = ssn;
			// Assign read only field.
			SSNField = ssn;
		}
		#endregion

		#region Methods
		// Bump the pay for this emp.
		public virtual void GiveBonus(float amount)
		{currPay += amount;}

		// Show state (could use ToString() as well) IO shouldn't really be in the class but...
		public virtual void DisplayStats()
		{
			Console.WriteLine("Name: {0}", fullName);
			Console.WriteLine("Pay: {0}", currPay);
			Console.WriteLine("ID: {0}", empID);
			Console.WriteLine("SSN: {0}", empSSN);
		}

		// Accessor & mutator for the FirstName.
		public string GetFullName() { return fullName; }
		public void SetFullName(string n)
		{
			// Remove any illegal characters (!,@,#,$,%),
			// check maximum length or case before making assignment.
			fullName = n;
		}

		//static method to get a comparer object
		public static Employee.EmpCompare GetComparer()
		{
			return new Employee.EmpCompare();
		}

		#endregion

		#region properties
		// A static property.
		public static string Company
		{
			get { return companyName; }
			set { companyName = value;}
		}

		// Property for the empID.
		public int EmpID
		{
			get {return empID;}
			set {empID = value;}
		}

		// Property for the currPay.
		public float Pay
		{
			get {return currPay;}
			set {currPay = value;}
		}

		// Another property for ssn.
		public string SSN
		{
			get { return empSSN; }
		}
		#endregion

		#region IComparable Members
		//default implementation to compare names
		public int CompareTo(object obj)
		{
			Employee emp = (Employee)obj;
			return (this.GetFullName()).CompareTo(emp.GetFullName());
		}

		//special implementation to compare ids
		public int CompareTo(string EmpID)
		{
			return (this.EmpID).CompareTo(EmpID);
		}
		#endregion

		#region ICloneable Members
		//implementation of ICloneable to be overriden by derived classes
		public virtual object Clone()
		{
			Object obj = new Object();
			return obj;
		}
		#endregion

		//nested class which implments IComparer
		public class EmpCompare : IComparer
		{
			//get the Employees to compare
			public int Compare(object obj1, object obj2)
			{
				Employee emp1 = (Employee)obj1;
				Employee emp2 = (Employee)obj2;
				return emp1.EmpID.CompareTo(emp2.EmpID);
			}
		}
	}
}



