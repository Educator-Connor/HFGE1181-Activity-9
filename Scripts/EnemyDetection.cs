using Unity.VisualScripting;
using UnityEngine;

public enum EnemyPerception 
{   
    Oblivious, 
    Suspicious, 
    Detected 
}
public class EnemyDetection : MonoBehaviour
{
    [Header("CURRENT STATE")]
    public EnemyPerception enemyPerception;


    private Transform playerTransform;
    [SerializeField]
    private LayerMask targetMask;

    [Header("DETECTION PARAM")]
    [SerializeField]
    private float suspicionRange, detectedRange;
    private Vector2 pointOfInterest;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerIsDetected();
    }

    public void CheckIfPlayerIsDetected()
    {
        if (!CheckPathToPlayer())
        {
            enemyPerception = EnemyPerception.Oblivious;
        }
        else if (CalculatePlayerDistance() < suspicionRange && CalculatePlayerDistance() > detectedRange)
        {
            enemyPerception = EnemyPerception.Suspicious;
            pointOfInterest = playerTransform.position;
        }
        else if (CalculatePlayerDistance() < detectedRange)
        {
            enemyPerception = EnemyPerception.Detected;
            pointOfInterest = playerTransform.position;
        }
        else
        {
            enemyPerception = EnemyPerception.Oblivious;
        }
    }

    public Vector2 GetPointOfInterest()
    { 
        return pointOfInterest; 
    }

    public EnemyPerception GetEnemyState()
    {
        return enemyPerception;
    }

    private float CalculatePlayerDistance()
    {
        float calculatedDistance = Vector2.Distance(transform.position, playerTransform.position);
        return calculatedDistance;
        
    }

    private bool CheckPathToPlayer()
    {
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, 1000f, targetMask);
        Debug.DrawRay(transform.position, directionToPlayer * hit.distance, Color.red);
        if (hit.collider != null)
        {
            return hit.collider.gameObject.CompareTag("Player");
        }
        else { return false; }
        
    }
}
