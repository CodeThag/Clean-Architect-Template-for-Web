using System;

namespace Application.Common.Models
{
    public class PagingInfo
    {
        public int FirstItem { get; set; }

        public int LastItem
        {
            get
            {
                var result = (CurrentPage * this.ItemsPerPage);

                if (TotalItems < result)
                {
                    result = TotalItems;
                }
                return result;
            }

        }

        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}
