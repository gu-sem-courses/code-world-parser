package assignment3;

public class Manager extends Employee {
	
	private String degree;
	private final double TAX = 0.1;
	private final double BONUS_BSC = 0.1;
	private final double BONUS_MSC = 0.2;
	private final double BONUS_PHD = 0.35;
	

	
	public Manager(String name, String id, double grossSalary, String degree){
		super(name,id,grossSalary);
		this.degree=degree;	
	}


	@Override 
	public double calculateSalary() {
		if (this.degree.equalsIgnoreCase("BSc")) {
			return super.calculateSalary()+ (BONUS_BSC*super.calculateSalary());
		}
		else if(this.degree.equalsIgnoreCase("MSc")) {
			return super.calculateSalary()+ (BONUS_MSC*super.calculateSalary());
		}
		else if(this.degree.equalsIgnoreCase("PhD")) {
			return super.calculateSalary()+ (BONUS_PHD*super.calculateSalary());
		}
		return 0;
	}

	
	@Override
	public double getNetSalary() {
		return this.calculateSalary()- (this.calculateSalary()*TAX);
	}
	
	
	public String getDegree() {
		return degree;
	}
	
	public void setDegree(String degree) {
		this.degree = degree;
	}

	
	
	@Override
	public String toString() {
		return super.getName()+" has a gross Salary of "+ this.calculateSalary()+" SEK per month. with a " + this.getDegree()+".";
	
	}
	
	
	
	
	
	
	
}
