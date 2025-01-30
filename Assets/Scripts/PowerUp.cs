using UnityEngine;

namespace Shmup
{
    public class PowerUp : MonoBehaviour
    {
        
        public bool activateShield;
        public bool activateDoubleShot;
        public bool increaseSpeed;
        public bool increaseHealth;
        public bool increaseAttackSpeed;
        public bool points;
        public bool Joker;
        public int pointValue;


        void Start()
        {
            Destroy(gameObject, 5);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
