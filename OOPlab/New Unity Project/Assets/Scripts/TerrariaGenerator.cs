using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TerrariaGenerator : MonoBehaviour
{
    private SpriteShapeController shape;
    [SerializeField] GameObject car_object;

    [SerializeField] int scale = 1000;
    [SerializeField] int start_num_of_points = 151;
    [SerializeField] int fixed_add_point = 7;
    
    public int start_point = 2;
    public float distance_btw_points;

    public float car_position;
    public float point_position;
    
    
    private void Start()
    {
        distance_btw_points = (float)scale / start_num_of_points;
        shape = GetComponent<SpriteShapeController>();
        TerrariaGenerat(shape, distance_btw_points, start_num_of_points);
    }

    private void FixedUpdate()
    {
        car_position = car_object.transform.position.x;
        point_position = shape.spline.GetPosition((start_num_of_points + start_point) / 2).x;
        if (car_position >= point_position)
        {
            
            shape = GetComponent<SpriteShapeController>();
            start_point += fixed_add_point;
            start_num_of_points += fixed_add_point;
            TerrariaGenerat(shape, distance_btw_points, start_num_of_points);
        }
        
    }


    public void TerrariaGenerat(SpriteShapeController _shape, float _distance_btw_points, int _add_num_of_points)
    { 
        
        _shape.spline.SetPosition(start_point, _shape.spline.GetPosition(start_point) + Vector3.right * scale);//2
        _shape.spline.SetPosition(start_point + 1, _shape.spline.GetPosition(start_point + 1) + Vector3.right * scale);//3

        float xPos = _shape.spline.GetPosition(start_point - 1).x + 10;//1
        _shape.spline.InsertPointAt(start_point, new Vector3(xPos, 4.1f * Mathf.PerlinNoise(2.1f * Random.Range(4.9f, 20.0f), 0)));//2 start->3
        
        for (int i = start_point-1; i < _add_num_of_points; i++)//1
        {
            xPos = _shape.spline.GetPosition(i+1).x + _distance_btw_points;//2
            _shape.spline.InsertPointAt(i + 2, new Vector3(xPos, 10.1f * Mathf.PerlinNoise(i * Random.Range(4.9f, 20.0f), 0)));//3
        }

        for (int i = start_point; i < _add_num_of_points + 3; i++)
        {
            _shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            _shape.spline.SetLeftTangent(i, new Vector3(-1, 0, 0));
            _shape.spline.SetRightTangent(i, new Vector3(1, 0, 0));
        }

        
        

    }

}
