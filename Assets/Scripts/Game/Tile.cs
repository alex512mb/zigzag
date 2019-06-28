using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public void Destroy()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Hide");
        Destroy(gameObject, 2);
    }
}
