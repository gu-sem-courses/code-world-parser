package assignment3; 

public class Employee implements Comparable<Employee>{
	
	private final int EQUAL_RESULT = 0;
	private final int INVERSE = -1;
	private String name;
	private String id;
	private double grossSalary;
	private final double TAX = 0.1;
	public static String choose;
	public static String orderBy;


	
	public Employee(String name, String id, double grossSalary) {
		this.name=name;
		this.id=id;
		this.grossSalary= grossSalary;
	}
	
	/*
	 * this method is to keep the getGrossSalary() from changing, 
	 * so all the changes will affect calculateSalary() not getGrossSalary().
	 */
	public double calculateSalary() { 
		return this.grossSalary;
	}
	
	public double getNetSalary() {
		return this.grossSalary- (this.grossSalary*TAX);
	}

	
	@Override
	public int compareTo(Employee newEmployee) {
		int result = this.getName().compareToIgnoreCase(newEmployee.getName());
		if (result == EQUAL_RESULT) {
			result = Double.compare(this.getGrossSalary(), newEmployee.getGrossSalary());
		}
		return result;
	}
	

		
		public String getName() {
			return name;
		}
		
		public void setName(String name) {
			this.name = name;
		}
		

		public String getId() {
			return id;
		}

		public double getGrossSalary() {
			return grossSalary;
		}

		public void setGrossSalary(double grossSalary) {
			this.grossSalary=grossSalary;
		}
	
	public String toString() {
		return this.name+"'s gross Salary is of "+ this.grossSalary+" SEK per month. ";
	}
	// CHALLENGE 1
		/*@Override 
		public int compareTo(Employee newEmployee) {
			if (choose.equalsIgnoreCase("Name")) {
				return compareByName(newEmployee);
			} else if (choose.equalsIgnoreCase("Net Salary")) {
				return compareByNetSalary(newEmployee);
			} else {
				return  ( this.getName().compareToIgnoreCase(newEmployee.getName()) )  ;
			}

		}
	/*
	 * we have two methods that will be called in the compareTo() 
	 *  so depending on the String (choose) we will call either 
	 *  compareByName() or compareByNetSalary().
	 *  
	 *   Regarding Challenge 1.
	 *
		
		public int compareByName (Employee newEmployee) {
			int result;
			result = this.getName().compareToIgnoreCase(newEmployee.getName());
			if(result== EQUAL_RESULT) {
				result= Double.compare(this.getNetSalary(), newEmployee.getNetSalary());
				
			}
			return result;
		}
		
			public int compareByNetSalary (Employee newEmployee) {
				int result = Double.compare(this.getNetSalary(), newEmployee.getNetSalary());
				if(result== EQUAL_RESULT) {
					result = this.getName().compareToIgnoreCase(newEmployee.getName());	
				}
				return result;
			}
  */
		
	/*	CHALLENGE 2
		@Override 
		public int compareTo(Employee newEmployee) {
			if (choose.equalsIgnoreCase("Name") && orderBy.equalsIgnoreCase("asc")) {
				return compareByName(newEmployee);
			}
			else if (choose.equalsIgnoreCase("Name") && orderBy.equalsIgnoreCase("dasc")) {
				return compareByNameDasc(newEmployee) ;
			} else if (choose.equalsIgnoreCase("Net Salary") && orderBy.equalsIgnoreCase("asc")) {
				return compareByNetSalary(newEmployee);
			} else if (choose.equalsIgnoreCase("Net Salary") && orderBy.equalsIgnoreCase("dasc")) {
				return compareByNetSalaryDasc(newEmployee);
			}else {
				return this.getName().compareToIgnoreCase(newEmployee.getName()) ;
			}

		}
		
		 
		public int compareByName(Employee newEmployee) {
			int result;
			result = this.getName().compareToIgnoreCase(newEmployee.getName());
			if (result == EQUAL_RESULT) {
				result = Double.compare(this.getNetSalary(), newEmployee.getNetSalary());
			}
			return result;
		}
		
		public int compareByNetSalary(Employee newEmployee) {
			int result = Double.compare(this.getNetSalary(), newEmployee.getNetSalary());
			if (result == EQUAL_RESULT) {
				result = this.getName().compareToIgnoreCase(newEmployee.getName());
			}
			return result;
		}
		
		public int compareByNetSalaryDasc(Employee newEmployee) {
			int result = ( Double.compare(this.getNetSalary(), newEmployee.getNetSalary()) * INVERSE );
			if (result == EQUAL_RESULT) {
				result = ( this.getName().compareToIgnoreCase(newEmployee.getName()) * INVERSE);
			}
			return result;
		}
		
		public int compareByNameDasc(Employee newEmployee) {
			int result;
			result = ( this.getName().compareToIgnoreCase(newEmployee.getName()) * INVERSE);
			if (result == EQUAL_RESULT) {
				result = ( Double.compare(this.getNetSalary(), newEmployee.getNetSalary()) * INVERSE );
			}
			return result;
		}
	*/	

}
