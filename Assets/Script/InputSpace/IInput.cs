using System;

namespace Script.InputSpace
{
    public interface IInput
    {
        event Action<float> OnLeftEvent;
        event Action<float> OnRightEvent;
        event Action<float> OnUpEvent;
        event Action<float> OnDownEvent;
        event Action OnSwitchEvent;
    }
}
