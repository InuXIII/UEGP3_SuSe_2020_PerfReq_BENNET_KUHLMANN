using System.Collections.Generic;
using UEGP3PR.Code.Runtime.Turnbased.SearchScripts;
using UnityEngine;

namespace UEGP3PR.Code.Runtime.Turnbased
{
	public class Grid : MonoBehaviour
	{
		[SerializeField] private GridNode _prefab;
		[SerializeField] private Marker _startMarkerPrefab;
		[SerializeField] private Marker _endMarkerPrefab;
		[SerializeField] private SearchBase _searchAlgorithm;
		[SerializeField] private float _width;
		[SerializeField] private float _height;

		private int _counter = 0;
		private List<GridNode> _nodes = new List<GridNode>();
		
		private void Awake()
		{
			//CreateGrid();
			// RandomlyScatterStart();
			// RandomlyScatterEnd();
			//InitNodes();
			GetGrid();
			LoadGrid(1);
		}

		// private void RandomlyScatterStart()
		// {
		// 	_searchAlgorithm.StartMarker = PlaceMarker(_startMarkerPrefab);
		// }
		//
		// private void RandomlyScatterEnd()
		// {
		// 	_searchAlgorithm.EndMarker = PlaceMarker(_endMarkerPrefab);
		// }
		//
		// private Marker PlaceMarker(Marker prefab)
		// {
		// 	int ranX = (int) Random.Range(0, _width);
		// 	int ranY = (int) Random.Range(0, _height);
		//
		// 	Marker placeMarker = Instantiate(prefab, transform.position + new Vector3(ranX, ranY, 0), Quaternion.identity, null);
		// 	//placeMarker.SetToClosestGridNode();
		// 	
		// 	return placeMarker;
		// }

		private void InitNodes()
		{
			foreach (GridNode gridNode in _nodes)
			{
				gridNode.Init();
			}
		}

		 private void CreateGrid()
		 {
		 	for (int x = 0; x < _width; x++)
		 	{
		 		for (int y = 0; y < _height; y++)
		 		{
		 			GridNode gridNode = Instantiate(_prefab, transform.position + new Vector3(x, y, 0), Quaternion.identity, transform);
				_nodes.Add(gridNode); 
		        } 
		    }
		}

		// Getting all Gridnodes, instead of getting all Gridnodes when instantiating them.
		private void GetGrid()
		{
			// GameObject[] children = new GameObject[transform.childCount];
			// for (int i = 0; i < children.Length-1; i++)
			// {
			// 	_nodes.Add(children[i].GetComponent<GridNode>());
			// }

			foreach (Transform child in transform)
			{
				_nodes.Add(child.GetComponent<GridNode>());
			}
			
			InitNodes();
		}

		public void SaveGrid(int slot)
		{
			_counter = 0;
			foreach (var VARIABLE in _nodes)
			{
				PlayerPrefs.SetString("" + slot + VARIABLE + _counter, VARIABLE.Type.ToString());
				Debug.Log(PlayerPrefs.GetString("" + VARIABLE));
				_counter++;
				PlayerPrefs.Save();
			}
		}

		public void LoadGrid(int slot)
		{
			_counter = 0;
			foreach (var VARIABLE in _nodes)
			{
				string key = PlayerPrefs.GetString("" + slot + VARIABLE + _counter);
				if (key == "Ground")
				{ 
					VARIABLE.SetGridNodeType(GridNodeType.Ground);
				}
				if (key == "Wall")
				{ 
					VARIABLE.SetGridNodeType(GridNodeType.Wall);
				}
				if (key == "Water")
				{ 
					VARIABLE.SetGridNodeType(GridNodeType.Water);
				}
				_counter++;
			}
		}
	}
}