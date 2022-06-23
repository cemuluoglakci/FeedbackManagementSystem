namespace ApplicationFMS.Interfaces
{
    public interface IPostSearchQuery : ISearchQuery
    {
        bool? IsChecked { get; set; }
    }
}
