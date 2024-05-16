namespace DistributedSynchronizationLibrary.Interfaces;

/// <summary>
/// Interface for distributed semaphore
/// </summary>
public interface IDistributedSemaphore
{
    /// <summary>
    /// Acquire semaphore async
    /// </summary>
    Task<bool> AcquireAsync(string key, TimeSpan timeout);

    /// <summary>
    /// Release semaphore async
    /// </summary>
    Task Release(string key);
}