using Application.Common.Interfaces;

namespace Application.Common.Models
{
    public class ApiSearchModel : IPayLoadObject
    {
        public string SearchText { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string Id { get; set; }

    }
}
