using System;

namespace Employees
{
	public class SalesPerson : Employee, ICloneable
	{
		protected int numberOfSales;
		private BonusInfo bonusInfo;

		#region Constructors
		public SalesPerson(){}

		public SalesPerson(string FullName, int empID, float currPay, string ssn, int numbOfSales)
			: base(FullName, empID, currPay, ssn)
		{
			numberOfSales = numbOfSales;
			bonusInfo = new BonusInfo(this);
		}
		#endregion

		#region Properties and methods
		public int NumbSales
		{
			get {return numberOfSales;}
			set { numberOfSales = value;}
		}
		
		//use bonusinfo to calculate bonus
		public override void GiveBonus(float amount)
		{
			bonusInfo.Bonus = amount;
			int salesBonus = 0;
			if(numberOfSales >= 0 && numberOfSales <= 100)
				salesBonus = 10;
			else if(numberOfSales >= 101 && numberOfSales <= 200)
				salesBonus = 15;
			else
				salesBonus = 20;	// Anything greater than 200.
			base.GiveBonus(bonusInfo.Bonus * salesBonus);
		}

		public override void DisplayStats()
		{
			base.DisplayStats();
			Console.WriteLine("Number of sales: {0}", numberOfSales);
		}
		#endregion

		#region ICloneable Members
		//Overriding ICloneable implementation
		public override object Clone()
		{
			SalesPerson salesPerson = new SalesPerson(this.GetFullName(), this.EmpID, this.Pay, this.SSN, this.NumbSales);
			return salesPerson;
		}
		#endregion
	}
}



