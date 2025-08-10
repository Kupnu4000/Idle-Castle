using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = System.Random;


namespace _Project.Scripts
{
	public class PipeLevelGenerator : MonoBehaviour
	{
		[Header("Colors")]
		[SerializeField] private Color _gridColor = Color.gray;
		[SerializeField] private Color _cellColor = Color.deepSkyBlue;
		[SerializeField] private Color _pipeColor = Color.yellowNice;

		[Header("Generation Settings")]
		[SerializeField] private int2 _levelSize = new int2(10, 10);
		[SerializeField] private int _pipeLength             = 5;
		[SerializeField] private int _pipeGenerationAttempts = 50;

		[SerializeField, HideInInspector] private List<int2> _pipe;

		private void OnValidate ()
		{
			if (_levelSize.x < 1) _levelSize.x = 1;
			if (_levelSize.y < 1) _levelSize.y = 1;

			int maxPipeLength = (int)(_levelSize.x * _levelSize.y * 0.75f);

			_pipeLength = Mathf.Clamp(_pipeLength, 1, maxPipeLength);
		}

		public void GenerateLevel ()
		{
			for (int i = 0; i < _pipeGenerationAttempts; i++)
			{
				if (GeneratePath(out _pipe))
					break;
			}
		}

		private const int MaxPipeGenAttempts = 10_000;

		private int _pipeGenAttempts;

		private bool GeneratePath (out List<int2> path)
		{
			Random prng = new Random();

			int2 startCoord = new int2(
				prng.Next(0, _levelSize.x),
				prng.Next(0, _levelSize.y)
			);

			path = new List<int2> { startCoord };

			HashSet<int2> visited = new HashSet<int2>();

			_pipeGenAttempts = MaxPipeGenAttempts;

			bool result = Backtrack(startCoord, path, visited, prng);

			path = result ? path : null;

			return result;
		}

		private bool Backtrack (int2 current, List<int2> path, HashSet<int2> visited, Random prng)
		{
			if (_pipeGenAttempts-- <= 0)
				return false;

			if (path.Count == _pipeLength)
				return true;

			IEnumerable<int2> neighbors = GetRandomNeighbors(current, prng);

			foreach (int2 neighbor in neighbors)
			{
				if (!IsWithinBounds(neighbor) || path.Contains(neighbor))
					continue;

				path.Add(neighbor);
				visited.Add(neighbor);

				if (Backtrack(neighbor, path, visited, prng))
					return true;

				path.RemoveAt(path.Count - 1);
				visited.Remove(neighbor);
			}

			return false;
		}

		private bool IsWithinBounds (int2 coord)
		{
			return coord.x >= 0 && coord.x < _levelSize.x &&
			       coord.y >= 0 && coord.y < _levelSize.y;
		}

		private static IEnumerable<int2> GetRandomNeighbors (int2 coord, Random prng)
		{
			return GetNeighbors(coord).OrderBy(_ => prng.Next());
		}

		private static IEnumerable<int2> GetNeighbors (int2 coord)
		{
			yield return coord + new int2(0,  1);  // Up
			yield return coord + new int2(1,  0);  // Right
			yield return coord + new int2(0,  -1); // Down
			yield return coord + new int2(-1, 0);  // Left
		}

		private void OnDrawGizmos ()
		{
			DrawGrid();
			DrawCells();
			DrawPath();
		}

		private void DrawGrid ()
		{
			Gizmos.color = _gridColor;

			Vector3 cellSize = new Vector3(1, 1, 0);

			for (int y = 0; y < _levelSize.y; y++)
			{
				for (int x = 0; x < _levelSize.x; x++)
				{
					Vector3 position = new Vector3(x, y, 0);
					Gizmos.DrawWireCube(position + cellSize * 0.5f, cellSize);
				}
			}
		}

		private void DrawCells ()
		{
			if (_pipe == null || _pipe.Count == 0)
				return;

			Gizmos.color = _cellColor;

			Vector3 cellSize = new Vector3(1, 1, 0);

			foreach (int2 coord in _pipe)
			{
				Vector3 position = new Vector3(coord.x, coord.y, 0);
				Gizmos.DrawCube(position + cellSize * 0.5f, cellSize);
			}
		}

		private void DrawPath ()
		{
			if (_pipe == null || _pipe.Count == 0)
				return;

			Vector3[] points = _pipe.Select(static coord => new Vector3(coord.x + 0.5f, coord.y + 0.5f, 0)).ToArray();

			Handles.color = _pipeColor;
			Handles.DrawPolyLine(points);
		}
	}
}
