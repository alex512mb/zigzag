using UnityEngine;

[DefaultExecutionOrder(-99)]
public class CristalController : MonoBehaviour
{
    public CristalSpawner cristalSpawner;


    private int countCristalCollected;
    public int scoresPerOneCristal = 1;


    public event System.Action<int> onCountScoresChanged;


    private void Awake()
    {
        cristalSpawner.onCristalSpawned += OnCristalSpawned;
    }


    private void OnCristalSpawned(Cristal cristal)
    {
        cristal.onCollected += OnCristalCollected;
    }
    private void OnCristalCollected()
    {
        countCristalCollected++;
        onCountScoresChanged?.Invoke(countCristalCollected * scoresPerOneCristal);
    }
}



