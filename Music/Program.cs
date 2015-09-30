using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using WAV;
using FFT;

namespace Music
{
    class Program
    {
        [DllImport("WAV.dll")]
        public static extern void ReadChunk(int[]chunkdata,int N,int chunkID);
        [DllImport("FFT.dll")]
        public static extern void CalNFFT(int Nx, double[] data);
        static void Main(string[] args)
        {
            int i = 0;
            int N = 1024;
            int[]chunk = new int[N];
            double[] data = new double[N];

            FileStream file = new FileStream("D:/out.txt",FileMode.Create);
            StreamWriter writer = new StreamWriter(file);
            WAV.WAVManager wavmgr = new WAVManager(@"D:\Mercy.wav");
            FFT.FFTManager fftmgr = new FFTManager(N);
            wavmgr.ReadChunk(chunk, N, 0);
            for (i = 0; i < N; i++)
            {
                data[i] = 0.3 * Math.Cos(2 * Math.PI * i / 512)
                        + 0.2 * Math.Cos(2 * Math.PI * i / 128)
                        + 0.5 * Math.Cos(2 * Math.PI * i / 32);
                //(double)(chunk[i] / 32768.0);
                writer.Write(data[i].ToString());
                writer.Write(" ");
            }
            writer.Write("\r\n");
            writer.Write("\r\n");
            fftmgr.CalNFFT(N, data);
            for (i = 0; i < N; i++)
            {
                Console.WriteLine(data[i].ToString());
                writer.Write(data[i].ToString());
                writer.Write(" ");
            }
            writer.Close();
        }
    }
}
