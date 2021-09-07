using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager instance;

    [SerializeField] private Text chronoText;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    public void UpdateChronoText(string newText)
    {
        chronoText.text = newText;
    }
}
