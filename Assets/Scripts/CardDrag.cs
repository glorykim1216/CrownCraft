using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrag : BaseObject
{
    GameObject go = null;
    //public Transform ShrinkPoint;
    //public Transform EndPoint;

    float ShrinkPoint = -420.0f;
    float EndPoint = -350.0f;

    UISprite _sprite;

    Transform redZone;

    Vector3 OrgPos;
    Vector3 OrgScale;

    Color OrgColor;

    bool IsField = false;
    bool IsRedZone = false;

    string spriteName;

    public void Init(Vector3 _orgPos, string _name, Transform _redZone)
    {
        //OrgPos = this.transform.position;
        //OrgScale = this.transform.localScale;
        //StrOrgSprite = SelfComponent<UISprite>().spriteName;

        _sprite = SelfComponent<UISprite>();
        redZone = _redZone;

        this.transform.localPosition = _orgPos;

        OrgPos = _orgPos;
        OrgScale = Vector3.one;
        spriteName = _name;
        SelfComponent<UISprite>().spriteName = spriteName;


    }

    public void RePos(Vector3 _orgPos)
    {
        OrgPos = _orgPos;
        _sprite.transform.localScale = Vector3.one;
    }

    // 마우스 오버
    void OnHover(bool isOver)
    {
        // 스프라이트 확대
        _sprite.transform.localScale = (isOver) ? Vector3.one * 1.2f : Vector3.one;
    }

    // 드래그
    void OnDrag(Vector2 delta)
    {
        //if (IsField == false)
        //   _sprite.transform.localPosition += (Vector3)delta;
        //else
        Vector3 _pos = UICamera.mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (_pos.y < 0)
        {
            _sprite.transform.position = _pos;
            IsRedZone = false;
        }
        else
        {
            _pos.y = 0;
            _sprite.transform.position = _pos;
            IsRedZone = true;
        }

        CardShrink();
    }

    void OnPress(bool isPressed)
    {
        if (isPressed)
        {
            OrgColor = _sprite.color;
            _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 0.5f);
            redZone.gameObject.SetActive(true);
        }
        else if (!isPressed)
        {
            _sprite.color = OrgColor;
            gameObject.GetComponent<UISprite>().spriteName = spriteName;
            this.transform.localPosition = OrgPos;

            if (IsField == true)
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    Vector3 _pos = hit.point;

                    if (IsRedZone == true)
                    {
                        _pos.z = -2;
                    }

                    // 프리펩 생성
                    go = Resources.Load("Prefabs/Actors/" + spriteName) as GameObject;

                    Instantiate(go, _pos, go.transform.rotation);

                    UI_Manager.Instance.MoveCard(OrgPos, spriteName);
                    gameObject.SetActive(false);
                    redZone.gameObject.SetActive(false);

                    Debug.Log(hit.point);

                }
            }

        }
    }

    void CardShrink()
    {
        if (IsField == false)
        {
            if (transform.localPosition.y < ShrinkPoint)
            {
                transform.localScale = Vector3.one;
            }
            else if (transform.localPosition.y >= ShrinkPoint && transform.localPosition.y <= EndPoint)
            {
                transform.localScale = Vector3.one * (1f - (transform.localPosition.y - ShrinkPoint) / (EndPoint - ShrinkPoint));
            }
            else
            {
                // 카드 이미지 변경
                IsField = true;
                transform.localScale = OrgScale;
                SelfComponent<UISprite>().spriteName = spriteName + "_PF";
            }
        }
        else
        {
            if (transform.localPosition.y >= ShrinkPoint && transform.localPosition.y <= EndPoint)
            {
                // 카드 이미지 복구
                IsField = false;
                transform.localScale = Vector3.zero;
                SelfComponent<UISprite>().spriteName = spriteName;
            }
        }
    }

}
