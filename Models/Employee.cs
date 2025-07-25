using HRWebApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Id is marked as auto-incrementable (analogous to IDENTITY) so that i don't have to set the id manually
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime HireDate { get; set; }
    public string Status { get; set; } = null!;

    public int OrganizationId { get; set; }
    public int ProfessionId { get; set; }
    public int CategoryId { get; set; }

    public Organization? Organization { get; set; } // '?' Make these fields not required for POST validation
    public Profession? Profession { get; set; }
    public Category? Category { get; set; }
}
