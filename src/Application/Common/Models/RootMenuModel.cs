using System.Collections.Generic;

namespace Application.Common.Models
{
    public class RootMenuModel
    {
        public RootMenuModel()
        {
            Children = new List<RootMenuModel>();
        }

        public string Id { get; set; }
        public string Text { get; set; }
        public List<RootMenuModel> Children { get; set; }
        public string Type { get; set; }
        public StateModel State { get; set; }
        public string Icon { get; set; }
    }
}
