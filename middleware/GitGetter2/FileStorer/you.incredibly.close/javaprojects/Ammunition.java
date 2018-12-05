package by.softclub.hometask1.ammunition;

public abstract class Ammunition {

    private String firm;
    private int size;
    private int cost;
    private int weight;

    public Ammunition(String firm, int size, int cost, int weight) {
        this.firm = firm;
        this.size = size;
        this.cost = cost;
        this.weight = weight;
    }

    public String getFirm() {
        return firm;
    }

    public int getSize() {
        return size;
    }

    public int getCost() {
        return cost;
    }

    public int getWeight() {
        return weight;
    }

    public void setFirm(String firm) {
        this.firm = firm;
    }

    public void setSize(int size) {
        this.size = size;
    }

    public void setCost(int cost) {
        this.cost = cost;
    }

    public void setWeight(int weight) {
        this.weight = weight;
    }

    @Override
    public String toString() {
        return "Firm: " + firm + ". Size: " + size + ". Cost: " + cost + ". Weight: " + weight + ".";
    }
}
