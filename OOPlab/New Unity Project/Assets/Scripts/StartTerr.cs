using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
public class StartTerr : MonoBehaviour
{
    public SpriteShapeController shape;

    void Start()
    {
        shape = GetComponent<SpriteShapeController>();
        shape.spline.SetPosition(2, shape.spline.GetPosition(2) + Vector3.right * 32.15f);
        shape.spline.SetPosition(3, shape.spline.GetPosition(3) + Vector3.right * 32.15f);
    }

   
}
