using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] queuePositions;

    [SerializeField] GameObject npcInstance;
    [SerializeField] GameObject[] npcType;

    public GameObject fleePos;
    public GameObject battlePos;
    GameObject setQueuePosition;

    public float timer;
    public float spawnTimer = 10;
    public float spawnDifference;

    public float spawnDiffHigh;
    public float spawnDiffLow;

    public List<GameObject> activeNpcs = new List<GameObject>();

    private void Start()
    {
        timer -= (timer + 1);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            spawnDifference = Random.Range(spawnDiffLow, spawnDiffHigh);
            timer = spawnTimer + spawnDifference;

            SetClass();
            if (activeNpcs.Count < queuePositions.Length)
            {
                var instance = Instantiate(npcInstance, gameObject.transform);
                instance.SetActive(true);
                //instance.setQueuePosition(queuePositions[activeNpcs.Count]);
                instance.GetComponent<NpcRequest>().GoalQueuePos = queuePositions[activeNpcs.Count];
                activeNpcs.Add(instance);
            }
        }
    }

    void SetClass()
    {
        npcInstance = npcType[Random.Range(0, npcType.Length)];
    }

    public void listRemove(GameObject obj)
    {
        activeNpcs.Remove(obj);
        Destroy(obj);
        for (int i = 0; i < activeNpcs.Count; i++)
        {
            activeNpcs[i].GetComponent<NpcRequest>().GoalQueuePos = queuePositions[i];
        }
    }

}
