namespace MyNotepad.Domain.Exceptions
{
    public class InvalidFieldException : Exception
    {
        public string FieldValue { get; set; }
        public string FieldName { get; set; }

        public InvalidFieldException(string message, string fieldName, string fieldValue) : base(message)
        {
            FieldName = fieldName;
            FieldValue = fieldValue;
        }
    }
}
