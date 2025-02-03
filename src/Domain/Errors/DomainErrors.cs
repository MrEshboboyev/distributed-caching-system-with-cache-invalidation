using Domain.Shared;

namespace Domain.Errors;

public static class DomainErrors
{
    #region User
    
    #region Entities
    
    public static class User
    {
        public static readonly Error EmailAlreadyInUse = new(
            "User.EmailAlreadyInUse",
            "The specified email is already in use");

        public static readonly Func<Guid, Error> NotFound = id => new Error(
            "User.NotFound",
            $"The user with the identifier {id} was not found.");

        public static readonly Error NotExist = new(
            "Users.NotExist",
            $"There is no users");

        public static readonly Error InvalidCredentials = new(
            "User.InvalidCredentials",
            "The provided credentials are invalid");
    }
    
    public static class Cache
    {
        public static readonly Error InvalidKey = new(
            "Cache.InvalidKey",
            "Cache key is invalid.");
        
        public static readonly Error InvalidExpiration = new(
            "Cache.InvalidExpiration",
            "Cache expiration is invalid.");
        
        public static readonly Error InvalidKeyFormat = new(
            "Cache.InvalidKeyFormat",
            "Cache key must match format [a-z0-9:-].");
        
        public static readonly Error CacheMiss = new(
            "Cache.Miss",
            "The requested item is not in the cache.");
        
        public static readonly Error InvalidationFailed = new(
            "Cache.InvalidationFailed", 
            "Failed to invalidate cache item.");
    }

    #endregion
    
    #region Value Objects

    public static class RateLimit
    {
        public static readonly Func<int, Error> InvalidMaxRequests = maxRequests => new Error(
            "RateLimit.InvalidMaxRequests",
            $"MaxRequests {maxRequests} must be greater than zero.");
        
        public static readonly Func<TimeSpan, Error> InvalidTimeWindow = timeWindow => new Error(
            "RateLimit.InvalidTimeWindow",
            $"TimeWindow {timeWindow} must be greater than zero.");
        
        public static readonly Error Exceeded = new Error(
            "RateLimit.Exceeded",
            "Too many requests. Please try again later.");
    }
    
    #endregion
    
    #endregion
}