using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace WAV
{
    public class WAVManager
    {
        FileStream wavfile;

        public String riff, wave, fmt, data;
        public int filesize, datasize, pcmsize, line,channel, fsam, kbps,block, bits;

        public void getwavinfo()
        {

            byte[] wavhead = new byte[44];
            wavfile.Read(wavhead, 0, 44);
            riff = System.Text.Encoding.Default.GetString(wavhead,0x00,4);
            filesize = System.BitConverter.ToInt32(wavhead, 0x04);
            wave = System.Text.Encoding.Default.GetString(wavhead, 0x08, 4);
            fmt = System.Text.Encoding.Default.GetString(wavhead, 0x0c, 4);
            pcmsize = System.BitConverter.ToInt32(wavhead, 0x10);
            line = System.BitConverter.ToInt16(wavhead, 0x14);
            channel = System.BitConverter.ToInt16(wavhead, 0x16);
            fsam = System.BitConverter.ToInt32(wavhead, 0x18);
            kbps = System.BitConverter.ToInt32(wavhead, 0x1c);
            block = System.BitConverter.ToInt16(wavhead, 0x20);
            bits = System.BitConverter.ToInt16(wavhead, 0x22);
            data = System.Text.Encoding.Default.GetString(wavhead, 0x24, 4);
            datasize = System.BitConverter.ToInt32(wavhead, 0x28);
        }
        public void ReadChunk(int[]chunkdata,int N,int chunkID)
        {
            int i = 0;
            byte[] buffer = new byte[N << 2];
            wavfile.Seek(0x2c+4*chunkID, SeekOrigin.Begin);
            wavfile.Read(buffer, 0, N << 2);
            for (i = 0; i < N; i++)
            {
                chunkdata[i] = System.BitConverter.ToInt16(buffer, 4*i);
            }
        }
        public WAVManager(String file_name)
        {
            wavfile = new FileStream(file_name, FileMode.Open);
            getwavinfo();
        }
    }
}
