namespace DemoAPIInterview.Contracts.Response
{
    public class ApiResponse
    {
        public int Status { get; set; }
        public bool Ok { get; set; }
        public dynamic Data { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
