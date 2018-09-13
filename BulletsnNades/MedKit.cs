using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace BulletsnNades
{
    class MedKit : GameObject
    {
        float heal;
        public MedKit(float posX, float posy)
            : base("MedKit", 50, 50, "MedKit.png")
        {
            ObjectManager.AddGameObject(this);
            Position.X = posX;
            Position.Y = posy + 8 + Height / 2;
            heal = PersistentData.medKitHeal;
            this.zOrder = 1;
            CollisionData.CollisionEnabled = true;
            CollisionData.SetCollisionData(Width, Height);
        }
        public override void CollisionReaction(CollisionInfo collisionInfo_)
        {
            base.CollisionReaction(collisionInfo_);
            GameObject with = collisionInfo_.collidedWithGameObject;
            if (with.Name == "PlayerBody")
            {
                if (PersistentData.Health + heal <= 100)
                {
                    PersistentData.Health += heal;
                }
                else
                {
                    PersistentData.Health = 100;
                }
                this.IsDead = true;
            }
        }
    }
}
