using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public BezierCurve curva;
    public int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions
    int elapsedFrames = 0;

    private void Update()
    {

        float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;

        transform.position = Vector3.Lerp(curva.GetPointAt(0), curva.GetPointAt(1), interpolationRatio);

        elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)

        
    }
}
