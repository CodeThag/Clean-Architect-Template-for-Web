namespace Application.Common.Models
{
    public class DataTableListRequestModel
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public string search { get; set; }
        public int sortColumn { get; set; }
        public string sortDirection { get; set; }
    }
}
