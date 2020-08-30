using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ZombieMoveController : MonoBehaviour
{
    private NavMeshAgent m_navMeshAgent = null;
    

    [SerializeField]
    private Transform m_targetTransform = null;

    private Animator m_animator = null;

    

    // Start is called before the first frame update
    void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();

        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (m_targetTransform != null)
        {
            m_navMeshAgent.SetDestination(m_targetTransform.position);
            if (m_navMeshAgent.remainingDistance > 0.1f)
            {
                m_animator.SetFloat("FrontVelocity", m_navMeshAgent.velocity.sqrMagnitude);
            }
        }
    }
}
