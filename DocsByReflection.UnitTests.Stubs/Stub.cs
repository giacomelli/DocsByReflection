using System.Collections.Generic;

namespace DocsByReflection.UnitTests.Stubs
{
	/// <summary>
	/// Stub class.
	/// </summary>
	public class Stub : StubBase<int, string>
	{
		#region Properties
		/// <summary>
		/// Gets or sets PropertyWithDoc.
		/// </summary>
		public string PropertyWithDoc { get; set; }

		public string PropertyWithoutDoc { get; set; }
		#endregion

		#region Methods
		/// <summary>
		/// MethodWithGenericParameter method.
		/// </summary>
		/// <param name="p">Generic parameter.</param>
		public void MethodWithGenericParameter(List<string> p)
		{
		}

		public void MethodWithoutDoc(string value)
		{
		}
		#endregion
	}
}
