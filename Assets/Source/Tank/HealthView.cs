using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Health _health;
    [SerializeField] private float _duration;

    private void OnEnable()
    {
        if (_slider == null)
        {
            throw new ArgumentNullException(nameof(_slider));
        }

        if (_health == null)
        {
            throw new ArgumentNullException(nameof(_health));
        }

        _slider.minValue = 0;
        _slider.maxValue = _health.Max;
        _slider.value = _slider.maxValue;

        _health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float current)
    {
        _slider.DOValue(current, _duration);
    }
}