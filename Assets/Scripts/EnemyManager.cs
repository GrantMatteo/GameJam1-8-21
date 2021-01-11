using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Tooltip("key is strength, value is enemy prefab")]
    public List<GameObject> enemies;
    public List<int> enemyStrengths;
    public Transform playerTransform;

    [Header("Wave settings")]
    public float startingIntensity = 5;
    public float intensityInc = 5;
    public float maxIntensity = 100;

    public float startingWaveDelay = 10;
    public float delayDec = 5;
    public float minDelay = 1;
    public float spawnDist = 10;
    public GameObject player;
    private float lastTime;
    private float curIntensity, curDelay;
    // Start is called before the first frame update
    void Start()
    {
        curIntensity = startingIntensity;
        lastTime = Time.time;
        curDelay = startingWaveDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastTime > curDelay)
        {
            lastTime = Time.time;
            SpawnWave();
        }   
    }
    void SpawnWave() 
    {
        Vector3 pVel = playerTransform.gameObject.GetComponent<Rigidbody2D>().velocity;
        List<GameObject> waveEnemies = new List<GameObject>();
        for (int i = 0; i < curIntensity;)
        {
            int rand = Random.Range(0, enemies.Count);
            if (enemyStrengths[rand] <= curIntensity - i)
            {
                waveEnemies.Add(enemies[rand]);
                i += enemyStrengths[rand];
            }
        }
        foreach (GameObject o in waveEnemies)
        {
            float rotAngle = Random.Range(0, 2 * Mathf.PI);
            Debug.Log("vel " + pVel);
            Debug.Log("VAL " + Vector3.Angle(pVel, new Vector3(Mathf.Cos(rotAngle), Mathf.Sin(rotAngle), pVel.z)));
            if (Vector3.Angle(pVel, new Vector3(Mathf.Cos(rotAngle), Mathf.Sin(rotAngle), pVel.z)) > 90){
                rotAngle = Random.Range(0, 2 * Mathf.PI);
            }
            Instantiate(o, new Vector2(playerTransform.position.x, playerTransform.position.y) + spawnDist*(new Vector2(Mathf.Sin(rotAngle), Mathf.Cos(rotAngle))), transform.rotation);
        }
        curIntensity += intensityInc;
        curDelay -= delayDec;
        curDelay = curDelay < minDelay ? minDelay : curDelay;
        curIntensity = curIntensity > maxIntensity ? maxIntensity : curIntensity;
    }

}
