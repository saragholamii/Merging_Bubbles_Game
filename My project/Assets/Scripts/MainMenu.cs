using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject bestScorePanel;
    public GameObject creditsPanel;
    public GameObject settingsPanel;

    [Header("Best Score UI")]
    public TMP_Text bestScoreText; // Assign a Text (or TMP_Text if using TextMeshPro)

    [Header("Scene Settings")]
    public string gameSceneName = "Game"; // change to your game scene name

    // --- Button Functions ---
    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void ShowBestScore()
    {
        if (bestScorePanel != null)
        {
            bestScorePanel.SetActive(true);

            // Load score from PlayerPrefs
            int bestScore = PlayerPrefs.GetInt("BestScore", 0);
            bestScoreText.text = bestScore.ToString();
        }
    }

    public void HideBestScore()
    {
        if (bestScorePanel != null)
            bestScorePanel.SetActive(false);
    }

    public void ShowCredits()
    {
        if (creditsPanel != null)
            creditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        if (creditsPanel != null)
            creditsPanel.SetActive(false);
    }
    
    public void ShowSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void HideSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }
    
    

    public void QuitGame()
    {
        Debug.Log("Quit Game!"); // Works only in build
        Application.Quit();
    }
}
