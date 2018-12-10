package assignment3;

import java.util.Collections;

public class Test {
	public static void main(String[] args) {
		ReusaxCorp company = new ReusaxCorp();
	    
        Employee emp1 = new RegularEmployee( "Ranim","ID1", 6408);
        Employee emp2 = new Manager ( "Majed","ID2",10000,"bsc");
        Employee emp3 = new Director ("Nazeeh","ID3", 62000,"msc","Human");
        Employee emp4 = new RegularEmployee( "Ranim","ID4", 100);
        Employee emp5 = new Intern ("Jawad","ID5",500, 6);
        Employee emp6 = new Intern ("Omar","ID5",23000,7);
        
        company.add(emp1);
        company.add(emp2);
        company.add(emp3);
        company.add(emp4);
        company.add(emp5);
     
        /*  
        * Adding an employee with an existing ID
        * company.add(emp6);
        */  
        System.out.println(company.employees);
        
        // Test Delete 
        
        /*
         company.remove("ID5");
        System.out.println(company.employees);
        */
        
        /*2
         * company.remove("ID5");
        System.out.println(company.employees);
        */
        
        // Test Update
        /*1
         * company.updateName("ID1", "Ellen");
        System.out.println(emp1);
        */
        
        /*2
        company.updateName("ID0", "Heba");
        */
        
        /*3
         * company.updateGrossSalary("ID2",5000);
        System.out.println(emp2);
        */
        
        
        /*4
         *   company.updateGrossSalary("ID0", 2000);
        */
        
        /*5
         * company.find("ID1");
        */
        
        /*6
         * company.find("ID0");
        */
        
        /* The number of employees in ReusaxCorp Company
         * company.getEmployeesNumber();
        */
       
        /*company.getTotalNetSalaries();
        company.getTotalGrossSalaries();
        */
        
        /*company.updateDegree("ID2", "msc");
        System.out.println(company.employees.get(1));
        */
        
        /*company.updateBenefit(10000);
        System.out.println(company.employees);
        */
        
        /*company.updateDirector("ID3", "phd", "Human"); 
        System.out.println(company.employees);
        */
       
        //System.out.println(company.employees);//error
        /*
        company.updateGPA("ID5", 9);
        System.out.println(company.employees.get(4).calculateSalary());
        */
        /*
        company.createRegularEmployee( "Alex","ID6", 6408);
        company.createDirector( "Hp","ID7", 100, "bsc", "Human");
        company.createManager( "Samsung","ID8",10,"phd");
        company.createIntern("LG","ID9", 2,2);
        System.out.println(company.employees);
        */
      
        
        /*company.sortBy();
        */
        
        /*company.promoteToDirector("ID1", "bsc", "Human");
        company.promoteToIntern("ID2", 4);
        company.promoteToManager("ID4", "bsc");
        company.promoteToRegularEmployee("ID5");*/
        
        /*Challenge 1®™ ©
         * company.sortBy("NamE");
        company.sortBy("NeT SaLaRy");*/
        
        /*Challenge 2®™ ©
        company.sortBy("NaMe","aSc");
        company.sortBy("NaMe","DaSc");
        company.sortBy("NeT SaLaRy","aSc");
        company.sortBy("NeT SaLaRy","DaSc");
        
        */
    }
}