using System;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private readonly string AttackTrigger = "Attack";

    [SerializeField] private float atackDelay;
    [SerializeField] private float detectionRadius;
    [SerializeField] private Animator _animator;
    [SerializeField] private Angel _angel;

    private List<Tank> targets = new List<Tank>();
    private Tank currentTarget;
    private float timer;

    private void OnEnable()
    {
        if (_animator == null)
        {
            throw new ArgumentNullException(nameof(_animator));
        }

        if (_angel == null)
        {
            throw new ArgumentNullException(nameof(_angel));
        }
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        targets.Clear(); // Очищаем список перед обновлением

        foreach (Collider2D collider in colliders)
        {
            Tank tank = collider.GetComponent<Tank>();

            if (tank != null && !targets.Contains(tank))
            {
                targets.Add(tank);
            }
        }

        if (targets.Count > 0)
        {
            currentTarget = targets[0];
        }
        else
        {
            currentTarget = null;
        }

        if (currentTarget != null)
        {
            RotateTo(currentTarget.transform);
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = atackDelay;
                _animator.SetTrigger(AttackTrigger);
            }
        }
    }

    private void RotateTo(Transform tank)
    {
        Vector2 direction = (tank.position - transform.position).normalized;
        _angel.Flip(direction);
    }
}