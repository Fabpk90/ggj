using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Trap
{
    public class Taupe : MonoBehaviour
    {       
        public int taupePoint = 0;//index of the spawn list

        private Sprite spriteTaupeVivante;
        private Sprite spriteTaupeMorte;

        private float timeSpawned = 0;
        public float timeBeforeDestroying;
        public float timeDying;

        public bool isBeingDestroy = false;
        private float startTimeDeath = -1;

        void Start()
        {
            taupePoint = ChasseTaupe.spawnPoint;
            
            spriteTaupeVivante = Resources.LoadAll<Sprite>("SpriteSheets/test/taupes")[1];
            spriteTaupeMorte = Resources.LoadAll<Sprite>("SpriteSheets/test/taupes")[2];

            GetComponent<SpriteRenderer>().sprite = spriteTaupeVivante;
            gameObject.AddComponent<BoxCollider>();
        }

        public void Update()
        {
            timeSpawned += Time.deltaTime;        

            //taupe getting away cuz too much time passed
            if (timeSpawned >= timeBeforeDestroying && !isBeingDestroy)
            {

                Debug.Log("timespawned over for taupe" + taupePoint + " life " + Life.life);

                isBeingDestroy = true;

                ChasseTaupe.AliveTaupe--;
                ChasseTaupe.removeFromTaupes(taupePoint);
                Life.life--;

                //Debug.Log(timeSpawned);

                //Lose();
            }

            if(isBeingDestroy)
            {
                if (startTimeDeath == -1)
                    startTimeDeath = Time.time;

                float step = (Time.time - startTimeDeath) / 5.0f;

                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.Lerp(GetComponent<SpriteRenderer>().color.a, 0.0f, step));
          
                if(GetComponent<SpriteRenderer>().color.a <= 0)
                    DestroyImmediate(gameObject);
            }
        }
        
        public void hitted()
        {
            GetComponent<SpriteRenderer>().sprite = spriteTaupeMorte;
          //  Destroy(gameObject, timeBeforeDestroying);
            DestroyImmediate(GetComponent<BoxCollider>());
            isBeingDestroy = true;

            ChasseTaupe.AliveTaupe--;
            ChasseTaupe.removeFromTaupes(taupePoint);
        }

        private void Lose()
        {
            //need to go to the gameover scene
            Life.life--;

            hitted();
        }

       

    }
}
