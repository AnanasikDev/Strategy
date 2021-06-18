using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    public void HardCoreMode()
    {
        SceneManager.LoadScene("main");
        while (true) if (SceneManager.GetActiveScene().isLoaded) break;
        InterScene.startMoney = 0;
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
        InterScene.startMoney = 100000;
    }
}
