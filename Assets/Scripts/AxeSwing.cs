using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSwing : MonoBehaviour
{
    private Animator animator;
    public GameObject player;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tree")
        {
            collision.gameObject.GetComponent<TreeLogic>().DamageTree(20);
        }
        Debug.Log(collision.gameObject.name);
    }
}
