//first index in databse is 0

//data format in database: (voltage : current : restistance)

namespace SBD_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            fileHandler file = new fileHandler();
            Console.WriteLine("Hello World!");

            //file.generateNodes(4);

            //file.writeBlock();
            for(int i =0; i < file.getFileSize(); i++)
            {
                Node value = file.readRecord(i);

                Console.WriteLine(value.ToString());
            }
            

            //file.writeBlock();

            Console.ReadLine();
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
    }
}
