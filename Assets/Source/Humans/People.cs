using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class People : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _duration;

    private void OnEnable()
    {
        if (_clips.Length == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_clips));
        }

        if (_audio == null)
        {
            throw new ArgumentNullException(nameof(_audio));
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Angel>(out Angel angel))
        {
            int randomIndex = Random.Range(0, _clips.Length);
            _animator.enabled = false;
            _spriteRenderer.DOColor(Color.red, _duration);
            _audio.PlayOneShot(_clips[randomIndex]);
        }
    }
}