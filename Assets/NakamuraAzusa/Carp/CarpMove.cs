using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CarpMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _interval = 0.5f;
    [SerializeField] private float _maxMoveDistance = 5f;

    private Coroutine CoolTimeCoroutine;

    private Vector3 _walkDirection;

    private void Awake()
    {
        if (_agent == null) _agent = GetComponent<NavMeshAgent>();

        _agent.updateRotation = false;
    }
    private void OnEnable()
    {
         ResetInfo();

         CoolTimeCoroutine = StartCoroutine(DecisionTargetCoolTimeRoutine(_interval));
    }

    private void OnDisable()
    {
        ResetInfo();
    }

    public void NavStop()
    {
        if (_agent != null) _agent.isStopped = true;
        ResetInfo();
    }

    public void NavStart()
    {
        if (_agent != null && _agent.isActiveAndEnabled && _agent.isOnNavMesh)
        {
            _agent.isStopped = false;
        }

        if (CoolTimeCoroutine == null)
        {
            StartCoroutine(DecisionTargetCoolTimeRoutine(_interval));
        }

    }

    private void SetNewTargetDestination()
    {
        if (_agent == null || !_agent.isActiveAndEnabled || !_agent.isOnNavMesh) return;

        float randomAngle = Random.Range(0f, 360f);
        _walkDirection = new Vector3(Mathf.Sin(randomAngle), 0, Mathf.Cos(randomAngle)).normalized;

        var soursPos = transform.position;
        var targetPos = soursPos + _walkDirection * _maxMoveDistance;

        var blocked = NavMesh.Raycast(soursPos, targetPos, out NavMeshHit hitInfo, NavMesh.AllAreas);

        if (blocked)
        {
            _agent.SetDestination(hitInfo.position);
        }
        else
        {
            _agent.SetDestination(targetPos);
        }
    }

    private IEnumerator DecisionTargetCoolTimeRoutine(float coolTime)
    {
        while (_agent != null && (!_agent.isActiveAndEnabled || !_agent.isOnNavMesh))
        {
            yield return null;
        }

        while (true)
        {
            if (_agent != null && !_agent.isStopped)
            {
                SetNewTargetDestination();
            }

            yield return new WaitForSeconds(coolTime);
        }
    }

    private void ResetInfo()
    {
        if(CoolTimeCoroutine != null)
        {
            StopCoroutine(CoolTimeCoroutine);
            CoolTimeCoroutine = null;
        }
    }
}
