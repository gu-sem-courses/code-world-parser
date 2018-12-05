package by.softclub.hometask1.ammunition;

public class Gloves extends Ammunition {

    private String material;

    public Gloves(String firm, int size, int cost, int weight, String material) {
        super(firm, size, cost, weight);
        this.material = material;
    }

    public String getMaterial() {
        return material;
    }

    public void setMaterial(String material) {
        this.material = material;
    }

    @Override
    public String toString() {
        return "\nGloves. " + super.toString() + " Material: "+ material;
    }
}
