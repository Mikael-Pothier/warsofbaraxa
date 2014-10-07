using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

namespace warsofbaraxa
{
    public class Carte
    {
        public Permanent perm;
        public String TypeCarte;
        public String NomCarte;
        public String Habilete;
        public int CoutBle;
        public int CoutBois;
        public int CoutGem;

        public Carte(String Nom, String Type, String habilete, int ble, int bois, int gem) 
        {
            TypeCarte = Type;
            NomCarte = Nom;
            Habilete = habilete;
            CoutBle = ble;
            CoutBois = bois;
            CoutGem = gem;
        }
        public Carte(String Nom, String Type, int ble, int bois, int gem)
        {
            TypeCarte = Type;
            NomCarte = Nom;
            Habilete = null;
            CoutBle = ble;
            CoutBois = bois;
            CoutGem = gem;           
        }
    }
    public class Permanent
    {
        public String TypePerm;
        public int Attaque;
        public int Vie;
        public int Armure;
        public bool aAttaque;

        public Permanent(String Type,int attaque,int vie,int armure)
        {
            TypePerm=Type;
            Attaque=attaque;
            Vie=vie;
            Armure=armure;
            aAttaque=true;
        }
    }
    public class Joueur
    {
        public String nom;
        public int vie;
        public int nbCarteDeck;
        public int nbCarteMain;
        public int nbCreatureBoard;
        public int nbBatimentBoard;

        public Joueur(String name)
        {
            nom = name;
            vie=30;
            nbCarteDeck=40;
            nbCarteMain=0;
            nbCreatureBoard=0;
            nbBatimentBoard=0;
        }
    }
}
