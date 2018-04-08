using System.Collections.Generic;

namespace DocsByReflection.UnitTests.Stubs
{
	/// <summary>
	/// Stub class.
	/// </summary>
	public class Stub : StubBase<int, string>
	{
        #region Fields
        /// <summary>
        /// Gets or sets FieldWithDoc.
        /// </summary>
        private string FieldWithDoc;

        private string FieldWithoutDoc;
        #endregion 

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

        /// <summary>
        /// MethodWithCollectionReturnType method.
        /// </summary>
        /// <returns>A new <see cref="Dictionary{TKey, TValue}"/>.</returns>
        public Dictionary<string, List<string>> MethodWithCollectionReturnType()
        {
            return new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// MethodWithGenericType method.
        /// </summary>
        /// <typeparam name="T">The generic type parameter.</typeparam>
        /// <param name="parameter">The value of the generic type parameter.</param>
        /// <returns>The <see cref="{T}"/> parameter supplied to the method.</returns>
        public T MethodWithGenericType<T>(T parameter)
        {
            return parameter;
        }

        /// <summary>
        /// MethodWithGenericCollectionType method.
        /// </summary>
        /// <typeparam name="T">The generic type parameter.</typeparam>
        /// <param name="parameter">The value of the generic type parameter.</param>
        /// <returns>The <see cref="{T}"/> parameter supplied to the method.</returns>
        public List<T> MethodWithGenericCollectionType<T>(List<T> parameter)
        {
            return parameter;
        }

        /// <summary>
        /// MethodWithOutParameter method.
        /// </summary>
        /// <param name="parameter">MethodWithOutParameter parameter.</param>
        public void MethodWithOutParameter(out bool parameter)
        {
            parameter = false;
        }

        /// <summary>
        /// MethodWithCollectionOutParameter method.
        /// </summary>
        /// <param name="parameter">MethodWithCollectionOutParameter parameter.</param>
        public void MethodWithCollectionOutParameter(out Dictionary<string, List<string>> parameter)
        {
            parameter = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// MethodWithCollectionOutGenericTypeParameter method.
        /// </summary>
        /// <param name="parameter">MethodWithCollectionOutGenericTypeParameter parameter.</param>
        public void MethodWithCollectionOutGenericTypeParameter<T>(out Dictionary<T, List<T>> parameter)
        {
            parameter = new Dictionary<T, List<T>>();
        }

		public void MethodWithoutDoc(string value)
		{
		}
        /// <summary>
        /// MethodWithComplexGenericParameter method.
        /// </summary>
        /// <param name="p">Generic dictionary parameter.</param>
        public void MethodWithComplexGenericParameter(Dictionary<string, string> p)
        {

        }

        /// <summary>
        /// MethodWithCollectionOfInnerClass method.
        /// </summary>
        /// <param name="p">Array of nested class parameter.</param>
        public void MethodWithCollectionOfInnerClass(InnerClass[] p)
        {

        }

        /// <summary>
        /// A nested class
        /// </summary>
        public class InnerClass
        { }
		#endregion
	}
}
