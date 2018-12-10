package by.softclub.hometask1.ammunition;

public class Jacket extends Ammunition {

    private String material;
    private String color;

    public Jacket(String firm, int size, int cost, int weight, String material, String color) {
        super(firm, size, cost, weight);
        this.material = material;
        this.color = color;
    }

    public String getMaterial() {
        return material;
    }

    public void setMaterial(String material) {
        this.material = material;
    }

    public String getColor() {
        return color;
    }

    public void setColor(String color) {
        this.color = color;
    }

    @Override
    public String toString() {
        return "\nJacket. " + super.toString() + " Material: " + material + ". Color: " + color + ".";
    }
}
