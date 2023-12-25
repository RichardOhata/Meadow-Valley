using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLogic : MonoBehaviour
{
    public float hp = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0) {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            Invoke("DestroyTree", 15.0f);
        }
    }

    public void DamageTree(float dmg)
    {
        Debug.Log(hp);
        hp -= dmg;
    }

    void DestroyTree()
    {
        Destroy(gameObject);
    }
}
