using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public UnityEngine.UI.Slider sensSlider;

    void Start()
    {
        GameInfo.gi.SetMenuState(GameMenu.MenuState.MainMenu);
    }

    public void LoadGame()
    {
        Application.LoadLevel(1);
    }
}
