using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BMPFilter
{
    class BitmapIO
    {
        BitmapHeader h; BitmapInfo inf; byte[, ,] bmp; int[, ,] newbmp;
        int w;
        public BitmapIO()
        {
            h = new BitmapHeader();
            inf = new BitmapInfo();

        }

        public byte[, ,] GetBmp()
        {
            return bmp;
        }

        public int[, ,] GetNewBmp()
        {
            return newbmp;
        }

        public BitmapHeader GetHeader()
        {
            return h;
        }

        public BitmapInfo GetInfo()
        {
            return inf;
        }

        public void ReadBmp(String name)
        {
            

            FileStream fs = new FileStream(name, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);

            h.bfType = br.ReadUInt16();
            h.bfSize = br.ReadUInt32();
            h.bfReserved1 = br.ReadUInt16();
            h.bfReserved2 = br.ReadUInt16();
            h.bfOffBits = br.ReadUInt32();

            inf.biSize = br.ReadUInt32();
            inf.biWidth = br.ReadInt32();
            inf.biHeight = br.ReadInt32();
            inf.biPlanes = br.ReadUInt16();
            inf.biBitCount = br.ReadUInt16();
            inf.biCompression = br.ReadUInt32();
            inf.biSizeImage = br.ReadUInt32();
            inf.biXPelsPerMeter = br.ReadInt32();
            inf.biYPelsPerMeter = br.ReadInt32();
            inf.biClrUsed = br.ReadUInt32();
            inf.biClrImportant = br.ReadUInt32();

            bmp = new byte[inf.biHeight, inf.biWidth, 3];
            newbmp = new int[inf.biHeight, inf.biWidth, 3];
            w = ((inf.biWidth * 3) % 4 == 0) ? inf.biWidth * 3 : inf.biWidth * 3 + 4 - ((inf.biWidth * 3) % 4);
            for (int i = 0; i < inf.biHeight; i++)
            {
                for (int j = 0; j < inf.biWidth; j++)
                {
                    bmp[i, j, 0] = br.ReadByte();
                    bmp[i, j, 1] = br.ReadByte();
                    bmp[i, j, 2] = br.ReadByte();
                }
                if (w > inf.biWidth * 3)
                {
                    for (int j = 0; j < w - inf.biWidth * 3; j++)
                    {
                        br.ReadByte();
                    }
                }
            }
            br.Close();
        }

        public void WriteBmp(String to)
        {
            FileStream fss = new FileStream(to, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fss);

            bw.Write(h.bfType);
            bw.Write(h.bfSize);
            bw.Write(h.bfReserved1);
            bw.Write(h.bfReserved2);
            bw.Write(h.bfOffBits);

            bw.Write(inf.biSize);
            bw.Write(inf.biWidth);
            bw.Write(inf.biHeight);
            bw.Write(inf.biPlanes);
            bw.Write(inf.biBitCount);
            bw.Write(inf.biCompression);
            bw.Write(inf.biSizeImage);
            bw.Write(inf.biXPelsPerMeter);
            bw.Write(inf.biYPelsPerMeter);
            bw.Write(inf.biClrUsed);
            bw.Write(inf.biClrImportant);

            for (int i = 0; i < inf.biHeight; i++)
            {
                for (int j = 0; j < inf.biWidth; j++)
                {
                    byte t = (byte)((newbmp[i, j, 2] > 255) ? 255 : ((newbmp[i, j, 2] < 0) ? 0 : (byte)newbmp[i, j, 2]));
                    bw.Write(t);
                    t = (byte)((newbmp[i, j, 1] > 255) ? 255 : ((newbmp[i, j, 1] < 0) ? 0 : newbmp[i, j, 1]));
                    bw.Write(t);
                    t = (byte)((newbmp[i, j, 0] > 255) ? 255 : ((newbmp[i, j, 0] < 0) ? 0 : newbmp[i, j, 0]));
                    bw.Write(t);
                }
                if (w > inf.biWidth * 3)
                {
                    for (int j = 0; j < w - inf.biWidth * 3; j++)
                    {
                        byte n = 0;
                        bw.Write(n);
                    }
                }
            }
            bw.Close();
            fss.Close();

        }
    }
}
