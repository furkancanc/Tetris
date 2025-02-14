using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    Board gameBoard;
    Spawner spawner;

    Shape activeShape;

    float dropInterval = .3f;
    float timeToDrop;

    float timeToNextKey;

    //[Range(.02f, 1f)]
    //float keyRepeatRate = .03f;

    float timeToNextKeyLeftRight;

    [Range(.02f, 1f)]
    public float keyRepeatRateLeftRight = .15f;

    float timeToNextKeyDown;

    [Range(.01f, 1f)]
    public float keyRepeatRateDown = .01f;

    float timeToNextKeyRotate;

    [Range(.02f, 1f)]
    public float keyRepeatRateRotate = .25f;

    bool gameOver = false;

    public GameObject gameOverPanel;

    SoundManager soundManager;

    public IconToggle rotIconToggle;
    bool clockwise = true;

    public GameObject pausePanel;
    public bool isPaused = false;

    ScoreManager scoreManager;

    private void Start()
    {
        timeToNextKeyLeftRight = Time.time + keyRepeatRateLeftRight;
        timeToNextKeyDown = Time.time + keyRepeatRateDown;
        timeToNextKeyRotate = Time.time + keyRepeatRateRotate;

        gameBoard = GameObject.FindFirstObjectByType<Board>();
        spawner = GameObject.FindFirstObjectByType<Spawner>();
        soundManager = GameObject.FindFirstObjectByType<SoundManager>();
        scoreManager = GameObject.FindFirstObjectByType<ScoreManager>();

        if (!gameBoard)
        {
            Debug.LogWarning("WARNING! There is no game board defined!");
        }

        if (!soundManager)
        {
            Debug.LogWarning("WARNING! There is no sound manager defined!");
        }

        if (!scoreManager)
        {
            Debug.LogWarning("WARNING! There is no score manager defined!");
        }

        if (!spawner)
        {
            Debug.LogWarning("WARNING! There is no spawner defined!");
        }
        else
        {
            spawner.transform.position = Vectorf.Round(spawner.transform.position);
            if (!activeShape)
            {
                activeShape = spawner.SpawnShape();
            }
        }

        if (gameOverPanel)
        {
            gameOverPanel.SetActive(false);
        }

        if (pausePanel)
        {
            pausePanel.SetActive(false);
        }
    }

    void PlayerInput()
    {
        if ((Input.GetButton("MoveRight")) && (Time.time > timeToNextKeyLeftRight) || (Input.GetButtonDown("MoveRight")))
        {
            activeShape.MoveRight();
            timeToNextKeyLeftRight = Time.time + keyRepeatRateLeftRight;

            if (!gameBoard.IsValidPosition(activeShape))
            {
                activeShape.MoveLeft();
                PlaySound(soundManager.errorSound, .5f);
            }
            else
            {
                PlaySound(soundManager.moveSound, .5f);
            }
        }

        else if ((Input.GetButton("MoveLeft")) && (Time.time > timeToNextKeyLeftRight) || (Input.GetButtonDown("MoveLeft")))
        {
            activeShape.MoveLeft();
            timeToNextKeyLeftRight = Time.time + keyRepeatRateLeftRight;

            if (!gameBoard.IsValidPosition(activeShape))
            {
                activeShape.MoveRight();
                PlaySound(soundManager.errorSound, .5f);
            }
            else
            {
                PlaySound(soundManager.moveSound, .5f);
            }
        }

        else if ((Input.GetButtonDown("Rotate")) && (Time.time > timeToNextKeyRotate))
        {
            //activeShape.RotateRight();
            activeShape.RotateClockwise(clockwise);
            timeToNextKeyRotate = Time.time + keyRepeatRateRotate;

            if (!gameBoard.IsValidPosition(activeShape))
            {
                activeShape.RotateClockwise(!clockwise);
                PlaySound(soundManager.errorSound, .5f);
            }
            else
            {
                PlaySound(soundManager.moveSound, .5f);
            }
        }
        else if (Input.GetButton("MoveDown") && (Time.time > timeToNextKeyDown) || Time.time > timeToDrop)
        {
            timeToDrop = Time.time + dropInterval;
            timeToNextKeyDown = Time.time + keyRepeatRateDown;
            activeShape.MoveDown();

            if (!gameBoard.IsValidPosition(activeShape))
            {
                if (gameBoard.IsOverLimit(activeShape))
                {
                    GameOver();
                }
                else
                {
                    LandShape();
                }
            }
        }

        else if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
    }

    private void LandShape()
    {
        timeToNextKeyLeftRight = Time.time;
        timeToNextKeyDown = Time.time;
        timeToNextKeyRotate = Time.time;

        activeShape.MoveUp();
        gameBoard.StoreShapeInGrid(activeShape);
        activeShape = spawner.SpawnShape();

        gameBoard.ClearAllRows();

        PlaySound(soundManager.dropSound);

        if (gameBoard.completedRows > 0)
        {
            scoreManager.ScoreLines(gameBoard.completedRows);
            if (gameBoard.completedRows > 1)
            {
                AudioClip randomVocal = soundManager.GetRandomClip(soundManager.vocalClips);
                PlaySound(randomVocal);
            }

            PlaySound(soundManager.clearRowSound);
        }
    }

    public void GameOver()
    {
        activeShape.MoveUp();
        gameOver = true;
        Debug.LogWarning(activeShape.name + " is over the limit");

        if (gameOverPanel)
        {
            gameOverPanel.SetActive(true);
        }

        PlaySound(soundManager.gameOverSound, .75f);
        PlaySound(soundManager.gameOverVocalClip);
    }

    private void Update()
    {
        if (!gameBoard || !spawner || !activeShape || gameOver || !soundManager || !scoreManager)
        {
            return;
        }

        PlayerInput();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void PlaySound(AudioClip clip, float volMultiplier = 1)
    {
        if (clip && soundManager.fxEnabled)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, Mathf.Clamp(soundManager.fxVolume * volMultiplier, .05f, 1f));
        }
    }

    public void ToggleRotDirection()
    {
        clockwise = !clockwise;
        if (rotIconToggle)
        {
            rotIconToggle.ToggleIcon(clockwise);
        }
    }

    public void TogglePause()
    {
        if (gameOver)
        {
            return;
        }

        isPaused = !isPaused;

        if (pausePanel)
        {
            pausePanel.SetActive(isPaused);

            if (soundManager)
            {
                soundManager.musicSource.volume = (isPaused) ? soundManager.musicVolume * .25f : soundManager.musicVolume;
            }

            Time.timeScale = isPaused ? 0 : 1f;
        }

    }
}
