package by.softclub.hometask1;

import by.softclub.hometask1.ammunition.Ammunition;
import by.softclub.hometask1.ammunition.Gloves;
import by.softclub.hometask1.ammunition.Helmet;
import by.softclub.hometask1.motorcyclist.Motorcyclist;

import java.util.ArrayList;

public class Main {
    public static void main(String[] args) {
        Motorcyclist motorcyclist = new Motorcyclist("James","Smith", 25, new ArrayList<Ammunition>());
        AmmunitionAction ammunitionAction = new AmmunitionAction();
        ammunitionAction.buyAmmunition(motorcyclist.getAmmunitions(),"Gloves", "Helmet", "Jacket");
        System.out.println(motorcyclist);
        ArrayList<Ammunition> foundAmmunArrList = new ArrayList<>();

        System.out.println("");
        foundAmmunArrList = ammunitionAction.findAmmunitionsByCost(motorcyclist.getAmmunitions(),5,151);
        for (Ammunition ammun: foundAmmunArrList){
            System.out.println(ammun);
        }
    }
}
