using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    bool IsInit = false;
    [SerializeField]
    List<string> playerDeck = new List<string>();

    public List<string> PLAYERDECK
    {
        get { return playerDeck; }
    }

    public void DeckInit()
    {
        if (IsInit == true)
            return;
    }

    public void GetLocalData()
    {
        string instanceStr = PlayerPrefs.GetString(ConstValue.LocalSave_DeckInstance, string.Empty);

        string[] array = instanceStr.Split('|');

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].Length <= 0)
                continue;

            AddCard(array[i]);
        }
    }

    public void AddCard(string CardKey)
    {
        for (int i = 0; i < playerDeck.Count; i++)
        {
            if (playerDeck[i] == CardKey)
            {
                playerDeck.Add(CardKey);
            }
        }
    }

    public void DeleteCard(string CardKey)
    {
        for (int i = 0; i < playerDeck.Count; i++)
        {
            if (playerDeck[i] == CardKey)
            {
                playerDeck.RemoveAt(i);
            }
        }
    }
    public void SetLocalData()
    {
        string resultStr = string.Empty;

        for (int i = 0; i < playerDeck.Count; i++)
        {
            string DeckStr = string.Empty;
            DeckStr += playerDeck[i].ToString();

            if (i != playerDeck.Count - 1)
                DeckStr += "|";

            resultStr += DeckStr;
        }
        PlayerPrefs.SetString(ConstValue.LocalSave_DeckInstance, resultStr);
        Debug.Log(resultStr);
    }
}