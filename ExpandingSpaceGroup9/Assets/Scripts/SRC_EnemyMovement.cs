using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SRC_EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Transform goal;
    private NavMeshAgent agent;

    void Start()
    {   //Initialize navigation and target
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Prefab_Player");
    }

    // Update is called once per frame
    void Update()
    {   //Move to player
        goal = player.transform;
        agent.destination = goal.position;

        //Set AI rotation to face the player
        Quaternion rotation = Quaternion.LookRotation(player.transform.position - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, 1.0f * Time.deltaTime);
    }
}
