using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesSpawner : MonoBehaviour
{
    public Player player;
    public Tile prefabTile;

    [Range(1, 5)]
    public int widthRoad = 1;

    [Range(15, 20)]
    public int maxCountTilesInGame = 10;

    [Range(3, 15)]
    public int countTilesForBegin = 5;

    public event System.Action<TileSegment> onSegmentCreated;

    private List<TileSegment> segments = new List<TileSegment>();
    private Queue<Vector2> queuePositions = new Queue<Vector2>();
    private float deley = 1;
    private Vector2 currentPos;


    private void Awake()
    {
        if (widthRoad < 1)
        {
            Debug.LogWarning("Width of road must be greater than 1");
        }
        if (player == null)
        {
            Debug.LogWarning("player does not assigned");
        }

        if (prefabTile == null)
        {
            Debug.LogWarning("tile does not assigned");
        }

        deley = 1 / player.speed;
        currentPos = transform.position;

        CreateFirstSegments();
    }
    private void OnEnable()
    {
        StartCoroutine(ProgrammWork());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }


    private IEnumerator ProgrammWork()
    {
        while (true)
        {
            yield return new WaitForSeconds(deley);

            CreateNextSegment();
            if (segments.Count >= maxCountTilesInGame)
            {
                DestroyPreviousSegment();
            }
        }
    }
    private void CreateFirstSegments()
    {
        for (int i = 0; i < maxCountTilesInGame - countTilesForBegin; i++)
        {
            CreateNextSegment();
        }
    }
    private void DestroyPreviousSegment()
    {
        TileSegment segment = segments[0];
        int countTilesToDispose = segment.GetCountTiles();
        for (int i = 0; i < countTilesToDispose; i++)
        {
            queuePositions.Dequeue();
        }

        segment.DestroyTiles();
        segments.RemoveAt(0);
    }
    private void CreateNextSegment()
    {
        //determine definitions
        Vector2 nextPos;
        int additionalTiles = widthRoad - 1;
        bool isUpDirection = Random.Range(0, 2) == 1;

        //determine random direction
        if (isUpDirection)
        {
            nextPos = currentPos + Vector2.up;
        }
        else
        {
            nextPos = currentPos + Vector2.right;
        }

        List<Tile> tiles = new List<Tile>();

        //Create segment depends on difficult
        for (int x = 0; x < additionalTiles; x++)
        {
            float pos_x = nextPos.x - 1 - x;
            for (int y = 0; y < additionalTiles; y++)
            {
                float pos_y = nextPos.y + 1 + y;
                Vector2 pos = new Vector2(pos_x, pos_y);
                if (!ExistTileInPos(pos))
                {
                    Tile tile = SpawnTile(pos);
                    tiles.Add(tile);
                }
            }
        }

        //Create next tile
        currentPos = nextPos;
        if (!ExistTileInPos(nextPos))
        {
            Tile mainTile = SpawnTile(nextPos);
            tiles.Add(mainTile);
        }


        TileSegment segment = new TileSegment(nextPos, tiles.ToArray());
        segments.Add(segment);

        onSegmentCreated?.Invoke(segment);
    }
    private Tile SpawnTile(Vector2 pos)
    {
        Tile tile = Instantiate(prefabTile, pos, prefabTile.transform.rotation);
        tile.transform.parent = transform;
        queuePositions.Enqueue(pos);
        return tile;
    }
    private bool ExistTileInPos(Vector2 pos)
    {
        return queuePositions.Contains(pos);
    }


    public struct TileSegment
    {
        public Vector2 pos;
        private Tile[] tiles;
        public TileSegment(Vector2 pos, Tile[] tiles)
        {
            this.pos = pos;
            this.tiles = new Tile[tiles.Length];
            System.Array.Copy(tiles, this.tiles, tiles.Length);
        }
        public void DestroyTiles()
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].Destroy();
            }
        }
        public int GetCountTiles()
        {
            return tiles.Length;
        }
        public Tile[] GetTiles()
        {
            return tiles;
        }
    }
}
