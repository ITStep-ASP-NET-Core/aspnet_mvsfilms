using System.ComponentModel.DataAnnotations;


namespace aspnet_mvsfilms.Annotations
{
	public class FutureDateAttribute : ValidationAttribute
	{
		public override bool IsValid ( object value )
		{
			if(value is DateTime date)
			{
				return date <= DateTime.Now;
			}
			return false;
		}
	}

}
