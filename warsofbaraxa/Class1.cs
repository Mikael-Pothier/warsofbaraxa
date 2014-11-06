using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace warsofbaraxa
{
    [Serializable]
    public class Carte
    {
        public int NoCarte;
        public Permanent perm;
        public String TypeCarte;
        public String NomCarte;
        public String Habilete;
        public int CoutBle;
        public int CoutBois;
        public int CoutGem;

        public Carte(int Numero,String Nom, String Type, String habilete, int ble, int bois, int gem) 
        {
            NoCarte = Numero;
            TypeCarte = Type;
            NomCarte = Nom;
            Habilete = habilete;
            CoutBle = ble;
            CoutBois = bois;
            CoutGem = gem;
        }
        public Carte(int Numero,String Nom, String Type, int ble, int bois, int gem)
        {
            NoCarte = Numero;
            TypeCarte = Type;
            NomCarte = Nom;
            Habilete = null;
            CoutBle = ble;
            CoutBois = bois;
            CoutGem = gem;           
        }
        public bool esthabileteNormal(string habilete)
        {
            return habilete == "rapide" || habilete == "provocation" || habilete == "attaque double" || habilete == "invisible" || habilete == "attaque puissante";
        }
        public void setHabileteNormal(string habilete)
        {
            if (habilete == "rapide")
                perm.aAttaque = false;
            else if (habilete == "provocation")
                perm.estTaunt = true;
            else if (habilete == "attaque double")
                perm.estAttaqueDouble = true;
            else if (habilete == "invisible")
                perm.estInvisible = true;
            else if (habilete == "attaque puissante")
                perm.estAttaquePuisante=true;
        }
    }

    [Serializable]
    public class Permanent
    {
        public String TypePerm;
        public int Attaque;
        public int Vie;
        public int Armure;
        private int basicArmor;
        public bool aAttaque;
        public bool estTaunt;
        public bool estAttaqueDouble;
        public bool aAttaquerDouble;
        public bool estInvisible;
        public bool estAttaquePuisante;
        public Permanent(String Type,int attaque,int vie,int armure)
        {
            TypePerm=Type;
            Attaque=attaque;
            Vie=vie;
            Armure=armure;
            basicArmor = armure;
            aAttaque=true;
            estTaunt = false;
            estAttaqueDouble = false;
            aAttaquerDouble = false;
            estInvisible = false;
            estAttaquePuisante = false;
        }
        public int getBasicArmor()
        {
            return basicArmor;
        }
    }

    [Serializable]
    public class Deck
    {
        public Carte[] CarteDeck;

        public Deck(Carte[] carte)
        {
            CarteDeck = new Carte[40];
            for (int i = 0; i<CarteDeck.Length; ++i)
            {
                CarteDeck[i] =  new Carte(carte[i].NoCarte,carte[i].NomCarte,carte[i].TypeCarte,carte[i].Habilete,carte[i].CoutBle,carte[i].CoutBois,carte[i].CoutGem);
                if (CarteDeck[i].TypeCarte == "Permanents")
                {
                    CarteDeck[i].perm = new Permanent(carte[i].perm.TypePerm, carte[i].perm.Attaque, carte[i].perm.Vie, carte[i].perm.Armure);
                    CarteDeck[i].setHabileteNormal(CarteDeck[i].Habilete);
                }
            }
        }
    }

    [Serializable]
    public class Joueur
    {
        public String nom;
        public Socket sckJoueur;
        public int nbDepart;
        public int vie;
        public int nbCarteDeck;
        public int nbCarteMain;
        public int nbCreatureBoard;
        public int nbBatimentBoard;
        public int nbBle;
        public int nbBois;
        public int nbGem;
        public bool Depart;

        public Joueur(String name)
        {
            Depart = false;
            nbDepart = 0;
            nom = name;
            vie=30;
            nbCarteDeck=40;
            nbCarteMain=0;
            nbCreatureBoard=0;
            nbBatimentBoard=0;
            nbBle = 0;
            nbBois = 0;
            nbGem = 0;
        }
    }
    [Serializable]
    public class PosZoneCombat
    {
        public Vector3 Pos;
        public bool EstOccupee;
        public Carte carte;
        public PosZoneCombat()
        {
            Pos = new Vector3();
            EstOccupee = false;
        }
    }
    public class StateObject
    {
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 256;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }
    public class ThreadLire
    {
        // Client socket.
        public Socket workSocket = null;
        public string message = "";

        public void doWork()
        {
            string mess = "";
            do
            {
                mess=recevoirResultat();
            } while (mess == null);
            message = mess;
        }
        private string recevoirResultat()
        {
            byte[] buff = new byte[workSocket.SendBufferSize];
            int bytesRead = workSocket.Receive(buff);
            byte[] formatted = new byte[bytesRead];

            for (int i = 0; i < bytesRead; i++)
            {
                formatted[i] = buff[i];
            }
            string strData = Encoding.ASCII.GetString(formatted);
            return strData;
        }
        public Carte RecevoirCarte()
        {
            Carte carte = null;
            try
            {
                byte[] buffer = new byte[1024*1024*50];
                int bytesRead = workSocket.Receive(buffer);
                BinaryFormatter receive = new BinaryFormatter();
                MemoryStream recstreams = new MemoryStream(buffer);
                carte = (Carte)receive.Deserialize(recstreams);

            }
            catch { Console.Write("Erreur de telechargement des données"); }
            return carte;            
        }
    }
}
