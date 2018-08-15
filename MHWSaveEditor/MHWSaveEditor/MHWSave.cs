using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHWSaveEditor
{
    public class MHWSave
    {
        public int FileSize
        {
            get
            {
                return data.Length;
            }
        }

        private byte[] data;
        private byte[] generatedChecksum;
        private bool isDecrypted;

        //File structure
        uint magic;
        byte[] checksum;
        uint size;

        public MHWSave(byte[] data)
        {
            this.data = data;
            //Taking the first 4 bytes to check if the save is encrypted.
            isDecrypted = (data[0] == 1 && data[1] == 0 && data[2] == 0 && data[3] == 0);
            if (!isDecrypted)
            {
                Console.WriteLine("Save seems to be encrypted. Decrypting...");
                Decrypt();
            }
            Serialize();
            generatedChecksum = GenerateChecksum();
            for(int i = 0; i < generatedChecksum.Length; i++)
            {
                if(checksum[i] != generatedChecksum[i])
                {
                    Console.WriteLine("Checksum is invalid!");
                    break;
                }
                if(i == generatedChecksum.Length - 1)
                    Console.WriteLine("Checksum is valid.");
            }
            Console.WriteLine("size:" + size.ToString("X"));
        }

        public void Decrypt()
        {
            if (isDecrypted)
                return;
            Console.WriteLine("Decrypting...");
            MemoryStream temp = new MemoryStream(data);
            Crypt.Decrypt(temp);
            temp.Dispose();
            isDecrypted = true;
        }

        public void Encrypt()
        {
            if (!isDecrypted)
                return;
            Console.WriteLine("Encrypting...");
            MemoryStream temp = new MemoryStream(data);
            Crypt.Encrypt(temp);
            temp.Dispose();
            isDecrypted = false;
        }

        public byte[] GenerateChecksum()
        {
            if (!isDecrypted)
                Decrypt();
            MemoryStream temp = new MemoryStream(data);
            //Skip header
            temp.Position = 0x40;
            var retV = Checksum.Generate(temp,size);
            temp.Dispose();
            Console.Write("generated checksum:");
            Helper.PrintByteArray(retV);
            return retV;
        }

        public void Save(string path)
        {
            Console.WriteLine("Saving to "+path);
            Stream fStream = File.Create(path);
            fStream.Write(data, 0, data.Length);
            fStream.Close();
            Console.WriteLine("Saved!");
        }

        public void FixChecksum()
        {
            generatedChecksum = GenerateChecksum();
            Array.Copy(generatedChecksum, 0, data, 0xC, generatedChecksum.Length);
        }

        //WIP
        private void Serialize()
        {
            MemoryStream temp = new MemoryStream(data);
            this.magic = temp.ReadUInt();
            temp.Position = 0xC;
            this.checksum = new byte[20];
            temp.Read(this.checksum, 0, this.checksum.Length);
            this.size = temp.ReadUInt();
            temp.Dispose();
            Console.WriteLine("magic:" + magic.ToString("X"));
            Console.Write("checksum:");
            Helper.PrintByteArray(checksum);
        }
    }
}
