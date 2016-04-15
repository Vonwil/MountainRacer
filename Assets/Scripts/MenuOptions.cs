using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    void Update()
    {
    }

	public void Quit()
    {
        Application.Quit();
    }

    public void GameLevel()
    {
        SceneManager.LoadScene("GameLevel");
    }

    public void ToStartMenu()
    {
        SceneManager.LoadScene("StartLevel");
    }
}
