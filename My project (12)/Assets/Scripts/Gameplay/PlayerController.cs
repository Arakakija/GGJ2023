using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public PlayerInventory inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ResetPlayer()
    {
        inventory.ResetInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
