using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingTimer : MonoBehaviour
{
    public float actualTimer;
    public float maxTimer;
    public Image timerImage;

    //[SerializeField] private bool mixer;
    //[SerializeField] private bool pan;

    public bool timeStart = false;

    public List<GameObject> ingredientsInPan = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        actualTimer = maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        
        //if (Input.GetKeyDown(KeyCode.P) && pan && !mixer)
        //{
        //    timeStart = true;
        //    actualTimer = maxTimer;
        //}

        //if (Input.GetKeyDown(KeyCode.M) && !pan && mixer)
        //{
        //    timeStart = true;
        //    actualTimer = maxTimer;
        //}

        if (timeStart)
        {
            actualTimer -= Time.deltaTime;
            timerImage.fillAmount = actualTimer / maxTimer;
            if(actualTimer <= 0)
            {
                for(int i = 0; i < ingredientsInPan.Count; i++)
                {
                    ingredientsInPan[i].GetComponent<IngredientManager>().state.isCooked = true;
                }

                timeStart = false;
                actualTimer = 0;
                StartCoroutine("TimerBack");
            }
        }
    }

    IEnumerator TimerBack()
    {
        yield return new WaitForSeconds(2f);

        actualTimer = maxTimer;
        timerImage.fillAmount = actualTimer / maxTimer;
    }
}
