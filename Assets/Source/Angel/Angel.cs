using System;
using Pathfinding;
using UnityEngine;
using UnityEngine.InputSystem;

public class Angel : MonoBehaviour
{
    private readonly string IsWalking = "IsWalking";
    private readonly string IsBackside = "IsBackside";

    [SerializeField] private AIPath _aiPath;
    [SerializeField] private Outline _outline;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private bool _isMoving;

    public event Action<Angel> Selected;

    private void OnEnable()
    {
        if (_aiPath == null)
        {
            throw new ArgumentNullException(nameof(_aiPath));
        }

        if (_outline == null)
        {
            throw new ArgumentNullException(nameof(_outline));
        }

        if (_animator == null)
        {
            throw new ArgumentNullException(nameof(_animator));
        }

        if (_spriteRenderer == null)
        {
            throw new ArgumentNullException(nameof(_spriteRenderer));
        }
    }

    private void Update()
    {
        if (_isMoving == true && _aiPath.reachedDestination == false)
        {
            LookOnTarget();
        }
        else if (_isMoving == true && _aiPath.reachedDestination == true)
        {
            _isMoving = false;
            _animator.SetBool(IsWalking, false);
        }
    }

    public void MoveTo(Vector3 position)
    {
        _outline.UpdateOutline(false);
        _aiPath.destination = position;
        _isMoving = true;
        _animator.SetBool(IsWalking, true);
        LookOnTarget();
    }

    private void OnMouseDown()
    {
        if (Mouse.current.leftButton.IsPressed())
        {
            _outline.UpdateOutline(true);
            Selected?.Invoke(this);
        }
    }

    private void LookOnTarget()
    {
        Vector2 direction = (_aiPath.steeringTarget - transform.position).normalized;
        _animator.SetBool(IsBackside, direction.y > 0.1f);
        _spriteRenderer.flipX = direction.x < 0f;
    }
}