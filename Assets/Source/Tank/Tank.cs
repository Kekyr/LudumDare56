using UnityEngine;
using System.Collections.Generic;

public class Tank : MonoBehaviour
{
    private List<Angel> targets = new List<Angel>();
    private Angel currentTarget;


    [SerializeField]private float atackDelay;
    private float timer;

    [SerializeField]private GameObject boomPrefab;

    [SerializeField]private Transform head;
    private SpriteRenderer headRenderer;

    [SerializeField] private Sprite headDown;
    [SerializeField] private Sprite headUp;

    private void Start()
    {
        headRenderer = head.gameObject.GetComponent<SpriteRenderer>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
       Angel angel = collision.gameObject.GetComponent<Angel>();
       if(angel != null && !targets.Contains(angel))
       {
            targets.Add(angel);
            if(currentTarget == null)
                currentTarget = angel;
       }
    }

    private void Update()
    {
        if(currentTarget!=null)
        {
            RotateTo(currentTarget.transform);
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                timer = atackDelay;
                GameObject boom = Instantiate(boomPrefab, currentTarget.transform.position, Quaternion.identity);
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

    headRenderer.sprite = Mathf.Abs(angle) <= 70  ? headUp : headDown; 
    headRenderer.flipX = angle < 0; 

   }

}
