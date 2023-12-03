namespace SBD_1
{
    class fileHandler
    {
        //can change
        private const int nodesInBlock = 10;                     //amount of nodes in one block
        private string file = "../../../drive.bin";
        //can't change
        private const int nodeSize = 3;                         //amount of float variables in one node
        private const int blockSize = nodeSize * nodesInBlock;  //amount of floats in one block
        private const int floatSize = 4;                          //size of float in bytes
        private float[] blockInMemory = new float[blockSize];
        private int blockInMemoryIndex = -1;
        private int nodesInFile = 0;                            //amount of nodes saved in file

        public fileHandler(string path = "../../../drive.bin")
        {
            this.file = path;
            Console.WriteLine("New handler created");
            nodesInFile = filesize()/(nodeSize*floatSize);
            Console.WriteLine("File is initialized with: {0} nodes", nodesInFile);
        }

        private void writeBlock(int blockIndex=0){
            Console.WriteLine("writing");
            //block in memory -> temp byte array -> save to pointer in file
            byte[] buffer = new byte[blockSize*floatSize];
            byte[] tmp;
            Console.WriteLine(blockInMemory[0].GetType().Name);
            for(int i = 0; i < blockSize; i++)
            {
                tmp = BitConverter.GetBytes(blockInMemory[i]);
                buffer[i*floatSize] = tmp[0];
                buffer[i*floatSize +1] = tmp[1];
                buffer[i*floatSize +2] = tmp[2];
                buffer[i*floatSize +3] = tmp[3];
            }

            using (BinaryWriter writer = new BinaryWriter(new FileStream(file, FileMode.Open)))
            {
                writer.BaseStream.Seek(blockIndex*floatSize, SeekOrigin.Begin);
                writer.Write(buffer, 0, blockSize * floatSize);
            }


        }
        private void readBlock(int blockIndex=0){
            Console.WriteLine("reading");
            byte[] buffer = new byte[blockSize * floatSize];
            using (BinaryReader reader = new BinaryReader(new FileStream(file, FileMode.Open)))
            {
                reader.BaseStream.Seek(blockIndex*(blockSize * floatSize), SeekOrigin.Begin);
                reader.Read(buffer, 0, blockSize*floatSize);
            }

            for(int i=0; i<blockSize;i++)
            {
                blockInMemory[i]=BitConverter.ToSingle(buffer, i*floatSize);
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

        public void writeRecordToFile(Node node, string filePath)
        {
            byte[] buffer = new byte[nodeSize * floatSize];
            float[] tmp_buff = new float[nodeSize];
            tmp_buff[0] = node.voltage;
            tmp_buff[1] = node.current;
            tmp_buff[2] = node.resistance;
            for (int j = 0; j < nodeSize; j++)
            {
                byte[] tmp = BitConverter.GetBytes(tmp_buff[j]);
                buffer[j * floatSize] = tmp[0];
                buffer[j * floatSize + 1] = tmp[1];
                buffer[j * floatSize + 2] = tmp[2];
                buffer[j * floatSize + 3] = tmp[3];
            }

            using (BinaryWriter writer = new BinaryWriter(new FileStream(filePath, FileMode.Open)))
            {
                writer.BaseStream.Seek(filesize(), SeekOrigin.Begin);
                writer.Write(buffer, 0, nodeSize * floatSize);
            }
        }

        private int filesize()
        {
            FileInfo fi = new FileInfo(file);
            return (int)fi.Length;
        }

        public int getNumberOfNodes()
        {
            return filesize() / (nodeSize * floatSize);
        }

        public void forceBlockWrite() { 
        }

        //generator losowych rekordów. Generuje 2 wartości (napiecie, natężenie)
        //Argumenty:
        //int amount - ilość rekordów do wygenerowania
        public void generateNodes(int amount)
        {
            Random rand = new Random();
            for(int i=0; i<amount; i++)
            {
                Node node = new Node(rand.Next(256), rand.Next(256));
                byte[] buffer = new byte[nodeSize * floatSize];
                float[] tmp_buff = new float[nodeSize];
                tmp_buff[0] = node.voltage;
                tmp_buff[1] = node.current;
                tmp_buff[2] = node.resistance;
                for (int j = 0; j < nodeSize; j++)
                {
                    byte[] tmp = BitConverter.GetBytes(tmp_buff[j]);
                    buffer[j * floatSize] = tmp[0];
                    buffer[j * floatSize + 1] = tmp[1];
                    buffer[j * floatSize + 2] = tmp[2];
                    buffer[j * floatSize + 3] = tmp[3];
                }

                using (BinaryWriter writer = new BinaryWriter(new FileStream(file, FileMode.Open)))
                {
                    writer.BaseStream.Seek(filesize(), SeekOrigin.Begin);
                    writer.Write(buffer, 0, nodeSize * floatSize);
                }

            }
        }
    }
}
