using Domain.Errors;
using Domain.Primitives;
using Domain.Shared;

namespace Domain.ValueObjects;

public sealed class CacheExpiration : ValueObject
{
    #region Constructors

    private CacheExpiration(TimeSpan absoluteExpiration, TimeSpan? slidingExpiration)
    {
        AbsoluteExpiration = absoluteExpiration;
        SlidingExpiration = slidingExpiration;
    }

    #endregion

    #region Properties

    public TimeSpan AbsoluteExpiration { get; }
    public TimeSpan? SlidingExpiration { get; }

    #endregion

    #region Factory Methods

    public static Result<CacheExpiration> Create(
        TimeSpan absoluteExpiration,
        TimeSpan? slidingExpiration = null)
    {
        if (absoluteExpiration <= TimeSpan.Zero)
            return Result.Failure<CacheExpiration>(
                DomainErrors.Cache.InvalidExpiration);

        return new CacheExpiration(absoluteExpiration, slidingExpiration);
    }

    #endregion

    #region Overrides

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return AbsoluteExpiration;
        yield return SlidingExpiration!;
    }

    #endregion
}