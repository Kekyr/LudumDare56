using System;
using Pathfinding;
using UnityEngine;
using UnityEngine.InputSystem;

public class Angel : MonoBehaviour
{
    [SerializeField] private AIPath _aiPath;
    [SerializeField] private Transform _target;

    public event Action<Angel> Selected;

    private void OnEnable()
    {
        if (_aiPath == null)
        {
            throw new ArgumentNullException(nameof(_aiPath));
        }

        if (_target == null)
        {
            throw new ArgumentNullException(nameof(_target));
        }
    }

    public void MoveTo(Vector3 position)
    {
        _aiPath.destination = _target.position;
    }

    private void OnMouseDown()
    {
        if (Mouse.current.leftButton.IsPressed())
        {
            Selected?.Invoke(this);
            Debug.Log("Selected!");
        }
    }
}