using UnityEngine;
using Rewired;

public enum GameState
{
    MENU,
    INGAME,
    PAUSE
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Player player;

    [Header("Settings")]
    [SerializeField] private float roundTime = 60;
    private float currentTime = 0;
    private GameState state;
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
        player = ReInput.players.GetPlayer(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(GameState.INGAME);
        currentTime = roundTime;
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
                    print("in menu");
                    canPlay = false;
                }
                break;
            case GameState.INGAME:
                {
                    canPlay = true;
                }
                break;
            case GameState.PAUSE:
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
    }
}
