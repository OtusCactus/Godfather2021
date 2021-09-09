using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CookingTimer : MonoBehaviour, IDropHandler
{
    public float actualTimer;
    public float maxTimer;
    public Image timerImage;

    //[SerializeField] private bool mixer;
    //[SerializeField] private bool pan;

    public bool timeStart = false;
    public bool timeStart2 = false;

    [SerializeField] private GameObject mealToSpawn;
    [SerializeField] private GameObject burntMeal;

    public List<GameObject> ingredientsInPan = new List<GameObject>();

    public CombinationResult mealServed;

    [SerializeField] private float actualTimerBurn;
    [SerializeField] private float maxTimerBurn;
    [SerializeField] private GameObject attention;

    [SerializeField] private Text nbIngredientsText;
    [SerializeField] private int nbIngredients = 0;

    // Start is called before the first frame update
    void Start()
    {
        actualTimer = maxTimer;

        actualTimerBurn = maxTimerBurn;

        attention.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (timeStart)
        {
            actualTimer -= Time.deltaTime;
            timerImage.fillAmount = actualTimer / maxTimer;
            gameObject.GetComponent<Animator>().SetBool("isCooking", true);

            if(actualTimer <= 0)
            {

                timeStart = false;
                actualTimer = 0;

                mealServed.ChangeAspect();
                mealServed.gameObject.SetActive(true);
                nbIngredientsText.gameObject.SetActive(false);

                timeStart2 = true;
            }
        }

        if (timeStart2 && mealServed != null)
        {
            BurnTheMeal();
        }
    }

    public void ResetPosition()
    {
        GetComponent<RectTransform>().position = GetComponent<DragDrop>().previousPos;
    }

    public void StartTimer()
    {
        for (int i = 0; i < ingredientsInPan.Count; i++)
        {
            ingredientsInPan[i].GetComponent<IngredientManager>().state.isCooked = true;
            ingredientsInPan[i].SetActive(false);
        }
        timeStart = true;

        GameObject go = Instantiate(mealToSpawn, GetComponent<RectTransform>().position, Quaternion.identity);
        go.transform.SetParent(InterfaceManager.instance.gamePanel.transform, true);
        mealServed = go.GetComponent<CombinationResult>();
        go.GetComponent<RectTransform>().localScale = Vector3.one;
        mealServed.cookingTimer = this;
        mealServed.isOnPan = true;
        go.SetActive(false);

        mealServed.Initialize(ingredientsInPan);

    }

    public void BurnTheMeal()
    {

        actualTimerBurn -= Time.deltaTime;

        if (actualTimerBurn <= maxTimerBurn / 2)
        {
            Debug.Log("ATTENTION");
            attention.gameObject.SetActive(true);
        }

        if (actualTimerBurn <= 0)
        {
            actualTimerBurn = 0;
            mealServed.isBurnt = true;
            mealServed.ChangeAspect();
            mealServed.mealScore = Mathf.Ceil(mealServed.mealScore / 2);

            timeStart2 = false;
            attention.gameObject.SetActive(false);

            TimerBack();
        }
    }

    public void TimerBack()
    {
        actualTimer = maxTimer;
        timerImage.fillAmount = actualTimer / maxTimer;
        timeStart = false;

        actualTimerBurn = maxTimerBurn;
        timeStart2 = false;
        attention.gameObject.SetActive(false);

        gameObject.GetComponent<Animator>().SetBool("isCooking", false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        //if an object has been droppped, put it in the right place
        if (eventData.pointerDrag != null && !eventData.pointerDrag.GetComponent<Knife>() && !eventData.pointerDrag.GetComponent<CombinationResult>())
        {
            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            eventData.pointerDrag.GetComponent<DragDrop>().previousPos = GetComponent<RectTransform>().position;
            eventData.pointerDrag.GetComponent<DragDrop>().droppedOnSlot = true;

            eventData.pointerDrag.gameObject.SetActive(false);

            ingredientsInPan.Add(eventData.pointerDrag);
            nbIngredients++;
            nbIngredientsText.text = "x" + nbIngredients;
            nbIngredientsText.gameObject.SetActive(true);

            //cookingTimer.timeStart = true;
            actualTimer = maxTimer;
        }
    }

}
