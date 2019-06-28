using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : MonoBehaviour
{
    public event System.Action onCollected;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onCollected?.Invoke();
        Destroy(gameObject);
    }
}
