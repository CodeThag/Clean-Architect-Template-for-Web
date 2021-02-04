using Application.Common.Attributes;

namespace Application.Common.Enums
{
    public enum Permission
    {
        [StringValue("MenuCollection.List")]
        MenuCollectionList,
        [StringValue("MenuCollection.Create")]
        MenuCollectionCreate,
        [StringValue("MenuCollection.Edit")]
        MenuCollectionEdit,
        [StringValue("MenuCollection.Delete")]
        MenuCollectionDelete    
    }
}
