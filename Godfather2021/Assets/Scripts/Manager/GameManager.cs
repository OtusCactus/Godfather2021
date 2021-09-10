using UnityEngine;
using Rewired;
using System.Collections;

public enum GameState
{
    MENU,
    INGAME,
    TUTO,
    RESULT
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Player player;

    public System.Action onStateChange;

    [Header("Settings")]
    [SerializeField] private float roundTime = 15;
    private float currentTime = 0;
    public GameState state;
    private GameState previousState;
    private bool canPlay = false;
    private bool lastMinutes = false;
    [SerializeField] private Sprite plate;
    public Animator panWarmer;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(GameState.MENU);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.INGAME)
        {
            if (lastMinutes)
            {
                currentTime -= Time.deltaTime;
                if (currentTime < 0)
                {
                    LevelManager.instance.finalScore = 0;
                    InterfaceManager.instance.mealImage.sprite = plate;
                    ChangeState(GameState.RESULT);
                }
                InterfaceManager.instance.UpdateChronoText(currentTime.ToString("00"));
            }
        }
        else if (state == GameState.TUTO)
        {
            if (Input.anyKeyDown)
            {
                ChangeState(GameState.INGAME);
            }
        }
    }

    public void ChangeState(GameState newState)
    {
        switch (newState)
        {
            case GameState.MENU:
                {
                    canPlay = false;
                    lastMinutes = false;
                }
                break;
            case GameState.INGAME:
                {
                    if(previousState == GameState.MENU)
                    {
                        AudioManager.instance.Play("Voice");
                        StartCoroutine(WaitForVoice());
                    }
                    canPlay = true;
                }
                break;
            case GameState.TUTO:
                {
                    canPlay = false;
                }
                break;
            case GameState.RESULT:
                {
                    canPlay = false;
                }
                break;
            default:
                {
                    throw new System.Exception("Switching to invalid state, aborting");
                }
        }
        previousState = state;
        state = newState;
        if (onStateChange != null) onStateChange.Invoke();
    }

    private IEnumerator WaitForVoice()
    {
        yield return new WaitForSeconds(AudioManager.instance.GetClipLength("Voice"));
        currentTime = roundTime;
        lastMinutes = true;
    }

}
