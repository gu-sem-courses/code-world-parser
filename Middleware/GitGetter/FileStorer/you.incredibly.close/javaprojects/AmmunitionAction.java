package by.softclub.hometask1;

import by.softclub.hometask1.ammunition.Ammunition;
import by.softclub.hometask1.ammunition.Gloves;
import by.softclub.hometask1.ammunition.Helmet;
import by.softclub.hometask1.ammunition.Jacket;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;

public class AmmunitionAction {

    public void buyAmmunition(ArrayList<Ammunition> ammunitionToBuyArrList, String... ammun) {
        Ammunition tempAmmun;
        for (int i = 0; i < ammun.length; i++) {
            tempAmmun = defineAmmunition(ammun[i]);
            if (tempAmmun != null) {
                ammunitionToBuyArrList.add(tempAmmun);
            }
        }
    }

    private Ammunition defineAmmunition(String type) {
        switch (type) {
            case "Gloves":
                return new Gloves("Glove's Firm", 1, 150, 200, "leather");
            case "Helmet":
                return new Helmet("Helmet's Firm", 10, 152, 955, "red");
            case "Jacket":
                return new Jacket("Jacket's Firm", 46, 560, 1005, "leather", "black");
            default:
                System.out.println("Sir, I haven`t " + type + ".");
                return null;
        }
    }

    public void sortAmmunitionByCost(ArrayList<Ammunition> ammunitionArrayList) {
        Collections.sort(ammunitionArrayList, new Comparator() {
            @Override
            public int compare(Object o1, Object o2) {
                Ammunition a1 = (Ammunition) o1;
                Ammunition a2 = (Ammunition) o2;
                if (a1.getCost() > a2.getCost()) {
                    return 1;
                }
                if (a2.getCost() > a1.getCost()) {
                    return -1;
                }
                return 0;
            }
        });
    }

    public void sortAmmunitionsByWeight(ArrayList<Ammunition> ammunitionArrayList) {
        Collections.sort(ammunitionArrayList, new Comparator() {
            @Override
            public int compare(Object o1, Object o2) {
                Ammunition a1 = (Ammunition) o1;
                Ammunition a2 = (Ammunition) o2;
                if (a1.getWeight() > a2.getWeight()) {
                    return 1;
                }
                if (a2.getWeight() > a1.getWeight()) {
                    return -1;
                }
                return 0;
            }
        });
    }

    public ArrayList<Ammunition> findAmmunitionsByCost(ArrayList<Ammunition> ammunitionArrayList, int lLim, int rLim) {
        ArrayList<Ammunition> foundAmmunArrList = new ArrayList<>();
        sortAmmunitionByCost(ammunitionArrayList);
        int lIndex = bSearch(ammunitionArrayList, lLim, 0, ammunitionArrayList.size());
        int rIndex = bSearch(ammunitionArrayList, rLim, 0, ammunitionArrayList.size());
        if (lIndex < 0) {
            lIndex = -1 * (lIndex + 1);
        }
        if (rIndex < 0) {
            rIndex = -1 * (rIndex + 1);
        }
        for (int i = 0; i < ammunitionArrayList.size(); i++) {
            if (i >= lIndex && i < rIndex) {
                foundAmmunArrList.add(ammunitionArrayList.get(i));
            }
        }
        return foundAmmunArrList;
    }

    private static int bSearch(ArrayList<Ammunition> list, Integer key,
                               int left, int right) {

        if (left >= right) {
            return -(left + 1);
        } else {
            int mid = (left + right) / 2;
            if (list.get(mid).getCost() == key) {
                int i = 1;
                while (true) {
                    if (mid - i >= 0) {
                        if (list.get(mid - i).getCost() == key) {
                            mid--;
                        } else {
                            return mid;
                        }
                    } else {
                        return mid;
                    }
                }

            }
            if (list.get(mid).getCost() > key) {
                right = mid;
            } else {
                left = mid + 1;
            }
            return bSearch(list, key, left, right);
        }
    }
}
