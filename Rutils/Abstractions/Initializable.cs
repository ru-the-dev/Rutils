using System;

namespace Rutils.Abstractions;

public abstract class Initializable
{
    private bool _isInitialized;

    public bool IsInitialized => _isInitialized;

    public async Task InitializeAsync()
    {
        if (_isInitialized)
            return;

        await OnInitializeAsync();

        _isInitialized = true;
    }

    protected abstract Task OnInitializeAsync();
}
