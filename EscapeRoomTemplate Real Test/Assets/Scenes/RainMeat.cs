using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainMeat : MonoBehaviour
{
    [SerializeField] private GameObject[] trashPrefabs;
    [SerializeField] private Transform rainCenterTransform;
    [SerializeField] private float totalTimeForTrashRain;
    [SerializeField] private float rainRange;
    [SerializeField] private float rainIntervalMin;
    [SerializeField] private float rainIntervalMax;
    [SerializeField] private bool isTimeBased;
    [SerializeField] private int totalToSpawn;


    private float _timeRemainingBeforeSpawn;
    private int _numTrashPrefabs;
    private Vector3 _rainCenterPosition;
    private Vector3 _spawnPosition;
    private Transform _trashParent;

    private const string TRASH_PARENT = "[Trash]";
    

    private void Start()
    {
        if (trashPrefabs == null || trashPrefabs.Length == 0)
        {
            Debug.Log("trashPrefabs is null or empty. Must be set in editor.", this);
            enabled = false;
            gameObject.SetActive(false);
            return;
        }

        if (rainCenterTransform == null)
        {
            Debug.Log("rainCenterTransform is null or empty. Must be set in editor.", this);
            enabled = false;
            gameObject.SetActive(false);
            return;
        }

        _trashParent = new GameObject(TRASH_PARENT).transform;
        _rainCenterPosition = rainCenterTransform.position;
        _spawnPosition = GetRandomSpawnPosition();
        _timeRemainingBeforeSpawn = GetRandomRainInterval();
        _numTrashPrefabs = trashPrefabs.Length;

        StartCoroutine(StartTrashRain());
    }

    private Vector3 GetRandomSpawnPosition()
    {
        var randomX = Random.Range(-rainRange, rainRange);
        var randomZ = Random.Range(-rainRange, rainRange);

        var adjustedX = _rainCenterPosition.x + randomX;
        var adjustedZ = _rainCenterPosition.z + randomZ;
        
        _spawnPosition = new Vector3(adjustedX, _rainCenterPosition.y, adjustedZ);

        return _spawnPosition;
    }
    
    private float GetRandomRainInterval()
    {
        var randomInterval = Random.Range(rainIntervalMin, rainIntervalMax);
        return randomInterval;
    }
    
    private GameObject GetRandomTrashPrefab()
    {
        var randomIndex = Random.Range(0, _numTrashPrefabs);
        var randomPrefab = trashPrefabs[randomIndex];
        
        return randomPrefab;
    }

    private void SpawnTrash()
    {
        var trashPrefab = GetRandomTrashPrefab();
        var trashInstance = Instantiate(trashPrefab,_trashParent);
        trashInstance.transform.position = GetRandomSpawnPosition();
        trashInstance.SetActive(true);
        _timeRemainingBeforeSpawn = GetRandomRainInterval();
    }
    
    private IEnumerator StartTrashRain()
    {
        //if isTimeBased
        if (isTimeBased)
        {
            float duration = totalTimeForTrashRain;
            float totalTime = 0f;

            while (totalTime <= duration)
            {
                totalTime += Time.deltaTime;

                _timeRemainingBeforeSpawn -= Time.deltaTime;
                if (_timeRemainingBeforeSpawn <= 0)
                {
                    SpawnTrash();
                }

                yield return null;
            }
        }

        if (!isTimeBased)
        {
            for(int i=0; i<totalToSpawn; i++)
            {
                SpawnTrash();
                yield return null;
            }
        }
        
    }
    
}
