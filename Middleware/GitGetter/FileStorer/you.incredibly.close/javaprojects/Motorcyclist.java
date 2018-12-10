package by.softclub.hometask1.motorcyclist;

import by.softclub.hometask1.ammunition.Ammunition;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;

public class Motorcyclist extends Human {

    private ArrayList<Ammunition> ammunitions;

    public Motorcyclist(String name, String surname, int age, ArrayList<Ammunition> ammunitions) {
        super(name, surname, age);
        this.ammunitions = ammunitions;
    }

    public ArrayList<Ammunition> getAmmunitions() {
        return ammunitions;
    }

    public void setAmmunitions(ArrayList<Ammunition> ammunitions) {
        this.ammunitions = ammunitions;
    }

    @Override
    public String toString() {
        StringBuilder description = new StringBuilder(super.toString());
        if (!ammunitions.isEmpty()) {
            description.append("\n" + "----------AMMUNITIONS----------\n");
            for (Ammunition ammunition : ammunitions) {
                description.append(new StringBuilder(ammunition.toString()));
            }
        }
        return new String(description);
    }
}
