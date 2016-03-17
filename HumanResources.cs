using System;

namespace Employees
{
	public class HumanResources : Employee, ICloneable
	{
		private int contacts;
		private BonusInfo bonusInfo;

		#region Constructors
		//Default constructor
		public HumanResources(){}
		
		//Custom constructor
		public HumanResources(string FullName, int empID, float currPay, string ssn, int cntcts)
			: base(FullName, empID, currPay, ssn)
		{
			contacts = cntcts;
			bonusInfo = new BonusInfo(this);
		}
		#endregion

		#region Methods and properties
		//use bonusinfo to calculate bonus
		public override void GiveBonus(float amount)
		{
			bonusInfo.Bonus = amount;
			base.GiveBonus(contacts*bonusInfo.Bonus);
		}

		public override void DisplayStats()
		{
			base.DisplayStats();
			Console.WriteLine("Number of Contacts: {0}", contacts);
		}

		public int Contacts
		{
			get {return contacts;}
			set {contacts = value;}
		}
		#endregion

		#region ICloneable Members
		//Overriding ICloneable implementation
		public override object Clone()
		{
			HumanResources humanResources = new HumanResources(this.GetFullName(), this.EmpID, this.Pay, this.SSN, this.Contacts);
			return humanResources;
		}
		#endregion
	}
}



