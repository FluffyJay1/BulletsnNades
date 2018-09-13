using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace BulletsnNades
{
    class HelperObject : GameObject
    {
        float bleedTimer;
        public int bleedTicks;
        public float xVel;
        public float yVel;
        public float bullet1CoolDown;
        public float bullet2CoolDown;
        public float bullet3CoolDown;
        public float bullet4CoolDown;

        public float grenade1CoolDown;
        public float grenade2CoolDown;
        public float grenade3CoolDown;
        public float grenade4CoolDown;

        public HelperObject()
            : base("HelperObject", 0, 0, null)
        {
            ObjectManager.AddGameObject(this);
            bleedTimer = 0;
            bleedTicks = 0;
            xVel = 0;
            yVel = 0;
        }
        public override void Update()
        {
            base.Update();
            Bleed();
        }
        void Bleed()
        {
            if (bleedTimer <= 0 && bleedTicks > 0)
            {
                bleedTimer = 0.4f;
                bleedTicks -= 1;
                PersistentData.Health -= PersistentData.bleedDamage;
            }
            if (bleedTimer > 0)
            {
                bleedTimer -= FrameRateController.FrameTime;
            }
        }
    }
}
