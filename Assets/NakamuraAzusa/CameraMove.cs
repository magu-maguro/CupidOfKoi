using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 0.1f;

    [SerializeField] private Transform _cameraTransform;

    private float _xRotation;
    private bool _isHolding = false;
    private Vector2 _moveVector;

    private void Update()
    {
        UpdateCameraMove();
    }

    private void UpdateCameraMove()
    {
        if (_isHolding && _moveVector != Vector2.zero)
        {
            float mouseX = _moveVector.x * _mouseSensitivity * -1;
            float mouseY = _moveVector.y * _mouseSensitivity;

            if (_cameraTransform != null)
            {
                _xRotation -= mouseY;
                _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
                _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            }

            transform.Rotate(Vector3.up * mouseX);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _moveVector = context.ReadValue<Vector2>();
    }

    public void OnHold(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isHolding = true;
        }
        else if (context.canceled)
        {
            _isHolding = false;
            _moveVector = Vector2.zero;
        }
    }
}
