using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace BulletsnNades
{
    class nullfield : GameObject
    {
        float timer;
        float hpPerSecond;
        public nullfield()
            : base("nullfield", 600, 600, "nullfield.png")
        {
            ObjectManager.AddGameObject(this);
            this.zOrder = 1;
            this.Opacity = 0;
            timer = PersistentData.nullFieldLength;
            CollisionData.CollisionEnabled = true;
            CollisionData.SetCollisionData(250);
        }
        public override void Update()
        {
            base.Update();
            hpPerSecond = PersistentData.nullFieldHeal * FrameRateController.FrameTime;
            if (timer >= PersistentData.nullFieldLength - 1)
            {
                Opacity += FrameRateController.FrameTime;
            }
            if (timer <= 1)
            {
                Opacity -= FrameRateController.FrameTime;
            }
            if (timer <= 0)
            {
                this.IsDead = true;
            }
            timer -= FrameRateController.FrameTime;
        }
        public override void CollisionReaction(CollisionInfo collisionInfo_)
        {
            base.CollisionReaction(collisionInfo_);
            GameObject with = collisionInfo_.collidedWithGameObject;
            if (with.Name == "PlayerBody")
            {
                if (PersistentData.Health <= 100)
                {
                    PersistentData.Health += hpPerSecond;
                }
            }
        }
    }
}
