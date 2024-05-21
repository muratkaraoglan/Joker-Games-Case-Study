using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Tile", fileName = "Tile Config")]
public class TileTypeScriptableObject : ScriptableObject
{
    [field: SerializeField] public Tile TilePrefab { get; private set; }
    [field: SerializeField] public float TileSpawnOffset { get; private set; }
    [field: SerializeField, Min(2)] public int TileCountOnEdge { get; private set; }


    [SerializeField] private List<TileTypeHolder> _tileTypes;

    public TileTypeHolder GetRandomTile()
    {
        return _tileTypes[UnityEngine.Random.Range(0, _tileTypes.Count)];
    }
    public List<TileTypeHolder> TileTypeHolderList => _tileTypes;
}

[Serializable]
public struct TileTypeHolder
{
    public string Name;
    public TileType Type;
    public Sprite TileSprite;
    public int MinAmount;
    public int MaxAmount;
    public ParticleSystem TileCollectParticlePrefab;
}

public enum TileType
{
    Empty,
    Apple,
    Pear,
    Strawberry
}
