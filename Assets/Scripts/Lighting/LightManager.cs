using Assets.Scripts.Util;
using Assets.Scripts.Util.ObjectPooling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Lighting
{
    public class LightManager : SingletonPersistentMonobehaviour<LightManager>
    {
        private static ObjectPool<LightMask> _objectPool = new ObjectPool<LightMask>();

        private List<LightMask> _spawnedLights = new List<LightMask>();

        public LightMask LightPrefab;


        public LightMask CreateLight (Vector3 position, float LightPower)
        {
            var light = _objectPool.GetObject(LightPrefab);
            light.transform.position = position;
            light.LightPower = LightPower;
            light.Expands = true;
            light.Fades = true;
            return light;
        }

        public void DestroyLight(LightMask light)
        {
            _objectPool.DestroyObject(light);
        }

    }
}
