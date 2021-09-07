using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager instance;

    [SerializeField] private Text chronoText;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        if (GameManager.instance)
        {
            GameManager.instance.onStateChange += () =>
            {
                if (this != null)
                {
                    if (GameManager.instance.state == GameState.MENU)
                    {
                        menuPanel.SetActive(true);
                        gamePanel.SetActive(false);
                    }
                    else if (GameManager.instance.state == GameState.INGAME)
                    {
                        menuPanel.SetActive(false);
                        gamePanel.SetActive(true);
                    }
                }
            };

        }
    }

    public void Play()
    {
        GameManager.instance.ChangeState(GameState.INGAME);
    }

    public void UpdateChronoText(string newText)
    {
        chronoText.text = newText;
    }
}
