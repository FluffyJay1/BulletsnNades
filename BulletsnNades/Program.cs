using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;

namespace BulletsnNades
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Game.Initialize(800, 600, 60, new TitleScreen());
            PersistentData.ResetData();
            //dont you DARE change the window dimensions
            while (Game.quit == false)
            {
                /* TO DO LIST
                 * What happens when the player dies---------------------CLOSE ENOUGH
                 * Enemy death animation---------------------------------DONE
                 * Make enemy AI better (moving after spotting player)---GET TO THIS LATER
                 * Health packs------------------------------------------DONE BIACH
                 * Pickups-----------------------------------------------EHH MAYBE NOT
                 * Other bullets-----------------------------------------DONE
                 *  Regular----------------------------------------------CHECK
                 *  Bleeding---------------------------------------------YISS
                 *  Bouncing---------------------------------------------YEAH
                 *  Explosive--------------------------------------------MADAFAKIN DOUBLE CHECK
                 *  (MAKE NPC'S SHOOT THEM AS WELL)----------------------okay den
                 * Grenades----------------------------------------------HOT DAMN WE STILL GOTTA DO THESE
                 *  Frag-------------------------------------------------YEAH BUDDY
                 *  Concussion-------------------------------------------MHMM
                 *  Sight------------------------------------------------MADAFAKIN FINALLYYYYYYYYYY
                 *  Anti-normal bullet field-----------------------------Heh, this'll be easy
                 * Beating the level-------------------------------------Just gotta add more levels
                 * Other levels------------------------------------------WORKIN ON IT
                 * Sounds------------------------------------------------SHIIIIIIIIIIIIIIIIET WE GON HAVE 2 WORK ON DIS
                 * Replace the mouse-------------------------------------DONE MADAFAKA
                 * Starting screen---------------------------------------COULD BE IMPROVED
                 * HUD--------------------------------------------------HIGHER PRIORITY
                 * Decorate the background and make it prettier---------LOWER PRIORITY
                 * If possible, support making the sight blocks 25 by 25
                 * Add noclip and impulse 101 cheats--------------------LOWEST PRIORITY
                 */
                if (InputManager.IsTriggered(Keys.Escape))
                {
                    Game.quit = true;
                }
                if (InputManager.IsTriggered(Keys.P))
                {
                    PersistentData.Impulse101();
                }
                Game.Update();
                Application.DoEvents();
            }
            
            Game.Destroy();
        }
    }
}
