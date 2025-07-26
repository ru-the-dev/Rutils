using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Rutils.Utility;


public class AsyncEventDelegate<TEventArgs> where TEventArgs : EventArgs
{
    // Type alias inside the class
    private readonly List<Func<object?, TEventArgs, Task>> _handlers = new();
    private readonly object _lock = new();

    public Action<AsyncEventDelegate<TEventArgs>, Func<object?, TEventArgs, Task>>? OnHandlerSubscribed;
    public Action<AsyncEventDelegate<TEventArgs>, Func<object?, TEventArgs, Task>>? OnHandlerUnsubscribed;

    public int HandlerCount
    {
        get
        {
            lock (_lock)
            {
                return _handlers.Count;
            }
        }
    }

    public void Subscribe(Func<object?, TEventArgs, Task> handler)
    {
        if (handler == null) throw new ArgumentNullException(nameof(handler));

        lock (_lock)
        {
            _handlers.Add(handler);
        }

        if (OnHandlerSubscribed != null)
        {
            OnHandlerSubscribed(this, handler);
        }
    }

    public void Unsubscribe(Func<object?, TEventArgs, Task> handler)
    {
        if (handler == null) return;

        lock (_lock)
        {
            _handlers.Remove(handler);
        }
        
        if (OnHandlerUnsubscribed != null)
        {
            OnHandlerUnsubscribed(this, handler);
        }
    }

    public void Clear()
    {
        lock (_lock)
        {
            _handlers.Clear();
        }
    }

    /// <summary>
    /// Invokes all handlers sequentially, the task completes when all tasks have completed
    /// </summary>
    public async Task InvokeSequentialAsync(object? sender, TEventArgs e)
    {
        var handlersToInvoke = GetHandlersCopy();
        
        foreach (var handler in handlersToInvoke)
        {
            // Start each handler and wait
            await handler(sender, e);
        }
    }

    /// <summary>
    /// Invokes all handlers concurrently and the task completes when all invoked handlers are finished running
    /// </summary>
    public async Task InvokeConcurrentAsync(object? sender, TEventArgs e)
    {
        var handlersToInvoke = GetHandlersCopy();
        
        if (handlersToInvoke.Length == 0) return;

        // Start all handlers concurrently
        var tasks = handlersToInvoke.Select(handler => handler(sender, e));
        
        try
        {
            await Task.WhenAll(tasks);
        }
        catch (Exception ex)
        {
            // Task.WhenAll wraps exceptions in AggregateException
            throw new AggregateException("One or more event handlers failed", ex);
        }
    }

    private Func<object?, TEventArgs, Task>[] GetHandlersCopy()
    {
        lock (_lock)
        {
            return _handlers.ToArray();
        }
    }

    // Operator overloads for convenience
    public static AsyncEventDelegate<TEventArgs> operator +(AsyncEventDelegate<TEventArgs> eventDelegate, Func<object?, TEventArgs, Task> handler)
    {
        eventDelegate.Subscribe(handler);
        return eventDelegate;
    }

    public static AsyncEventDelegate<TEventArgs> operator -(AsyncEventDelegate<TEventArgs> eventDelegate, Func<object?, TEventArgs, Task> handler)
    {
        eventDelegate.Unsubscribe(handler);
        return eventDelegate;
    }
}