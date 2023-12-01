using System;

public interface IObservable<T> : IReadValue<T>
{
    event Action<T> OnValueChanged;
}