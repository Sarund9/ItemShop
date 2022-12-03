using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemShop
{
    public class CameraController : MonoBehaviour
    {

        [SerializeField]
        Camera cam;

        [SerializeField, Range(.001f, .1f)]
        float baseLerp = .01f, zoomLerp = .01f;

        /*
        Follow Player
        - When Talking: focus on the

         
         */
        HashSet<Modifier> modifiers = new();

        public Modifier CreateModifier()
        {
            var mod = new Modifier(this);
            modifiers.Add(mod);
            return mod;
        }

        private bool DeleteModifier(Modifier modifier)
        {
            return modifiers.Remove(modifier);
        }

        private void LateUpdate()
        {
            Vector3 desiredPos = Vector3.zero;
            float desiredZoom = 4;
            float lerp = baseLerp;
            foreach (var mod in modifiers)
            {
                if (mod.Target)
                {
                    desiredPos = Vector2.Lerp(desiredPos, mod.Target.position, mod.TargetWeight);
                }
                if (mod.Zoom is float f)
                {
                    desiredZoom = f;
                }
                desiredPos += new Vector3(mod.Offset.x, mod.Offset.y, 0);
            }

            desiredPos.z = -10;
            transform.position = Vector3.Lerp(transform.position, desiredPos, lerp);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, desiredZoom, zoomLerp);
        }

        public class Modifier
        {
            private readonly CameraController controller;

            public Modifier(CameraController controller)
            {
                this.controller = controller;
            }

            /// <summary> If not null, Camera moves to this by TargetWeight </summary>
            public Transform Target { get; set; }

            /// <summary> If Target is not null, determines Interpolation </summary>
            public float TargetWeight { get; set; } = 1.0f;

            /// <summary> If not null, Camera zooms out to this </summary>
            public float? Zoom { get; set; }

            public Vector2 Offset { get; set; }

            public bool Disable() => controller.DeleteModifier(this);
        }
    }
}
