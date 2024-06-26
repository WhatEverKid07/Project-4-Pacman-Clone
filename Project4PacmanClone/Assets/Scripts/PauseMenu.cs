using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GhostAbility ghostAbility;
    [SerializeField] private PacmanAbility pacmanAbility;

    public GameObject pauseMenu;
    public static bool gameIsPaused = false;
    private InputAction menu;

    [SerializeField] private InputActionAsset menuControl;
    [SerializeField] private Button firstButton;

    private void OnEnable()
    {
        var playerActionMap = menuControl.FindActionMap("Controls");
        menu = playerActionMap.FindAction("PauseMenu");
        menu.Enable();

        menu.performed += OnPauseMenuPerformed;
    }

    private void OnDisable()
    {
        menu.performed -= OnPauseMenuPerformed;
        menu.Disable();
    }

    private void OnPauseMenuPerformed(InputAction.CallbackContext context)
    {
        if (gameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
        pacmanAbility.canUseController = true;

        ghostAbility.canUseController = true;
        playerMovement.canMove = true;
        playerMovement.canMove2 = true;

        gameIsPaused = false;
    }

    public void Resume()
    {
        pacmanAbility.canUseController = true;
        ghostAbility.canUseController = true;
        playerMovement.canMove = true;

        playerMovement.canMove2 = true;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        EventSystem.current.SetSelectedGameObject(null);
    }

    void Pause()
    {
        pacmanAbility.canUseController = false;
        ghostAbility.canUseController = false;
        playerMovement.canMove = false;

        playerMovement.canMove2 = false;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);

        gameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(SelectFirstButton());
    }

    private IEnumerator SelectFirstButton()
    {
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
    }
}
