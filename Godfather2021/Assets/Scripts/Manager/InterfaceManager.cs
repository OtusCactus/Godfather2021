using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager instance;

    public Canvas canvas;
    [SerializeField] private Text chronoText;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject resultPanel;
    public GameObject gamePanel;

    public Text scoreText;
    public Image mealImage;

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
                    switch (GameManager.instance.state)
                    {
                        case GameState.MENU:
                            {
                                menuPanel.SetActive(true);
                                //gamePanel.SetActive(false);
                                resultPanel.SetActive(false);
                            }
                            break;
                        case GameState.INGAME:
                            {
                                menuPanel.SetActive(false);
                                gamePanel.SetActive(true);
                                resultPanel.SetActive(false);
                            }
                            break;
                        case GameState.PAUSE:
                            //
                            break;
                        case GameState.RESULT:
                            {
                                menuPanel.SetActive(false);
                                gamePanel.SetActive(false);

                                scoreText.text = "Score : " + LevelManager.instance.score.ToString() + "%";
                                //todo image aspect
                                //mealImage.color = LevelManager.instance.recipe.
                                resultPanel.SetActive(true);
                            }
                            break;
                        default:
                            break;
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

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
