using System;

namespace Employees
{
	public class BonusInfo
	{
		private float bonus;
		private Employee employee;

		#region Constructors
		//custom constructor
		public BonusInfo(Employee emp)
		{
			bonus = 0;
			employee = emp;
		}
		#endregion

		#region Methods and properties
		//reset the bonus
		public void ClearBonuses()
		{
			bonus = 0;
		}

		//Property for the bonus
		public float Bonus
		{
			get
			{
				return bonus;
			}
			set
			{
				try
				{
					//throw an exception if the value is outside the range for that particular type
					if((value < 0)||((employee is Manager)&&(value > 2000))||
						((employee is Consultant)&&(value > 1000.00))||
						((employee is HumanResources)&&(value > 500.00))|| 
						((employee is SalesPerson)&&(((employee is PTSalesPerson)&&
						(value > 1000.00))||(value > 4000.00))))
							throw new BonusRangeException();
					else bonus = value;
				}
				catch  
				{
					Console.WriteLine("\nBonus out of range for employee of {0} type! Bonus not set\n", employee.GetType());
				}
			}
		}
		#endregion
	}
}



