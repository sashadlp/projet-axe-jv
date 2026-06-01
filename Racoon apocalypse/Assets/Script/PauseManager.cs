using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseManager : MonoBehaviour
{
    
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;        
        isPaused = true;
    }

   
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f;          
        isPaused = false;
    }

   
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Main menue");
    }
}