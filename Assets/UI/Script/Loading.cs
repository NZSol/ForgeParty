using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class Loading : MonoBehaviour
{
    public bool contextReceived = false;
    public bool loaded = false;
    [SerializeField] Text text = null;

    private void Awake()
    {
        contextReceived = false;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(4f);

        StartCoroutine(LoadGame());
    }
    public void setContextTrue()
    {
        if (loaded)
        {
            contextReceived = true;
        }
    }

    private void Update()
    {
        if (contextReceived && loaded)
        {
            loaded = false;
            contextReceived = false;
            AsyncOperation gameLevel = SceneManager.LoadSceneAsync(2);
        }
        if (loaded)
        {
            text.text = "Press Any button to start";
        }
    }

    IEnumerator LoadGame()
    {
        loaded = true;
        yield return null;
    }
}
