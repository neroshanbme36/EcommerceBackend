using System.Collections.Generic;

namespace Api.Errors
{
    // System Exception Model
    public class ApiValidationErrorResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; } = new List<string>();

        public ApiValidationErrorResponse() : base(400) { }
    }
}
