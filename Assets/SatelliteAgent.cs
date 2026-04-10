using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class SatelliteAgent : Agent
{
    public Transform earth;

    public override void OnEpisodeBegin()
    {
        transform.position = earth.position + Random.onUnitSphere * 15f;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position - earth.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float x = actions.ContinuousActions[0];
        float z = actions.ContinuousActions[1];

        Vector3 move = new Vector3(x, 0, z);
        transform.position += move * 10f * Time.deltaTime;

        float dist = Vector3.Distance(transform.position, earth.position);

        if (Mathf.Abs(dist - 15f) < 1f)
            AddReward(0.1f);
        else
            AddReward(-0.1f);
    }
}