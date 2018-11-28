package assignment3;


import java.util.ArrayList;
import java.util.Collections;

import javax.swing.JOptionPane;

import assignment2.Country;

public class ReusaxCorp {
	
	private Employee employee;
	ArrayList<Employee> employees;
	private  final int NOT_FOUND_IN_LIST = -1;
	

	
	public ReusaxCorp() { 
		this.employees = new ArrayList<Employee>();
	}
	

	
	public void remove(String id) {
        Employee temp = checkID(id);
		if (temp!=null) {
			this.employees.remove(temp);
		}
		else {
			System.out.println("An employee of ID ("+id+") is not registered in the system.");
		}
	}
	/*
	 * This method checks if an Employee with a specific ID exists in the ArrayList
	 * and it returns the checked Employee if it exists in the ArrayList. if not, it will return null.
	 */

	
	public Employee checkID(String id) {
		for (int i = 0; i < this.employees.size(); i++) {
		if (this.employees.get(i).getId().equalsIgnoreCase(id)) {
			return this.employees.get(i) ;
		}
	}
		return null;
	}
	

	
	public void add(Employee employee) {
		Employee temp = checkID(employee.getId());
		if (temp!=null) {
			System.out.println("Error! This ID ("+ employee.getId()+") exists.");
		}
		else {
			this.employees.add(employee);
		}
		}

	/*
	 * The two methods will pass throw every element in the ArrayList and store every net salary/ gross salary in the double sum
	 * then add all of the net salaries/ gross salaries together.
	 */
	
	public void  getTotalNetSalaries() {
		double sum=0;
		for (int i = 0; i < this.employees.size(); i++) {
			sum+= this.employees.get(i).getNetSalary();
		}
System.out.println("The total net salaries in ReuaxCorp company is: " +sum+" SEK.");
	}
	

	
	public void getTotalGrossSalaries() {
		double sum=0;
		for (int i = 0; i < this.employees.size(); i++) {
			sum+= employees.get(i).calculateSalary();
		}
		System.out.println("The total gross salaries in ReuaxCorp company is: " +sum+" SEK.");
	}
	

	
	public void getEmployeesNumber() {
		int result = employees.size();
		System.out.println("The number of employees in the ReusaxCorp is: "+result + " employee(s). ");
	}
	
	
	public void find(String id) {
		Employee temp= checkID(id);
		if (temp!= null) {
			System.out.println(temp);
		} else {
			System.out.println("An employee of ID ("+id+") is not registered in the system.");
		}
	}
	

	
	public void updateName(String id, String name ) {
	Employee temp = checkID(id);
	if (temp!= null) {
		temp.setName(name);
	}
			 else {
				System.out.println("An employee of ID ("+id+") is not registered in the system.");
			}
		}
	
	
	public void updateGrossSalary(String id, double grossSalary) {
	Employee temp = checkID(id);
	if (temp!= null) {
		temp.setGrossSalary(grossSalary);
	}
			 else {
				System.out.println("An employee of ID ("+id+") is not registered in the system.");
			}

	}
	
	/*
	 * This method will return the position of the element in the ArrayList 
	 * that contains the String id in the method's signature.
	 */

	int getPosition (String id) {
		
		for (int i = 0; i < this.employees.size(); i++) {
		if (this.employees.get(i).getId().equalsIgnoreCase(id)) {
			return i ;
		}
	}
		return NOT_FOUND_IN_LIST;}
	
	
	public void updateDegree (String id,String degree) {
		try {
			int position = getPosition(id);
			if (position!= NOT_FOUND_IN_LIST) {
				Employee temp = employees.get(position);// Pick up the element in the position that you find it
				employees.remove(id); // Delete a reference by it's ID
				employees.set(position, new Manager(temp.getName(), temp.getId(), temp.getGrossSalary(), degree));
		        // Create new reference with a new degree and department in the same position.
		
			}
			else {System.out.println("A manager of ID ("+ id +") is not registered in the system.");}
		} catch (Exception e) {
			System.out.println("A manager of ID ("+ id +") is not registered in the system.");
		}
		
	}
	
	
	void updateDirector (String id, String degree,String department) {
        try {
            int position = getPosition(id);
            if (position != NOT_FOUND_IN_LIST) {
                Employee temp = employees.get(position);
                employees.remove(id); 
                employees.set(position, new Director( temp.getName(),temp.getId(), temp.calculateSalary(), degree,department )); 
               
            } else {
                System.out.println("A Manager of ID " + id + " is not registered in the system");
            }
        } catch (Exception e) {
            System.out.println("A Manager of ID " + id + " is not registered in the system");
        }
    }
	


	public void updateBenefit(double benefit) {
        Director.benefit= benefit; //all the directors' benefits will change since benefit is static.
		
	}
	
 	
	public void updateGPA (String id,int gpa) {
		try {
		
			int position = getPosition(id);
			if (position!= NOT_FOUND_IN_LIST) {
				Employee temp = employees.get(position); 
				employees.remove(id); 
				employees.set(position, new Intern(temp.getName(), temp.getId(), temp.getGrossSalary(), gpa));
				
		System.out.println(employees.set(position, new Intern(temp.getName(), temp.getId(), temp.getGrossSalary(), gpa)));
		
		
			}
			else {System.out.println("A manager of ID ("+ id +") is not registered in the system.");}
		} catch (Exception e) {
			System.out.println("A manager of ID ("+ id +") is not registered in the system.");
		}
		
	}
	
		public void createRegularEmployee(String name, String id, double grossSalary) {
				if (checkID(id)!=null) {
					System.out.println("Error! This ID ("+ id+") exists.");
				}
				else {
					Employee newRegularEmployee = new RegularEmployee (name, id , grossSalary);
					add(newRegularEmployee);
				}
				}
		
		public void createManager(String name, String id, double grossSalary, String degree) {
				if (checkID(id)!=null) {
						System.out.println("Error! This ID ("+ id+") exists.");
				}
				else {
					Employee newManager = new Manager (name, id , grossSalary, degree);
					add(newManager);
				}
				}
		
			public void createDirector(String name, String id, double grossSalary, String degree, String department) {
					if (checkID(id)!=null) {
						System.out.println("Error! This ID ("+ id+") exists.");
					}
						else {
							Employee newDirector = new Director (name, id , grossSalary,degree, department);
							add(newDirector);
						}
						}
			
			public void createIntern(String name, String id, double grossSalary, int gpa) {
					if (checkID(id)!=null) {
						System.out.println("Error! This ID ("+ id+") exists.");
					}
						else {
							Employee newIntern = new Intern (name, id , grossSalary,gpa);
							add(newIntern);
						}
						}
			
			  /*
		     * Another idea®™ ©
		     * BAD 
		     * DOWNCASTING !!!!!!!!!!!!!
		     * REASON: Each Director is an Employee, but not every Employee is a Director ;) .
		     * Employee temp = checkID(ID);
		     * Director temp2 = (Director) temp;
		     * for (int i = 0; i < employees.size(); i++) { 
		     * if (employees.get(i) instanceof Director)®™ © { 
		     * ((Director) employees.get(i)).setBenefit(benefit);
		     * } 
		     * You have to put benefit as private and insert getter and setter for it. 
		     *  
		     * }
		     */
		    
		    /* 
		     * 
		     * GOOD
		     * NO DOWNCASTING!
		     * REASON: There is no casting in those methods, but we create new references of type Employees
		     * in the same position of old employee type , but with new object of type employee 
		     * LOOK AT THE COMMENTS DOWN 
		     */
	       
	       public void promoteToRegularEmployee(String id) {
	   		try {
				int position = getPosition(id);
				if (position!= NOT_FOUND_IN_LIST) {
					Employee temp = employees.get(position);
					employees.remove(id);
					employees.set(position, new RegularEmployee(temp.getName(), temp.getId(), temp.getGrossSalary()));
			
				}
				else {System.out.println("A Regular Employee of ID ("+ id +") is not registered in the system.");}
			} catch (Exception e) {
				System.out.println("A Regular Employee of ID ("+ id +") is not registered in the system.");
			}
			
	       }

	       public void promoteToManager(String id, String degree) {
	   		try {
				int position = getPosition(id);
				if (position!= NOT_FOUND_IN_LIST) {
					Employee temp = employees.get(position);
					employees.remove(id);
					employees.set(position, new Manager(temp.getName(), temp.getId(), temp.getGrossSalary(), degree));
			
				}
				else {System.out.println("A manager of ID ("+ id +") is not registered in the system.");}
			} catch (Exception e) {
				System.out.println("A manager of ID ("+ id +") is not registered in the system.");
			}
			
	       }

	       public void promoteToDirector(String id, String degree, String department) {
	   		try {
				int position = getPosition(id);
				if (position!= NOT_FOUND_IN_LIST) {
					Employee temp = employees.get(position);
					employees.remove(id);
					employees.set(position, new Director(temp.getName(), temp.getId(), temp.getGrossSalary(), degree, department));
			
				}
				else {System.out.println("A director of ID ("+ id +") is not registered in the system.");}
			} catch (Exception e) {
				System.out.println("A director of ID ("+ id +") is not registered in the system.");
			}
			
	       }

	       public void promoteToIntern(String id, int gpa) {
	   		try {
				int position = getPosition(id);
				if (position!= NOT_FOUND_IN_LIST) {
					Employee temp = employees.get(position);
					employees.remove(id);
					employees.set(position, new Intern(temp.getName(), temp.getId(), temp.getGrossSalary(), gpa));
			
				}
				else {System.out.println("An Intern of ID ("+ id +") is not registered in the system.");}
			} catch (Exception e) {
				System.out.println("An Intern of ID ("+ id +") is not registered in the system.");
			}
			
	       }
	       
	    void sortBy() {
	    	Collections.sort(employees);
	    } 
	   	// CHALLENGE 1 
	   	
	      /*
	   	*  This challenge method will sort a list by specific parameter.
	   	*
	   	*/
	    void sortBy(String choose) {
            Employee.choose = choose;
            Collections.sort(employees);
        }
	    
		//CHALLENGE2
	    
        void sortBy(String choose, String orderBy) {
            Employee.choose = choose;
            Employee.orderBy = orderBy;
            Collections.sort(employees);
    }
	   
	  
	
}
