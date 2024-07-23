namespace rsr.Max.Data.Searches
{
    public interface ISearchBuilder<T>
    {
        IQueryable<T> Build(IQueryable<T> entities);
    } 
}
