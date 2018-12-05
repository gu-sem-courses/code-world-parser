package by.softclub.hometask1.ammunition;

public class Helmet extends Ammunition {

    private String color;

    public Helmet(String firm, int size, int cost, int weight, String color) {
        super(firm, size, cost, weight);
        this.color = color;
    }

    public String getColor() {
        return color;
    }

    public void setColor(String color) {
        this.color = color;
    }

    @Override
    public String toString() {
        return "\n" + "Helmet. " + super.toString() + " Color: " + color ;
    }
}
