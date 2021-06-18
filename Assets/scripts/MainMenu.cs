using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    public void ClassicMode()
    {
        SceneManager.LoadScene("main");
        while (true) if (SceneManager.GetActiveScene().isLoaded) break;
        InterScene.startMoney = 100;
    }
    public void SandBoxMode()
    {
        SceneManager.LoadScene("main");
        while (true) if (SceneManager.GetActiveScene().isLoaded) break;
        InterScene.startMoney = 1000000;
    }
}
