namespace SkiServiceAPI.Common
{

    /// <summary>
    /// Thought about using this as validation result for the services
    /// maybe implement it later
    /// 
    /// just sitting here for now
    /// </summary>
    public class ValidationResult
    {

        public bool IsValid { get; set; }

        public string Property { get; set; }

        public string Message { get; set; }

        public object? AdditionalDetails { get; set; }

        public static ValidationResult Error(string property, string message, object additionalDetails)
        {
            return new ValidationResult { 
                IsValid = false,
                Property = property,
                Message = message,
                AdditionalDetails = additionalDetails
            };
        }

        public static ValidationResult Success()
        {
            return new ValidationResult
            {
                IsValid = true,
                Property = string.Empty,
                Message = string.Empty,
                AdditionalDetails = null
            };
        }

    }
}
