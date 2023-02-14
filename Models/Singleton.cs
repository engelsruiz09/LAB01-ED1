using System.Numerics;
using System.Collections.Generic;
using ELineales;
namespace LAB01_ED1_G.Models.Data
{
    public sealed class Singleton
    {
        public readonly static Singleton _instance = new Singleton();
        public System.Collections.Generic.List<jugador> PlayerList;
        public readonly static Singleton _instance1 = new Singleton();
        public ELineales.DoubleList<jugador> PlayerDList;
        public readonly static Singleton _instance2 = new Singleton();
        public System.Collections.Generic.List<jugador> PlayerSearch;
        public readonly static Singleton _instance3 = new Singleton();
        public ELineales.DoubleList<jugador> PlayerDSearch;
        private Singleton()
        {
            PlayerList = new System.Collections.Generic.List<jugador>();
            PlayerDList = new ELineales.DoubleList<jugador>();
            PlayerSearch = new System.Collections.Generic.List<jugador>();
            PlayerDSearch = new ELineales.DoubleList<jugador>();
        }
        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }
        public static Singleton Instance1
        {
            get
            {
                return _instance1;
            }
        }
        public static Singleton Instance2
        {
            get
            {
                return _instance2;
            }
        }
        public static Singleton Instance3
        {
            get
            {
                return _instance3;
            }
        }
    }
}
