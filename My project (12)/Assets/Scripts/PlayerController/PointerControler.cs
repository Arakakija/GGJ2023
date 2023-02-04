using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerControler : MonoBehaviour
{
    private GameObject selectedObject;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Click(mousePosition);
    }

    void Click(Vector3 mousePosition)
    {
        if (Input.GetMouseButtonDown(0) && Physics2D.OverlapPoint(mousePosition))
        {
            Collider2D[] results = Physics2D.OverlapPointAll(mousePosition);
            for(int i = 0; i < results.Length; i++)
            {
                if (results[i].GetComponent<Clickeable>())
                {
                    results[i].GetComponent<Clickeable>().OnClick();
                }
            }
            
        }
    }
}
