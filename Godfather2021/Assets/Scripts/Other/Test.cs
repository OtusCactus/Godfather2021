using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Test : MonoBehaviour
{

    public static Player player;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetButtonDown("Right"))
        {
            print("miaou");
        }
    }
}
