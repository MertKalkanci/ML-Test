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
        index = 0;
        transform.position = restart.position;
        transform.rotation = restart.rotation;
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> actions = actionsOut.DiscreteActions;
        actions[0] = (int)Input.GetAxis("Horizontal");
        actions[1] = (int)Input.GetAxis("Vertical");
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        carController.stateHorizontal = actions.DiscreteActions[0];
        carController.stateVertical = actions.DiscreteActions[1];
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(checkpoints[index].position);
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
            }
        }
    }

}
