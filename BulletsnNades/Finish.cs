using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace BulletsnNades
{
    class Finish : GameObject
    {
        public Finish(float posX, float posY)
            : base("Finish", 50, 86, "door.png")
        {
            ObjectManager.AddGameObject(this);
            Position.X = posX;
            Position.Y = posY + 16 + Height / 2;
            CollisionData.CollisionEnabled = true;
            CollisionData.SetCollisionData(Width, Height);
        }
        public override void Update()
        {
            base.Update();
        }
        public override void CollisionReaction(CollisionInfo collisionInfo_)
        {
            base.CollisionReaction(collisionInfo_);
            GameObject with = collisionInfo_.collidedWithGameObject;
            if (with.Name == "PlayerBody")
            {
                PersistentData.Level += 1;
                if (PersistentData.Level == 1)
                {
                    GameStateManager.GoToState(new Level1());
                }
                if (PersistentData.Level == 2)
                {
                    GameStateManager.GoToState(new Level2());
                }
                if (PersistentData.Level == 3)
                {
                    GameStateManager.GoToState(new Level3());
                }
                if (PersistentData.Level == 4)
                {
                    GameStateManager.GoToState(new Level4());
                }
                if (PersistentData.Level == 5)
                {
                    GameStateManager.GoToState(new Level5());
                }
                if (PersistentData.Level == 6)
                {
                    GameStateManager.GoToState(new Level6());
                }
                if (PersistentData.Level == 7)
                {
                    GameStateManager.GoToState(new TitleScreen());
                }
            }
        }
    }
}
