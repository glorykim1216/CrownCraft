using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CardDeck : MonoBehaviour
{
    public Transform []Cards = new Transform[4];
    void Start()
    {
        Cards[0] = transform.FindChild("Cards").FindChild("Card_1");
        Cards[1] = transform.FindChild("Cards").FindChild("Card_2");
        Cards[2] = transform.FindChild("Cards").FindChild("Card_3");
        Cards[3] = transform.FindChild("Cards").FindChild("Card_4");
    }

    void Update()
    {

    }
}
