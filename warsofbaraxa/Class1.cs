using System;
using System.Collections.Generic;
using System.Text;
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

        public Permanent(String Type,int attaque,int vie,int armure)
        {
            TypePerm=Type;
            Attaque=attaque;
            Vie=vie;
            Armure=armure;
            basicArmor = armure;
            aAttaque=false;
            estTaunt = false;
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

        public Deck(Carte[] Carte)
        {
            CarteDeck = Carte;
        }
    }

    [Serializable]
    public class Joueur
    {
        public String nom;
        public Socket sckJoueur;
        public int vie;
        public int nbCarteDeck;
        public int nbCarteMain;
        public int nbCreatureBoard;
        public int nbBatimentBoard;
        public int nbBle;
        public int nbBois;
        public int nbGem;

        public Joueur(String name)
        {
            
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

        public PosZoneCombat()
        {
            Pos = new Vector3(1, 1, 1);
        }
    }
}
