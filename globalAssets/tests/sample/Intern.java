package assignment3;

public class Intern extends Employee {
	
	private int gpa;
	private  final int NO_SALARY = 0;
	private  final int BENEFIT = 1000;
	private final int GPA_LOW= 5;
	private final int GPA_HIGH = 8;
	private  final int MAX_GPA = 10;
	

	
	public Intern(String name, String id, double grossSalary, int gpa) {
		super(name,id,grossSalary);
		this.gpa=gpa;
	}


	@Override
	public double calculateSalary() {
		double total = NO_SALARY;
		if (this.getGpa() <= GPA_LOW) {
			total = NO_SALARY;
		}
		else if (this.getGpa() > GPA_LOW && this.getGpa() <= GPA_HIGH) {
			total = super.getGrossSalary();
		}
		else if (this.getGpa()<= MAX_GPA){
			total= super.getGrossSalary() + BENEFIT;
		}
		else {
			System.out.println("ERROR! GPA can't be greater than 10 or less than 0.");
			total = super.getGrossSalary();
		}
		
		return total;
	}

	
	@Override
	public double getNetSalary() {
		return this.calculateSalary();
	}
	
	public int getGpa() {
		return gpa;
	}

	public void setGpa(int gpa) {
		this.gpa = gpa;
	}

	
	@Override
	public String toString() {
		return super.getName()+" has a gross Salary of "+ this.calculateSalary()+" SEK per month. with " + this.getGpa() + " GPA."; 
	
	}
		 
		
	}
