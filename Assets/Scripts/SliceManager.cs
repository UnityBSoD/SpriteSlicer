using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpriteSlicer
{
    public class SliceManager : MonoBehaviour
    {
        public Material sliceMaterial;

        public static SliceManager Instance
        { get { return _instance; } }

        static SliceManager _instance;
        [Range(5, 500)] public float force = 5;

        void Awake()
        {
            _instance = this;
        }

        public void Slice(Transform _target, Vector2 _startPos, Vector2 _endPos)
        {
            var sliceEngine = new SliceEngine(_target, _startPos, _endPos);

            sliceEngine.Slice();
        }

        public void ChangeForce(bool _bool)
        {
            if (_bool)
            {
                force = 50;
            }
            else
            {
                force = 5;
            }
        }
    }

}
