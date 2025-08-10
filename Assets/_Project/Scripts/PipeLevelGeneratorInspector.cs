using System;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;


namespace _Project.Scripts
{
	[CustomEditor(typeof(PipeLevelGenerator))]
	public class PipeLevelGeneratorInspector : Editor
	{
		private PipeLevelGenerator _generator;

		private void OnEnable ()
		{
			_generator = (PipeLevelGenerator)target;
		}

		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI();

			EditorGUILayout.Space();

			if (GUILayout.Button("Generate Level", GUILayout.Height(30)))
			{
				long timestamp = Stopwatch.GetTimestamp();

				_generator.GenerateLevel();

				long elapsed = Stopwatch.GetTimestamp() - timestamp;

				Debug.Log(TimeSpan.FromTicks(elapsed).TotalMilliseconds);

				EditorUtility.SetDirty(_generator);
				SceneView.RepaintAll();
			}
		}
	}
}
