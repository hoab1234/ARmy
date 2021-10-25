namespace Mapbox.Examples
{
    using UnityEngine;
    using Mapbox.Utils;
    using Mapbox.Unity.Map;
    using Mapbox.Unity.MeshGeneration.Factories;
    using Mapbox.Unity.Utilities;
    using System.Collections.Generic;

    public class SpawnOnMap : MonoBehaviour
    {
        public static SpawnOnMap instance;

        [SerializeField]
        AbstractMap _map;

        [SerializeField]
        [Geocode]
        string[] _locationStrings;
        Vector2d[] _locations;

        [SerializeField]
        float _spawnScale = 100f;

        [SerializeField]
        GameObject _markerPrefab;

        [SerializeField]
        Transform zoomCamera;

        public bool isChangeScale = true;

        public Texture[] photos;
		public string[] titles;
        public string[] infos;

        List<GameObject> _spawnedObjects;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            _locations = new Vector2d[_locationStrings.Length];
            _spawnedObjects = new List<GameObject>();
            for (int i = 0; i < _locationStrings.Length; i++)
            {
                var locationString = _locationStrings[i];
                _locations[i] = Conversions.StringToLatLon(locationString);
                var instance = Instantiate(_markerPrefab);
                MarkerInfo markerInfo = instance.GetComponent<MarkerInfo>();
                markerInfo.index = i;
                instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
                instance.transform.localPosition = new Vector3(instance.transform.localPosition.x, 1.6f *(11 - GetNewRangeValue(-180, 3200, zoomCamera.transform.localPosition.z, 1, 10)), instance.transform.localPosition.z);
                instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                _spawnedObjects.Add(instance);
            }

            ChangeSpawnedObjectScaleAndY();
        }

        private void Update()
        {
            if (isChangeScale)
            {
                ChangeSpawnedObjectScaleAndY();
                // isChangeScale = false;
            }
        }

        private float GetNewRangeValue(float Range1Min, float Range1Max, float Range1SelectedValue, float Range2Min, float Range2Max)
        {
            float range1Percent = (Range1SelectedValue - Range1Min) / (Range1Max - Range1Min) * 100;
            float range2NewValue = (Range2Max - Range2Min) * range1Percent / 100 + Range2Min;

            return range2NewValue;
        }

        private void ChangeSpawnedObjectScaleAndY()
        {
            int count = _spawnedObjects.Count;
            for (int i = 0; i < count; i++)
            {
                var spawnedObject = _spawnedObjects[i];
                var location = _locations[i];
                spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
                spawnedObject.transform.localPosition = new Vector3(spawnedObject.transform.localPosition.x, 1.6f *(11 - GetNewRangeValue(-180, 3200, zoomCamera.transform.localPosition.z, 1, 10)), spawnedObject.transform.localPosition.z);
                spawnedObject.transform.localScale = Vector3.one * 2 * (11 - GetNewRangeValue(-180, 3200, zoomCamera.transform.localPosition.z, 1, 10));
            }
        }
    }
}