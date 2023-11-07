using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SBD_1
{
    class fileHandler
    {
        private const int nodeSize = 3;                         //amount of int in one node
        private const int nodesInBlock = 1;                     //amount of nodes in one block
        private const int blockSize = nodeSize * nodesInBlock;  //amount of int in one block
        private const string file = "../../../test.bin";
        private const int intSize = 4;                          //size of int in bytes
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
                reader.BaseStream.Seek(blockIndex*(blockSize * intSize), SeekOrigin.Begin);
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
            int blockIndex = index / nodesInBlock;
            if(blockInMemoryIndex!=blockIndex)
            {
                readBlock(blockIndex);
            }
            int internalIndex = index % nodesInBlock;

            Node readValue = new Node(blockInMemory[internalIndex*nodeSize], blockInMemory[internalIndex*nodeSize+1], blockInMemory[internalIndex*nodeSize+2]);

            return readValue;

        }
    }
}
