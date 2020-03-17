using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBubble : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        Invoke("RandomThing", 0.5f);
    }

    void RandomThing()
    {
        float randomTime = Random.Range(5, 10);
      animator.SetTrigger("goBubble");
        Invoke("RandomThing", randomTime);
    }
}
