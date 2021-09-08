using UnityEngine;
using Rewired;

public enum GameState
{
    MENU,
    INGAME,
    PAUSE,
    RESULT
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Player player;

    public System.Action onStateChange;

    [Header("Settings")]
    [SerializeField] private float roundTime = 60;
    private float currentTime = 0;
    public GameState state;
    private GameState previousState;
    private bool canPlay = false;

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
        if (canPlay)
        {
            currentTime -= Time.deltaTime;
            if(currentTime < 0)
            {
                ChangeState(GameState.MENU);
            }
            InterfaceManager.instance.UpdateChronoText(currentTime.ToString("00"));
        }
    }

    public void ChangeState(GameState newState)
    {
        switch (newState)
        {
            case GameState.MENU:
                {
                    canPlay = false;
                }
                break;
            case GameState.INGAME:
                {
                    if(previousState == GameState.MENU)
                    {
                        currentTime = roundTime;
                    }
                    canPlay = true;
                }
                break;
            case GameState.PAUSE:
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
}
