using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITools : MonoSingleton<UITools>
{
    // DontDestroy Root -> Loading Bar
    GameObject SubRoot = null;

    Dictionary<eUIType, GameObject> DicUI = new Dictionary<eUIType, GameObject>();

    // DontDestroy 전용
    Dictionary<eUIType, GameObject> DicSubUI = new Dictionary<eUIType, GameObject>();

    Camera UICamera = null;
    Camera UI_CAMERA
    {
        get
        {
            if (UICamera == null)
            {
                UICamera = NGUITools.FindCameraForLayer(LayerMask.NameToLayer("UI"));
            }
            return UICamera;
        }
    }

    GameObject GetUI(eUIType _uiType, bool isDontDestroy)
    {
        if (isDontDestroy == false)
        {
            if (DicUI.ContainsKey(_uiType) == true)
            {
                return DicUI[_uiType];
            }
        }
        else
        {
            if (DicSubUI.ContainsKey(_uiType) == true)
            {
                return DicSubUI[_uiType];
            }
        }

        GameObject makeUI = null;
        GameObject prefabUI = Resources.Load("Prefabs/UI/" + _uiType.ToString()) as GameObject;

        if (prefabUI != null)
        {
            if (isDontDestroy == false)
            {
                // 사라지게
                // UICamera Child
                makeUI = NGUITools.AddChild(UI_CAMERA.gameObject, prefabUI);
                DicUI.Add(_uiType, makeUI);
            }
            else
            {
                // SubRoot를 태워서 사라지지 않게
                // UITools 아래에 SubRoot
                // SubRoot Child
                if (SubRoot == null)
                {
                    GameObject root = Resources.Load("Prefabs/UI/SubUI_Root") as GameObject;

                    // this :: UITools : Monosingleton ( DontDestroy )
                    SubRoot = NGUITools.AddChild(this.gameObject, root);
                    SubRoot.layer = LayerMask.NameToLayer("UI");
                }

                makeUI = NGUITools.AddChild(SubRoot, prefabUI);
                DicSubUI.Add(_uiType, makeUI);
            }
            makeUI.SetActive(false);    // 꺼서 반환
        }
        return makeUI;
    }

    public GameObject ShowUI(eUIType _uiType, bool isSub = false)
    {
        GameObject showObject = GetUI(_uiType, isSub);
        if (showObject != null && showObject.activeSelf == false)
        {
            showObject.SetActive(true);
        }

        return showObject;
    }

    public void HideUI(eUIType _uiType, bool isSub = false)
    {
        GameObject showObject = GetUI(_uiType, isSub);
        if (showObject != null && showObject.activeSelf == true)
        {
            showObject.SetActive(false);
        }
    }

    public void ShowLoadingUI(float value)
    {
        GameObject loadingUI = GetUI(eUIType.PF_UI_LOADING, true);
        if (loadingUI == null)
            return;

        if (loadingUI.activeSelf == false)
            loadingUI.SetActive(true);

        UI_LoadingBar loading = loadingUI.GetComponent<UI_LoadingBar>();

        if (loading == null)
            return;

        loading.SetValue(value);
    }
    public void Clear()
    {
        foreach (KeyValuePair<eUIType, GameObject> pair in DicUI)
        {
            Destroy(pair.Value);
        }

        DicUI.Clear();
    }
}
