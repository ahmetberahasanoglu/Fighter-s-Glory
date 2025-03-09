using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button newGameButton;
    public Button continueButton;
    public Button settingsButton;
    public Button exitButton;

    void Start()
    {
        continueButton.interactable = PlayerPrefs.HasKey("SavedGame");

        newGameButton.onClick.AddListener(StartNewGame);
        continueButton.onClick.AddListener(ContinueGame);
        settingsButton.onClick.AddListener(OpenSettings);
        exitButton.onClick.AddListener(ExitGame);
    }

    void StartNewGame()
    {
        PlayerPrefs.DeleteKey("SavedGame");
        SceneManager.LoadScene("GameScene");
    }

    void ContinueGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    void OpenSettings()
    {
        Debug.Log("Ayarlar açýldý");
        // Ayarlar menüsü açýlacak
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
