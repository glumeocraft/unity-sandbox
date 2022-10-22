using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _baseTile, _grassTile, _mountainTile, _swampTile;
    private Dictionary<Vector2, Tile> _tiles;
    public static GridManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void GenerateGrid()
    {
        SpriteRenderer spriteRenderer = _baseTile.GetComponent<SpriteRenderer>();
        float horizontalSize = spriteRenderer.sprite.bounds.size.x + 0.002f;
        float verticalSize = spriteRenderer.sprite.bounds.size.y;

        _tiles = new Dictionary<Vector2, Tile>();

        Debug.Log($"Horizontal size is {horizontalSize}");
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                //set horizontal difference
                float y;
                if (j % 2 == 0) y = (i * horizontalSize) + horizontalSize / 2;
                else y = (i * horizontalSize) + horizontalSize;

                //set vertical difference
                float x;
                x = j * verticalSize * 0.764f;

                //get random tile from swamp,grass,mountain
                var randomNumber = Random.Range(0,10);
                Tile randomTile;
                if (randomNumber > 8) randomTile = _mountainTile;
                else if (randomNumber > 7) randomTile = _swampTile;
                else randomTile = _grassTile;
                
                //spawn tile
                var spawnedTile = Instantiate(randomTile, new Vector3(y, x), Quaternion.identity);
                spawnedTile.name = $"Tile {i} {j}";
                if (i > 13)
                {
                    spawnedTile.Playable = false;
                } else
                {
                    spawnedTile.Playable = true;
                }
                _tiles[new Vector2(i, j)] = spawnedTile;
            }

        }
        GameManager.Instance.UpdateGameState(GameManager.GameState.SpawnUnits);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }

    public Tile GetTeam1SpawnTile()
    {
        return _tiles.Where(t => t.Key.x == 2).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetTeam2SpawnTile()
    {
        return _tiles.Where(t => t.Key.x == 4).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetTeam3SpawnTile()
    {
        return _tiles.Where(t => t.Key.x == 6).OrderBy(t => Random.value).First().Value;
    }

    public void DestoryGrid()
    {
        foreach (var key in _tiles.Keys)
        {
            if (_tiles[key].OccupiedArmies.Count > 0)
            {
                foreach (var army in _tiles[key].OccupiedArmies)
                { 
                    if (army.soldiers.Count > 0)
                    {
                        foreach (var soldier in army.soldiers)
                        {
                            Destroy(soldier.gameObject);
                        }
                        army.soldiers.Clear();
                    }
                    Destroy(army.gameObject);
                }
            }
            Destroy(_tiles[key].gameObject);
        }
        _tiles.Clear();
    }
}
