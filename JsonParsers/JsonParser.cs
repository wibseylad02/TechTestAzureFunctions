namespace TechTestAzureFunctions.JsonParsers
{
    /// <summary>
    /// Class to extract data values from JSON input strings
    /// </summary>
    public class JsonParser
    {
        private readonly string _jsonInput;


        public JsonParser(string jsonInput)
        {
            _jsonInput = jsonInput;
        }

        /// <summary>
        /// Gets the CustomerId value from the JSON input string.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException">if input string is null or empty or whitespace, or if it is too long or short.</exception>
        public string GetCustomerId()
        {
            if (string.IsNullOrWhiteSpace(_jsonInput))
            {
                throw new ArgumentException("JSON input string cannot be null or empty or whitespace", nameof(_jsonInput));
            }

            const string customerIdKey = "CustomerId";
            const int customerIdLength = 24; // Length of the CustomerId value without quotes and leading space.

            var customerIdIndex = _jsonInput.IndexOf(customerIdKey, StringComparison.OrdinalIgnoreCase);

            if (customerIdIndex > 0)
            {
                var startIndex = _jsonInput.IndexOf(':', customerIdIndex) + 1;

                // Trim any whitespace or quotes around the value.
                // The CustomerId value is 24 characters long,  but includes quotes and a leading space, which will be trimmed out.
                var customerIdValue = _jsonInput.Substring(startIndex, customerIdLength + 3).Trim().Trim('"');

                // DEVELOPER NOTE:
                // A CustomerId value of > 24 characters will not be stored in the database.
                // ASSUMPTION - If the value is too short, also throw an exception.
                if (customerIdValue.Length > customerIdLength)
                    throw new ArgumentException($"CustomerId value is too long. Expected {customerIdLength} characters, but got {customerIdValue.Length} characters.", nameof(_jsonInput));

                if (customerIdValue.Length < customerIdLength)
                    throw new ArgumentException($"CustomerId value is too short. Expected {customerIdLength} characters, but got {customerIdValue.Length} characters.", nameof(_jsonInput));

                return customerIdValue;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the Property value from the JSON input string.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException">if input string is null or empty or whitespace.</exception>
        public string GetProperty()
        {
            if (string.IsNullOrWhiteSpace(_jsonInput))
            {
                throw new ArgumentException("JSON input string cannot be null or empty or whitespace", nameof(_jsonInput));
            }

            const string propertyKey = "Property";

            var propertyIndex = _jsonInput.IndexOf(propertyKey, StringComparison.OrdinalIgnoreCase);

            if (propertyIndex > 0)
            {
                // takes us to the first " of the Property value, allowing for a leading space.
                var startIndex = _jsonInput.IndexOf(':', propertyIndex) + 2; 

                var terminalString = _jsonInput.Substring(startIndex);
                terminalString = terminalString.Trim('"');
                terminalString = terminalString.Trim('\\');

                // takes us from immediately after the first " to the second " of the Property value, then back one character to exclude the trailing quote.
                var endIndex = terminalString.IndexOf('"', StringComparison.OrdinalIgnoreCase);

                if (endIndex <= 0)
                {
                    // If we can't find the end index (e.g. because the Property value was []), return an empty string.
                    return string.Empty;
                }

                // Trim any whitespace or quotes around the value.
                // The property value is variable in length up to 30 characters long,  but includes quotes and a leading space.
                var propertyValue = terminalString.Substring(0, endIndex).Trim().Trim('"');
                
                return propertyValue;
            }

            return string.Empty;
        }
    }
}
