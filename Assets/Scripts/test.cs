using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Rigidbody rigid;
    float MoveLine = -1.6f;
    float rotate = 45f;

    void Start()
    {
        rigid = transform.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(temp);
        }
    }
    private void FixedUpdate()
    {
        Run();
        Turn();
    }
    void Run()
    {
        rigid.MovePosition(rigid.position + transform.forward * Time.deltaTime);
    }
    void Turn()
    {
        //if (Vector3.Distance(transform.position, Line.transform.position) > 0)
        if (transform.position.x - MoveLine > -1.0f && transform.position.x - MoveLine < 1.0f)
        {
            rigid.rotation = Quaternion.Slerp(rigid.rotation, Quaternion.Euler(0, 0, 0), 2 * Time.deltaTime);
        }
        else if (transform.position.x - MoveLine > 0)
        {
            rigid.rotation = Quaternion.Slerp(rigid.rotation, Quaternion.Euler(0, -rotate, 0), 2 * Time.deltaTime);
        }
        else
            rigid.rotation = Quaternion.Slerp(rigid.rotation, Quaternion.Euler(0, rotate, 0), 2 * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cube")
        {
            rotate = 90f;
        }
    }
}
