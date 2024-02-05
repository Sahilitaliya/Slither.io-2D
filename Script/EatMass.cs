using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Eatmass : MonoBehaviour
{
    public static Eatmass Instance;
    
    [SerializeField] public List<Transform> Segments;
    [SerializeField] Transform Tail;
    [SerializeField] Transform Parent;
    [SerializeField] public GameObject GanrateFood;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] TextMeshProUGUI Foodtxt;
    [SerializeField] ParticleSystem particl;
    [SerializeField] AudioSource SoundSource;
    [SerializeField] AudioClip[] EatVoice;
    public Vector3 lastPlayerPosition;
    int FoodCount = 0;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Segments = new List<Transform>();
        Segments.Add(this.transform);
        StartCoroutine(GenerateMassContinuously());
        particl.Pause();

        for (int k = 0; k < 15; k++)
        {
            Transform segment = Instantiate(Tail);
            segment.position = Segments[Segments.Count - 1].position;
            Segments.Add(segment);
        }
    }
    private void Update()
    {
        lastPlayerPosition = transform.position;
    }
    private void FixedUpdate()
    {
        for (int i = Segments.Count - 1; i > 0; i--)
        {
            Segments[i].position = Segments[i - 1].position;
        }
    }
    IEnumerator GenerateMassContinuously()
    {
        while (true)
        {
            GenerateMass(2);
            yield return new WaitForSeconds(1.5f);
        }
    }
    void GenerateMass(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newImage = Instantiate(GanrateFood, Parent);
            newImage.transform.position = new Vector2(Random.Range(-38, 38), Random.Range(-18, 18));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mass")
        {
            Grow(5, 1.01f);

            FoodCount++;
            Foodtxt.text = FoodCount.ToString();

            Destroy(collision.gameObject);

            SoundSource.clip = EatVoice[0];
            SoundSource.Play();
            particl.Play();
        }
        if (collision.gameObject.tag == "Wall")
        {
            if (gameObject.tag == "Player")
            {
                for (int i = 0; i < Segments.Count; i++)
                {
                    Destroy(Segments[i].gameObject);
                }

                Vector3 foodPosition = new Vector3(lastPlayerPosition.x, lastPlayerPosition.y, lastPlayerPosition.z);
                Instantiate(GanrateFood, foodPosition, Quaternion.identity);

                SceneManager.LoadScene(0);
            }
            //else
            //{
            //    Destroy(gameObject);
            //    BotGanrate.Instance.Bot.Remove(gameObject);

            //    for (int i = 0; i < Segments.Count; i++)
            //    {
            //        Destroy(Segments[i].gameObject);
            //    }

            //    Vector3 foodPosition = new Vector3(lastPlayerPosition.x, lastPlayerPosition.y, lastPlayerPosition.z);
            //    Instantiate(GanrateFood, foodPosition, Quaternion.identity);
            //}
        }
        if (collision.gameObject.tag == "Bot")
        {
            for (int i = 0; i < Segments.Count; i++)
            {
                Destroy(Segments[i].gameObject);
            }

            Vector3 foodPosition = new Vector3(lastPlayerPosition.x, lastPlayerPosition.y, lastPlayerPosition.z);
            Instantiate(GanrateFood, foodPosition, Quaternion.identity);

            SceneManager.LoadScene(0);
        }
    }
    private void Grow(int count, float targetScale)
    {
        for (int i = 0; i < count; i++)
        {
            Transform segment = Instantiate(Tail);
            segment.position = Segments[Segments.Count - 1].position;
            Segments.Add(segment);

            Vector2 TargateSize = new Vector2(targetScale, targetScale);
            segment.localScale = TargateSize;
            targetScale += 0.01f;
        }
    }
}