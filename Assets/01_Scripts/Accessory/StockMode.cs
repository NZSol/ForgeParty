using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockMode : MonoBehaviour
{
    [SerializeField] WinLose gameState = null;

    public int lives = 0;
    public int LivesMax = 5;

    // Start is called before the first frame update
    void Start()
    {
        lives = LivesMax;
    }

    public void ReduceChances()
    {
        lives--;
    }

    private void Update()
    {
        if (lives <= 0)
        {
            gameState.Lose();
        }
    }
}
