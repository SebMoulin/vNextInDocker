using AutoMapper;

namespace TEK.Recruit.Facade.Services
{
    public class MappingService : IHandleMapping
    {
        public TResult Map<TSource, TResult>(TSource source)
        {
            return Mapper.Map<TSource, TResult>(source);
        }
    }
}
