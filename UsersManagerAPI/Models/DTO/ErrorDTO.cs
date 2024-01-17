namespace ClientRegistryAPI.Models.DTO
{
    /// <summary>
    /// ErrorDTO for error responses. Gives an error description.
    /// </summary>
    public class ErrorDTO
    {
        public string FieldName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
