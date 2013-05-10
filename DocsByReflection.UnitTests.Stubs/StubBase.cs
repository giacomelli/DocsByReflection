namespace DocsByReflection.UnitTests.Stubs
{
	public abstract class StubBase<TKey>
	{
		#region Properties
		/// <summary>
		/// Gets or sets PropertyGenericOnBaseClassWithDoc.
		/// </summary>
		public TKey PropertyGenericOnBaseClassWithDoc { get; set; }

		/// <summary>
		/// Gets or sets PropertyOnBaseClassWithDoc.
		/// </summary>
		public string PropertyOnBaseClassWithDoc { get; set; }
		#endregion
	}
}
