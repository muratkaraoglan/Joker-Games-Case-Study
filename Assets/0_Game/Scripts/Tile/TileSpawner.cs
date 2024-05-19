using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-9)]
public class TileSpawner : MonoBehaviour
{
    [SerializeField] private TileTypeScriptableObject _tileConfig;
    [SerializeField] private float _tileSpawnMoveTargetTime = .3f;

    Tile firstTile = null;
    Vector3[] spawnDirections = new Vector3[] { Vector3.forward, Vector3.right, Vector3.back, Vector3.left };
    private IEnumerator Start()
    {
        //transform.position = new Vector3(_tileCountOnEdge, 2f, _tileCountOnEdge);// for animation
        Vector3 targetPosition = Vector3.zero;
        Vector3 spawnOffset = Vector3.zero;
        Tile backTile = null;

        int count = 0;
        for (int i = 0; i < spawnDirections.Length; i++)
        {
            for (int j = 0; j < _tileConfig.TileCountOnEdge; j++)
            {
                count++;
                targetPosition = spawnDirections[i] * j * _tileConfig.TileSpawnOffset + spawnOffset;

                Tile tile = Instantiate(_tileConfig.TilePrefab, targetPosition + Vector3.up * 2f, Quaternion.identity);
                tile.name = count.ToString();
                Vector3 lookDirection = Quaternion.Euler(0, 90, 0) * spawnDirections[i];

                bool isFirstItemInRow = j == 0;
                if (isFirstItemInRow) lookDirection = Quaternion.Euler(0, 45, 0) * spawnDirections[i];

                tile.Init(_tileConfig.GetRandomTile(), lookDirection, count, isFirstItemInRow);

                Tween move = new MoveTween(tile.transform, tile.transform.position, targetPosition, _tileSpawnMoveTargetTime, Easing.Linear);

                if (backTile != null)
                {
                    backTile.SetNextTile(tile);
                }
                else
                {
                    firstTile = tile;
                    firstTile.Reset();//Starting tile set empty
                }
                backTile = tile;
                yield return new WaitForSeconds(.1f);

            }
            spawnOffset = targetPosition + spawnDirections[i] * _tileConfig.TileSpawnOffset;
        }
        backTile.SetNextTile(firstTile);


    }

    public Tile FirstTile => firstTile;

}
