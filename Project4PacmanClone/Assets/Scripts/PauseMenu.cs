using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void OnEnable()
    {
        // Find the action map and the action
        var playerActionMap = menuControl.FindActionMap("Controls");
        menu = playerActionMap.FindAction("PauseMenu");

        menu.Enable();
        menu.performed += OnJumpPerformed;
    }
    private void OnDisable()
    {
        menu.performed -= OnJumpPerformed;
        menu.Disable();
    }
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        print("PauseMenu");
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
        //Cursor.lockState = CursorLockMode.Locked;
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
    }
}
