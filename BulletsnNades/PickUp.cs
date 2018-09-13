using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace BulletsnNades
{
    class PickUp : GameObject
    {
        string item;
        public PickUp(float posX, float posy, string image)
            : base("PickUp", 20, 20, image)
        {
            ObjectManager.AddGameObject(this);
            Position.X = posX;
            Position.Y = posy + 8 + Height / 2;
            this.zOrder = 1;
            item = image;
            CollisionData.CollisionEnabled = true;
            CollisionData.SetCollisionData(Width, Height);
        }
        public override void CollisionReaction(CollisionInfo collisionInfo_)
        {
            base.CollisionReaction(collisionInfo_);
            GameObject with = collisionInfo_.collidedWithGameObject;
            if (with.Name == "PlayerBody")
            {
                if (item == "bullet2.png")
                {
                    PersistentData.bullet2 = true;
                }
                if (item == "bullet3.png")
                {
                    PersistentData.bullet3 = true;
                }
                if (item == "bullet4.png")
                {
                    PersistentData.bullet4 = true;
                }
                if (item == "grenade2.png")
                {
                    PersistentData.grenade2 = true;
                }
                if (item == "grenade3.png")
                {
                    PersistentData.grenade3 = true;
                }
                if (item == "grenade4.png")
                {
                    PersistentData.grenade4 = true;
                }
                this.IsDead = true;
            }
        }
    }
}
