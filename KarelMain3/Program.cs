using KarelTheRobot.Library; // based on 
using KarelTheRobot.Library.Config;

namespace KarelMain
{
    internal class Program
    {
        // set your console font to Consolas.
        static void Main(string[] args)
        {
            Steps();
            //ClearBorderBeepers();
            //MoveBeeperAcrossBoundary();
        }
        private static void Steps()
        {
            World world = new World(WorldConfig.CornerBeepers, 200);
            Robot karel = new Robot(world);
            
            int beeperCount = 1; //Robot starts with 1. changing this 
                                 //Does not change the number of beepers the robot has.

            karel.TurnOn();
            karel.Move();
            karel.PutBeeper();

            while (karel.IsFrontClear)
            {
                if (karel.AreAnyBeepersInBag)
                {
                    karel.PutBeeper();
                }
                karel.Move();
            }

            world.Log("Turning Off");
            karel.TurnOff();
        }
        private static void MoveBeeperAcrossBoundary()
        {
            World world = new World(WorldConfig.FromJson("crossTheBoundary.json"));
            Robot karel = new Robot(world);

            var streetCount = 0;

            karel.TurnOn();

            karel.Move();
            karel.PickBeeper();

            while (karel.IsFrontClear)
            {
                karel.Move();
            }

            karel.TurnLeft();

            while (!karel.IsRightClear)
            {
                streetCount++;
                karel.Move();
            }

            TurnRight(karel);
            karel.Move();

            while (!karel.IsRightClear)
            {
                karel.Move();
            }

            TurnRight(karel);

            for (int i = 0; i < streetCount; i++)
            {
                karel.Move();
            }

            karel.TurnLeft();

            while (karel.IsFrontClear)
            {
                karel.Move();
            }

            TurnAround(karel);
            karel.Move();
            karel.PutBeeper();
            TurnAround(karel);
            karel.Move();

            karel.TurnOff();
        }

        private static void ClearBorderBeepers()
        {
            var world = new World(WorldConfig.FromJson("borderBeepers.json"));
            var karel = new Robot(world);

            int beeperCount = 0;

            karel.TurnOn();

            while (karel.IsFrontClear)
            {
                karel.Move();
            }

            karel.TurnLeft();

            while (karel.IsNextToBeeper)
            {
                world.Log($"I've got {beeperCount} beepers!");
                beeperCount++;
                karel.PickBeeper();
                if (!karel.IsFrontClear)
                {
                    karel.TurnLeft();
                }
                karel.Move();
            }

            karel.TurnOff();
        }

        private static void TurnRight(Robot karel)
        {
            karel.TurnLeft();
            karel.TurnLeft();
            karel.TurnLeft();
        }

        private static void TurnAround(Robot karel)
        {
            karel.TurnLeft();
            karel.TurnLeft();
        }
    }
}
