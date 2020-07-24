using System;
using System.Security.Cryptography;

namespace BlackjackSimulationFrame
{
    public static class RandomGen
    {
        private static readonly RNGCryptoServiceProvider Global = new RNGCryptoServiceProvider();
        [ThreadStatic]
        private static Random _local;

        public static int Next()
        {
            Random inst = _local;
            
            if (inst == null)
            {
                byte[] buffer = new byte[4];
                Global.GetBytes(buffer);
                _local = inst = new Random(BitConverter.ToInt32(buffer, 0));
            }
            
            return inst.Next();
        }
        
        public static int Next(int maxValue)
        {
            Random inst = _local;
            
            if (inst == null)
            {
                byte[] buffer = new byte[4];
                Global.GetBytes(buffer);
                _local = inst = new Random(BitConverter.ToInt32(buffer, 0));
            }
            
            return inst.Next(maxValue);
        }
        
        public static int Next(int minValue, int maxValue)
        {
            Random inst = _local;
            
            if (inst == null)
            {
                byte[] buffer = new byte[4];
                Global.GetBytes(buffer);
                _local = inst = new Random(BitConverter.ToInt32(buffer, 0));
            }
            
            return inst.Next(minValue, maxValue);
        }
    } 
}