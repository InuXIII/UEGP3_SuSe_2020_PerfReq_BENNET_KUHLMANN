using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UEGP3PR.Code.Runtime.Turnbased
{
	public abstract class SearchBase : MonoBehaviour
	{
		[SerializeField] private bool _animate;
		[SerializeField] private float _timeBetweenSteps;
		protected Marker _startMarker;
		protected Marker _endMarker;
		protected List<GridNode> _openList = new List<GridNode>();
		protected Dictionary<GridNode, GridNode> _visited = new Dictionary<GridNode, GridNode>();
		protected GridNode _startNode;
		protected GridNode _goalNode;
		public Marker StartMarker { set => _startMarker = value; }
		public Marker EndMarker { set => _endMarker = value; }

		public void Search()
		{
			if (_animate)
			{
				StartCoroutine((IEnumerator) SearchInternalCoroutine());
			}
			else
			{
				SearchInternal();
			}
		}

		private IEnumerator SearchInternalCoroutine()
		{
			InitializeSearch();
			yield return new WaitForSeconds(_timeBetweenSteps);

			while (_openList.Count > 0)
			{
				if (StepToGoal())
				{
					break;
				}

				yield return new WaitForSeconds(_timeBetweenSteps);
			}
			
			// reconstruct path
			BuildPath();
		}

		private void BuildPath()
		{
			GridNode current = _goalNode;
			List<GridNode> path = new List<GridNode>();

			while (!current.Equals(_startNode))
			{
				path.Add(current);
				current = _visited[current];
			}

			path.Add(_startNode);
			path.Reverse();
			foreach (GridNode gridNode in path)
			{
				gridNode.SetGridNodeSearchState(GridNodeSearchState.PartOfPath);
			}
		}

		private void SearchInternal()
		{
			// initialise search
			InitializeSearch();

			// perfom search loop
			while (_openList.Count > 0)
			{
				if (StepToGoal())
				{
					break;
				}
			}

			// reconstruct path 
			BuildPath();
		}

		protected abstract void InitializeSearch();
		protected abstract bool StepToGoal();
	}
}