using System.Collections.Generic;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Algorithms;
using Gamelogic.Grids2.Graph;
using UnityEngine;
using System.Linq;

namespace Gamelogic.Grids2.Examples
{
	public class SpaceFillingCurvesExample : GLMonoBehaviour
	{
		#region Constants
		private const int HilbertMax = 1024;
		private const int PeanoMax = 729;
		private const int GosperMax = 2401;

		#endregion

		#region  Types

		public enum CurveType
		{
			Hilbert,
			Peano,
			Gosper
		}

		#endregion

		#region Public Fields
		[Header("Runtime tweakables")]
		public CurveType curve;

		[Range(0f, 1f)]
		[Comment("Count is normalized, it means that:\n0 is 0 points\n1 is a constant maxPoints for a given curve")]
		public float count;

		[Header("Other")]
		public bool stretchMarker;
		public GameObject marker;
		

		
		public Gradient gradient;
		#endregion

		#region Private Fields

		private List<GameObject> markers;
		private ObservedValue<int> pointCount;
		private GridMap<GridPoint2> gridMap = null;
		//private IGenerator<GridPoint2> generator = null;
		private List<GridPoint2> points;
		private int pointMax;
		public ObservedValue<CurveType> observedCurve;

		#endregion

		#region Unity Messages
		public void Start()
		{
			markers = new List<GameObject>();

			observedCurve = new ObservedValue<CurveType>(curve);
			observedCurve.OnValueChange += UpdateCurveType;

			pointCount = new ObservedValue<int>(pointMax);
			pointCount.OnValueChange += UpdateCurvePoints;

			UpdateCurveType();
		}

		public void Update()
		{
			pointCount.Value = Mathf.FloorToInt(Mathf.Lerp(1, pointMax, count));
			observedCurve.Value = curve;
		}
		#endregion

		#region Public Methods

		public void Generate()
		{
			var previous = default(GameObject);

			for (var i = 0; i < pointCount.Value; i++)
			{
				var position = gridMap.GridToWorld(points[i]);
				var name = i.ToString();
				var color = gradient.Evaluate(i / (pointCount.Value - 1f));

				previous = CreateMarker(i, previous, position, name, color);
			}

			for (var i = pointCount.Value; i < markers.Count; i++)
			{
				markers[i].SetActive(false);
			}
		}
		#endregion

		#region Private Members
		private void UpdateCurveType()
		{
			markers.ForEach(m => Destroy(m));
			markers.Clear();
			IGenerator<GridPoint2> generator = null;

			switch (curve)
			{
				case CurveType.Hilbert:
					pointMax = HilbertMax;
					gridMap = GetRectMap();
					generator = SpaceFillingCurveGenerator.Hilbert();
					break;

				case CurveType.Peano:
					pointMax = PeanoMax;
					gridMap = GetRectMap();
					generator = SpaceFillingCurveGenerator.Peano();
					break;

				case CurveType.Gosper:
					pointMax = GosperMax;
					gridMap = GetHexMap();
					generator = SpaceFillingCurveGenerator.Gosper();
					break;
			}
			points = generator.Next(pointMax).ToList();
			//Generate();
		}

		public void UpdateCurvePoints()
		{
			Generate();
		}

		private GridMap<GridPoint2> GetHexMap()
		{
			var spaceMap = Map
				.Linear(PointyHexPoint.SpaceMapTransform)
				.PreScale(Vector3.one * 0.5f);
			var roundMap = Map.RectRound();

			return new GridMap<GridPoint2>(spaceMap, roundMap);
		}

		private GridMap<GridPoint2> GetRectMap()
		{
			var spaceMap = Map.Linear(Matrixf33.Identity);
			var roundMap = Map.RectRound();

			return new GridMap<GridPoint2>(spaceMap, roundMap);
		}

		private GameObject CreateMarker(int index, GameObject previous, Vector3 position, string markerName, Color color)
		{
			var newMarker = default(GameObject);

			if (markers.Count <= index)
			{
				newMarker = Instantiate(marker);
				markers.Add(newMarker);

				newMarker.transform.parent = transform;
				newMarker.transform.position = position;
				newMarker.name = markerName;

				var material = newMarker.GetComponent<Renderer>().sharedMaterial;

				material = new Material(material);
				newMarker.GetComponent<Renderer>().sharedMaterial = material;
			}
			else
			{
				newMarker = markers[index];
				newMarker.SetActive(true);
			}

			newMarker.GetComponent<Renderer>().sharedMaterial.SetColor("_EmissionColor", color);
			newMarker.transform.localScale = Vector3.one * 0.25f;

			if (newMarker.transform.childCount > 0)
			{
				newMarker.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial =
					newMarker.GetComponent<Renderer>().sharedMaterial;
			}

			if (stretchMarker && previous != null)
			{
				previous.transform.LookAt(newMarker.transform.position);
				previous.transform.SetScaleZ(Vector3.Distance(previous.transform.position, newMarker.transform.position));
			}

			return newMarker;
		}

		#endregion
	}
}