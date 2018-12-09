package assignment3;

public class RegularEmployee  extends Employee{

	private  final double TAX = 0.1;
	
	RegularEmployee(String name, String id, double grossSalary){
		super(name,id,grossSalary);
		
	}
	@Override
	public double getNetSalary() {
		double netSalary;
		netSalary = super.getGrossSalary()- (super.getGrossSalary()*TAX);
		return netSalary;
	}
	public String toString() {
		return super.getName()+"'s gross Salary is of "+ super.getGrossSalary()+" SEK per month. ";
	}
}
