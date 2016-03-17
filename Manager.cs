using System;

namespace Employees
{
	public class Manager : Employee, ICloneable
	{
		private ulong numberOfOptions;
		private BonusInfo bonusInfo;

		#region Constructors
		public Manager(){}

		public Manager(string FullName, int empID, float currPay, string ssn, ulong numbOfOpts)
			: base(FullName, empID, currPay, ssn)
		{
			// This point of data belongs with us!
			numberOfOptions = numbOfOpts;
			bonusInfo = new BonusInfo(this);
		}
		#endregion

		#region Methods and properties
		//use bonusinfo to calculate bonus
		public override void GiveBonus(float amount)
		{
			bonusInfo.Bonus = amount;
			// Increase salary.
			base.GiveBonus(bonusInfo.Bonus);

			// And give some new stock options...
			Random r = new Random();
			numberOfOptions += (ulong)r.Next(500);
		}

		public override void DisplayStats()
		{
			base.DisplayStats();
			Console.WriteLine("Number of stock options: {0}", numberOfOptions);
		}

		public ulong NumbOpts
		{
			get {return numberOfOptions;}
			set { numberOfOptions = value;}
		}
		#endregion

		#region ICloneable Members
		//Overriding ICloneable implementation
		public override object Clone()
		{
			Manager manager = new Manager(this.GetFullName(), this.EmpID, this.Pay, this.SSN, this.NumbOpts);
			return manager;
		}
		#endregion
	}
}



