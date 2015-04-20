using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour
{
    public enum MenuState
    {
        NoMenu,
        PauseMenu,
        EndMenu
    }

    public GameObject endMenu;

    private MenuState currentState = MenuState.NoMenu;    

    public void SetMenuState(MenuState state)
    {
        currentState = state;

        endMenu.SetActive(false);

        switch (state)
        {
            case MenuState.NoMenu:
                break;
            case MenuState.PauseMenu:
                break;
            case MenuState.EndMenu:
                endMenu.SetActive(true);
                break;
        }
    }

    public MenuState GetMenuState()
    {
        return currentState;
    }
}
