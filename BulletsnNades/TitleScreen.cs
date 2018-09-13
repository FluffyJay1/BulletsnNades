using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;
using System.Windows.Forms;

namespace BulletsnNades
{
    class TitleScreen : State
    {
        public override void Create()
        {
            base.Create();
            GameObject background = new GameObject("TitleScreenScreen", 800, 600, "titlescreen.png");
            ObjectManager.AddGameObject(background);
            SoundManager.AddBackgroundMusic("BGMusic", "FluffyJay1Theme.wav", true);
            SoundManager.PlayBackgroundMusic("BGMusic");
        }
        public override void Update()
        {
            base.Update();

            if (InputManager.IsPressed(Keys.Space))
            {
                GameStateManager.GoToState(new Level1());
            }
        }
    }
}
