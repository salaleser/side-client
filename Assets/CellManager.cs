using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
	public static CellManager instance;

	public List<Cell> cells;
	public GameObject cell;
	public NetworkManager networkManager;

	private const int Width = 8;
	private const int Height = 8;

	public static CellManager Instance()
    {
		return instance;
    }

	public class Cell
	{
		public GameObject O;
		public int X;
		public int Y;

		public Cell(GameObject o, int x, int y)
        {
			O = o;
			X = x;
			Y = y;
        }
	}

	private void Start()
	{
		instance = this;

		StartCoroutine(networkManager.Location(-9));

		cells = new();
		for (var i = 0; i < Width; i++)
		{
			for (var j = 0; j < Height; j++)
			{
				var o = Instantiate(cell, new Vector3(i, 0, j), Quaternion.identity);
				cells.Add(new Cell(o, i, j));
			}
		}
	}
}
