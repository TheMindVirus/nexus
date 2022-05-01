using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 view = new Vector3(0, 0, 0);
    Vector3 direction = new Vector3(0, 0, 0);
    Vector3 newpos = new Vector3(0, 0, 0);

    float ViewX = 0.0f;
    float ViewY = 0.0f;
    float ViewZ = 0.0f;
    float DirectionX = 0.0f;
    float DirectionY = 0.0f;
    float DirectionZ = 0.0f;
    float Floor = 1.0f;

    float level1 = 1.0f;
    float level2 = 10.0f;
    float wall1 = 49.0f;
    float wall2 = 24.0f;

    public void SetViewX(float vx) { ViewX = vx; }
    public void SetViewY(float vy) { ViewY = vy; }
    public void SetViewZ(float vz) { ViewZ = vz; }
    public void SetDirectionX(float dx) { DirectionX = dx; }
    public void SetDirectionY(float dy) { DirectionY = dy; }
    public void SetDirectionZ(float dz) { DirectionZ = dz; }
    public void SetFloor(float lvl) { Floor = lvl; }
    public void SetFOV(float fov) { Camera.main.fieldOfView = fov; }

    public void UpdateMovement()
    {
        view = new Vector3(transform.eulerAngles.x + ViewX,
                           transform.eulerAngles.y + ViewY,
                           transform.eulerAngles.z + ViewZ);
        //BUG: Looking up and down causes strobe
        if ((view.x < 180.0f) && (view.x > 85.0f)) { view.x = 85.0f; }
        else if ((view.x > 180.0f) && (view.x < 275.0f)) { view.x = 275.0f; }
        transform.eulerAngles = view;

        direction = new Vector3(DirectionX, DirectionY, DirectionZ);
        transform.Translate(direction);

        newpos = transform.position;
        if (Floor < level1) { Floor = level1; }
        if (Floor > level2) { Floor = level2; }
        if (newpos.y < Floor) { newpos.y = Floor; }
        if (newpos.y > Floor) { newpos.y = Floor; }
        if (newpos.x < -wall1) { newpos.x = -wall1; }
        if (newpos.x > wall1) { newpos.x = wall1; }
        if (newpos.z < -wall2) { newpos.z = -wall2; }
        if (newpos.z > wall2) { newpos.z = wall2; }
        transform.position = newpos;
    }
}