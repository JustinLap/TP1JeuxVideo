using System;
using UnityEngine;
using Collectibles;

public class CollectibleSpawner : MonoBehaviour
{
    [Header("Pools")]
    [SerializeField] private ObjectPool healthPool;
    [SerializeField] private ObjectPool missilesPool;
    [SerializeField] private ObjectPool armorPool;

    public void SpawnRandomCollectible(Vector3 position)
    {
        Collectible.CollectibleType type = (Collectible.CollectibleType)UnityEngine.Random.Range(0, 3);

        ObjectPool pool = type switch
        {
            Collectible.CollectibleType.Health => healthPool,
            Collectible.CollectibleType.Missile => missilesPool,
            Collectible.CollectibleType.Armor => armorPool,
            _ => null
        };

        if (pool == null) return;

        var collectible = pool.Get();
        if (collectible == null) return;

        collectible.transform.position = position + Vector3.up * 1f;
        collectible.transform.rotation = Quaternion.identity;

        var collectibleComponent = collectible.GetComponent<Collectible>();
        if (collectibleComponent != null)
        {
            collectibleComponent.Initialize(type);
        }
    }
}