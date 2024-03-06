using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject player;
    
    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool waitingForNextWave = false;
    
    public float spawnRadius = 10f;
    
    static WaveSpawner instance;
    
    // Neue Variablen für die Schübe
    public int enemiesPerBurst = 8; // Anzahl der Feinde pro Schub
    public float timeBetweenBursts = 4f; // Zeit in Sekunden zwischen den Schüben
    
    public static WaveSpawner GetInstance() {
        return instance;
    }
    
    void Awake() {
        if(instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    
    private void Start()
    {
        StartNextWave();
    }
    
    void Update()
    {
        if (enemiesAlive <= 0 && !waitingForNextWave)
        {
            StartNextWave();
        }
    }
    
    void StartNextWave()
    {
        waitingForNextWave = true;
        StartCoroutine(WaitAndStartNextWave());
    }
    
    IEnumerator WaitAndStartNextWave()
    {
        yield return new WaitForSeconds(5f);
        currentWave++;
        GameManager.GetInstance().UpdateCurrentWaveLabel(currentWave);
        waitingForNextWave = false;
        int totalEnemiesToSpawn = currentWave * 10;
        int bursts = Mathf.CeilToInt((float)totalEnemiesToSpawn / enemiesPerBurst);
        
        for (int i = 0; i < bursts; i++)
        {
            int enemiesThisBurst = Mathf.Min(enemiesPerBurst, totalEnemiesToSpawn - (i * enemiesPerBurst));
            StartCoroutine(SpawnBurst(enemiesThisBurst));
            yield return new WaitForSeconds(timeBetweenBursts);
        }
    }
    
    IEnumerator SpawnBurst(int enemies)
    {
        for (int i = 0; i < enemies; i++)
        {
            SpawnEnemy();
            yield return null; // Optional: Wartezeit zwischen dem Spawnen einzelner Feinde in einem Schub
        }
    }
    
    void SpawnEnemy()
    {
        Vector3 spawnPoint = RandomPointAroundPlayer(spawnRadius);
        if (spawnPoint != Vector3.zero)
        {
            Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
            enemiesAlive++;
        }
    }
    
    public void OnEnemyDeath()
    {
        enemiesAlive--;
    }

    Vector3 RandomPointAroundPlayer(float radius) {
    Vector3 spawnPoint = Vector3.zero;
    int attempts = 0;
    const int maxAttempts = 10; // Maximale Anzahl von Versuchen, um einen gültigen Spawn-Punkt zu finden
    const float minDistanceToPlayer = 3f; // Mindestabstand vom Spieler, um zu verhindern, dass Gegner zu nahe spawnen

    do {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += player.transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas)) {
            float distance = Vector3.Distance(hit.position, player.transform.position);
            if (distance >= minDistanceToPlayer) {
                spawnPoint = hit.position;
                break; // Ein gültiger Spawn-Punkt wurde gefunden
            }
        }
        attempts++;
    } while (attempts < maxAttempts && spawnPoint == Vector3.zero);

    return spawnPoint; // Gibt den gefundenen Punkt zurück oder Vector3.zero, falls kein gültiger Punkt gefunden wurde
}
}
