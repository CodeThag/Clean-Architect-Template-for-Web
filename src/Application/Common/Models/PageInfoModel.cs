namespace Application.Common.Models
{
    public class PageInfoModel
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public int PageSize { get; set; }
        public string CurrentKeywords { get; set; }
        public int TotalCount { get; set; }
    }
}
