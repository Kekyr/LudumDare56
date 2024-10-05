using System;
using Pathfinding;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private AIPath _ai;

    private void OnEnable()
    {
        if (_target == null)
        {
            throw new ArgumentNullException(nameof(_target));
        }

        if (_ai == null)
        {
            throw new ArgumentNullException(nameof(_ai));
        }
    }

    private void Update()
    {
        //_ai.destination = _target.position;
    }
}