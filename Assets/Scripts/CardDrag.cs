using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrag : BaseObject
{
    GameCharacter gameCharacter = null;
    //public Transform ShrinkPoint;
    //public Transform EndPoint;

    float ShrinkPoint = -420.0f;
    float EndPoint = -350.0f;

    UISprite _sprite;
    UISprite _coolTime;

    Transform redZone;

    Vector3 OrgPos;
    Vector3 OrgScale;

    Color OrgColor;

    BoxCollider boxColl;

    bool IsField = false;
    bool IsRedZone = false;
    bool IsUse = false;
    string spriteName;

    public void Update()
    {
        if (IsUse == true)
        {
            _coolTime.fillAmount = 1 - GameManager.Instance.MANA * 1 / (0.1f * (float)gameCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.COST));
            if (_coolTime.fillAmount <= 0)
            {
                boxColl.enabled = true;
            }
            else
            {
                if (boxColl.enabled == true)
                    boxColl.enabled = false;
            }
        }
    }

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

        gameCharacter = CharacterManager.Instance.AddCharacter(_name);

        _coolTime = FindInChild("CoolTime").GetComponent<UISprite>();
        boxColl = transform.GetComponent<BoxCollider>();
    }

    // OrgPos 위치, CoolTime 활성화
    public void RePos(Vector3 _orgPos)
    {
        OrgPos = _orgPos;
        _sprite.transform.localScale = Vector3.one;
        IsUse = true;
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
                if (GameManager.Instance.MANA < (float)gameCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.COST) * 0.1f)
                {
                    Debug.Log("마나 부족");
                }
                else
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
                        //--------------------------------------------------------------------------------------------------
                        //Test Enmey 생성
                        if (_pos.z > 10)
                            _pos.z += 1;
                        _pos.z *= -1;
                        // 프리펩 생성
                        GameObject Prefab = Resources.Load("Prefabs/Actors/Enemy/" + spriteName) as GameObject;
                        GameObject a = Instantiate(Prefab, _pos, Prefab.transform.rotation);


                        //--------------------------------------------------------------------------------------------------
                        // 서버에 생성을 알림
                        GameClient.Instance.AddUnit(_pos, spriteName);

                        UI_Manager.Instance.MoveCard(OrgPos, spriteName);
                        gameObject.SetActive(false);
                        redZone.gameObject.SetActive(false);

                        GameManager.Instance.DecreaseMana((float)gameCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.COST) * 0.1f);

                        Debug.Log(hit.point);
                    }
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
                transform.localScale = OrgScale * 3 + new Vector3(0, -1.5f, 0); // 크기 변경
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
