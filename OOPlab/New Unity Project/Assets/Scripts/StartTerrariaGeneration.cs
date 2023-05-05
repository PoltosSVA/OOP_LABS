using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class StartTerrariaGeneration : MonoBehaviour
{
    private SpriteShapeController shape;
    [SerializeField] GameObject car_object;

    [SerializeField] float scale = 80.67f;
    [SerializeField] int start_num_of_points = 151;
    

    
    public float distance_btw_points;



    private void Start()
    {
        distance_btw_points = (float)scale / start_num_of_points;
        shape = GetComponent<SpriteShapeController>();
        TerrariaGenerat(shape, distance_btw_points, start_num_of_points);
    }


    public void TerrariaGenerat(SpriteShapeController _shape, float _distance_btw_points, int _add_num_of_points)
    {

        _shape.spline.SetPosition(1, _shape.spline.GetPosition(1) - Vector3.right * scale);//2
        _shape.spline.SetPosition(0, _shape.spline.GetPosition(0) - Vector3.right * scale);//3

        float xPos = _shape.spline.GetPosition(1).x+1;//1
        _shape.spline.InsertPointAt(2, new Vector3(xPos, 1.0f * Mathf.PerlinNoise(2.1f * Random.Range(4.9f, 20.0f), 0)));//2 start->3

        for (int i = 1; i < _add_num_of_points-1; i++)//1
        {
            xPos = _shape.spline.GetPosition(i + 1).x + _distance_btw_points;//2
            _shape.spline.InsertPointAt(i + 2, new Vector3(xPos, 10.1f * Mathf.PerlinNoise(i * Random.Range(4.9f, 20.0f), 0)));//3
        }

        xPos = _shape.spline.GetPosition(_add_num_of_points).x + _distance_btw_points;//2
        _shape.spline.InsertPointAt(_add_num_of_points+1, new Vector3(xPos, 10.1f * Mathf.PerlinNoise(_add_num_of_points * Random.Range(4.9f, 20.0f), 0)));//3

        for (int i = 2; i < _add_num_of_points+2; i++)
        {
            _shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            _shape.spline.SetLeftTangent(i, new Vector3(-1, 0, 0));
            _shape.spline.SetRightTangent(i, new Vector3(1, 0, 0));
        }




    }

}
