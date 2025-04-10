using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 GetPosition();
        void OnClickDown(InputAction.CallbackContext context);
        void OnClickUp(InputAction.CallbackContext context);
        void IsEnabledInputs(bool value);
        event Action ClickDown;
        event Action ClickUp;
    }
}