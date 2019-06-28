using System.Collections;

public class IntSequenceEnumerator : IEnumerator
{
    private int lenght;
    private int current;
    object IEnumerator.Current => current;

    public IntSequenceEnumerator(int lenght)
    {
        this.lenght = lenght;
    }

    bool IEnumerator.MoveNext()
    {
        current++;
        return current < lenght;
    }

    void IEnumerator.Reset()
    {
        current = 0;
    }
}
