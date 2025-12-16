using System;
using R3;

public interface IInputProvider
{
    // 押された瞬間のイベントを通知
    IObservable<Unit> OnMoveLeft { get; }
    IObservable<Unit> OnMoveRight { get; }
    IObservable<Unit> OnSoftDrop { get; }
    IObservable<Unit> OnHardDrop { get; }
    IObservable<Unit> OnRotateClockwise { get; }
    IObservable<Unit> OnRotateCounterClockwise { get; }
    IObservable<Unit> OnHold { get; }
}