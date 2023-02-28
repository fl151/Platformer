using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnPoint : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [Range(0, 100)]
    [SerializeField] private int _chance;

    private void Awake()
    {
        if (Random.Range(0, 101) <= _chance)
        {
            Instantiate(_prefab, gameObject.transform);
        }
    }
}
