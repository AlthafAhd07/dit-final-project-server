namespace SkillsInternationalServer.Utilities
{
    public class ApiResponse<T>
    {
        public bool success { get; set; }
        public string? message { get; set; } = "Process completed successfully!";
        public T? data { get; set; }
        public Error? error { get; set; }
    }

    public class Error
    {
        public string message { get; set; }
        public string code { get; set; }
    }

}
