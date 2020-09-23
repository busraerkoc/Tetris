using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    Grid grid;
    void Start()
    {
    }
    public void MoveLeft()
    {
        transform.position += new Vector3(-1, 0, 0);
    }
    public void MoveRight()
    {
        transform.position +=new Vector3(1, 0, 0);
    }
    
    public void MoveUp()
    {
        transform.position += new Vector3(0, 1, 0);
    }

    public void MoveDown()
    {
        transform.position += new Vector3(0, -1, 0);
    }

    public void RotateRight()
    {
        transform.Rotate(0, 0, -90);
    }

    public void RotateLeft()
    {
        transform.Rotate(0, 0, 90);
    }
    public void RotateClockwise(bool clockwise)
	{
		if (clockwise)
		{
			RotateRight();
		}
		else
		{
			RotateLeft();
		}
	}
}

