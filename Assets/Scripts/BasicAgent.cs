using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class BasicAgent : Agent
{
    [SerializeField] Transform reward, parent;
    [SerializeField] float moveX, moveZ,rangeReward = 10f,speed = 0.2f;
    [SerializeField] Vector3 oldPosition;
    public override void OnActionReceived(ActionBuffers actions)
    {
        moveX = actions.ContinuousActions[0];
        moveZ = actions.ContinuousActions[1];
        transform.position += new Vector3(moveX, 0, moveZ) * speed;

        float diffOld = Vector3.Distance(oldPosition, reward.position);
        float diffNew = Vector3.Distance(transform.position, reward.position);
        oldPosition = transform.position;

        AddReward((diffNew - diffOld) * 0.1f);
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(reward.position);
        sensor.AddObservation(transform.position);
    }
    public override void OnEpisodeBegin()
    {
        oldPosition = parent.position;
        transform.position = parent.position;
        reward.position = new Vector3(Random.Range(-rangeReward, rangeReward), 0, Random.Range(-rangeReward, rangeReward)) + parent.position;
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> actionsToManipulate = actionsOut.ContinuousActions;
        actionsToManipulate[0] = Input.GetAxis("Horizontal");
        actionsToManipulate[1] = Input.GetAxis("Vertical");

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Reward"))
        {
            AddReward(1f);
            EndEpisode();
        }
        else if (other.CompareTag("Wall"))
        {
            AddReward(-1f);
            EndEpisode();
        }
    }
}
