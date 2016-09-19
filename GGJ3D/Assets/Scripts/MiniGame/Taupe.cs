using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Trap
{
    class Taupe : MonoBehaviour
    {
        private Sprite spriteTaupe;
        private UInt32 timeStart;

        public Taupe( Vector3 pos, Sprite sprite)
        {
            this.timeStart = (UInt32)Time.time;
            transform.position = pos;
            spriteTaupe = sprite;
        }

        public void Update()
        {
            
        }
    }
}
