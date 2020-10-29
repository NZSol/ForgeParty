using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(4f);

        StartCoroutine(LoadGame());
    }

  

    IEnumerator LoadGame()
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(2);

        yield return new WaitForEndOfFrame();
    }
}
