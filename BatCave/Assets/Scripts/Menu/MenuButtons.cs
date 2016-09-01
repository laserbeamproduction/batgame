using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartEndless()
    {
        SceneManager.LoadScene("scene_One");
    }

    public void StartArcade()
    {

    }

    public void OpenSettings()
    {

    }

    public void OpenShop()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("scene_One"); //temp
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu"); //temp
    }

    public void Leaderboards()
    {

    }
}
