using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
 
public class CloseOnContextLoss : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool inContext;
    GameObject myGO;

    private void Awake()
    {
        myGO = gameObject;
    }
 
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !inContext)
        {
            UIManager.OnCloseDiary?.Invoke();
            myGO.SetActive(inContext);
        }
    }
 
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("entro");
        inContext = true;
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Salio");
        inContext = false;
    }
}