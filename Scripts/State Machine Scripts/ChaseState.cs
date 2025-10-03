using UnityEngine;

public class ChaseState : MonoBehaviour
{
    private EnemyStatManager stats;
    private EnemyDetection enemyDetection;

    [Header("MOVEMENT PARAMETERS")]
    public EnemyPerception enemyPerception;


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


        if (Vector3.Distance(transform.position, enemyDetection.GetPointOfInterest()) <= 0.01f)
        {

        }
        else
        {
            Chase(enemyDetection.GetPointOfInterest());
        }
    }

    public void Chase(Vector2 playerTransform)
    {
        if (stats.GetStat("Chase Speed") != null)
        {
            Vector3 pointOfInterest = playerTransform;
            transform.position += (pointOfInterest - transform.position).normalized * stats.GetStat("Chase Speed").statValue * Time.deltaTime;

        }
    }
}
