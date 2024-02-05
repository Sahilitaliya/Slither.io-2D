using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using System.Collections;

public class BotMove : MonoBehaviour
{
    bool isMoving;
    [SerializeField] List<Transform> Segment;
    [SerializeField] Transform Tail;

    void Start()
    {
        Segment = new List<Transform>();
        Segment.Add(this.transform);

        StartCoroutine(MoveSnake());

        for (int i = 0; i < 15; i++)
        {
            Transform segment = Instantiate(Tail);
            segment.position = Segment[Segment.Count - 1].position;
            Segment.Add(segment);
        }
    }

    IEnumerator MoveSnake()
    {
        while (true)
        {
            if (!isMoving)
            {
                isMoving = true;

                // Use a fixed speed for the movement
                float speed = 5f;

                // Calculate a random direction for movement
                Vector2 randomDirection = Random.insideUnitCircle.normalized;

                // Calculate the target position based on the random direction and speed
                Vector3 targetPosition = transform.position + new Vector3(randomDirection.x, randomDirection.y, 0f) * speed;

                // Calculate the duration based on the distance to the target position
                float duration = Vector3.Distance(transform.position, targetPosition) / speed;

                yield return DOTween.To(() => transform.position, x => transform.position = x, targetPosition, duration)
                    .SetEase(Ease.Linear)
                    .WaitForCompletion();

                isMoving = false;
            }

            yield return null;
        }
    }

    private void FixedUpdate()
    {
        for (int i = Segment.Count - 1; i > 0; i--)
        {
            Segment[i].position = Segment[i - 1].position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            BotGanrate.Instance.Bot.Remove(gameObject);

            for (int i = 0; i < Segment.Count; i++)
            {
                Destroy(Segment[i].gameObject);
            }

            //Vector3 foodPosition = new Vector3(lastPlayerPosition.x, lastPlayerPosition.y, lastPlayerPosition.z);
            //Instantiate(GanrateFood, foodPosition, Quaternion.identity);

        }
        if (collision.gameObject.tag == "Mass")
        {
            GrowBot(5, 1.01f);
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tail")
        {
            BotGanrate.Instance.Bot.Remove(gameObject);

            for(int i=0; i<Segment.Count; i++)
            {
                Destroy(Segment[i].gameObject);
            }

        }
    }
    public void GrowBot(int count, float targateSize)
    {
        for (int i = 0; i < count; i++)
        {
            Transform segment = Instantiate(Tail);
            segment.position = Segment[Segment.Count - 1].position;
            Segment.Add(segment);

            Vector2 TargateSize = new Vector2(targateSize, targateSize);
            segment.localScale = TargateSize;
            targateSize += 0.01f;
        }
    }
}