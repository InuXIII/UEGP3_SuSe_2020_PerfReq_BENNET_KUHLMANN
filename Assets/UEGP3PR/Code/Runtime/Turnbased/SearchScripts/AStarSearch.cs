using System.Collections.Generic;
using System.Linq;

namespace UEGP3PR.Code.Runtime.Turnbased.SearchScripts
{
    public class AStarSearch : SearchBase
    {
        protected override void InitializeSearch()
        {
            foreach (GridNode gridNode in _visited.Keys)
            {
                gridNode.Reset();
            }
			
            _openList = new List<GridNode>();
            _visited = new Dictionary<GridNode, GridNode>();

            _openList.Add(_startNode);

            _startNode.CostSoFar = 0;
        }

        protected override bool StepToGoal()
        {
            // sort all 
            _openList = _openList.OrderBy(n => n.CostSoFar + n.Heuristic).ToList();
            GridNode current = _openList[0];
			
            // goal found
            if (current == _goalNode)
            {
                return true;
            }

            foreach (GridNode next in current.Neighbours)
            {
                if (next.IsWall)
                {
                    continue;
                }

                float newCost = current.CostSoFar + next.Cost;
                bool alreadyVisited = _visited.ContainsKey(next);
                var nextCostSoFar = newCost;
                if (alreadyVisited)
                {
                    if (nextCostSoFar < next.CostSoFar)
                    {
                        next.CostSoFar = nextCostSoFar;
                        _visited[next] = current;
                        _openList.Add(next);
                        next.SetGridNodeSearchState(GridNodeSearchState.Queue);
                    }
                }
                else
                {
                    next.Heuristic = GetHeuristic(_goalNode, next);
					
                    _openList.Add(next);
                    _visited.Add(next, current);

                    next.CostSoFar = nextCostSoFar;
                    next.SetGridNodeSearchState(GridNodeSearchState.Queue);
                }
            }

            _openList.Remove(current);
            current.SetGridNodeSearchState(GridNodeSearchState.Processed);
            // not yet finished
            return false;
        }
        private float GetHeuristic(GridNode goal, GridNode next)
        {
            return (goal.transform.position - next.transform.position).magnitude;
        }
    }
}