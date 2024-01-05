using System;
using System.IO;

namespace Microsoft.Xna.Framework.Audio
{
    internal static class AudioUtil
    {
        /// <summary>
        /// Takes WAV data and appends a header to it.
        /// </summary>
        internal static byte[] FormatWavData(byte[] buffer, int sampleRate, int channels)
        {
            //buffer should contain 16-bit PCM wave data
            short bitsPerSample = 16;

            using (var mStream = new MemoryStream(44 + buffer.Length))
            using (var writer = new BinaryWriter(mStream))
            {
                writer.Write("RIFF".ToCharArray()); //chunk id
                writer.Write(36 + buffer.Length); //chunk size
                writer.Write("WAVE".ToCharArray()); //RIFF type

                writer.Write("fmt ".ToCharArray()); //chunk id
                writer.Write(16); //format header size
                writer.Write((short)1); //format (PCM)
                writer.Write((short)channels);
                writer.Write(sampleRate);
                short blockAlign = (short)(bitsPerSample / 8 * channels);
                writer.Write(sampleRate * blockAlign); //byte rate
                writer.Write(blockAlign);
                writer.Write(bitsPerSample);

                writer.Write("data".ToCharArray()); //chunk id
                writer.Write(buffer.Length); //data size

                writer.Write(buffer);

                return mStream.ToArray();
            }
        }
    }
}
