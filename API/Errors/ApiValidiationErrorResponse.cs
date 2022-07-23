namespace API.Errors
{
    public class ApiValidiationErrorResponse : ApiResponse
    {
        public ApiValidiationErrorResponse() : base(400)
        {
        }
        public IEnumerable<string> Errors { get; set; }
    }
}
