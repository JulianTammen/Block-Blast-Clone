using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Tiles : MonoBehaviour
{
	public List<Vector2> _2x2Block = new List<Vector2>() {Vector2.zero, Vector2.right, Vector2.up, new Vector2(1, 1)};
	public List<Vector2> _ZZ1 = new List<Vector2>() {Vector2.zero, Vector2.up, Vector2.right, new Vector2(1, -1)};
}
