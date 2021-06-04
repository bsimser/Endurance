using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
	public Text TextControl;

    void Start()
    {
        LoadScene();
    }

	// Update is called once per frame
	void Update()
	{
        //TextControl.text = string.Format("{0}%", (progress * 100f).ToString("0.0"));
	}

    public void LoadScene()
    {
        //Start loading the Scene asynchronously and output the progress bar
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Game");
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            TextControl.text = $"{asyncOperation.progress * 100}%";

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                TextControl.text = "Press the space bar to continue";
                //Wait to you press the space key to activate the Scene
                if (Input.GetKeyDown(KeyCode.Space))
                    //Activate the Scene
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
