using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public UIMenu uiMenu;
    public UIGame prefabUIGame;
    public GameObject prefabUIDefeat;
    public TilesSpawner tilesSpawner;
    public Player player;
    public CristalController cristalController;


    private int countTilesPlayerOn;
    private int CountTilesPlayerOn
    {
        get => countTilesPlayerOn;
        set
        {
            if (value != countTilesPlayerOn)
            {
                if (value == 0)
                {
                    OnGameEnd();
                }
                countTilesPlayerOn = value;
            }
        }
    }
    private bool gameEnded;


    private void Awake()
    {
        StopGame();
    }
    private void Start()
    {
        if (player == null)
        {
            Debug.LogWarning("player does not assigned");
        }

        player.OnEnterToTile += OnPlayerEnterToTile;
        player.OnExitFromTile += OnPlayerExitFromTile;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gameEnded)
            {
                RestartLevel();
            }
            else if (!player.enabled)
            {
                StartGame();
            }
        }
    }


    private void OnPlayerExitFromTile()
    {
        CountTilesPlayerOn--;
    }
    private void OnPlayerEnterToTile()
    {
        CountTilesPlayerOn++;
    }
    private void OnGameEnd()
    {
        gameEnded = true;
        StopGame();
        ShowUIDefeat();
    }


    private void StartGame()
    {
        player.enabled = true;
        tilesSpawner.enabled = true;

        HideUIMenu();

        ShowUIGame();
    }
    private void StopGame()
    {
        player.enabled = false;
        tilesSpawner.enabled = false;
    }
    private void HideUIMenu()
    {
        uiMenu.gameObject.SetActive(false);
    }
    private void ShowUIGame()
    {
        UIGame ui = Instantiate(prefabUIGame);
        cristalController.onCountScoresChanged += ui.SetScore;
    }
    private void ShowUIDefeat()
    {
        Instantiate(prefabUIDefeat);
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
