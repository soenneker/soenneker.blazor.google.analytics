using Microsoft.JSInterop;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.Google.Analytics.Tests;

internal sealed class TestJsObjectReference : IJSObjectReference
{
    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args) => ValueTask.FromResult(default(TValue)!);

    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args) =>
        cancellationToken.IsCancellationRequested ? ValueTask.FromCanceled<TValue>(cancellationToken) : ValueTask.FromResult(default(TValue)!);

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
