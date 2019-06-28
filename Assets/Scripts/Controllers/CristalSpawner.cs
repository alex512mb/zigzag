using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalSpawner : MonoBehaviour
{
    public TilesSpawner tilesSpawner;
    public Cristal prefabCristal;
    public CristalSpawningOption cristalSpawningOption;
    public int segmentsPerCristal = 5;


    private IEnumerator cristalSpawnEnumerator;
    private int indexCurrentSegment;
    private int indexNextSegment;

    public event System.Action<Cristal> onCristalSpawned;


    private void Awake()
    {
        tilesSpawner.onSegmentCreated += OnSegmentCreated;

        if (cristalSpawningOption == CristalSpawningOption.Random)
        {
            cristalSpawnEnumerator = new IntRandomEnumerator(segmentsPerCristal);
        }
        else
        {
            cristalSpawnEnumerator = new IntSequenceEnumerator(segmentsPerCristal);
        }
    }

    private int GetNextIndexOfSegment()
    {
        if (!cristalSpawnEnumerator.MoveNext())
        {
            cristalSpawnEnumerator.Reset();
        }
        int current = (int)cristalSpawnEnumerator.Current;

        return current;


    }
    private void OnSegmentCreated(TilesSpawner.TileSegment segment)
    {
        indexCurrentSegment++;
        int indexSegmentInBlock = indexCurrentSegment % segmentsPerCristal;
        if (indexSegmentInBlock == indexNextSegment)
        {
            Tile[] tiles = segment.GetTiles();
            Vector2 randomPos = tiles[Random.Range(0, tiles.Length)].transform.position;
            CreateCristal(randomPos);
        }

        if (indexSegmentInBlock == 0)
        {
            indexNextSegment = GetNextIndexOfSegment();
        }
    }
    private void CreateCristal(Vector2 pos)
    {
        Cristal cristal = Instantiate(prefabCristal, pos, prefabCristal.transform.rotation);

        onCristalSpawned?.Invoke(cristal);
    }

    
}

public enum CristalSpawningOption
{
    Random,
    Sequence,
}
