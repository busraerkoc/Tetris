using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    Grid grid;
    Spawn spawn;
    Shape activeShape;
    Shape shape;
    float timeNextSpawn;
    float timeToDrop;
    float dropIntervalModded = 0.5f;
    float nextLeftRightTime;
    float nextDownTime;
    float nextRotateTime;
    float repeatRateDown = 0.01f;
    float repeatLeftRightTime = 0.03f;
    float repeatRateRotate = 0.25f;
    bool clockwise = true;
	string swipeDirection = "";
    string swipeEndDirection = "";
    public GameObject GameOverPanel;
    public bool isPaused = false;
    public GameObject PausePanel;

    void OnEnable()
    {
        TouchControl.SwipeEvent += SwipeHandler;
        TouchControl.SwipeEndEvent += SwipeEndHandler;
    }
    void OnDisable()
    {
        TouchControl.SwipeEvent -= SwipeHandler;
        TouchControl.SwipeEndEvent -= SwipeEndHandler;
    }
    void MoveRight()
    {
        activeShape.MoveRight();
        nextLeftRightTime = Time.time + repeatLeftRightTime;
        if(!grid.CheckValidPosition((activeShape)))
        {
            activeShape.MoveLeft(); //previous position
        }
    }

    void MoveLeft()
    {
        activeShape.MoveLeft();
        nextLeftRightTime = Time.time + repeatLeftRightTime;
        if(!grid.CheckValidPosition(activeShape))
        {
            activeShape.MoveRight(); // prevoius position
        }
    }

    void MoveDown()
    {
        timeToDrop = Time.time + dropIntervalModded;
        nextDownTime = Time.time + repeatRateDown;
        activeShape.MoveDown();
        if(!grid.CheckValidPosition(activeShape))
        { 
            activeShape.MoveUp();
            if(grid.CheckGridFull(activeShape))
            {
                GameOver();
            }
            else
            {
                grid.StoreShape(activeShape);
                activeShape = spawn.SpawnShape();
                nextLeftRightTime= Time.time;
		        nextDownTime = Time.time;
		        nextRotateTime = Time.time;
                grid.ClearAllRows();
                grid.Score();
 
            }
        }
    }
    void Rotate()
    {
        activeShape.RotateClockwise(clockwise);
        nextRotateTime = Time.time + repeatRateRotate;
		if (!grid.CheckValidPosition (activeShape)) 
		{
			activeShape.RotateClockwise (!clockwise);
        }
	}

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.FindObjectOfType<Grid>();
        spawn = GameObject.FindObjectOfType<Spawn>();
        nextDownTime = Time.time + repeatRateDown;
        nextLeftRightTime = Time.time + repeatLeftRightTime;
        nextRotateTime = Time.time + repeatRateRotate;
        if(spawn)
        {
            spawn.transform.position = Vectord.Round(spawn.transform.position);
            if(!activeShape)
            {
                //activeShape = spawn.GetRandomShape();
                activeShape = spawn.SpawnShape();
            }  
        }
        else{
            Debug.LogWarning("WARNING! There is no spawner defined!");
        }

    }
    void Update () 
	{   
        if(!activeShape || !spawn)
        {
            return;
        }
        UserInput();
        
    }
    void UserInput()
    {
        if(Time.time > timeToDrop)
        {
            MoveDown();
        }
        else if(swipeDirection == "right" && Time.time > nextLeftRightTime || swipeEndDirection == "right")
        {
            MoveRight();
            swipeEndDirection = "";
            swipeDirection = "";
        }
        else if (swipeDirection == "left" && Time.time > nextLeftRightTime || swipeEndDirection == "left")
        {
            MoveLeft();
            swipeEndDirection = "";
            swipeDirection = "";
        }
        else if (swipeEndDirection == "up")
        {
            Rotate();
            swipeEndDirection = "";
        }
        else if (swipeDirection == "down" && Time.time > nextDownTime)
        {
            MoveDown();
            swipeDirection = "";
            swipeEndDirection = "";
        }
    }
    string GetDirection(Vector2 swipeMovement)
    {
        string direction = "";
        //horizontal
        if (Mathf.Abs(swipeMovement.x) > Mathf.Abs(swipeMovement.y))
		{
			direction = (swipeMovement.x >= 0) ? "right":"left";
		}
		// vertical
		else
		{
			direction = (swipeMovement.y >= 0) ? "up":"down";
		}
		return direction;
    }
    void SwipeHandler(Vector2 swipeMovement)
    {
        swipeDirection = GetDirection(swipeMovement);
    }

    void SwipeEndHandler(Vector2 swipeMovement)
    {
        swipeEndDirection = GetDirection(swipeMovement);
    }
    
    void GameOver()
    {
        GameOverPanel.SetActive(true);
    }
    
    public void Pause()
    {
        isPaused = !isPaused;
        if(PausePanel)
        {
            PausePanel.SetActive(isPaused);
            Time.timeScale = (isPaused) ? 0 : 1;  // When timeScale is set to zero the game is basically paused if all your functions are frame rate independent. When timeScale is 1.0 time passes as fast as realtime.
        }
    }
}
