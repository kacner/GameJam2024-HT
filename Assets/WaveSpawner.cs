using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] AllEnemyes;
    public int[] enemycost;

    [Space(40)]

    public float waveIntensity = 10f;
    private float CurrentWaveIntensity;
    [SerializeField] private GameObject target;

    [Space(20)]

    public float TimeBetweenWaves = 20f;
    public float TimeBetweenSpawns = 2f;
    public bool IsWaveActive = false;
    public bool IsWaveOnCooldown = false;

    [Space(20)]

    [SerializeField] private GameObject[] spawnpoints;
    [SerializeField] public List<GameObject> allSpawnedEnemyes;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(StartWaveSpawning());
        }

        if (allSpawnedEnemyes.Count == 0)
            IsWaveActive = false;
    }

    IEnumerator StartWaveSpawning()
    {
        while (true)//may cause memory leak
        {
            while (IsWaveActive) //may cause memory leak
            {
                yield return null;
            }

            IsWaveOnCooldown = true;
            yield return new WaitForSeconds(TimeBetweenWaves);
            IsWaveOnCooldown = false;
            StartCoroutine(spawnWave());
            waveIntensity++;

        }
    }

    IEnumerator spawnWave()
    {
        IsWaveActive = true;
        CurrentWaveIntensity = waveIntensity;

        while (CurrentWaveIntensity > 0)
        {
            int randomnum = Random.RandomRange(0, AllEnemyes.Length);
            int randomspawn = Random.RandomRange(0, spawnpoints.Length);


            GameObject Spawed = Instantiate(AllEnemyes[randomnum], spawnpoints[randomspawn].transform.position, Quaternion.identity);
            Spawed.GetComponent<EnemyAi>().Target = target;
            allSpawnedEnemyes.Add(Spawed);
            Spawed.GetComponent<EnemyHp>().wavespawner = this;

            CurrentWaveIntensity -= enemycost[randomnum];

            yield return new WaitForSeconds(TimeBetweenSpawns);
        }
    }
}
