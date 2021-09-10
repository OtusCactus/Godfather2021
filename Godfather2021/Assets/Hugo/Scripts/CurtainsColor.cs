using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurtainsColor : MonoBehaviour
{
    public List<Color> curtainsColor;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.onStateChange += () => 
        {
            if(GameManager.instance.state == GameState.RESULT)
            {
                ChangeAspectCurtains();
            }
        };

        ChangeAspectCurtains();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAspectCurtains()
    {
        
        if (LevelManager.instance.finalScore >= 75)
        {
            AudioManager.instance.Play("WinGood");
            gameObject.GetComponent<Image>().color = curtainsColor[0];
        }
        else if (LevelManager.instance.finalScore >= 50)
        {
            AudioManager.instance.Play("WinPrettyGood");
            gameObject.GetComponent<Image>().color = curtainsColor[1];
        }
        else if (LevelManager.instance.finalScore >= 25)
        {
            AudioManager.instance.Play("WinNotGood");
            gameObject.GetComponent<Image>().color = curtainsColor[2];
        }
        else
        {
            AudioManager.instance.Play("WinBad");
            gameObject.GetComponent<Image>().color = curtainsColor[3];
        }
    }
}
