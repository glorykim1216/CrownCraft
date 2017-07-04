using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoManager : MonoBehaviour
{
    void Start()
    {
        UI_Tools.Instance.ShowUI(eUIType.PF_UI_LOGO);
    }
}
