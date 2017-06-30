using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CardDeck : BaseObject
{
    public Transform[] Cards = new Transform[4];


    void Start()
    {
        Transform trans = FindInChild("Cards");

        for (int i = 0; i < 4; i++)
        {
            Cards[i] = trans.FindChild("Card_" + i);
        }
    }

    void Update()
    {

    }

    public void NextCard()
    {
        
    }
}
