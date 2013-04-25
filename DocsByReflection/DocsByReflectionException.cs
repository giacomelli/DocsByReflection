//Except where stated all code and programs in this project are the copyright of Jim Blackler, 2008.
//jimblackler@gmail.com
//
//This is free software. Libraries and programs are distributed under the terms of the GNU Lesser
//General Public License. Please see the files COPYING and COPYING.LESSER.

using System;
using System.Runtime.Serialization;

namespace DocsByReflection
{
    /// <summary>
    /// An exception thrown by the DocsByReflection library
    /// </summary>
   	[Serializable]
    public class DocsByReflectionException : Exception
	{
		#region Constructors
		/// <summary>
		/// Initializes a new <see cref="DocsByReflectionException"/> class.
		/// </summary>
		public DocsByReflectionException()
		{
		}

		/// <summary>
		/// Initializes a new <see cref="DocsByReflectionException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		public DocsByReflectionException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new <see cref="DocsByReflectionException"/> class.
		/// </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
		protected DocsByReflectionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}


		/// <summary>
        /// Initializes a new exception instance with the specified
        /// error message and a reference to the inner exception that is the cause of
        /// this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or null if none.</param>
        public DocsByReflectionException(string message, Exception innerException)
            : base(message, innerException)
        {

		}
		#endregion
	}
}
