using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookVersion2 : MonoBehaviour
{

    [Flags]
    public enum RotationDirections
    {
        None,
        Horizontal = (1 << 0),
        Vertical = (1 << 1)
    }

    [SerializeField] private RotationDirections rotationDirections;

    [SerializeField] private Vector2 acceleration;

    [SerializeField] private Vector2 sensitivity;

    [SerializeField] private float maxVerticalAngleFromHorizon;

    private Vector2 velocity;
    private Vector2 rotation;

    private Vector2 look;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 wantedVelocity = look * sensitivity;

        //if((rotationDirections & RotationDirections.Horizontal) == 0)
        //{
        //    wantedVelocity.x = 0;
        //}
        //if ((rotationDirections & RotationDirections.Horizontal) == 0)
        //{
        //    wantedVelocity.y = 0;
        //}

        velocity = new Vector2(
        Mathf.MoveTowards(velocity.x, wantedVelocity.x, acceleration.x * Time.deltaTime),
        Mathf.MoveTowards(velocity.y, wantedVelocity.y, acceleration.y * Time.deltaTime));
        
        rotation += velocity * Time.deltaTime;
        rotation.y = ClampVerticalAngle(rotation.y);

        transform.localEulerAngles = new Vector3(rotation.y, rotation.x, 0);
    }

    public void GetInput(Vector2 input)
    {
        look = input;
    }

    private float ClampVerticalAngle(float angle)
    {
        return Mathf.Clamp(angle, -maxVerticalAngleFromHorizon, maxVerticalAngleFromHorizon);
    }
}
