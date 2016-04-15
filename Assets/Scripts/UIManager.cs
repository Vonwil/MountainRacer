using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    bool isPaused;
    public GameObject pauseMenu;

	// Use this for initialization
	void Start ()
    {
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (isPaused == true)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0.0f;
            }
            else if (isPaused == false)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }
}
