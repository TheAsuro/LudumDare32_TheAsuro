using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour
{
    public enum MenuState
    {
        NoMenu,
        PauseMenu,
        EndMenu,
        MainMenu
    }

    public GameObject endMenu;

    private MenuState currentState = MenuState.NoMenu;    

    public void SetMenuState(MenuState state)
    {
        currentState = state;

        endMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        switch (state)
        {
            case MenuState.NoMenu:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case MenuState.PauseMenu:
                break;
            case MenuState.EndMenu:
                endMenu.SetActive(true);
                break;
            case MenuState.MainMenu:
                break;
        }
    }

    public MenuState GetMenuState()
    {
        return currentState;
    }
}
