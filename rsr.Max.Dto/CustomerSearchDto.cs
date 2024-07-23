namespace rsr.Max.Dto;

public class CustomerSearchDto: SearchDto {
    public Guid? CustomerResourceId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}