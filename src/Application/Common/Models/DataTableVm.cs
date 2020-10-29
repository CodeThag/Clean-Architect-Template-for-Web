using System.Collections.Generic;

namespace Application.Common.Models
{
    public class DataTableVm<T> : PageInfoModel where T : class
    {
        public IEnumerable<T> data { get; set; }
    }
}
