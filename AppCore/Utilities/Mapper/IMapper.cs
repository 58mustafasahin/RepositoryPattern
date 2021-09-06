namespace AppCore.Utilities.Mapper
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);

    }
}
