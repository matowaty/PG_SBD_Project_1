using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SBD_1
{
    class fileHandler
    {
        private const int structSize = 3;
        private const int blockSize = structSize * 1;
        private const string file = "../../../test.bin";
        private const int intSize = 4;
        private int[] blockInMemory = new int[blockSize];
        private int blockInMemoryIndex = -1;

        public fileHandler()
        {
            Console.WriteLine("new handler created");
        }

        public void writeBlock(int blockIndex=1){
            Console.WriteLine("writing");
            //block in memory -> temp byte array -> save to pointer in file
            byte[] buffer = new byte[blockSize*intSize];
            byte[] tmp = new byte[intSize];
            Console.WriteLine(blockInMemory[0].GetType().Name);
            for(int i = 0; i < blockSize; i++)
            {
                tmp = BitConverter.GetBytes(blockInMemory[i]);
                buffer[i*intSize] = tmp[0];
                buffer[i*intSize +1] = tmp[1];
                buffer[i*intSize +2] = tmp[2];
                buffer[i*intSize +3] = tmp[3];
            }
            Console.WriteLine(buffer);
            buffer[1] = 0;
            buffer[3] = 0;
            Console.WriteLine(buffer);
            using (BinaryWriter writer = new BinaryWriter(new FileStream(file, FileMode.Open)))
            {
                writer.BaseStream.Seek(blockIndex*intSize, SeekOrigin.Begin);
                writer.Write(buffer, 0, blockSize * intSize);
            }


        }
        private void readBlock(int blockIndex=0){
            byte[] buffer = new byte[blockSize * intSize];
            using (BinaryReader reader = new BinaryReader(new FileStream(file, FileMode.Open)))
            {
                reader.BaseStream.Seek(blockIndex, SeekOrigin.Begin);
                reader.Read(buffer, 0, blockSize*intSize);
            }

            for(int i=0; i<blockSize;i++)
            {
                blockInMemory[i]=BitConverter.ToInt32(buffer, i*intSize);
            }
            blockInMemoryIndex = blockIndex;
        }

        public Node readRecord(int index)
        {
            int blockIndex = index / blockSize;
            if(blockInMemoryIndex!=blockIndex)
            {
                readBlock(blockIndex);
            }
            int internalIndex = index % blockSize;

            Node readValue = new Node(blockInMemory[internalIndex*structSize], blockInMemory[internalIndex*structSize+1], blockInMemory[internalIndex*structSize+2]);

            return readValue;

        }
    }
}
