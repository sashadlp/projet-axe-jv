using UnityEngine;

public class Gameloop : MonoBehaviour
{
    public bool isGameRunning = false;
    public GameObject victoryScreen;
   

    public Transform PlayerTransform { get; private set; }

    public static Gameloop Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            PlayerTransform = playerObj.transform;
        }
    }

    public void Start() 
    {
        isGameRunning = true;
        Time.timeScale = 1f; 
        
        if (victoryScreen != null) victoryScreen.SetActive(false);

        Debug.Log("Partie lancée ! Objectif : Atteindre la zone de victoire.");
    }

    public void Update() 
    {
        if (!isGameRunning) return;
    }
    
    public void VictoryCondition() 
    {
        if (isGameRunning)
        {
            TriggerVictory();
        }
    }

    public void TriggerVictory() 
    {
        isGameRunning = false;
        Time.timeScale = 0f; // On fige le jeu
        
        if (victoryScreen != null)
        {
            // 1. On récupère le script attaché à ton écran de victoire
            VictoryScreen screenScript = victoryScreen.GetComponent<VictoryScreen>();
            
            // 2. On récupère le score actuel depuis le ScoreManager
            int scoreFinal = 0;
            if (ScoreManager.instance != null)
            {
                scoreFinal = ScoreManager.instance.GetScore();
            }

            // 3. La Gameloop ordonne à l'écran de s'afficher EN LUI PASSANT LE SCORE
            screenScript.Show(scoreFinal);
        }
        
        Debug.Log("Victoire ! damien a pu se sauver ");
    }
}