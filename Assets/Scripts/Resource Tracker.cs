using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTracker : MonoBehaviour
{

    public int woodCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void incWood(int amount)
    {
        woodCount += amount;
    }

    public bool decWood(int amount)
    {
        if (woodCount >= amount)
        {
            woodCount -= amount;
            return true;
        } else
        {
            return false;
        }
       
    }
}
