package assignment3;

public class Director extends Manager {
	
	private String department;
	static double benefit = 5000;
	private final double TAX_LOW= 0.1;
	private final double TAX_BETWEEN = 0.2;
	private final double TAX_HIGH = 0.4;
	private final int LOW_INCOME = 30000;
	private final int HIGHT_INCOME = 50000;


	
		Director(String name, String id, double grossSalary, String degree, String department){
			super(name, id, grossSalary, degree);
			this.department=department;
		}


	@Override 
	public double calculateSalary() {
	
			return super.getGrossSalary() + benefit ;
		
	}

	@Override
	public double getNetSalary() {
		double total = 0;
		if (this.calculateSalary() < LOW_INCOME) {
			total = this.calculateSalary() - (TAX_LOW * this.calculateSalary() );
		}
		else if (this.calculateSalary() >= LOW_INCOME && this.calculateSalary() <= HIGHT_INCOME) {
			total = this.calculateSalary() - (TAX_BETWEEN * this.calculateSalary() );
		}
		else {
			double total1 = this.calculateSalary() - LOW_INCOME;
			double total2 = this.calculateSalary() - (LOW_INCOME*TAX_BETWEEN) -(total1 *  TAX_HIGH);
			total = total2;
		}
		
		return total;
	}
	
	public String getDepartment() {
		return department;
	}
	public void setDepartment(String department) {
		this.department = department;
	}

	
	public double getBenefit() {
		return benefit;
	}
	public void setBenefit(double benefit) {
		 Director.benefit = benefit;	
	}

	@Override
	public String toString() {
		return super.getName()+" has a gross Salary of "+ this.calculateSalary()+" SEK per month. with a " + super.getDegree()+" and leads "+this.getDepartment()+" department.";
	
	}
	
		 
	}
