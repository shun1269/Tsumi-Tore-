using System;

public interface IInputProvider
{
    IObservable<Unit> OnMoveLeft { get; }
    IObservable<Unit> OnMoveRight { get; }
    IObservable<Unit>
}