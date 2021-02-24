using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UEGP3PR.Code.Runtime.Turnbased
{
	public abstract class SearchBase : MonoBehaviour
	{
		[SerializeField] private bool _animate;
		[SerializeField] private float _timeBetweenSteps;
		protected List<GridNode> _openList = new List<GridNode>();
		protected Dictionary<GridNode, GridNode> _visited = new Dictionary<GridNode, GridNode>();
		protected GridNode _startNode;
		protected GridNode _goalNode;
		public List<GridNode> _path = new List<GridNode>();

		public void Search(GridNode startNode,GridNode endNode)
		{
			_startNode = startNode;
			_goalNode = endNode;
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

			while (!current.Equals(_startNode))
			{
				_path.Add(current);
				current = _visited[current];
			}

			_path.Add(_startNode);
			_path.Reverse();
			foreach (GridNode gridNode in _path)
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