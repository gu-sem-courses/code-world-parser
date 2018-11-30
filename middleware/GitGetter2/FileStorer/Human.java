package vlad;

import java.util.Arrays;

public class Human {
    private String name;
    private String surname;
    private int year;
    private int iq;
    private Pet pet;
    private Human mother;
    private Human father;
    private String[][] schedule;

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getSurname() {
        return surname;
    }

    public void setSurname(String surname) {
        this.surname = surname;
    }

    public void setYear(int year) {
        this.year = year;
    }

    public int getYear() {
        return year;
    }

    public void setIq(int iq) {
        this.iq = iq;
    }

    public int getIq() {
        return iq;
    }

    public void greetPet(Pet pet) {
        System.out.println("Привет, " + pet.getNickname());
    }

    public void describePet(Pet pet) {
        System.out.println("У меня есть " + pet.getSpecies() + ", ему " + pet.getAge() + " года, он очень хитрый!");
    }

    public Human() { }

    public Human(String name, String surname, int year) {
        this.name = name;
        this.surname = surname;
        this.year = year;
    }

    public Human(String name, String surname, int year, Human mother, Human father) {
        this.name = name;
        this.surname = surname;
        this.year = year;
        this.mother = mother;
        this.father = father;
    }

    public Human(String name, String surname, int year, int iq, Pet pet, Human mother, Human father, String[][] schedule) {
        this.name = name;
        this.surname = surname;
        this.year = year;
        this.iq = iq;
        this.pet = pet;
        this.mother = mother;
        this.father = father;
        this.schedule = schedule;
    }

    public String toString() {
        return "Human{name=" + name + ", surname=" + surname + ", year=" + year + "}";
    }

    public String toString(Human mother, Human father) {
        return "Human{name=" + name + ", surname=" + surname + ", year=" + year + ", iq=" + iq +
                ", mother=" + (mother != null ? (mother.getName() + " " + mother.getSurname()) : null) +
                ", father=" + (father != null ? (father.getName() + " " + father.getSurname()) : null) + "}";
    }

    public String toString(Human mother, Human father, Pet pet) {
        return "Human{name=" + name + ", surname=" + surname + ", year=" + year + ", iq=" + iq +
                ", mother=" + (mother != null ? (mother.getName() + " " + mother.getSurname()) : null) +
                ", father=" + (father != null ? (father.getName() + " " + father.getSurname()) : null) +
                ", pet=" + (pet != null ? ("{nickname=" + pet.getNickname() + ", age=" + pet.getAge() +
                ", trickLevel=" + pet.getTrickLevel() + ", habits=" + Arrays.toString(pet.getHabits()) +
                "}}") : null);
    }
}

