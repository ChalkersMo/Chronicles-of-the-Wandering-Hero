using DG.Tweening;
using System.Collections;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class BearMovementScr : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform[] targets;
    Transform target;
    [SerializeField] float timeToStand;
    [SerializeField] float patrolingSpeed;
    float distance;

    bool isPatroling;
    bool isStanding;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Patrol());
    }
    private void Update()
    {
        if (isPatroling != false && target != null && isStanding != true)
        {
            target.DORotate(target.position, 0.7f);
            distance = Vector3.Distance(target.position, transform.position);
            agent.SetDestination(target.position);
        }

        if (distance <= 1 && isPatroling != false && isStanding != true)
        {
            StartCoroutine(Patrol());
        }
    }
    IEnumerator Patrol()
    {
        isPatroling = true;
        isStanding = true;
        agent.speed = 0;
        yield return new WaitForSeconds(timeToStand);
        int rand = Random.Range(0, targets.Length);
        target = targets[rand];
        agent.speed = patrolingSpeed;
        agent.stoppingDistance = 0;
        isStanding = false;
        StopCoroutine(Patrol());
    }
}
