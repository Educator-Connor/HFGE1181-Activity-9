using System.Collections;
using UnityEngine;

public class SeekState : MonoBehaviour
{
    private EnemyStatManager stats;
    private EnemyDetection enemyDetection;

    [Header("MOVEMENT PARAMETERS")]
    public EnemyPerception enemyPerception;
    [SerializeField]
    private int wanderRadius;
    public float idleTime;
    private Vector3 searchArea;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stats = GetComponent<EnemyStatManager>();

        enemyDetection = GetComponent<EnemyDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyDetection.GetEnemyState() != enemyPerception)
        {
            return;
        }

      

        if (Vector3.Distance(transform.position, searchArea) < 0.1f)
        {
            
            StartCoroutine(IdleDelay());
        }
        else
        {
            SearchArea();
        }
    }

    public void Seek()
    {
        Vector2 pointOfInterest = enemyDetection.GetPointOfInterest();
        searchArea.x = Random.Range(pointOfInterest.x - wanderRadius, pointOfInterest.x + wanderRadius);
        searchArea.y = Random.Range(pointOfInterest.y - wanderRadius, pointOfInterest.y + wanderRadius);
    }

    public void SearchArea()
    {
        if (stats.GetStat("Movement Speed") != null)
        {
            transform.position += (searchArea - transform.position).normalized * stats.GetStat("Movement Speed").statValue * Time.deltaTime;
        }
    }

    public IEnumerator IdleDelay()
    {
        yield return new WaitForSecondsRealtime(idleTime);
        Seek();
    }

}
