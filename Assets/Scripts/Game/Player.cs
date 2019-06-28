using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    public Vector2 firstDirection = Vector2.up;
    public Vector2 secondDirection = Vector2.right;

    private Vector2 currentDirection;
    private bool isFirstDirection = true;

    public event Action OnEnterToTile;
    public event Action OnExitFromTile;


    private void Awake()
    {
        currentDirection = firstDirection;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Tile>())
            OnEnterToTile?.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Tile>())
            OnExitFromTile?.Invoke();
    }
    private void Update()
    {
        Vector2 currForce = speed * Time.deltaTime * currentDirection.normalized;
        transform.Translate(currForce);

        if (Input.GetMouseButtonDown(0))
        {
            SwitchDirection();
        }
    }


    private void SwitchDirection()
    {
        isFirstDirection = !isFirstDirection;
        currentDirection = isFirstDirection ? firstDirection : secondDirection;
    }
}
