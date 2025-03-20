namespace SharedFramework.Extensions;

public static class TaskExtensions
{
    public static T WaitResult<T>(this Task<T> task) => task
        .GetAwaiter()
        .GetResult();
}
