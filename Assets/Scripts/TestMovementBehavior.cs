using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestMovementBehavior : MonoBehaviour
{
    public List<GameObject> Goals;
    private int currentGoalIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = Goals[currentGoalIndex].transform.position;
            if(currentGoalIndex == Goals.Count - 1)
            {
                currentGoalIndex = 0;
            }
            else
            {
                currentGoalIndex++;
            }
        }
    }
}
