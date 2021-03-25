using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] queuePositions = new GameObject[0];
    [SerializeField] GameObject GodObj = null;

    [SerializeField] int PlayersCount = 0;

    [SerializeField] LevelSet level = null;

    [SerializeField] GameObject npcInstance = null;
    [SerializeField] GameObject[] npcType = new GameObject[0];


    public GameObject fleePos = null;
    public GameObject battlePos = null;

    public float timer = 0;
    public float spawnTimer = 10;
    public float spawnDifference = 0;

    [SerializeField] float spawnDiffHigh = 0;
    [SerializeField] float spawnDiffLow = 0;

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

        PlayersCount = GodObj.GetComponent<StartPos>().playerCount;
        queuePositions = level.CurLvl.gameObject.GetComponent<GetQueue>().myQueuePoints.ToArray();

        timer -= (timer + 1);

    }

    void getPlayerCount()
    {
        PlayersCount = GodObj.GetComponent<StartPos>().playerArray.Length;
        switch (PlayersCount)
        {
            case 1:
                print(PlayersCount);
                spawnTimer = 15;
                spawnDiffLow = -5;
                spawnDiffHigh = 10;
                break;

            case 2:
                print(PlayersCount);
                spawnTimer = 15;
                spawnDiffLow = -5;
                spawnDiffLow = 5;
                break;

            case 3:
                print(PlayersCount);
                spawnTimer = 10;
                spawnDiffLow = -6;
                spawnDiffHigh = 4;
                break;

            case 4:
                print(PlayersCount);
                spawnTimer = 10;
                spawnDiffLow = -6;
                spawnDiffHigh = 4;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayersCount == 0)
        {
            getPlayerCount();
        }
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            spawnDifference = Random.Range(spawnDiffLow, spawnDiffHigh);
            print("hitting");
            timer = spawnTimer + spawnDifference;

            SetClass();
            if (activeNpcs.Count < queuePositions.Length)
            {
                var instance = Instantiate(npcInstance, gameObject.transform);
                instance.SetActive(true);
                //instance.setQueuePosition(queuePositions[activeNpcs.Count]);
                var instanceComponent = instance.GetComponent<NpcRequest>();
                instanceComponent.GoalQueuePos = queuePositions[activeNpcs.Count];
                activeNpcs.Add(instance);
                instanceComponent.myTeamList = TeamWeaponList;
                instanceComponent.playersCount = PlayersCount;
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
