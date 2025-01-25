using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class PlayerInput: IDisposable
    {
        public event Action ClickDown;
        public event Action ClickUp;
        
        private Inputs _inputs;
        private InputAction _position;
        private InputAction _click;
        
        public PlayerInput()
        {
            _inputs = new Inputs();
            _inputs.Enable();
            _inputs.Player.Click.performed += OnClickDown;
            _inputs.Player.Click.canceled += OnClickUp;
        }
        
        public void DisableInputs() =>  _inputs.Disable();

        public void Dispose()
        {
            _inputs?.Dispose();
            if (_inputs == null) return;
            _inputs.Player.Click.performed -= OnClickDown;
            _inputs.Player.Click.canceled -= OnClickUp;
        }

        public Vector2 GetPosition() => _inputs.Player.Position.ReadValue<Vector2>();

        private void OnClickDown(InputAction.CallbackContext context) => ClickDown?.Invoke();
        private void OnClickUp(InputAction.CallbackContext context) => ClickUp?.Invoke();
    }
}