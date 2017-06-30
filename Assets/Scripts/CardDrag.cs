using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrag : BaseObject
{
    GameObject go = null;
    public Transform ShrinkPoint;
    public Transform EndPoint;

    UISprite _sprite;
    Vector3 OrgPos;
    Vector3 OrgScale;

    Color OrgColor;

    bool IsField = false;

    string StrOrgSprite;

    void Start()
    {
        OrgPos = this.transform.position;
        OrgScale = this.transform.localScale;
        StrOrgSprite = SelfComponent<UISprite>().spriteName;// this.gameObject.GetComponent<UISprite>().spriteName;

        _sprite = SelfComponent<UISprite>();
    }

    // 마우스 오버
    void OnHover(bool isOver)
    {
        // 스프라이트 확대
        _sprite.cachedTransform.localScale = (isOver) ? Vector3.one * 1.2f : Vector3.one;
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
            gameObject.GetComponent<UISprite>().spriteName = StrOrgSprite;
            this.transform.position = OrgPos;

            if (IsField == true)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.tag == "Ground")
                    {
                        // 프리펩 생성
                        go = Resources.Load("Prefabs/" + StrOrgSprite) as GameObject;
                        //go.GetComponent<Actor>().TEAM_TYPE = eTeamType.TEAM_2;
                        GameObject temp = Instantiate(go, hit.point, go.transform.rotation);
                        //temp.transform.FindChild("Toon Knight-Brown").FindChild("Knight").GetComponent<SkinnedMeshRenderer>().materials[0].mainTexture = Resources.Load("Textures/ToonKnightBlue") as Texture;
                        
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
            if (transform.position.y < ShrinkPoint.position.y)
            {
                transform.localScale = Vector3.one;
            }
            else if (transform.position.y >= ShrinkPoint.position.y && transform.position.y <= EndPoint.position.y)
            {
                transform.localScale = Vector3.one * (1f - (transform.position.y - ShrinkPoint.position.y) / (EndPoint.position.y - ShrinkPoint.position.y));
            }
            else
            {
                // 카드 이미지 변경
                IsField = true;
                transform.localScale = OrgScale;
                SelfComponent<UISprite>().spriteName = StrOrgSprite + "PF";
            }
        }
        else
        {
            if (transform.position.y >= ShrinkPoint.position.y && transform.position.y <= EndPoint.position.y)
            {
                // 카드 이미지 복구
                IsField = false;
                transform.localScale = Vector3.zero;
                SelfComponent<UISprite>().spriteName = StrOrgSprite;
            }
        }
    }

}
