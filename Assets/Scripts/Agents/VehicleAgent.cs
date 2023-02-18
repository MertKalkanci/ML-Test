using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class VehicleAgent : Agent
{
    [SerializeField] private Transform restart;
    [SerializeField] private int index = 0;
    [SerializeField] private Transform[] checkpoints;
    [SerializeField] private WheelDrive carController;
    public override void OnEpisodeBegin()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        index = 0;
        transform.position = restart.position;
        transform.rotation = restart.rotation;
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> actions = actionsOut.DiscreteActions;
        actions[0] = (int)Input.GetAxis("Horizontal") == 1 ? 2 : 0;
        actions[1] = (int)Input.GetAxis("Vertical") == 1 ? 2 : 0;
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        carController.stateHorizontal = actions.DiscreteActions[0];
        carController.stateVertical = actions.DiscreteActions[1];
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        float directionDot = Vector3.Dot(transform.forward, checkpoints[index].position);
        sensor.AddObservation(directionDot);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wall"))
        {
            Debug.Log("Wall");
            AddReward(-10f);
            EndEpisode();
        }    
        else if(other.CompareTag("Reward"))
        {
            if(other.transform == checkpoints[index])
            {
                Debug.Log("Reward :" + index);
                AddReward(5f * index);
                index++;
                if (index >= checkpoints.Length)
                    index = 0;
            }
        }
    }

}
