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

    public List<GameObject> ingredientsInPan = new List<GameObject>();

    public CombinationResult mealServed;

    [SerializeField] private float actualTimerBurn;
    [SerializeField] private float maxTimerBurn;
    [SerializeField] private GameObject attention;

    [SerializeField] private Text nbIngredientsText;
    [SerializeField] private int nbIngredients = 0;

    public GameObject timers;


    public bool isOnFire = false;

    public Image statut;
    public Sprite prepared;
    public Sprite burnt;


    // Start is called before the first frame update
    void Start()
    {
        actualTimer = maxTimer;

        actualTimerBurn = maxTimerBurn;

        attention.SetActive(false);
        timers.SetActive(false);
        gameObject.transform.localScale = new Vector3(1, 1, 1);


        GetComponent<DragDrop>().onDraggingEnd += () =>
        {
            if (GetComponent<DragDrop>().droppedOnSlot)
            {
                //cookingTimer.TimerBack();
                //if (isOnFire)
                //{
                //    cookingTimer.ResetPosition();
                //}
                isOnFire = false;
            }
            else
            {
                if (isOnFire)
                {
                    //cookingTimer.mealServed = this;
                }
            }
        };

        statut.gameObject.SetActive(false);

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
                nbIngredients = 0;
                nbIngredientsText.gameObject.SetActive(false);

                statut.gameObject.SetActive(true);
                statut.sprite = prepared;

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
        timers.SetActive(false);
        statut.gameObject.SetActive(false);

        gameObject.transform.localScale = new Vector3(1, 1, 1);

        if (!GetComponent<DragDrop>().previousSlot.isOccupied)
        {
            GetComponent<RectTransform>().position = GetComponent<DragDrop>().previousPos;
        }
        else
        {
            for (int i = 0; i < InterfaceManager.instance.grid.transform.childCount; ++i)
            {
                if (!InterfaceManager.instance.grid.transform.GetChild(i).GetComponent<ItemSlot>().isOccupied)
                {
                    Vector2 correctPos = InterfaceManager.instance.grid.transform.GetChild(i).GetComponent<RectTransform>().position;
                    GetComponent<RectTransform>().position = correctPos;

                    InterfaceManager.instance.grid.transform.GetChild(i).GetComponent<ItemSlot>().isOccupied = true;
                    InterfaceManager.instance.grid.transform.GetChild(i).GetComponent<ItemSlot>().myItem = this.gameObject;
                }
            }
        }
    }

    public void StartTimer()
    {
        if(ingredientsInPan.Count != 0)
        {
            AudioManager.instance.Play("PanCooking");
            timers.SetActive(true);

            gameObject.transform.localScale = new Vector3(2, 2, 2);

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

            mealServed.image.color = new Color(mealServed.image.color.r, mealServed.image.color.g, mealServed.image.color.b, 0f);
        }

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

            statut.sprite = burnt;

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
        AudioManager.instance.Stop("PanCooking");
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

            AddIngredient(eventData.pointerDrag.gameObject);

        }
    }

    public void AddIngredient(GameObject objectToAdd)
    {
        ingredientsInPan.Add(objectToAdd);
        nbIngredients++;
        nbIngredientsText.text = "x" + nbIngredients;
        nbIngredientsText.gameObject.SetActive(true);

        //cookingTimer.timeStart = true;
        actualTimer = maxTimer;
    }

}
