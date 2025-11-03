using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] public GameObject LoseMenu;
    [SerializeField] public GameObject WinMenu;

    void Start()
    {
        LoseMenu.SetActive(false);
        WinMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}
