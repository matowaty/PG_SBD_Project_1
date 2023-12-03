//first index in databse is 0

//data format in database: (voltage : current : restistance)

namespace SBD_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            fileHandler drive = new fileHandler();
            fileHandler[] pipes = new fileHandler[2];
            pipes[0] = new fileHandler("../../../pipe1.bin");
            pipes[1] = new fileHandler("../../../pipe2.bin");
            Console.WriteLine("Starting simulation");

            //pipes[1].generateNodes(1);

            //drive.writeBlock();
            for(int i = 0; i < drive.getNumberOfNodes(); i++)
            {
                Node value = drive.readRecord(i);

                Console.WriteLine(value.ToString());
            }
            

            //file.writeBlock();

            Console.ReadLine();

            /*bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }*/
        }
    }

    public struct Node
    {
        public float voltage;
        public float current;
        public float resistance;
        public Node(float voltage, float current, float resistance)
        {
            this.voltage = voltage;
            this.current = current;
            this.resistance = resistance;
        }

        public Node(float voltage, float current)
        {
            this.voltage = voltage;
            this.current = current;
            this.resistance = voltage/current;
        }

        public override string ToString()
        {
            return voltage.ToString() + " " + current.ToString() + " " + resistance.ToString();
        }

        //main menu loop
        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Reverse String");
            Console.WriteLine("2) Remove Whitespace");
            Console.WriteLine("3) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    return true;
                case "2":
                    return true;
                case "3":
                    return false;
                default:
                    return true;
            }
        }
    }
}
