using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour
{
    public GameObject endMenu;

    public enum MenuState
    {
        NoMenu,
        PauseMenu,
        EndMenu
    }

    public void SetMenuState(MenuState state)
    {
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
}
