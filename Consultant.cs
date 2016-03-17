using System;

namespace Employees
{
	public class Consultant : Employee
	{
		private float overtime;
		private BonusInfo bonusInfo;

		#region Constructors
		//Default constructor
		public Consultant(){}
		
		//Custom constructor
		public Consultant(string FullName, int empID, float currPay, string ssn, float ovTime) 
			: base(FullName, empID, currPay, ssn)
		{
			overtime = ovTime;
			bonusInfo = new BonusInfo(this);
		}
		#endregion

		#region Methods and properties
		//use bonusinfo to calculate bonus
		public override void GiveBonus(float amount)
		{
			bonusInfo.Bonus = amount;
			base.GiveBonus(overtime*bonusInfo.Bonus);
		}

		//Display the details
		public override void DisplayStats()
		{
			base.DisplayStats();
			Console.WriteLine("Overtime worked: {0}", overtime);
		}

		//Property for the overtime
		public float OverTime 
		{
			get {return overtime;}
			set { overtime = value;}
		}
		#endregion

		#region ICloneable Members
		//Overriding ICloneable implementation
		public override object Clone()
		{
			Consultant consultant = new Consultant(this.GetFullName(), this.EmpID, this.Pay, 
				this.SSN, this.OverTime);
			return consultant;
		}
		#endregion
	}
}
