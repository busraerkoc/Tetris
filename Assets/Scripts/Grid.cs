using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    // a SpriteRenderer that will be instantiated in a grid to create our board
	public Transform emptySprite;

	// the height of the board
	public int height = 30;

	// width of the board
	public int width = 10;

	// number of rows where we won't have grid lines at the top
	public int header = 8;
    // store inactive shapes here
	Transform[,] board;
	public int numberOfClearedRows;
	public int score = 0;
	public Text scoreText;
	public Text highScoreText;


    void Awake()
	{
		board = new Transform[width,height];
	}

    // Start is called before the first frame update
    void Start()
    {
    	DrawGrid(); 
		highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public bool CheckGridFull(Shape shape)  // Game over condition
	{
		foreach (Transform child in shape.transform)
		{
			if(child.transform.position.y >= height - header)
			{
				return true;
			}
		}
		return false;
	}
	public bool CheckGridBorder(Vector2 pos)
	{
    	return ((int)pos.x >=0 && (int)pos.x < width && (int)pos.y >= 0);
	}

	public bool CheckIsFull(int x, int y, Shape shape)
	{
		return (board[x,y] !=null && board[x,y].parent != shape.transform);
	}
	public bool CheckValidPosition(Shape shape)
	{
		foreach (Transform child in shape.transform)
		{
			Vector2 pos = Vectord.Round(child.position);
			if(!CheckGridBorder(pos))
			{
				return false;
			}
			if(CheckIsFull((int)pos.x, (int)pos.y, shape))
			{
				return false;
			}
   		}
		return true;
	}

    	// draw our empty board with our empty sprite object
	void DrawGrid() {
		if (emptySprite != null)
		{
			for (int y = 0; y < height -  header; y++)
			{
				for (int x = 0; x < width; x++) 
				{
					Instantiate(emptySprite, new Vector3(x, y, 0), Quaternion.identity);
				}
			}
		}
	}

	public void StoreShape(Shape shape)
	{
		foreach (Transform child in shape.transform)
		{
			Vector2 pos = Vectord.Round(child.position);
			board[(int) pos.x, (int) pos.y] = child;
		}
	}
	public bool CheckRow(int y)  // Check the row is full, if row is full it returns true
	{
		for (int x = 0; x < width; x++)
		{
			if(board[x, y]==null)
			{
				return false;
			}
		}
		numberOfClearedRows +=1;
		return true;
	}
	public void ClearRow(int y)
	{
		for(int x = 0; x < width; x++)
		{
			Destroy(board[x, y].gameObject);  // Destroy()  Removes a GameObject, component or asset.
			board[x, y] = null;
		}
	}
	
	public void ShiftRowDown(int y)
	{
		for(int x = 0; x < width; x++)
		{
			if(board[x, y]!= null)
			{
				board[x, y-1] = board[x, y];
				board[x, y] = null;
				board[x, y-1].position+= new Vector3(0, -1, 0);
			}
		}
	}

	public void ShiftAllRows(int y)
	{
		for (int i = y; i < height; i++)
		{
			ShiftRowDown(i);
		}
	}

	public void ClearAllRows()
	{
		for(int y = 0; y < height; y++)
		{
			if(CheckRow(y))
			{
				ClearRow(y);
				ShiftAllRows(y+1);
				y--;
			}
		}
	}
	public void Score()
	{
		if(numberOfClearedRows > 0)
		{
			switch(numberOfClearedRows)
			{
				case 1: 
					score += 40;
					break;
				case 2:
					score +=100;
					break;
				case 3:
					score += 300;
					break;
				case 4:
					score += 1200;
					break;
			}
			numberOfClearedRows = 0;
			HighScore();
			UpdateUIText();
		}
	}
	void UpdateUIText()
	{
		scoreText.text = score.ToString();
		highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
	}

	public void HighScore()
	{
		if (score > PlayerPrefs.GetInt("HighScore",0))
		{
			PlayerPrefs.SetInt("HighScore", score);
			highScoreText.text = score.ToString();
		}
	}
}
