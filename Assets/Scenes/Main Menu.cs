using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public void mainMenu(int level)
    {
        Application.LoadLevel(level);
    }
}
