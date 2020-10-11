using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{

    public int npcCount = 0;
    public List<GameObject> queueCount = new List<GameObject>();

    [SerializeField] GameObject npcInstance;
    [SerializeField] GameObject[] npcType;

    public float timer;
    public float spawnTimer;
    public float spawnDifference;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            spawnDifference = Random.Range(-5, 10);
            timer = spawnTimer + spawnDifference;

            SetClass();
            if (npcCount <= queueCount.Count)
            {
                Instantiate(npcInstance);
            }
        }
    }

    void SetClass()
    {
        npcInstance = npcType[Random.Range(0, npcType.Length + 1)];
    }


}
