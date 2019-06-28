using System.Collections;
using UnityEngine;

public class IntRandomEnumerator : IEnumerator
{
    private int lenght;
    private int current;
    object IEnumerator.Current => current;

    public IntRandomEnumerator(int lenght)
    {
        this.lenght = lenght;
    }

    bool IEnumerator.MoveNext()
    {
        current = Random.Range(0, lenght);
        return true;
    }

    void IEnumerator.Reset()
    {
        current = 0;
    }
}
