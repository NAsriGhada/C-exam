#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace csharpexam.Models;
public class MeetUp
{
    [Key]
    public int MeetUpId {get;set;}


    [Required]
        [MinLength(2, ErrorMessage="Title must be 2 characters or longer!")]
        public string Title {get;set;}
    

    [Required]
    [MinLength(10, ErrorMessage="Description must be 10 characters or longer!")]
    public string Description {get;set;}
 
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [CustomValidation(typeof(CustomValidationMethods), nameof(CustomValidationMethods.FutureDate))]
    public DateTime Date { get; set; }


    
    
        [Required]
        [Display(Name = "Duration")]
        public int ActDuration {get;set;}

        [Required]
        public string ActUnit {get;set;}
    
    
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    
    
    public int UserId { get; set; }
    public User? User { get; set; }

    public List<Participation> ParticipantsList { get; set; } = new List<Participation>();


public class CustomValidationMethods
    {
        public static ValidationResult FutureDate(DateTime date)
        {
            return DateTime.Compare(date, DateTime.Today) < 0 ? new ValidationResult("Date cannot be in the Past") : ValidationResult.Success;
        }
    }



    public string DateDisplay( DateTime Date)
    {

        string[] d = {"January","February","March","April","May","June","July","August","September","October","November","December"};
        int a = Date.Year ;
        int b = Date.Month ;
       
        int c = Date.Day;
        string details = ($"{c} {d[b-1]}, {a}");
        // Console.WriteLine(details,"📱📱📱📱📱📱📱📱🛡️🛡️🛡️🛡️🛡️🛡️");
        return  details;
    }

  
 
    }