using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Floater : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody body;

    // Depth at which object starts to experience buoyancy
    public float depthBefSub;

    // Amount of buoyant force applied
    public float displacementAmt;

    // Number of points applying buoyant force
    public int floaters;

    // Drag coefficient in water
    public float waterDrag;

    // Angular drag coefficient in water
    public float waterAngularDrag;

    // Water reference
    public WaterSurface water;

    // Holds parameters for searching the water surface
    WaterSearchParameters waterSearchParameters;

    // Stores results of water surface search
    WaterSearchResult waterSearchResult;


    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.AddForceAtPosition(Physics.gravity / floaters, transform.position, ForceMode.Acceleration);

        waterSearchParameters.startPositionWS = transform.position;

        water.ProjectPointOnWaterSurface(waterSearchParameters, out waterSearchResult);

        if(transform.position.y < waterSearchResult.projectedPositionWS.y) 
        {
            float displacementMulti = Mathf.Clamp01((waterSearchResult.projectedPositionWS.y - transform.position.y) / depthBefSub) * displacementAmt;

            body.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMulti, 0f), transform.position, ForceMode.Acceleration);

            body.AddForce(displacementMulti * -body.linearVelocity * Time.fixedDeltaTime, ForceMode.VelocityChange);

            body.AddTorque(displacementMulti * -body.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
