using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    void Awake()
    {
        GameInfo.gi.SetMenuState(GameMenu.MenuState.MainMenu);
    }

    public void LoadGame()
    {
        Application.LoadLevel(1);
    }
}
