using System;
using System.Data;

namespace OCT_WEIGHT.DB2
{
    public static class CompressionData
    {

        public static string CompressDataSet(DataSet ds)
        {
            //1. 데이터셋 Serialize
            ds.RemotingFormat = SerializationFormat.Binary;
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bf.Serialize(ms, ds);
            byte[] inbyt = ms.ToArray();

            //2. 데이터 압축
            System.IO.MemoryStream objStream = new System.IO.MemoryStream();
            System.IO.Compression.DeflateStream objZS = new System.IO.Compression.DeflateStream(objStream, System.IO.Compression.CompressionMode.Compress);
            objZS.Write(inbyt, 0, inbyt.Length);
            objZS.Flush();
            objZS.Close();

            //3. 데이터 리턴
            byte[] data = objStream.ToArray();
            return Convert.ToBase64String(data, 0, data.Length);
        }
        public static DataSet DeCompressData(byte[] str)
        {

//            DataSet ds = new DataSet();
            System.IO.MemoryStream objStream = new System.IO.MemoryStream(str);
            objStream.Seek(0, 0);
            System.IO.Compression.DeflateStream objZS = new System.IO.Compression.DeflateStream(objStream, System.IO.Compression.CompressionMode.Decompress, true);
            byte[] buffer = ReadFullStream(objZS, str.Length);
            objZS.Flush();
            objZS.Close();
            System.IO.MemoryStream outStream = new System.IO.MemoryStream(buffer);
            outStream.Seek(0, 0);
 //           ds.RemotingFormat = SerializationFormat.Binary;
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            return (DataSet)bf.Deserialize(outStream, null);
        }        

        private static byte[] ReadFullStream(System.IO.Stream stream, int byteSize)
        {
            byte[] buffer = new byte[2048];
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                        return ms.ToArray();
                    ms.Write(buffer, 0, read);
                }
            }            
        }
    }

}
