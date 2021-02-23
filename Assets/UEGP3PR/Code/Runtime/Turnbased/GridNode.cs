using System;
using System.Collections.Generic;
using UnityEngine;

namespace UEGP3PR.Code.Runtime.Turnbased
{
	public class GridNode : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer _image = default;
		[SerializeField] private SpriteRenderer _searchStateImage = default;
		public float CostSoFar { get; set; }
		public IEnumerable<GridNode> Neighbours => _neighbours;
		public bool IsWall => _type == GridNodeType.Wall;
		public float Heuristic { get; set; }
		private List<GridNode> _neighbours = new List<GridNode>();
		private GridNodeType _type;
		private GridNodeSearchState _searchState;

		public GridNodeType Type => _type;
		public float Cost
		{
			get
			{
				switch (_type)
				{
					case GridNodeType.Ground:
						return 1;
					case GridNodeType.Wall:
						return int.MaxValue;
					case GridNodeType.Water:
						return SearchSettingsScript.Instance.WaterNodeCost;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		public void Init()
		{
			Reset();
			FindNeighbours();
			SetGridNodeType(GridNodeType.Ground);
			SetGridNodeSearchState(GridNodeSearchState.None);

			// _searchStateImage.enabled = false;
		}

		private void OnMouseDown()
		{
			switch (_type)
			{
				case GridNodeType.Ground:
					SetGridNodeType(GridNodeType.Wall);
					break;
				case GridNodeType.Wall:
					SetGridNodeType(GridNodeType.Water);
					break;
				case GridNodeType.Water:
					SetGridNodeType(GridNodeType.Ground);
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void Reset()
		{
			SetGridNodeSearchState(GridNodeSearchState.None);
			CostSoFar = -1000;
		}
		

		public void SetGridNodeSearchState(GridNodeSearchState state)
		{
			_searchState = state;
			_searchStateImage.enabled = true;
			switch (state)
			{
				case GridNodeSearchState.None:
					_searchStateImage.color = SearchSettingsScript.Instance.NoneNodeColor;
					break;
				case GridNodeSearchState.Queue:
					_searchStateImage.color = SearchSettingsScript.Instance.QueueNodeColor;
					break;
				case GridNodeSearchState.Processed:
					_searchStateImage.color = SearchSettingsScript.Instance.ProcessedNodeColor;
					break;
				case GridNodeSearchState.PartOfPath:
					_searchStateImage.color = SearchSettingsScript.Instance.PathNodeColor;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(state), state, null);
			}
		}

		public void SetGridNodeType(GridNodeType type)
		{
			_type = type;
			switch (type)
			{
				case GridNodeType.Ground:
					_image.color = SearchSettingsScript.Instance.GroundNodeColor;
					break;
				case GridNodeType.Wall:
					_image.color = SearchSettingsScript.Instance.WallNodeColor;
					break;
				case GridNodeType.Water:
					_image.color = SearchSettingsScript.Instance.WaterNodeColor;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}
		}

		private void FindNeighbours()
		{
			RaycastHit2D raycastHit2D = Physics2D.Linecast(transform.position, transform.position + Vector3.up, ~LayerMask.NameToLayer("Grid"));
			if ((raycastHit2D.collider != null) && raycastHit2D.collider.TryGetComponent(out GridNode node))
			{
				_neighbours.Add(node);
			}
			raycastHit2D = Physics2D.Linecast(transform.position, transform.position + Vector3.right, ~LayerMask.NameToLayer("Grid"));
			if ((raycastHit2D.collider != null) && raycastHit2D.collider.TryGetComponent(out node))
			{
				_neighbours.Add(node);
			}
			raycastHit2D = Physics2D.Linecast(transform.position, transform.position + Vector3.down, ~LayerMask.NameToLayer("Grid"));
			if ((raycastHit2D.collider != null) && raycastHit2D.collider.TryGetComponent(out node))
			{
				_neighbours.Add(node);
			}
			raycastHit2D = Physics2D.Linecast(transform.position, transform.position + Vector3.left, ~LayerMask.NameToLayer("Grid"));
			if ((raycastHit2D.collider != null) && raycastHit2D.collider.TryGetComponent(out node))
			{
				_neighbours.Add(node);
			}

		}
		
	}
}