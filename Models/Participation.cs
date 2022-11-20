#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace csharpexam.Models;

public class Participation
    {
        [Key]
        public int ParticipationId {get;set;}
        public int UserId {get;set;}
        public int MeetUpId {get;set;}
        public User? User {get;set;}
        public MeetUp? MeetUp {get;set;}

    }