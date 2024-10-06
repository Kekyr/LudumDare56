using System;
using UnityEngine;

public class SpriteOutline : MonoBehaviour
{
    [SerializeField] private Color _color = Color.white;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private void OnEnable()
    {
        if (_spriteRenderer == null)
        {
            throw new ArgumentNullException(nameof(_spriteRenderer));
        }
    }

    public void UpdateOutline(bool outline)
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        _spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Outline", outline ? 1f : 0);
        mpb.SetColor("_OutlineColor", _color);
        _spriteRenderer.SetPropertyBlock(mpb);
    }
}