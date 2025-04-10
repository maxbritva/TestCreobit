using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input
{
    public class InputService: IDisposable, IInputService
    {
        public event Action ClickDown;
        public event Action ClickUp;
        private Inputs _inputs;
        private InputAction _position;
        private InputAction _click;
        
        public InputService()
        {
            _inputs = new Inputs();
            _inputs.Player.Click.performed += OnClickDown;
            _inputs.Player.Click.canceled += OnClickUp;
        }
        public void Dispose()
        {
            _inputs?.Dispose();
            if (_inputs == null) return;
            _inputs.Player.Click.performed -= OnClickDown;
            _inputs.Player.Click.canceled -= OnClickUp;
        }

        public void IsEnabledInputs(bool value)
        {
            if (value)
                _inputs.Enable();
            else
                _inputs.Disable();
        }

        public Vector2 GetPosition() => _inputs.Player.Position.ReadValue<Vector2>();
        public void OnClickDown(InputAction.CallbackContext context) => ClickDown?.Invoke();
        public void OnClickUp(InputAction.CallbackContext context) => ClickUp?.Invoke();
    }
}