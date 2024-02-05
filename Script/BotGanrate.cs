using System.Collections.Generic;
using UnityEngine;

public class BotGanrate : MonoBehaviour
{
    [SerializeField] private GameObject GanrateBot;
    [SerializeField] public List<GameObject> Bot;

    public static BotGanrate Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject RandomPos = Instantiate(GanrateBot);
            RandomPos.transform.position = new Vector3(Random.Range(-38f, 38f), Random.Range(-18, 18));
            Bot.Add(RandomPos);
        }
    }
    private void Update()
    {
        if(Bot.Count < 5)
        {
            //Debug.Log(Bot.Count);
            GameObject RandomPos = Instantiate(GanrateBot);
            RandomPos.transform.position = new Vector3(Random.Range(-38f, 38f), Random.Range(-18, 18));
            Bot.Add(RandomPos);
        }
    }
} 