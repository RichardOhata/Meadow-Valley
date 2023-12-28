using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLogic : MonoBehaviour
{
    public float hp = 100;
    public AudioSource woodChopSFX;
    public GameObject woodPrefab;
    public bool spawnedWood = false;

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0) {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            DropWood();
            Invoke("DestroyTree", 15.0f);
        }
    }

    public void DamageTree(float dmg)
    {
        woodChopSFX.Play();
        hp -= dmg;
    }
    void DropWood()
    {
       if (!spawnedWood)
        {
            int newWoodNum = Random.Range(1, 4);
            for (int i = 0; i < newWoodNum; i++)
            {
                GameObject newWood = Instantiate(woodPrefab, transform.parent.position, transform.parent.rotation);
                newWood.transform.parent = null;
            }
            spawnedWood = true;
        }
    }
    void DestroyTree()
    {
        Destroy(transform.parent.gameObject);
    }

  
}
