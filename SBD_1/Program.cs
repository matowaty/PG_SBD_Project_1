//first index in databse is 0

//data format in database: (voltage : current : restistance)

using System.ComponentModel.DataAnnotations;

namespace SBD_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            fileHandler file = new fileHandler();
            Console.WriteLine("Hello World!");

            //file.writeBlock();
            Node value = file.readRecord(1);

            Console.WriteLine(value.ToString());

            //file.writeBlock();

            
            




            Console.ReadLine();
        }
    }

    public struct Node
    {
        public int voltage;
        public int current;
        public int resistance;
        public Node(int voltage, int current, int resistance)
        {
            this.voltage = voltage;
            this.current = current;
            this.resistance = resistance;
        }

        public override string ToString()
        {
            return voltage.ToString() + " " + current.ToString() + " " + resistance.ToString();
        }
    }
}
