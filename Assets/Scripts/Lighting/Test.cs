using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Lighting
{
    public class Test : MonoBehaviour
    {
        public InputField input;

        public void TestLight()
        {
            LightManager.Instance.CreateLight(this.transform.position, float.Parse(input.text));
        }
    }
}
