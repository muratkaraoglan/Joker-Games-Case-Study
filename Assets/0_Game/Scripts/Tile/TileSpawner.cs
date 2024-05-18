using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private TileTypeScriptableObject _tileConfig;
    [SerializeField] private float _tileSpawnAnimatiomTime;

    Vector3[] spawnDirections = new Vector3[] { Vector3.forward, Vector3.right, Vector3.back, Vector3.left };
    private void Start()
    {
        //transform.position = new Vector3(_tileCountOnEdge, 2f, _tileCountOnEdge);// for animation
        Vector3 targetPosition = Vector3.zero;
        Vector3 spawnOffset = Vector3.zero;
        Tile backTile = null;
        Tile firstTile = null;
        int count = 0;
        for (int i = 0; i < spawnDirections.Length; i++)
        {
            for (int j = 0; j < _tileConfig.TileCountOnEdge; j++)
            {
                count++;
                targetPosition = spawnDirections[i] * j * _tileConfig.TileSpawnOffset + spawnOffset;

                Tile tile = Instantiate(_tileConfig.TilePrefab, targetPosition, Quaternion.identity);
                tile.Init(_tileConfig.GetRandomTile(), spawnDirections[i], count);
                if (backTile != null)
                {
                    backTile.SetNextTile(tile);
                }
                else
                {
                    firstTile = tile;
                    firstTile.Reset();
                }
                backTile = tile;
                //Transform tile = Instantiate(_tilePrefab, targetPosition, Quaternion.identity).transform;// for animation
                //StartCoroutine(SpawnAnimation(tile, tile.position, targetPosition, _tileSpawnAnimatiomTime));// for animation
                //yield return new WaitForSeconds(.2f);// for animation
            }
            spawnOffset = targetPosition + spawnDirections[i] * _tileConfig.TileSpawnOffset;
        } 
        backTile.SetNextTile(firstTile);
    }

    IEnumerator SpawnAnimation(Transform target, Vector3 from, Vector3 to, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime <= duration)
        {
            target.position = Vector3.Lerp(from, to, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        target.position = to;
    }


}
