using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    public void LoadTownScene()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LoadLocationScene()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}