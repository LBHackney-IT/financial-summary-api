using System.Collections;
using System.Text;

namespace FinancialSummaryApi.V1.Exceptions.Models
{
    /// <summary>
    /// A collection of ValidationError objects that is used to collect
    /// errors that occur doing calls to the Validate method.
    /// </summary>
    public class ValidationErrorCollection : CollectionBase
    {
        /// <summary>
        /// Adds a new error to the collection
        /// <seealso>Class ValidationError</seealso>
        /// </summary>
        /// <param name="error">Validation Error object</param>
        /// <returns>Void</returns>
        public void Add(ValidationError error)
        {
            this.List.Add((object) error);
        }

        /// <summary>
        /// Adds a new error to the collection
        /// <seealso>Class ValidationErrorCollection</seealso>
        /// </summary>
        /// <param name="message">Message of the error</param>
        /// <param name="fieldName">
        /// optional field name that it applies to (used for DataBinding errors on
        /// controls)
        /// </param>
        /// <param name="id">An optional ID you assign the error</param>
        /// <returns>Void</returns>
        public void Add(string message, string fieldName = "", string id = "")
        {
            this.Add(new ValidationError()
            {
                Message = message,
                ControlId = fieldName,
                Id = id
            });
        }

        /// <summary>
        /// Removes the item specified in the index from the Error collection
        /// </summary>
        /// <param name="index"></param>
        public void Remove(int index)
        {
            if (index <= this.List.Count - 1 && index >= 0)
                return;
            this.List.RemoveAt(index);
        }

        /// <summary>
        /// Returns a string representation of the errors in this collection.
        /// The string is separated by CR LF after each line.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.Count < 1)
                return "";
            StringBuilder stringBuilder = new StringBuilder(128);
            foreach (ValidationError validationError in (CollectionBase) this)
                stringBuilder.AppendLine(validationError.Message);
            return stringBuilder.ToString();
        }
    }
}
