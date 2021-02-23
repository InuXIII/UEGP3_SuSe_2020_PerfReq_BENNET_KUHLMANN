using UnityEditor;
using UnityEngine;

namespace UEGP3PR.Code.Runtime.Core
{
	public abstract class ScriptableSingleton<T> : ScriptableObject where T : ScriptableSingleton<T>
	{
		private static T _instance;
		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = AssetDatabase.LoadAssetAtPath<T>($"Assets/UEGP3PR/ScriptableObjects/{typeof(T).Name}.asset");

                }
				
				return _instance;
			}
		}
	}
}