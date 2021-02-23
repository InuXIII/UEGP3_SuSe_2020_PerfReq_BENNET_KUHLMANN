using UEGP3PR.Code.Runtime.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace UEGP3PR.Code.Runtime.Turnbased
{
	[CreateAssetMenu(fileName = "SearchSettingsScript", menuName = "SearchAlgorithms/New SearchSettingsScript", order = 200)]
	public class SearchSettingsScript : ScriptableSingleton<SearchSettingsScript>
	{
		[FormerlySerializedAs("_normalNodeColor")]
		[Header("Colors for Search States")]
		[SerializeField] private Color _noneNodeColor;
		[FormerlySerializedAs("_QueueNodeColor")] [SerializeField] private Color _queueNodeColor;
		[SerializeField] private Color _processedNodeColor;
		[SerializeField] private Color _pathNodeColor;
		[Header("Colors for Node Types")]
		[SerializeField] private Color _groundNodeColor;
		[SerializeField] private Color _wallNodeColor;
		[SerializeField] private Color _waterNodeColor;
		
		[SerializeField] private int _waterNodeCost;
		
		
		public Color NoneNodeColor => _noneNodeColor;
		public Color WallNodeColor => _wallNodeColor;
		public Color WaterNodeColor => _waterNodeColor;
		public Color GroundNodeColor => _groundNodeColor;
		public Color QueueNodeColor => _queueNodeColor;
		public Color ProcessedNodeColor => _processedNodeColor;
		public Color PathNodeColor => _pathNodeColor;


		public int WaterNodeCost => _waterNodeCost;
	}
}