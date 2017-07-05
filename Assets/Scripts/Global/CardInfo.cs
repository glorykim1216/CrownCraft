using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInfo : BaseObject
{
    [SerializeField]
    string TemplateKey = string.Empty;

    // Use this for initialization
    void Start()
    {
        //GameCharacter gameCharacter = CharacterManager.Instance.AddCharacter(TemplateKey);
        Transform temp = FindInChild("CardImage");
        temp.GetComponent<UISprite>().spriteName = TemplateKey;
    }


}
