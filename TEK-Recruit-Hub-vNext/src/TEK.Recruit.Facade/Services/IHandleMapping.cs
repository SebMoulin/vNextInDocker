namespace TEK.Recruit.Facade.Services
{
    public interface IHandleMapping
    {
        TResult Map<TSource, TResult>(TSource source);
    }
}
