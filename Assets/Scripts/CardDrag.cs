using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrag : BaseObject
{
    GameObject go = null;
    //public Transform ShrinkPoint;
    //public Transform EndPoint;

    float ShrinkPoint = -420.0f;
    float EndPoint = -300.0f;

    UISprite _sprite;
    Vector3 OrgPos;
    Vector3 OrgScale;

    Color OrgColor;

    bool IsField = false;

    string spriteName;

    public void Init(Vector3 _orgPos, string _name)
    {
        //OrgPos = this.transform.position;
        //OrgScale = this.transform.localScale;
        //StrOrgSprite = SelfComponent<UISprite>().spriteName;

        _sprite = SelfComponent<UISprite>();

        this.transform.localPosition = _orgPos;

        OrgPos = _orgPos;
        OrgScale = Vector3.one;
        spriteName = _name;
        SelfComponent<UISprite>().spriteName = spriteName;

    }

    public void RePos(Vector3 _orgPos)
    {
        OrgPos = _orgPos;
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
        _sprite.transform.position = UICamera.mainCamera.ScreenToWorldPoint(Input.mousePosition);

        CardShrink();
    }

    void OnPress(bool isPressed)
    {
        if (isPressed)
        {
            OrgColor = _sprite.color;
            _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 0.5f);
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
                    //if (hit.collider.tag == "Ground")
                    {
                        // 프리펩 생성
                        go = Resources.Load("Prefabs/Actors/" + spriteName) as GameObject;
                        //go.GetComponent<Actor>().TEAM_TYPE = eTeamType.TEAM_2;
                        GameObject temp = Instantiate(go, hit.point, go.transform.rotation);
                        //temp.transform.FindChild("Toon Knight-Brown").FindChild("Knight").GetComponent<SkinnedMeshRenderer>().materials[0].mainTexture = Resources.Load("Textures/ToonKnightBlue") as Texture;

                        UI_Manager.Instance.MoveCard(OrgPos);
                        gameObject.SetActive(false);
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
