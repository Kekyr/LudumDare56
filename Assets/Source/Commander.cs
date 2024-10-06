using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Commander : MonoBehaviour
{
    [SerializeField] private Angel[] _angels;
    [SerializeField] private Camera _camera;

    private Angel _selectedAngel;

    private void OnEnable()
    {
        if (_angels == null)
        {
            throw new ArgumentNullException(nameof(_angels));
        }

        if (_camera == null)
        {
            throw new ArgumentNullException(nameof(_camera));
        }

        foreach (Angel angel in _angels)
        {
            angel.Selected += OnSelected;
        }
    }

    private void OnDisable()
    {
        foreach (Angel angel in _angels)
        {
            angel.Selected -= OnSelected;
        }
    }

    private void Update()
    {
        if (_selectedAngel != null && Mouse.current.rightButton.IsPressed())
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            mousePosition.z = _camera.nearClipPlane;
            Vector3 movePosition = _camera.ScreenToWorldPoint(mousePosition);
            _selectedAngel.MoveTo(movePosition);
            _selectedAngel = null;
        }
    }

    private void OnSelected(Angel angel)
    {
        _selectedAngel = angel;
    }
}