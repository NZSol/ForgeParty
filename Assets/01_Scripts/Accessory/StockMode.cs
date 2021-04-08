using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StockMode : MonoBehaviour
{
    [SerializeField] WinLose gameState = null;
    [SerializeField] GameObject stockCanvas = null;

    public Image[] lifeIcons = null;


    public int lives = 0;
    public int LivesMax = 5;

    // Start is called before the first frame update
    void Start()
    {
        lives = LivesMax;
        stockCanvas.SetActive(true);
    }

    public void ReduceChances()
    {
        lives--;
        for (int i = 4; i >= lives; i--)
        {
            print(lifeIcons[i]);
            lifeIcons[i].enabled = true;
        }
    }

    private void Update()
    {
        if (lives <= 0)
        {
            gameState.Lose();
        }
    }
}
