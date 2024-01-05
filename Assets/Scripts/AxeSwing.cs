using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSwing : MonoBehaviour
{
    private Animator animator;
    public GameObject player;
    public MeshCollider axeCollider; 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && player.GetComponent<PlayerPickUpLogic>().inHandItem != null) {
            PerformSwing();
                }
    }

    public void PerformSwing()
    {
        animator.SetTrigger("Base_Attack");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tree")
        {
            other.gameObject.GetComponent<TreeLogic>().DamageTree(20);
        }
    }
}
