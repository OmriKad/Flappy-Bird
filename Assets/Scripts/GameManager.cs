using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private static bool _pendingAutoStart = false;

    [SerializeField] private GameObject _gameOverCanvas;

    private bool _isGameStarted = false;
    private bool _isGameOver = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        // Show the title screen first: the bird floats idle in the air until you click Play
        ShowTitleScreen();

        // If Play was clicked to restart after a game-over, the scene was reloaded -
        // carry on straight into gameplay so it stays a single click.
        if (_pendingAutoStart)
        {
            _pendingAutoStart = false;
            StartGame();
        }
    }

    private void ShowTitleScreen()
    {
        _gameOverCanvas.SetActive(true);
        SetGameOverArtActive(false); // hide the "Game Over" art on the start screen; keep Title + Play button

        _isGameStarted = false;
        Time.timeScale = 0f; // freeze physics so the bird holds still in the air
    }

    public void StartGame()
    {
        // Clicking Play after a game-over means "play again": fully restart the scene.
        if (_isGameOver)
        {
            _isGameOver = false;
            _pendingAutoStart = true; // auto-run the freshly reloaded scene
            RestartGame();
            return;
        }

        // Prevent double-clicking Play on the title screen.
        if (_isGameStarted)
        {
            return;
        }

        _isGameStarted = true;
        _gameOverCanvas.SetActive(false);
        Time.timeScale = 1f; // resume the game - the bird falls and the player's flaps matter
    }

    public void GameOver()
    {
        _isGameOver = true;
        _gameOverCanvas.SetActive(true);
        SetGameOverArtActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // The "Game Over" art lives as a child of the canvas; toggle it on/off by name
    private void SetGameOverArtActive(bool active)
    {
        if (_gameOverCanvas == null)
        {
            return;
        }

        Transform art = _gameOverCanvas.transform.Find("GameOver");
        if (art != null)
        {
            art.gameObject.SetActive(active);
        }
    }
}
