using System;
using UnityEngine;
using System.Collections.Generic;

public class Tank : MonoBehaviour
{
    [SerializeField] private float boomOffset = 0.3f;
    [SerializeField] private float atackDelay;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float delay;

    [SerializeField] private Sprite headDown;
    [SerializeField] private Sprite headUp;
    [SerializeField] private GameObject boomPrefab;
    [SerializeField] private Transform head;
    [SerializeField] private Health health;

    private List<Angel> targets = new List<Angel>();
    private Angel currentTarget;
    private SpriteRenderer headRenderer;
    private float timer;

    public Health HealthPoints => health;

    private void OnEnable()
    {
        if (health == null)
        {
            throw new ArgumentNullException(nameof(health));
        }

        health.Died += OnDead;
    }

    private void OnDisable()
    {
        health.Died -= OnDead;
    }

    private void Start()
    {
        headRenderer = head.gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Находим всех объектов Angel в радиусе detectionRadius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        targets.Clear(); // Очищаем список перед обновлением

        foreach (Collider2D collider in colliders)
        {
            Angel angel = collider.GetComponent<Angel>();

            if (angel != null && !targets.Contains(angel))
            {
                targets.Add(angel);
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
                Vector3 boomPos = new Vector3(currentTarget.transform.position.x,
                    currentTarget.transform.position.y + boomOffset, currentTarget.transform.position.z);
                GameObject boom = Instantiate(boomPrefab, boomPos, Quaternion.identity);
                Destroy(boom, 0.4f);
            }
        }
    }

    private void RotateTo(Transform angel)
    {
        Vector2 headPos = new Vector2(head.position.x, head.position.y);
        Vector2 angelPos = new Vector2(angel.position.x, angel.position.y);
        Vector2 directionToAngel = angelPos - headPos;
        float angle = Vector2.SignedAngle(transform.up, directionToAngel);

        headRenderer.sprite = Mathf.Abs(angle) <= 70 ? headUp : headDown;
        headRenderer.flipX = angle < 0;
    }

    private void OnDead()
    {
        head.gameObject.SetActive(false);
        Destroy(transform.parent.gameObject, delay);
    }
}