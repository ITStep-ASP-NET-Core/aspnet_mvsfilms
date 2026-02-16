using System.ComponentModel.DataAnnotations;

namespace aspnet_mvsfilms.Annotations
{
	public class AllowedExtensionsAttribute : ValidationAttribute
	{
		private readonly string[] _extensions;
		public AllowedExtensionsAttribute ( string[] extensions )
		{
			_extensions = extensions;
		}

		public override bool IsValid ( object value )
		{
			if(value is IFormFile file)
			{
				if(_extensions.Contains(Path.GetExtension(file.FileName).ToLower()))
				{
					return true;
				}
			}
			return false;
		}
		public override string FormatErrorMessage ( string name )
		{
			return base.FormatErrorMessage(string.Join(", ", _extensions).ToString());
		}
	}
}
