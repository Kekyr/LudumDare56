using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _max;
    private float _current;

    public event Action<float> HealthChanged;
    public event Action Died;

    public float Max => _max;

    private void OnEnable()
    {
        _current = _max;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(damage));
        }

        _current -= damage;
        HealthChanged?.Invoke(_current);

        if (_current <= 0)
        {
            Died?.Invoke();
        }
    }
}
