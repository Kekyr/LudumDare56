using System;
using Pathfinding;
using UnityEngine;
using UnityEngine.InputSystem;

public class Angel : MonoBehaviour
{
    [SerializeField] private AIPath _aiPath;

    public event Action<Angel> Selected;

    private void OnEnable()
    {
        if (_aiPath == null)
        {
            throw new ArgumentNullException(nameof(_aiPath));
        }
    }

    public void MoveTo(Vector3 position)
    {
        _aiPath.destination = position;
    }

    private void OnMouseDown()
    {
        if (Mouse.current.leftButton.IsPressed())
        {
            Selected?.Invoke(this);
        }
    }
}