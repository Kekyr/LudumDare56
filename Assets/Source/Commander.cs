using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Commander : MonoBehaviour
{
    [SerializeField] private Angel _angel;
    [SerializeField] private Transform _target;
    [SerializeField] private Camera _camera;

    private Angel _selectedAngel;

    private void OnEnable()
    {
        if (_angel == null)
        {
            throw new ArgumentNullException(nameof(_angel));
        }

        if (_target == null)
        {
            throw new ArgumentNullException(nameof(_target));
        }

        if (_camera == null)
        {
            throw new ArgumentNullException(nameof(_camera));
        }

        _angel.Selected += OnSelected;
    }

    private void OnDisable()
    {
        _angel.Selected -= OnSelected;
    }

    private void Update()
    {
        if (_selectedAngel != null && Mouse.current.rightButton.IsPressed())
        {
            Debug.Log("Moving to position!");
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            Debug.Log($"MousePosition: X:{mousePosition.x} Y:{mousePosition.y} Z:{mousePosition.z}");
            mousePosition.z = _camera.nearClipPlane;
            Debug.Log($"MousePosition: X:{mousePosition.x} Y:{mousePosition.y} Z:{mousePosition.z}");
            Vector3 movePosition = _camera.ScreenToWorldPoint(mousePosition);
            Debug.Log($"MovePosition: {movePosition.x} {movePosition.y}");
            _target.position = movePosition;
            _selectedAngel.MoveTo(movePosition);
            _selectedAngel = null;
        }
    }

    private void OnSelected(Angel angel)
    {
        _selectedAngel = angel;
    }
}