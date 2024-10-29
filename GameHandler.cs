using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class GameHandler : MonoBehaviour
{
	public int rows;
	public int cols;
	public GameObject tile;
	public GameObject emptyTile;
	public int[,] grid;
	public List<Vector2> finalTilePositions = new List<Vector2>();
	public GameObject[,] tileGrid; 
	public int tilesPlacedCounter;
	

	void Start()
	{
		SpawnNewTiles();
		grid = new int[rows, cols];
		tileGrid = new GameObject[rows, cols];

		for(int i = 0; i < cols; i++)
		{
			for(int j = 0; j < rows; j++)
			{
				tileGrid[i, j] = Instantiate(emptyTile,  new Vector3(i + 0.5f, j + 0.5f, 0), Quaternion.identity, GameObject.Find("- Tiles").transform);
			}
		}
	}
	
	void Update()
	{
		CheckGrid();
		RefreshGrid();
	}


	public void AddToGrid(List<Vector2> tilePosList, GameObject tile)
	{	  		
		for(int k = 0; k < tilePosList.Count; k++)
		{
			if(tile.transform.position.x + tilePosList[k].x >= 0 && tile.transform.position.y + tilePosList[k].y >= 0 && tile.transform.position.x + tilePosList[k].x <= rows - 1 && tile.transform.position.y + tilePosList[k].y <= cols - 1 && grid[Mathf.RoundToInt(tile.transform.position.x + tilePosList[k].x), Mathf.RoundToInt(tile.transform.position.y + tilePosList[k].y)] == 0)
			{
				finalTilePositions.Add(new Vector2(tile.transform.position.x + tilePosList[k].x, tile.transform.position.y + tilePosList[k].y));
				//Destroy(tile.gameObject.GetComponentInChildren<BoxCollider2D>());
				Destroy(tile);
			}					
		}
		
		if(finalTilePositions.Count == tilePosList.Count)
		{
			foreach(Vector2 finalTilePos in finalTilePositions)
			{
				if(grid[Mathf.RoundToInt(finalTilePos.x), Mathf.RoundToInt(finalTilePos.y)] == 0)
				{
					grid[Mathf.RoundToInt(finalTilePos.x), Mathf.RoundToInt(finalTilePos.y)] = 1;	
					tileGrid[Mathf.RoundToInt(finalTilePos.x), Mathf.RoundToInt(finalTilePos.y)] = GameObject.Find("Tile");
				}
				else
				{
					ResetTile(tile, gameObject.GetComponent<DragAndDrop>().origin);
				}		
			}		
		}
		else
		{
			ResetTile(tile, gameObject.GetComponent<DragAndDrop>().origin);
		}
		
		finalTilePositions.Clear();
	}
	
	public void ResetTile(GameObject tile, Vector2 origin)
	{
		tile.transform.position = origin;
	}


	void CheckGrid()
	{
		for (int row = 0; row < rows; row++)
		{
			if (IsColumnFilled(row))
			{
				Debug.Log("Column " + row + " is filled");
				
				for(int i = 0; i < cols; i++)
				{
					grid[row, i] = 0;
					tileGrid[row, i] = GameObject.Find("EmptyTile");
				}
			}
		}

		for (int col = 0; col < cols; col++)
		{
			if (IsRowFilled(col))
			{
				Debug.Log("Row " + col + " is filled");
				
				for(int i = 0; i < rows; i++)
				{
					grid[i, col] = 0;
					tileGrid[i, col] = GameObject.Find("EmptyTile");
				}
			}
		}
	}


	bool IsColumnFilled(int rowIndex)
	{
		for (int col = 0; col < cols; col++)
		{
			if (grid[rowIndex, col] == 0)
			{
				return false;  
			}
		}
		return true;  
	}
	
	bool IsRowFilled(int colIndex)
	{
		for (int row = 0; row < rows; row++)
		{
			if (grid[row, colIndex] == 0)
			{
				return false;  
			}
		}
		return true;  
	}
	
	public void SpawnNewTiles()
	{
		for(int i = 0; i < 3; i++)
		{
			Instantiate(GameObject.Find("2x2Block"), new Vector2(i * 3, -3), Quaternion.identity, GameObject.Find("- Tiles").transform);
		}
		
		
	}
	
	public void RefreshGrid()
	{
		for(int i = 0; i < cols; i++)
		{
			for(int j = 0; j < rows; j++)
			{
				if(grid[i, j] == 1)
				{
					tileGrid[i, j].GetComponent<SpriteRenderer>().color = Color.red;
				}
				else
				{
					tileGrid[i, j].GetComponent<SpriteRenderer>().color = Color.white;
				}
			}
		}
	}
}



public class Square
{
	public Vector2 pos {get; set;}
	public bool isEmpty {get; set;}
	public GameObject square {get; set;}
	public bool isDraggable {get; set;}
}