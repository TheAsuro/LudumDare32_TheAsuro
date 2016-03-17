using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public UnityEngine.UI.Slider sensSlider;

    void Start()
    {
        GameInfo.gi.SetMenuState(GameMenu.MenuState.MainMenu);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
