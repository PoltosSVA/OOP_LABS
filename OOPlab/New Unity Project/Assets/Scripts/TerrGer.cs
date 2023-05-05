using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TerrGer : MonoBehaviour
{
    public SpriteShapeController shape;





    public GameObject fuel, car, bridge, finish;

    private float pos;

    private float checker = 0f;

    public int num_of_fuel_box = 0;

    private float start_point = 100f;

    private int count = 0;

    public int scale = 1000;
    public int num_points = 150;

    private void FixedUpdate()
    {

        int fuel_long = scale - 100;

        float sum_proc = (float)fuel_long / (float)num_of_fuel_box;

        pos = car.transform.position.x;

        if (pos >= start_point)
        {
            start_point += sum_proc;
            AddFuel();
        }



    }

    private void AddFuel()
    {

        int fuel_long = scale - 100;

        float sum_proc = (float)fuel_long / (float)num_of_fuel_box;


        if (start_point <= scale)
        {

            Instantiate(fuel, new Vector3(start_point, 11.0f), Quaternion.identity);

        }


    }



    private void Start()
    {


        int coin_long = scale - 100;

        float sum_proc = (float)coin_long / (float)num_of_fuel_box;



        shape = GetComponent<SpriteShapeController>();

        float distance_btwPoints = (float)scale / (float)num_points;
        float distance_btw_hills = (float)distance_btwPoints * 2.0f;
        float save = (float)scale / (float)num_points;


        shape.spline.SetPosition(2, shape.spline.GetPosition(2) + Vector3.right * scale);
        shape.spline.SetPosition(3, shape.spline.GetPosition(3) + Vector3.right * scale);



        finish.transform.position = new Vector3(shape.spline.GetPosition(2).x, 8.0f);




        if (start_point <= scale)
        {

            Instantiate(fuel, new Vector3(start_point, 11.0f), Quaternion.identity);

        }




        for (int i = 0; i < num_points; i++)
        {
            checker = Random.Range(4.0f, 6.0f);


            if (i % 30 == 0 && i != 0)
            {

                float xPosB = shape.spline.GetPosition(i + 1).x + distance_btw_hills;
                float yPosB = Random.Range(-2.0f, 2.7f);

                num_points -= 2;

                distance_btwPoints = (float)scale / (float)num_points;

                float axieX = shape.spline.GetPosition(i).x;

                shape.spline.SetPosition(i, new Vector3(axieX, 6.4f));
                shape.spline.InsertPointAt(i + 2, new Vector3(xPosB, yPosB));

                i++;

                Instantiate(bridge, new Vector3(shape.spline.GetPosition(i + 1).x + distance_btw_hills + 5.3f, 17.0f), Quaternion.identity);



                float newX = shape.spline.GetPosition(i + 1).x + distance_btw_hills;
                num_points -= 1;

                distance_btwPoints = (float)scale / (float)num_points;

                shape.spline.InsertPointAt(i + 2, new Vector3(newX, Random.Range(10.0f, 11.0f)));

                i++;


                distance_btwPoints = (float)scale / (float)num_points;


            }

            distance_btwPoints = (float)scale / (float)num_points;

            float xPos = shape.spline.GetPosition(i + 1).x + distance_btwPoints;
            float yPos = 7 * Mathf.PerlinNoise(i * checker, 0);

            shape.spline.InsertPointAt(i + 2, new Vector3(xPos, yPos));


        }

        for (int i = 2; i < num_points + 2; i++)
        {
            shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            shape.spline.SetLeftTangent(i, new Vector3(-2.5f, 0, 9));
            shape.spline.SetRightTangent(i, new Vector3(2.5f, 0, 0));
        }


    }
}
