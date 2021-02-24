using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UEGP3PR.Code.Runtime.Turnbased;
using UnityEngine;

namespace UEGP3PR.Code.Runtime.Turnbased
{
    public class Enemy : Marker
    {
        [SerializeField] private float _searchRadius;
        [SerializeField]private LayerMask _playerLayer;
        [SerializeField]private SearchBase _aStar;
        private Collider2D _overlapCircle;
        private GridNode _goalNode;
        private GridNode _startNode;
        private EnemyStates _currentenemyStates;
        [SerializeField]private GameObject[] _wayPoints;
        
        
        public void SetEnemyState(EnemyStates NewEnemyState)
        {
            _currentenemyStates = NewEnemyState;
            switch (NewEnemyState)
                {
                    case EnemyStates.Start:
                        SetEnemyState(EnemyStates.WalkToPoint);
                        return;
                    case EnemyStates.StayAtPoint:
                        FindPlayer();
                        return;
                    case EnemyStates.WalkToPoint:
                            PatrolPoints();
                        return;
                    case EnemyStates.FindPathToTarget:
                            FindPathToPlayer();
                        return;
                    case EnemyStates.MoveToTarget:
                            MoveAlongPath();
                        return;
                    case EnemyStates.End:
                        return;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(NewEnemyState));
                }
        }

        private void Awake()
        {
            SetToClosestGridNode();
            SetEnemyState(EnemyStates.Start);
        }

        void Start()
        {
           //FindPlayer();
           // _goalNode = _overlapCircle.gameObject.GetComponent<Player>().ClosestGridNode;
          //  _startNode = ClosestGridNode;
           // FindPathToPlayer();

        }

        private void FindPathToPlayer()
        {
            _aStar.Search(_startNode,_goalNode);
            SetEnemyState(EnemyStates.MoveToTarget);

        }

        private void MoveAlongPath()
        {

            foreach (var VARIABLE in _aStar._path)
            {
                this.transform.position = (VARIABLE.transform.position);
                
            }

            SetEnemyState(EnemyStates.End);
        }


        private void PatrolPoints()
        {
            SetToClosestGridNode();
            WayPointMarker marker = _wayPoints[0].GetComponent<WayPointMarker>();
            _startNode = ClosestGridNode;
            _goalNode = marker.ClosestGridNode;
            _aStar.Search(_startNode, _goalNode);
            SetEnemyState(EnemyStates.StayAtPoint);
            this.transform.position = _goalNode.transform.position;

        }


        //returns GridNode that player stands on
        
        private void FindPlayer()
        {
            _overlapCircle = Physics2D.OverlapCircle(transform.position, _searchRadius, _playerLayer);
            if (_overlapCircle != null)
            {
                SetEnemyState(EnemyStates.FindPathToTarget);
            }
        }

    }
    
    
}

