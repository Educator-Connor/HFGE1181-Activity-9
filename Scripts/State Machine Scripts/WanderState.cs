using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class WanderState : MonoBehaviour
{
    private EnemyStatManager stats;
    private EnemyDetection enemyDetection;

    [Header("MOVEMENT PARAMETERS")]
    public EnemyPerception enemyPerception;
    [SerializeField]
    private int wanderRadius;
    private Vector3 wanderPoint;
    public float idleTime;

    [SerializeField]
    private Vector3 rallyPoint;

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


        if (Vector3.Distance(transform.position, wanderPoint) < 0.1f)
        {
            StartCoroutine(IdleDelay());
        }
        else
        {
            Wander();
        }
    }

    public void SelectWanderPoint()
    {
        wanderPoint = rallyPoint;
        wanderPoint.x = Random.Range(rallyPoint.x - wanderRadius, rallyPoint.x + wanderRadius);
        wanderPoint.y = Random.Range(rallyPoint.y - wanderRadius, rallyPoint.y + wanderRadius);
    }

    public void Wander()
    {
        if (stats.GetStat("Movement Speed") != null)
        {
            transform.position += (wanderPoint - transform.position).normalized * stats.GetStat("Movement Speed").statValue * Time.deltaTime;
        }

    }

    public IEnumerator IdleDelay()
    {
        yield return new WaitForSecondsRealtime(idleTime);
        SelectWanderPoint();
    }
}
