/*
 * Graphics and Interaction (COMP30019) Project 1
 * Team: Trent Branson and Jin Wei Loh
 * Code adapted from COMP30019: Workshop 2
 * Movement of the sun, based on origin
 */

using UnityEngine;
using System.Collections;

public class PointLight : MonoBehaviour
{
    // The color of the point light
    public Color color = Color.white;

    public Vector3 GetWorldPosition()
    {
        return this.transform.position;
    }
}
