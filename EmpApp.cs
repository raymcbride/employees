using System;
using System.IO;
using System.Collections;

namespace Employees
{
	#region TheMachine Helper class
	internal class TheMachine
	{
		// fire everyone >:-)
		public static void FireThisPerson(Employee e)
		{
			// Figure out which type I have using 'is'.
			if( e is SalesPerson)
			{
				Console.WriteLine("-> Lost a sales person named {0}", e.GetFullName());
				Console.WriteLine("-> {0} made {1} sales...", e.GetFullName(), 
((SalesPerson)e).NumbSales);
			}
			if( e is Manager)
			{
				Console.WriteLine("-> Lost a suit named {0}", e.GetFullName());
				Console.WriteLine("-> {0} had {1} stock options...", e.GetFullName(), 
((Manager)e).NumbOpts);
			}
		}
	}
	#endregion

	public class EmpApp
	{
		//private Company employees;
		//private FileInfo payFile;
		//private float payrollTotal;

		//public EmpApp()
		//{
			//employees = new Company();
			//payFile  = new FileInfo("pay.txt");
			//payrollTotal = 0;
		//}

		//public void Add(Employee emp)
		//{
		//	employees.AddEmployee(emp);
		//}

		//public void DeleteEmployeeType(Type T)
		//{
		//	for(int i = 0; i < employees.Count; i++)
		//	{
		//		if(T == ((employees[i]).GetType()))
		//			employees.RemoveEmployee(((Employee)employees[i]).EmpID);
		//	}
		//}

		//public void Display(Employee emp)
		//{
		//	emp.DisplayStats();
		//	Console.WriteLine();
		//}

		//public void ComputePayroll()
		//{
		//	StreamWriter writer = new StreamWriter("pay.txt", true);
		//	foreach(Employee emp in employees)
		//	{
		//		if (emp is Manager)
		//			emp.GiveBonus(1000);
		//		if (emp is SalesPerson)
		//		{
		//			if (emp is PTSalesPerson)
		//				emp.GiveBonus(50);
		//			else emp.GiveBonus(100);
		//		}
		//		if (emp is HumanResources)
		//			emp.GiveBonus(10);
		//		if (emp is Consultant)
		//			emp.GiveBonus(100);
		//		payrollTotal += emp.Pay;
		//		writer.WriteLine("Name: " + emp.GetFullName() + " Pay: " + emp.Pay + "\n");
		//	}
		//	writer.Close();
		//	Console.WriteLine("The total Payroll is: " + payrollTotal);
		//}


		public static void Main(string[] args)
		{
			#region Uncomment to test abstract base class.
			// Employee e = new Employee();

			// Uncomment to test readonly field.
			// Employee brenner = new Employee();
			// brenner.SSNField = "666-66-6666";
			#endregion

			//Console.WriteLine("***** These folks work at {0} *****", Employee.Company);

			//Manager chucky = new Manager("Chucky", 92, 100000, "333-23-2322", 9000);
			//chucky.GiveBonus(300);
			//chucky.DisplayStats();
			//Console.WriteLine();

			//SalesPerson fran = new SalesPerson("Fran", 93, 30000, "932-32-3232", 31);
			//fran.GiveBonus(200);
			//fran.DisplayStats();
			//Console.WriteLine();

			//#region Casting examples
			//Console.WriteLine("***** Casting examples *****");
			//// A Manager 'is-a' object.
			//object frank = new Manager("Frank Zappa", 9, 40000, "111-11-1111", 5);

			//// A Manager 'is-a' Employee too.
			//Employee moonUnit = new Manager("MoonUnit Zappa", 2, 20000, "101-11-1321", 1);

			//// A PTSales dude(tte) is a Sales dude(tte)
			//SalesPerson jill = new PTSalesPerson("Jill", 834, 100000, "111-12-1119", 90);

			//// Cast from the generic System.Object into a strongly typed Manager.
			//Console.WriteLine("***** Counting frank's stock options *****");
			//Manager mgr = (Manager)frank;
			//Console.WriteLine("Frank's options: {0}", mgr.NumbOpts);
			//Console.WriteLine("Frank's options: {0}", ((Manager)frank).NumbOpts);

			//Console.WriteLine("\n***** Firing employees using casting operations! *****");
			//TheMachine.FireThisPerson( moonUnit);
			//Console.WriteLine();
			//TheMachine.FireThisPerson( jill);
			//Console.WriteLine();

			//// Error!  Must cast when moving from base to derived!
			//// TheMachine.FireThisPerson(frank);

			//TheMachine.FireThisPerson((Employee)frank);
			//Console.WriteLine();
			//#endregion

			//Console.ReadLine();
			Company company = new Company();
			Consultant Guiness = new Consultant("Arthur Guiness", 1234, 60000, "344-12-0986", 25);
			Manager Smith = new Manager("John Smith", 1346, 100000, "876-43-9000", 12345);
			SalesPerson Artois = new SalesPerson("Stella Artois", 2345, 30000, "677-54-6575", 12);
			PTSalesPerson Bull = new PTSalesPerson("John Bull", 2354, 25000, "546-65-6574", 6);
			HumanResources Adnams = new HumanResources("John Adnams", 2566, 40000, "213-43-2431", 5);
			Consultant Scrumpy = new Consultant("Jack Scrumpy", 1236, 60000, "347-54-6767", 20);
			company.AddEmployee(Guiness);
			company.AddEmployee(Smith);
			company.AddEmployee(Artois);
			company.AddEmployee(Bull);
			company.AddEmployee(Adnams);
			company.Display(Artois);
			company.AddEmployee(Scrumpy);
			Console.WriteLine();
			Type T = typeof(Employees.SalesPerson);
			company.DeleteEmployeeType(T);
			Console.WriteLine("SalesPeople deleted");
			company.Display(Guiness);
			company.Display(Artois);
			company.AddEmployee(Artois);
			Console.WriteLine("Stella added");
			company.Display(Artois);
			Console.WriteLine();
			company.Display(company.WithID(1234));
			Console.WriteLine();
			company.Display(company.FindName("John"));
			company.CalculatePayroll(500, 4500, 1500, 500, 500);
			//company.DisplayPayroll();
			company.NameSort();
			foreach(Employee emp in company)
			{
				emp.DisplayStats();
				Console.WriteLine();
			}
			company.EmpIDSort();
			company.RemoveEmployee(1236);
			Console.WriteLine("Jack Scrumpy removed");
			company.Display(Scrumpy);
			foreach(Employee emp in company)
			{
				emp.DisplayStats();
				Console.WriteLine();
			}
			Company comp = new Company();
			comp = company;
			HumanResources hr = new HumanResources();
			hr = (HumanResources)Adnams.Clone();
			Consultant e = new Consultant();
			e = (Consultant)Guiness.Clone();
			hr.DisplayStats();
			e.DisplayStats();
			Console.WriteLine();
			Consultant c = new Consultant();
			c = (Consultant)company["Arthur Guiness"];
			c.DisplayStats();
			Console.WriteLine();
			IMyInterface imy = company as IMyInterface;
			company.Display((Employee)imy[2354]);
			company.Display(company.WithID(1));
			company.Display(company.FindName("lkjhkj"));
			company.Display((Employee)company["dghfg"]);
			company++;
			Console.WriteLine();
			PTSalesPerson pt;
			pt = (PTSalesPerson)Bull.Clone();
			pt.DisplayStats();
		}
	}
}



