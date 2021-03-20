using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] queuePositions = new GameObject[0];
    [SerializeField] GameObject GodObj = null;

    [SerializeField] GameObject npcInstance = null;
    [SerializeField] GameObject[] npcType = new GameObject[0];


    public GameObject fleePos = null;
    public GameObject battlePos = null;
    GameObject setQueuePosition = null;

    public float timer = 0;
    public float spawnTimer = 10;
    public float spawnDifference = 0;

    public float spawnDiffHigh = 0;
    public float spawnDiffLow = 0;

    public List<GameObject> activeNpcs = new List<GameObject>();
    [SerializeField]
    GameObject TeamWeaponList = null;
    private void Start()
    {
        StartCoroutine(DelayedStart());
    }
    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(0.5f);

        queuePositions = GodObj.GetComponent<LevelSet>().CurLvl.gameObject.GetComponent<GetQueue>().myQueuePoints.ToArray();

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
                instance.GetComponent<NpcRequest>().myTeamList = TeamWeaponList;
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
        for (int i = 0; i < activeNpcs.Count; i++)
        {
            activeNpcs[i].GetComponent<NpcRequest>().GoalQueuePos = queuePositions[i];
        }
    }

}
