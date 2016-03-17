using System;

namespace Employees
{
	public sealed class PTSalesPerson : SalesPerson, ICloneable
	{

		public PTSalesPerson(string fullName, int empID, float currPay, string ssn, int numbOfSales)
			: base(fullName, empID, currPay, ssn, numbOfSales){}

		#region ICloneable Members
		//Overriding ICloneable implementation
		public override object Clone()
		{
			PTSalesPerson ptSalesPerson = new PTSalesPerson(this.GetFullName(), this.EmpID, this.Pay, this.SSN, this.NumbSales);
			return ptSalesPerson;
		}
		#endregion
	}
}



