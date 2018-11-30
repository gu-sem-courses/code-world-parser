package vlad;

public class Main {


    public static void main(String[] args) {

        Human human1 = new Human("Petya", "Frolov", 30);

        //family 1
        Human mother2 = new Human();
        mother2.setName("Elena");
        mother2.setSurname("Lietun");

        Human father2 = new Human();
        father2.setName("Dmitiy");
        father2.setSurname("Lietun");
        Human human2 = new Human("Vlad", "Lietun", 23, mother2, father2);

        //family 2
        Pet pet3 = new Pet("dog", "Bublik", 2, 80, new String[]{"eat", "sleep"});
        Human mother3 = new Human();
        mother3.setName("Anna");
        mother3.setSurname("Frolova");

        Human father3 = new Human();
        father3.setName("Nikolay");
        father3.setSurname("Frolov");
        Human human3 = new Human("Denis", "Frolov", 20, 80, pet3, mother3, father3, new String[][]{{"monday", "go to gym"},{"friday", "study"}});

        System.out.println(human1.toString());
        System.out.println(pet3.toString());
        System.out.println(human2.toString(mother2, father2));
        System.out.println(human3.toString(mother3, father3, pet3));
    }
}

