using System.Collections.Generic;
using System.Data.Common;
using UnityEditor.SearchService;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
	private bool isDragging = false;
	private Vector3 offset;
	public GameObject draggedTile;
	public Vector2 origin;

	private void Start()
	{
		
	}


	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);		
			
			if (hit.collider != null)
			{	
				draggedTile = hit.transform.gameObject.transform.parent.gameObject;		
				offset = draggedTile.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
				isDragging = true;
			}
			
			origin = draggedTile.transform.position;
		}

		if (Input.GetMouseButtonUp(0))
		{
			isDragging = false;
			
			if(draggedTile != null)
			{
				Vector2 roundedPos = new Vector2(Mathf.RoundToInt(draggedTile.transform.position.x), Mathf.RoundToInt(draggedTile.transform.position.y));

				draggedTile.transform.position = roundedPos;
				
				
				
				switch(draggedTile.tag)
				{
					case "2x2Block":
						GameObject.Find("GameHandler").GetComponent<GameHandler>().AddToGrid(GameObject.Find("GameHandler").GetComponent<Tiles>()._2x2Block, draggedTile);
						break;
					
					case "ZZ1":
						GameObject.Find("GameHandler").GetComponent<GameHandler>().AddToGrid(GameObject.Find("GameHandler").GetComponent<Tiles>()._ZZ1, draggedTile);
						break;
					
					default:
						Debug.Log("draggedTile.name was not found");
						break;
				}
			}			
		}

		if (isDragging)
		{
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			draggedTile.transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, transform.position.z);
		}
	}
}